-- liquibase formatted sql

-- changeset MarkGent1:1789533200000-1 splitStatements:false

-- Consolidated parallel import, retained-source ledger and worker metrics.
-- This changeset is rerunnable and never resets an existing import plan.
-- Destination clearing is an explicit preparation option for fresh run 1 only;
-- deploying this file never truncates business tables.
-- Version 4.3 permits valid own-row foreign keys and adds scoped, multi-pass
-- deferred recovery. Version 4.2 added bounded, least-busy-table scheduling and
-- per-table batch caps. Existing plans and deferred evidence are never reset.
-- Stop old worker invocations before upgrading: their existing claims are kept.
-- Version 4.1 separated planning, fast parallel bulk movement and slow row-level
-- diagnosis. It also detects self-referencing foreign keys and loads those tables
-- in parent-ready passes owned by one worker. The source rows plus this durable
-- queue are the recovery checkpoint; a worker may disappear at any point without
-- losing a committed source row.

CREATE TABLE IF NOT EXISTS cads.cts_parallel_import_runs
(
    run_id              bigint GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    status              text NOT NULL CHECK (status IN
                            ('planning', 'ready', 'processing bulk',
                             'bulk complete', 'processing errors',
                             'complete', 'complete with errors', 'failed')),
    delete_source       boolean NOT NULL,
    truncate_cts        boolean NOT NULL DEFAULT false,
    range_size          bigint NOT NULL CHECK (range_size > 0),
    created_at          timestamp with time zone NOT NULL DEFAULT clock_timestamp(),
    bulk_completed_at   timestamp with time zone,
    completed_at        timestamp with time zone,
    last_error_message  text
);

-- Record the destructive option without changing the behaviour of existing runs.
ALTER TABLE cads.cts_parallel_import_runs
    ADD COLUMN IF NOT EXISTS truncate_cts boolean NOT NULL DEFAULT false;

CREATE TABLE IF NOT EXISTS cads.cts_parallel_import_dependencies
(
    run_id       bigint NOT NULL REFERENCES cads.cts_parallel_import_runs(run_id) ON DELETE CASCADE,
    child_table  text NOT NULL,
    parent_table text NOT NULL,
    PRIMARY KEY (run_id, child_table, parent_table)
);

CREATE INDEX IF NOT EXISTS cts_parallel_import_dependencies_parent_idx
    ON cads.cts_parallel_import_dependencies (run_id, parent_table);

CREATE TABLE IF NOT EXISTS cads.cts_parallel_import_status
(
    run_id                   bigint NOT NULL REFERENCES cads.cts_parallel_import_runs(run_id) ON DELETE CASCADE,
    table_name               text NOT NULL,
    dependency_depth         integer NOT NULL,
    original_total           bigint NOT NULL DEFAULT 0,
    number_migrated          bigint NOT NULL DEFAULT 0,
    number_deferred          bigint NOT NULL DEFAULT 0,
    status                   text NOT NULL CHECK (status IN
                                 ('not started', 'blocked by parents', 'ready',
                                  'processing bulk', 'bulk complete',
                                  'bulk complete with deferred rows',
                                  'processing errors', 'complete',
                                  'complete with errors', 'failed')),
    started_at               timestamp with time zone,
    bulk_completed_at        timestamp with time zone,
    completed_at             timestamp with time zone,
    updated_at               timestamp with time zone NOT NULL DEFAULT clock_timestamp(),
    last_error_message       text,
    PRIMARY KEY (run_id, table_name)
);

CREATE INDEX IF NOT EXISTS cts_parallel_import_status_claim_idx
    ON cads.cts_parallel_import_status (run_id, status, dependency_depth);

-- A work row owns an inclusive, non-overlapping trans_id range. Different workers
-- can therefore work on the same large table without ever sharing source IDs.
CREATE TABLE IF NOT EXISTS cads.cts_parallel_import_work_queue
(
    work_id                 bigint GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    run_id                  bigint NOT NULL REFERENCES cads.cts_parallel_import_runs(run_id) ON DELETE CASCADE,
    table_name              text NOT NULL,
    dependency_depth        integer NOT NULL,
    minimum_trans_id        bigint NOT NULL,
    maximum_trans_id        bigint NOT NULL,
    state                   text NOT NULL CHECK (state IN
                                ('blocked', 'ready', 'processing',
                                 'complete', 'complete with deferred rows', 'failed')),
    claimed_by              text,
    claimed_backend_pid     integer,
    claimed_at              timestamp with time zone,
    heartbeat_at            timestamp with time zone,
    rows_migrated           bigint NOT NULL DEFAULT 0,
    rows_deferred           bigint NOT NULL DEFAULT 0,
    completed_at            timestamp with time zone,
    last_error_message      text,
    UNIQUE (run_id, table_name, minimum_trans_id, maximum_trans_id),
    CHECK (maximum_trans_id >= minimum_trans_id)
);

CREATE INDEX IF NOT EXISTS cts_parallel_import_work_claim_idx
    ON cads.cts_parallel_import_work_queue
       (run_id, state, dependency_depth, table_name, minimum_trans_id);

-- These are per-table policies shared by all runs. Missing entries use four
-- workers and the caller's batch size. A batch_size override is a ceiling, not
-- permission to exceed the caller's requested size. Stale processing claims
-- still consume slots until explicitly recovered; never steal live work.
CREATE TABLE IF NOT EXISTS cads.cts_parallel_import_table_settings
(
    table_name text PRIMARY KEY,
    max_workers integer NOT NULL DEFAULT 4 CHECK (max_workers > 0),
    batch_size integer CHECK (batch_size > 0)
);

-- A conservative starting experiment for the observed MultiXact hotspot.
-- Re-deployment must not overwrite operator tuning.
INSERT INTO cads.cts_parallel_import_table_settings (table_name, max_workers, batch_size)
VALUES ('ct_valid_applications', 4, 10000)
ON CONFLICT (table_name) DO NOTHING;

CREATE INDEX IF NOT EXISTS cts_parallel_import_processing_table_idx
    ON cads.cts_parallel_import_work_queue (run_id, table_name)
    WHERE state = 'processing';

CREATE TABLE IF NOT EXISTS cads.cts_parallel_import_workers
(
    run_id              bigint NOT NULL REFERENCES cads.cts_parallel_import_runs(run_id) ON DELETE CASCADE,
    worker_name         text NOT NULL,
    backend_pid         integer,
    status              text NOT NULL CHECK (status IN ('starting', 'working', 'waiting', 'complete', 'failed')),
    current_work_id     bigint,
    current_table_name  text,
    batches_committed   bigint NOT NULL DEFAULT 0,
    rows_migrated       bigint NOT NULL DEFAULT 0,
    rows_deferred       bigint NOT NULL DEFAULT 0,
    started_at          timestamp with time zone NOT NULL DEFAULT clock_timestamp(),
    heartbeat_at        timestamp with time zone NOT NULL DEFAULT clock_timestamp(),
    completed_at        timestamp with time zone,
    last_error_message  text,
    PRIMARY KEY (run_id, worker_name)
);

-- Rows enter this table only after a batch has been divided down to the configured
-- threshold. Bulk workers skip them; the diagnostic procedure revisits them later.
CREATE TABLE IF NOT EXISTS cads.cts_parallel_import_deferred_rows
(
    run_id             bigint NOT NULL REFERENCES cads.cts_parallel_import_runs(run_id) ON DELETE CASCADE,
    table_name         text NOT NULL,
    transaction_row_id bigint NOT NULL,
    dependency_depth  integer NOT NULL,
    status             text NOT NULL DEFAULT 'pending' CHECK (status IN ('pending', 'resolved', 'error')),
    bulk_error_message text,
    retry_count        integer NOT NULL DEFAULT 0,
    error_sqlstate     text,
    error_message      text,
    error_detail       text,
    error_hint         text,
    error_context      text,
    deferred_at        timestamp with time zone NOT NULL DEFAULT clock_timestamp(),
    last_attempted_at  timestamp with time zone,
    resolved_at        timestamp with time zone,
    PRIMARY KEY (run_id, table_name, transaction_row_id)
);

CREATE INDEX IF NOT EXISTS cts_parallel_import_deferred_claim_idx
    ON cads.cts_parallel_import_deferred_rows
       (run_id, status, dependency_depth, table_name, transaction_row_id);

-- Preserve existing plans, checkpoints, deferred rows and history on deployment.
-- Retained-source mode uses the same ledger name as the former serial importer
-- so installations upgrading from that importer keep their copied-row evidence.
CREATE TABLE IF NOT EXISTS cads.cts_bulk_import_migrated_rows
(
    table_name text NOT NULL,
    transaction_row_id bigint NOT NULL,
    migrated_at timestamptz NOT NULL DEFAULT clock_timestamp(),
    PRIMARY KEY (table_name, transaction_row_id)
);

-- Ensure every eligible source table has the critical worker access path. It
-- supports the planner's B-row count/min/max and each worker's ordered trans_id
-- range scan. The deterministic 63-byte-safe name matches the version 3 index,
-- so rerunning this script performs only a quick existence check when present.
DO $indexes$
DECLARE
    v_table record;
    v_index_name text;
BEGIN
    FOR v_table IN
        SELECT source.oid, source.relname AS table_name
        FROM pg_class source
        JOIN pg_namespace source_schema
          ON source_schema.oid = source.relnamespace
        WHERE source_schema.nspname = 'cts_transactions'
          AND source.relkind IN ('r', 'p')
          AND to_regclass(format('cts.%I', source.relname)) IS NOT NULL
          AND EXISTS (
              SELECT FROM pg_attribute column_definition
              WHERE column_definition.attrelid = source.oid
                AND column_definition.attname = 'trans_id'
                AND NOT column_definition.attisdropped
          )
          AND EXISTS (
              SELECT FROM pg_attribute column_definition
              WHERE column_definition.attrelid = source.oid
                AND column_definition.attname = 'trans_type'
                AND NOT column_definition.attisdropped
          )
    LOOP
        v_index_name := left(v_table.table_name, 25)
                        || '_bulk_trans_type_b_trans_id_'
                        || substr(md5(v_table.table_name), 1, 6)
                        || '_idx';

        EXECUTE format(
            'CREATE INDEX IF NOT EXISTS %I '
            || 'ON cts_transactions.%I (trans_id) '
            || 'WHERE trans_type = ''B''',
            v_index_name,
            v_table.table_name
        );
    END LOOP;
END;
$indexes$;

-- Build one immutable dependency plan and fixed original total per table. The
-- caller receives the run_id and passes it to every worker session.
-- This four-argument overload deliberately has no defaults: the existing
-- three-argument/defaulted API below delegates with clearing disabled. Keeping
-- its signature preserves existing dependencies/grants and avoids ambiguous
-- default-argument overload resolution on upgraded installations.
CREATE OR REPLACE PROCEDURE cads.prepare_cts_parallel_bulk_import(
    IN p_delete_source boolean,
    IN p_range_size bigint,
    INOUT p_run_id bigint,
    IN p_truncate_cts boolean
)
LANGUAGE plpgsql
AS $procedure$
DECLARE
    v_table record;
    v_total bigint;
    v_min_id bigint;
    v_max_id bigint;
    v_truncate_tables text;
BEGIN
    IF p_delete_source IS NULL THEN
        RAISE EXCEPTION 'p_delete_source must be true or false';
    END IF;
    IF p_range_size IS NULL OR p_range_size < 1 THEN
        RAISE EXCEPTION 'p_range_size must be greater than zero';
    END IF;
    IF p_truncate_cts IS NULL THEN
        RAISE EXCEPTION 'p_truncate_cts must be true or false';
    END IF;

    -- Serialise planning only. Workers do not take this global planner lock.
    IF NOT pg_try_advisory_xact_lock(hashtextextended('cads.cts_parallel_import_planner', 0)) THEN
        RAISE EXCEPTION 'Another CTS parallel import is currently being planned';
    END IF;

    IF EXISTS (
        SELECT FROM cads.cts_parallel_import_runs
        WHERE status IN ('planning', 'ready', 'processing bulk', 'processing errors')
    ) THEN
        RAISE EXCEPTION 'An unfinished CTS parallel import run already exists';
    END IF;

    -- Never turn a resume or a later import into a destination reset. In
    -- particular, clearing after delete-source batches would destroy the only
    -- remaining copy of their migrated records.
    IF p_truncate_cts AND EXISTS (SELECT FROM cads.cts_parallel_import_runs) THEN
        RAISE EXCEPTION 'p_truncate_cts is permitted only when preparing fresh run 1; import history already exists';
    END IF;
    -- Do not silently invalidate evidence from the former retained-source
    -- importer. Reconciliation of an existing ledger is a separate operation.
    IF p_truncate_cts AND EXISTS (SELECT FROM cads.cts_bulk_import_migrated_rows) THEN
        RAISE EXCEPTION 'Cannot clear CTS while the retained-source migration ledger contains rows';
    END IF;

    INSERT INTO cads.cts_parallel_import_runs (status, delete_source, range_size, truncate_cts)
    VALUES ('planning', p_delete_source, p_range_size, p_truncate_cts)
    RETURNING run_id INTO p_run_id;

    IF p_truncate_cts AND p_run_id <> 1 THEN
        RAISE EXCEPTION 'p_truncate_cts requires generated run ID 1, but generated %; no destination tables were cleared', p_run_id;
    END IF;

    -- Persist all direct child -> parent edges between eligible paired tables.
    INSERT INTO cads.cts_parallel_import_dependencies (run_id, child_table, parent_table)
    SELECT DISTINCT p_run_id, child.relname, parent.relname
    FROM pg_constraint fk
    JOIN pg_class child ON child.oid = fk.conrelid
    JOIN pg_namespace child_ns ON child_ns.oid = child.relnamespace
    JOIN pg_class parent ON parent.oid = fk.confrelid
    JOIN pg_namespace parent_ns ON parent_ns.oid = parent.relnamespace
    WHERE fk.contype = 'f'
      AND child_ns.nspname = 'cts'
      AND parent_ns.nspname = 'cts'
      AND child.relname <> parent.relname
      AND to_regclass(format('cts_transactions.%I', child.relname)) IS NOT NULL
      AND to_regclass(format('cts_transactions.%I', parent.relname)) IS NOT NULL
      AND EXISTS (SELECT FROM information_schema.columns c WHERE c.table_schema = 'cts_transactions' AND c.table_name = child.relname AND c.column_name = 'trans_id')
      AND EXISTS (SELECT FROM information_schema.columns c WHERE c.table_schema = 'cts_transactions' AND c.table_name = child.relname AND c.column_name = 'trans_type')
      AND EXISTS (SELECT FROM information_schema.columns c WHERE c.table_schema = 'cts_transactions' AND c.table_name = parent.relname AND c.column_name = 'trans_id')
      AND EXISTS (SELECT FROM information_schema.columns c WHERE c.table_schema = 'cts_transactions' AND c.table_name = parent.relname AND c.column_name = 'trans_type');

    -- Reject cross-table cycles. They need a separately designed constraint-group
    -- load and cannot safely be released by a parent-completion barrier.
    IF EXISTS (
        WITH RECURSIVE walk AS (
            SELECT child_table AS start_table, parent_table AS current_table,
                   ARRAY[child_table, parent_table]::text[] AS path
            FROM cads.cts_parallel_import_dependencies WHERE run_id = p_run_id
            UNION ALL
            SELECT walk.start_table, edge.parent_table, walk.path || edge.parent_table
            FROM walk
            JOIN cads.cts_parallel_import_dependencies edge
              ON edge.run_id = p_run_id AND edge.child_table = walk.current_table
            WHERE NOT edge.parent_table = ANY(walk.path)
        )
        SELECT FROM walk w
        JOIN cads.cts_parallel_import_dependencies edge
          ON edge.run_id = p_run_id
         AND edge.child_table = w.current_table
         AND edge.parent_table = w.start_table
    ) THEN
        RAISE EXCEPTION 'The CTS foreign-key graph contains a cycle; parallel plan aborted';
    END IF;

    -- Calculate longest parent distance, then count and range-plan every table.
    FOR v_table IN
        WITH RECURSIVE eligible AS (
            SELECT source.relname AS table_name
            FROM pg_class source
            JOIN pg_namespace ns ON ns.oid = source.relnamespace
            WHERE ns.nspname = 'cts_transactions'
              AND source.relkind IN ('r', 'p')
              AND to_regclass(format('cts.%I', source.relname)) IS NOT NULL
              AND EXISTS (SELECT FROM pg_attribute a WHERE a.attrelid = source.oid AND a.attname = 'trans_id' AND NOT a.attisdropped)
              AND EXISTS (SELECT FROM pg_attribute a WHERE a.attrelid = source.oid AND a.attname = 'trans_type' AND NOT a.attisdropped)
        ), paths AS (
            SELECT table_name AS start_table, table_name AS current_table, 0 AS depth
            FROM eligible
            UNION ALL
            SELECT paths.start_table, edge.parent_table, paths.depth + 1
            FROM paths
            JOIN cads.cts_parallel_import_dependencies edge
              ON edge.run_id = p_run_id AND edge.child_table = paths.current_table
        )
        SELECT eligible.table_name, max(paths.depth)::integer AS dependency_depth
        FROM eligible JOIN paths ON paths.start_table = eligible.table_name
        GROUP BY eligible.table_name
        ORDER BY max(paths.depth), eligible.table_name
    LOOP
        EXECUTE format(
            'SELECT count(*), min(trans_id), max(trans_id) '
            || 'FROM cts_transactions.%I WHERE trans_type = ''B''',
            v_table.table_name
        ) INTO v_total, v_min_id, v_max_id;

        INSERT INTO cads.cts_parallel_import_status (
            run_id, table_name, dependency_depth, original_total, status
        ) VALUES (
            p_run_id, v_table.table_name, v_table.dependency_depth, v_total,
            CASE
                WHEN v_total = 0 THEN 'bulk complete'
                WHEN v_table.dependency_depth = 0 THEN 'ready'
                ELSE 'blocked by parents'
            END
        );

        IF v_total > 0 THEN
            -- A table-level dependency graph cannot order rows that reference
            -- other rows in the same table. Give every self-referencing table
            -- one work range so only one worker owns it. The worker then loads
            -- it in iterative parent-ready passes.
            IF EXISTS (
                SELECT
                FROM pg_constraint self_fk
                WHERE self_fk.contype = 'f'
                  AND self_fk.conrelid = format('cts.%I', v_table.table_name)::regclass
                  AND self_fk.confrelid = self_fk.conrelid
            ) THEN
                INSERT INTO cads.cts_parallel_import_work_queue (
                    run_id, table_name, dependency_depth,
                    minimum_trans_id, maximum_trans_id, state
                ) VALUES (
                    p_run_id, v_table.table_name, v_table.dependency_depth,
                    v_min_id, v_max_id,
                    CASE WHEN v_table.dependency_depth = 0 THEN 'ready' ELSE 'blocked' END
                );

                RAISE NOTICE
                    'Table % has self-referencing foreign keys; planned as one range for iterative parent-ready loading.',
                    v_table.table_name;
            ELSE
                INSERT INTO cads.cts_parallel_import_work_queue (
                    run_id, table_name, dependency_depth,
                    minimum_trans_id, maximum_trans_id, state
                )
                SELECT p_run_id, v_table.table_name, v_table.dependency_depth,
                       range_start,
                       LEAST(range_start + p_range_size - 1, v_max_id),
                       CASE WHEN v_table.dependency_depth = 0 THEN 'ready' ELSE 'blocked' END
                FROM generate_series(v_min_id, v_max_id, p_range_size) AS range_start;
            END IF;
        END IF;

        RAISE NOTICE 'Planned table % at depth %: % bulk row(s), trans_id % to %.',
            v_table.table_name, v_table.dependency_depth, v_total, v_min_id, v_max_id;
    END LOOP;

    -- Empty parents are already complete, so release children whose entire parent
    -- set is now terminal before workers start.
    UPDATE cads.cts_parallel_import_work_queue work
    SET state = 'ready'
    WHERE work.run_id = p_run_id AND work.state = 'blocked'
      AND NOT EXISTS (
          SELECT FROM cads.cts_parallel_import_dependencies dependency
          JOIN cads.cts_parallel_import_status parent_status
            ON parent_status.run_id = dependency.run_id
           AND parent_status.table_name = dependency.parent_table
          WHERE dependency.run_id = work.run_id
            AND dependency.child_table = work.table_name
            AND parent_status.status NOT IN ('bulk complete', 'bulk complete with deferred rows')
      );

    UPDATE cads.cts_parallel_import_status status
    SET status = 'ready', updated_at = clock_timestamp()
    WHERE status.run_id = p_run_id AND status.status = 'blocked by parents'
      AND EXISTS (SELECT FROM cads.cts_parallel_import_work_queue work WHERE work.run_id = status.run_id AND work.table_name = status.table_name AND work.state = 'ready');

    IF p_truncate_cts THEN
        -- All planning/validation above and the clear below share one transaction.
        -- No worker can see the new ready plan until preparation commits. A failed
        -- TRUNCATE or later preparation error rolls back both the clear and plan.
        -- Clear ALL ordinary/partitioned CTS tables, not just source-matched ones.
        -- Do not reset identities, drop constraints/indexes or use CASCADE.
        IF EXISTS (
            SELECT FROM pg_class c JOIN pg_namespace n ON n.oid = c.relnamespace
            WHERE n.nspname = 'cts' AND c.relkind = 'f'
        ) THEN
            RAISE EXCEPTION 'Cannot clear all CTS tables: foreign tables require separate handling';
        END IF;

        -- TRUNCATE includes inheritance/partition descendants. Refuse a layout
        -- that would implicitly reach another schema (or foreign storage).
        IF EXISTS (
            WITH RECURSIVE descendants AS (
                SELECT c.oid FROM pg_class c
                JOIN pg_namespace n ON n.oid = c.relnamespace
                WHERE n.nspname = 'cts' AND c.relkind IN ('r', 'p')
                UNION
                SELECT i.inhrelid FROM pg_inherits i
                JOIN descendants d ON d.oid = i.inhparent
            )
            SELECT FROM descendants d
            JOIN pg_class c ON c.oid = d.oid
            JOIN pg_namespace n ON n.oid = c.relnamespace
            WHERE n.nspname <> 'cts' OR c.relkind NOT IN ('r', 'p')
        ) THEN
            RAISE EXCEPTION 'Cannot clear CTS: a table has descendants outside CTS or in foreign storage';
        END IF;

        -- List roots only; descendants inside CTS are included automatically.
        -- A single statement handles internal parent/child FKs regardless of order.
        SELECT string_agg(format('%I.%I', n.nspname, c.relname), ', ' ORDER BY c.relname)
        INTO v_truncate_tables
        FROM pg_class c
        JOIN pg_namespace n ON n.oid = c.relnamespace
        WHERE n.nspname = 'cts' AND c.relkind IN ('r', 'p')
          AND NOT EXISTS (
              SELECT FROM pg_inherits i
              JOIN pg_class parent ON parent.oid = i.inhparent
              JOIN pg_namespace parent_ns ON parent_ns.oid = parent.relnamespace
              WHERE i.inhrelid = c.oid AND parent_ns.nspname = 'cts'
          );

        IF v_truncate_tables IS NULL THEN
            RAISE EXCEPTION 'Cannot clear CTS: no destination tables found';
        END IF;

        -- A fresh one-off run is the explicit boundary for database-level
        -- performance measurements. Reset shared PostgreSQL statistics immediately
        -- before the destructive clear, never during a normal redeploy/resume.
        BEGIN
            PERFORM pg_catalog.pg_stat_reset();
            IF to_regprocedure('pg_catalog.pg_stat_statements_reset()') IS NOT NULL THEN
                EXECUTE 'SELECT pg_catalog.pg_stat_statements_reset()';
            END IF;
        EXCEPTION WHEN insufficient_privilege THEN
            RAISE EXCEPTION 'p_truncate_cts requires permission to reset pg_stat and pg_stat_statements before clearing CTS';
        END;

        RAISE NOTICE 'Run 1: pg_stat and pg_stat_statements reset; clearing all CTS destination tables before workers start: %', v_truncate_tables;
        EXECUTE 'TRUNCATE TABLE ' || v_truncate_tables || ' CONTINUE IDENTITY RESTRICT';
        RAISE NOTICE 'Run 1: CTS destination tables cleared; source rows and import history retained. Clear becomes permanent when preparation commits.';
    END IF;

    UPDATE cads.cts_parallel_import_runs SET status = 'ready' WHERE run_id = p_run_id;
    RAISE NOTICE 'CTS parallel import plan % is ready. Start multiple workers with CALL cads.run_cts_parallel_bulk_worker(%, ...).', p_run_id, p_run_id;
END;
$procedure$;

-- Compatibility entry point: old positional/named calls and default arguments
-- continue to work and NEVER clear the destination. Retain the original routine
-- rather than dropping it, so existing grants and dependent objects survive.
CREATE OR REPLACE PROCEDURE cads.prepare_cts_parallel_bulk_import(
    IN p_delete_source boolean DEFAULT true,
    IN p_range_size bigint DEFAULT 5000000,
    INOUT p_run_id bigint DEFAULT NULL
)
LANGUAGE plpgsql
AS $procedure$
BEGIN
    CALL cads.prepare_cts_parallel_bulk_import(
        p_delete_source, p_range_size, p_run_id, false
    );
END;
$procedure$;

-- Each invocation is a long-lived worker. Queue-row locking and non-overlapping
-- ranges provide logical exclusion; source FOR UPDATE also protects against
-- unrelated concurrent consumers. Do not skip locked source rows: an empty
-- skipped batch must not be mistaken for a finished range or an orphaned chain.
CREATE OR REPLACE PROCEDURE cads.run_cts_parallel_bulk_worker(
    IN p_run_id bigint,
    IN p_worker_name text,
    IN p_batch_size integer DEFAULT 100000,
    IN p_defer_threshold integer DEFAULT 100
)
LANGUAGE plpgsql
AS $procedure$
DECLARE
    v_work record;
    v_run record;
    v_column_list text;
    v_select_list text;
    v_self_reference_predicate text;
    v_ids bigint[];
    v_effective_size integer;
    v_batch_cap integer;
    v_affected bigint;
    v_message text;
    v_detail text;
    v_hint text;
    v_context text;
    v_sqlstate text;
    v_failed boolean;
    v_deferred_count bigint;
    v_unfinished bigint;
BEGIN
    IF p_worker_name IS NULL OR btrim(p_worker_name) = '' THEN RAISE EXCEPTION 'p_worker_name is required'; END IF;
    IF current_setting('transaction_isolation') <> 'read committed' THEN
        RAISE EXCEPTION 'Parallel workers require READ COMMITTED isolation for safe claim admission';
    END IF;
    IF p_batch_size IS NULL OR p_batch_size < 1 THEN RAISE EXCEPTION 'p_batch_size must be positive'; END IF;
    IF p_defer_threshold IS NULL OR p_defer_threshold < 1 THEN RAISE EXCEPTION 'p_defer_threshold must be positive'; END IF;

    SELECT * INTO v_run FROM cads.cts_parallel_import_runs WHERE run_id = p_run_id;
    IF NOT FOUND THEN RAISE EXCEPTION 'Parallel import run % does not exist', p_run_id; END IF;
    IF v_run.status NOT IN ('ready', 'processing bulk') THEN RAISE EXCEPTION 'Run % is in state %', p_run_id, v_run.status; END IF;

    INSERT INTO cads.cts_parallel_import_workers (run_id, worker_name, backend_pid, status)
    VALUES (p_run_id, p_worker_name, pg_backend_pid(), 'starting')
    ON CONFLICT (run_id, worker_name) DO UPDATE
       SET backend_pid = EXCLUDED.backend_pid, status = 'starting',
           current_work_id = NULL, current_table_name = NULL,
           started_at = clock_timestamp(), heartbeat_at = clock_timestamp(),
           completed_at = NULL, last_error_message = NULL;

    UPDATE cads.cts_parallel_import_runs SET status = 'processing bulk' WHERE run_id = p_run_id;
    COMMIT;
    RAISE NOTICE 'CTS parallel worker % started for run % on backend PID %.', p_worker_name, p_run_id, pg_backend_pid();

    LOOP
        -- Serialize only admission, never the actual data migration. The count
        -- and claim below are separate statements under READ COMMITTED, so a
        -- competing claimant sees the preceding committed claim. Locking only
        -- individual queue rows would allow concurrent counts to exceed a cap.
        PERFORM pg_advisory_xact_lock(
            hashtextextended(format('cads.cts_parallel_claim.%s', p_run_id), 0)
        );
        -- Release newly eligible child ranges after all parent tables have reached
        -- a bulk terminal state.
        UPDATE cads.cts_parallel_import_work_queue work
        SET state = 'ready'
        WHERE work.run_id = p_run_id AND work.state = 'blocked'
          AND NOT EXISTS (
              SELECT FROM cads.cts_parallel_import_dependencies dependency
              JOIN cads.cts_parallel_import_status parent_status
                ON parent_status.run_id = dependency.run_id
               AND parent_status.table_name = dependency.parent_table
              WHERE dependency.run_id = work.run_id
                AND dependency.child_table = work.table_name
                AND parent_status.status NOT IN ('bulk complete', 'bulk complete with deferred rows')
          );

        UPDATE cads.cts_parallel_import_status status
        SET status = 'ready', updated_at = clock_timestamp()
        WHERE status.run_id = p_run_id AND status.status = 'blocked by parents'
          AND EXISTS (SELECT FROM cads.cts_parallel_import_work_queue work WHERE work.run_id = status.run_id AND work.table_name = status.table_name AND work.state = 'ready');

        -- Prefer tables with fewer assigned ranges, then the least recently
        -- claimed table. Dependencies, not a global depth barrier, decide which
        -- tables are eligible. This spreads a shared pool across independent
        -- tables instead of filling every range of the first alphabetical table.
        WITH candidate AS (
            SELECT work.work_id
            FROM cads.cts_parallel_import_work_queue work
            LEFT JOIN cads.cts_parallel_import_table_settings settings
              ON settings.table_name = work.table_name
            CROSS JOIN LATERAL (
                SELECT count(*) AS active_ranges
                FROM cads.cts_parallel_import_work_queue assigned
                WHERE assigned.run_id = work.run_id
                  AND assigned.table_name = work.table_name
                  AND assigned.state = 'processing'
            ) slots
            CROSS JOIN LATERAL (
                SELECT max(previous.claimed_at) AS last_claimed
                FROM cads.cts_parallel_import_work_queue previous
                WHERE previous.run_id = work.run_id
                  AND previous.table_name = work.table_name
            ) history
            WHERE work.run_id = p_run_id AND work.state = 'ready'
              AND slots.active_ranges < CASE WHEN EXISTS (
                  SELECT FROM pg_constraint fk
                  WHERE fk.contype = 'f'
                    AND fk.conrelid = to_regclass(format('cts.%I', work.table_name))
                    AND fk.confrelid = fk.conrelid
              ) THEN 1 ELSE COALESCE(settings.max_workers, 4) END
              AND NOT EXISTS (
                  SELECT FROM cads.cts_parallel_import_dependencies dependency
                  JOIN cads.cts_parallel_import_status parent_status
                    ON parent_status.run_id = dependency.run_id
                   AND parent_status.table_name = dependency.parent_table
                  WHERE dependency.run_id = work.run_id
                    AND dependency.child_table = work.table_name
                    AND parent_status.status NOT IN ('bulk complete', 'bulk complete with deferred rows')
              )
            ORDER BY slots.active_ranges, history.last_claimed NULLS FIRST,
                     work.dependency_depth, work.table_name, work.minimum_trans_id
            FOR UPDATE OF work SKIP LOCKED
            LIMIT 1
        )
        UPDATE cads.cts_parallel_import_work_queue work
        SET state = 'processing', claimed_by = p_worker_name,
            claimed_backend_pid = pg_backend_pid(), claimed_at = clock_timestamp(),
            heartbeat_at = clock_timestamp()
        FROM candidate
        WHERE work.work_id = candidate.work_id
        RETURNING work.* INTO v_work;

        IF NOT FOUND THEN
            SELECT count(*) INTO v_unfinished
            FROM cads.cts_parallel_import_work_queue
            WHERE run_id = p_run_id AND state IN ('blocked', 'ready', 'processing');

            IF v_unfinished = 0 THEN EXIT; END IF;

            UPDATE cads.cts_parallel_import_workers
            SET status = 'waiting', current_work_id = NULL,
                current_table_name = NULL, heartbeat_at = clock_timestamp()
            WHERE run_id = p_run_id AND worker_name = p_worker_name;
            COMMIT;
            PERFORM pg_sleep(1);
            CONTINUE;
        END IF;

        UPDATE cads.cts_parallel_import_workers
        SET status = 'working', current_work_id = v_work.work_id,
            current_table_name = v_work.table_name, heartbeat_at = clock_timestamp()
        WHERE run_id = p_run_id AND worker_name = p_worker_name;
        UPDATE cads.cts_parallel_import_status
        SET status = 'processing bulk', started_at = COALESCE(started_at, clock_timestamp()),
            updated_at = clock_timestamp()
        WHERE run_id = p_run_id AND table_name = v_work.table_name;
        COMMIT;

        SELECT string_agg(format('%I', target.column_name), ', ' ORDER BY target.ordinal_position),
               string_agg(format('src.%I', target.column_name), ', ' ORDER BY target.ordinal_position)
        INTO v_column_list, v_select_list
        FROM information_schema.columns target
        JOIN information_schema.columns source
          ON source.table_schema = 'cts_transactions' AND source.table_name = target.table_name AND source.column_name = target.column_name
        WHERE target.table_schema = 'cts' AND target.table_name = v_work.table_name
          AND target.is_generated = 'NEVER' AND target.identity_generation IS DISTINCT FROM 'ALWAYS';
        IF v_column_list IS NULL THEN RAISE EXCEPTION 'Table % has no shared writable columns', v_work.table_name; END IF;

        -- Build one eligibility predicate for every self-referencing FK on this
        -- table. MATCH SIMPLE permits any null child key; MATCH FULL permits an
        -- entirely null child key. A row can satisfy its OWN referenced key in
        -- the same INSERT (e.g. cry_id = cry_cry_id_main_eu); all key columns must
        -- match for a composite FK. Otherwise the parent must exist in cts.
        -- Each self FK must independently qualify. Re-running after every commit
        -- walks self-referencing chains from their roots towards their leaves.
        SELECT string_agg(
                   format(
                       '((%s) OR (%s) OR EXISTS (SELECT FROM cts.%I self_parent WHERE %s))',
                       self_fk.null_predicate,
                       self_fk.own_row_predicate,
                       v_work.table_name,
                       self_fk.join_predicate
                   ),
                   ' AND ' ORDER BY self_fk.constraint_oid
               )
        INTO v_self_reference_predicate
        FROM (
            SELECT
                fk.oid AS constraint_oid,
                CASE fk.confmatchtype
                    WHEN 'f' THEN string_agg(
                        format('src.%I IS NULL', child_attribute.attname),
                        ' AND ' ORDER BY key_column.ordinality
                    )
                    ELSE string_agg(
                        format('src.%I IS NULL', child_attribute.attname),
                        ' OR ' ORDER BY key_column.ordinality
                    )
                END AS null_predicate,
                string_agg(
                    format(
                        'self_parent.%I = src.%I',
                        parent_attribute.attname,
                        child_attribute.attname
                    ),
                    ' AND ' ORDER BY key_column.ordinality
                ) AS join_predicate,
                string_agg(
                    format('src.%I = src.%I', parent_attribute.attname, child_attribute.attname),
                    ' AND ' ORDER BY key_column.ordinality
                ) AS own_row_predicate
            FROM pg_constraint fk
            CROSS JOIN LATERAL unnest(fk.conkey, fk.confkey)
                WITH ORDINALITY AS key_column(child_attnum, parent_attnum, ordinality)
            JOIN pg_attribute child_attribute
              ON child_attribute.attrelid = fk.conrelid
             AND child_attribute.attnum = key_column.child_attnum
            JOIN pg_attribute parent_attribute
              ON parent_attribute.attrelid = fk.confrelid
             AND parent_attribute.attnum = key_column.parent_attnum
            WHERE fk.contype = 'f'
              AND fk.conrelid = format('cts.%I', v_work.table_name)::regclass
              AND fk.confrelid = fk.conrelid
            GROUP BY fk.oid, fk.confmatchtype
        ) self_fk;

        v_self_reference_predicate := COALESCE(v_self_reference_predicate, 'TRUE');

        v_effective_size := p_batch_size;
        LOOP
            -- Re-read the batch ceiling at batch boundaries. A setting change
            -- never interrupts an in-flight batch; smaller caller sizes win.
            SELECT LEAST(p_batch_size, COALESCE((
                SELECT settings.batch_size
                FROM cads.cts_parallel_import_table_settings settings
                WHERE settings.table_name = v_work.table_name
            ), p_batch_size)) INTO v_batch_cap;
            v_effective_size := LEAST(v_effective_size, v_batch_cap);
            v_ids := NULL;
            v_failed := false;
            -- Acquire row locks INSIDE the rollback scope that modifies them.
            -- Locking before an EXCEPTION/savepoint and deleting inside it can
            -- itself create MultiXacts. On failure all row locks and DML in this
            -- attempt roll back; durable range ownership still excludes peers.
            BEGIN
                EXECUTE format(
                    'SELECT array_agg(x.trans_id ORDER BY x.trans_id) FROM ('
                    || 'SELECT src.trans_id FROM cts_transactions.%I src '
                    || 'WHERE src.trans_type = ''B'' AND src.trans_id BETWEEN $1 AND $2 '
                    || 'AND NOT EXISTS (SELECT FROM cads.cts_parallel_import_deferred_rows d '
                    || 'WHERE d.run_id = $3 AND d.table_name = $4 AND d.transaction_row_id = src.trans_id) '
                    || 'AND NOT EXISTS (SELECT FROM cads.cts_bulk_import_migrated_rows m '
                    || 'WHERE m.table_name = $4 AND m.transaction_row_id = src.trans_id) '
                    || 'AND (%s) '
                    || 'ORDER BY src.trans_id LIMIT $5 FOR UPDATE OF src) x',
                    v_work.table_name,
                    v_self_reference_predicate
                ) INTO v_ids USING v_work.minimum_trans_id, v_work.maximum_trans_id,
                                   p_run_id, v_work.table_name, v_effective_size;

                IF v_ids IS NOT NULL THEN
                    EXECUTE format(
                        'INSERT INTO cts.%I (%s) SELECT %s FROM cts_transactions.%I src '
                        || 'WHERE src.trans_id = ANY($1) AND src.trans_type = ''B''',
                        v_work.table_name, v_column_list, v_select_list, v_work.table_name
                    ) USING v_ids;
                    GET DIAGNOSTICS v_affected = ROW_COUNT;
                    IF v_affected <> cardinality(v_ids) THEN RAISE EXCEPTION 'Selected %, inserted %', cardinality(v_ids), v_affected; END IF;

                    IF v_run.delete_source THEN
                        EXECUTE format('DELETE FROM cts_transactions.%I WHERE trans_id = ANY($1) AND trans_type = ''B''', v_work.table_name) USING v_ids;
                        GET DIAGNOSTICS v_affected = ROW_COUNT;
                        IF v_affected <> cardinality(v_ids) THEN RAISE EXCEPTION 'Inserted %, deleted %', cardinality(v_ids), v_affected; END IF;
                    ELSE
                        INSERT INTO cads.cts_bulk_import_migrated_rows (table_name, transaction_row_id)
                        SELECT v_work.table_name, unnest(v_ids) ON CONFLICT DO NOTHING;
                        GET DIAGNOSTICS v_affected = ROW_COUNT;
                        IF v_affected <> cardinality(v_ids) THEN RAISE EXCEPTION 'Inserted %, checkpointed %', cardinality(v_ids), v_affected; END IF;
                    END IF;

                    UPDATE cads.cts_parallel_import_work_queue
                    SET rows_migrated = rows_migrated + cardinality(v_ids), heartbeat_at = clock_timestamp()
                    WHERE work_id = v_work.work_id;
                    UPDATE cads.cts_parallel_import_status
                    SET number_migrated = number_migrated + cardinality(v_ids), updated_at = clock_timestamp()
                    WHERE run_id = p_run_id AND table_name = v_work.table_name;
                    UPDATE cads.cts_parallel_import_workers
                    SET batches_committed = batches_committed + 1,
                        rows_migrated = rows_migrated + cardinality(v_ids), heartbeat_at = clock_timestamp()
                    WHERE run_id = p_run_id AND worker_name = p_worker_name;
                END IF;
            EXCEPTION WHEN OTHERS THEN
                -- Selection/infrastructure failures are not row errors. Never
                -- defer an old ID list when no batch was successfully selected.
                IF v_ids IS NULL THEN RAISE; END IF;
                GET STACKED DIAGNOSTICS v_message = MESSAGE_TEXT;
                v_failed := true;
            END;

            IF v_ids IS NULL THEN
                -- For a self-referencing table, no eligible IDs after all prior
                -- commits means every remaining row has an unresolved self-parent
                -- (an orphan, a cycle, or a parent deferred for another error).
                -- Preserve those records in the source and defer them for later
                -- row-level diagnosis instead of claiming bulk completion.
                IF v_self_reference_predicate <> 'TRUE' THEN
                    v_message := 'No parent-ready path remains for one or more self-referencing foreign keys';
                    EXECUTE format(
                        'INSERT INTO cads.cts_parallel_import_deferred_rows '
                        || '(run_id, table_name, transaction_row_id, dependency_depth, bulk_error_message) '
                        || 'SELECT $3, $4, src.trans_id, $5, $6 '
                        || 'FROM cts_transactions.%I src '
                        || 'WHERE src.trans_type = ''B'' AND src.trans_id BETWEEN $1 AND $2 '
                        || 'AND NOT EXISTS (SELECT FROM cads.cts_parallel_import_deferred_rows d '
                        || 'WHERE d.run_id = $3 AND d.table_name = $4 AND d.transaction_row_id = src.trans_id) '
                        || 'AND NOT EXISTS (SELECT FROM cads.cts_bulk_import_migrated_rows m '
                        || 'WHERE m.table_name = $4 AND m.transaction_row_id = src.trans_id) '
                        || 'ON CONFLICT (run_id, table_name, transaction_row_id) DO NOTHING',
                        v_work.table_name
                    ) USING v_work.minimum_trans_id, v_work.maximum_trans_id,
                              p_run_id, v_work.table_name, v_work.dependency_depth, v_message;
                    GET DIAGNOSTICS v_deferred_count = ROW_COUNT;

                    IF v_deferred_count > 0 THEN
                        UPDATE cads.cts_parallel_import_work_queue
                        SET rows_deferred = rows_deferred + v_deferred_count,
                            heartbeat_at = clock_timestamp(), last_error_message = v_message
                        WHERE work_id = v_work.work_id;
                        UPDATE cads.cts_parallel_import_status
                        SET number_deferred = number_deferred + v_deferred_count,
                            updated_at = clock_timestamp(), last_error_message = v_message
                        WHERE run_id = p_run_id AND table_name = v_work.table_name;
                        UPDATE cads.cts_parallel_import_workers
                        SET rows_deferred = rows_deferred + v_deferred_count,
                            heartbeat_at = clock_timestamp(), last_error_message = v_message
                        WHERE run_id = p_run_id AND worker_name = p_worker_name;
                        COMMIT;
                        RAISE NOTICE
                            'Worker % deferred % unresolved self-referencing row(s) from table % after all parent-ready passes.',
                            p_worker_name, v_deferred_count, v_work.table_name;
                    END IF;
                END IF;
                EXIT;
            END IF;

            IF v_failed THEN
                IF cardinality(v_ids) > p_defer_threshold THEN
                    v_effective_size := GREATEST(p_defer_threshold, cardinality(v_ids) / 2);
                    UPDATE cads.cts_parallel_import_work_queue
                    SET last_error_message = v_message, heartbeat_at = clock_timestamp()
                    WHERE work_id = v_work.work_id;
                    UPDATE cads.cts_parallel_import_workers SET heartbeat_at = clock_timestamp(), last_error_message = v_message
                    WHERE run_id = p_run_id AND worker_name = p_worker_name;
                    COMMIT;
                    RAISE NOTICE 'Worker % table % reduced batch to % after: %', p_worker_name, v_work.table_name, v_effective_size, v_message;
                ELSE
                    INSERT INTO cads.cts_parallel_import_deferred_rows (
                        run_id, table_name, transaction_row_id, dependency_depth, bulk_error_message
                    ) SELECT p_run_id, v_work.table_name, unnest(v_ids), v_work.dependency_depth, v_message
                    ON CONFLICT (run_id, table_name, transaction_row_id) DO NOTHING;
                    GET DIAGNOSTICS v_deferred_count = ROW_COUNT;
                    UPDATE cads.cts_parallel_import_work_queue
                    SET rows_deferred = rows_deferred + v_deferred_count,
                        heartbeat_at = clock_timestamp(), last_error_message = v_message
                    WHERE work_id = v_work.work_id;
                    UPDATE cads.cts_parallel_import_status
                    SET number_deferred = number_deferred + v_deferred_count,
                        updated_at = clock_timestamp(), last_error_message = v_message
                    WHERE run_id = p_run_id AND table_name = v_work.table_name;
                    UPDATE cads.cts_parallel_import_workers
                    SET rows_deferred = rows_deferred + v_deferred_count,
                        heartbeat_at = clock_timestamp(), last_error_message = v_message
                    WHERE run_id = p_run_id AND worker_name = p_worker_name;
                    v_effective_size := v_batch_cap;
                    COMMIT;
                    RAISE NOTICE 'Worker % deferred % row(s) from table % for later diagnosis.', p_worker_name, v_deferred_count, v_work.table_name;
                END IF;
            ELSE
                COMMIT;
                v_effective_size := LEAST(v_batch_cap::bigint, v_effective_size::bigint * 2)::integer;
                RAISE NOTICE 'Worker % committed % row(s) for %.', p_worker_name, cardinality(v_ids), v_work.table_name;
            END IF;
        END LOOP;

        SELECT rows_deferred INTO v_deferred_count FROM cads.cts_parallel_import_work_queue WHERE work_id = v_work.work_id FOR UPDATE;
        UPDATE cads.cts_parallel_import_work_queue
        SET state = CASE WHEN v_deferred_count > 0 THEN 'complete with deferred rows' ELSE 'complete' END,
            completed_at = clock_timestamp(), heartbeat_at = clock_timestamp()
        WHERE work_id = v_work.work_id;

        -- Lock the table status while checking whether this was its final range.
        PERFORM 1 FROM cads.cts_parallel_import_status
        WHERE run_id = p_run_id AND table_name = v_work.table_name FOR UPDATE;
        SELECT count(*) INTO v_unfinished
        FROM cads.cts_parallel_import_work_queue
        WHERE run_id = p_run_id AND table_name = v_work.table_name
          AND state IN ('blocked', 'ready', 'processing');
        IF v_unfinished = 0 THEN
            SELECT count(*) INTO v_deferred_count
            FROM cads.cts_parallel_import_deferred_rows
            WHERE run_id = p_run_id AND table_name = v_work.table_name AND status <> 'resolved';
            UPDATE cads.cts_parallel_import_status
            SET status = CASE WHEN v_deferred_count > 0 THEN 'bulk complete with deferred rows' ELSE 'bulk complete' END,
                number_deferred = v_deferred_count, bulk_completed_at = clock_timestamp(), updated_at = clock_timestamp()
            WHERE run_id = p_run_id AND table_name = v_work.table_name;
        END IF;
        COMMIT;
    END LOOP;

    UPDATE cads.cts_parallel_import_runs
    SET status = 'bulk complete', bulk_completed_at = clock_timestamp()
    WHERE run_id = p_run_id
      AND NOT EXISTS (SELECT FROM cads.cts_parallel_import_status WHERE run_id = p_run_id AND status NOT IN ('bulk complete', 'bulk complete with deferred rows'));
    UPDATE cads.cts_parallel_import_workers
    SET status = 'complete', current_work_id = NULL, current_table_name = NULL,
        heartbeat_at = clock_timestamp(), completed_at = clock_timestamp()
    WHERE run_id = p_run_id AND worker_name = p_worker_name;
    COMMIT;
    RAISE NOTICE 'CTS parallel worker % finished run %.', p_worker_name, p_run_id;
END;
$procedure$;

-- Retry all deferred rows only after bulk completion, or one bulk-finished
-- table during an existing run. The latter repairs old country deferrals without
-- reopening completed ranges or resetting the plan. Both entry points share one
-- per-run retry lock. Never run row retries alongside bulk work on the SAME table.
CREATE OR REPLACE PROCEDURE cads.retry_cts_parallel_deferred_rows(
    IN p_run_id bigint,
    IN p_table_name text
)
LANGUAGE plpgsql
AS $procedure$
DECLARE
    v_row record;
    v_run record;
    v_column_list text;
    v_select_list text;
    v_affected bigint;
    v_deleted bigint;
    v_message text;
    v_detail text;
    v_hint text;
    v_context text;
    v_sqlstate text;
    v_pass integer := 0;
    v_pass_resolved bigint;
    v_total_resolved bigint := 0;
BEGIN
    SELECT * INTO v_run FROM cads.cts_parallel_import_runs WHERE run_id = p_run_id FOR UPDATE;
    IF NOT FOUND THEN RAISE EXCEPTION 'Parallel import run % does not exist', p_run_id; END IF;
    IF p_table_name IS NULL THEN
        IF v_run.status NOT IN ('bulk complete', 'processing errors', 'complete with errors') THEN
            RAISE EXCEPTION 'Run % bulk phase is not complete', p_run_id;
        END IF;
    ELSE
        IF v_run.status NOT IN ('ready', 'processing bulk', 'bulk complete', 'processing errors', 'complete with errors') THEN
            RAISE EXCEPTION 'Run % is not available for deferred recovery (status %)', p_run_id, v_run.status;
        END IF;
        IF NOT EXISTS (
            SELECT FROM cads.cts_parallel_import_status
            WHERE run_id = p_run_id AND table_name = p_table_name
              AND status IN ('bulk complete', 'bulk complete with deferred rows', 'complete', 'complete with errors')
        ) THEN
            RAISE EXCEPTION 'Table % must finish its bulk phase before deferred recovery', p_table_name;
        END IF;
    END IF;
    IF EXISTS (
        SELECT FROM cads.cts_parallel_import_work_queue
        WHERE run_id = p_run_id AND (p_table_name IS NULL OR table_name = p_table_name)
          AND state NOT IN ('complete', 'complete with deferred rows')
    ) THEN
        RAISE EXCEPTION 'Selected tables still have unfinished bulk ranges; deferred recovery refused';
    END IF;

    IF NOT pg_try_advisory_lock(
        hashtextextended(format('cads.cts_parallel_retry.%s', p_run_id), 0)
    ) THEN
        RAISE EXCEPTION 'Deferred-row retry is already running for run %', p_run_id;
    END IF;
    -- A scoped repair must not change a processing-bulk run into error-processing
    -- or complete, because other bulk workers may still be working.
    IF p_table_name IS NULL THEN
        UPDATE cads.cts_parallel_import_runs SET status = 'processing errors' WHERE run_id = p_run_id;
    END IF;
    COMMIT;
    RAISE NOTICE 'Deferred recovery started: run %, table %, backend PID %.',
        p_run_id, COALESCE(p_table_name, 'ALL'), pg_backend_pid();

    LOOP
        v_pass := v_pass + 1;
        v_pass_resolved := 0;
        -- Repeat parent-first passes while successes occur. A self-parent may
        -- have a higher transaction ID than its child. A single pass is therefore
        -- insufficient even when tables are ordered by dependency depth.
        FOR v_row IN
            SELECT deferred.* FROM cads.cts_parallel_import_deferred_rows deferred
            WHERE deferred.run_id = p_run_id AND deferred.status IN ('pending', 'error')
              AND (p_table_name IS NULL OR deferred.table_name = p_table_name)
            ORDER BY deferred.dependency_depth, deferred.table_name, deferred.transaction_row_id
        LOOP
            BEGIN
                SELECT string_agg(format('%I', target.column_name), ', ' ORDER BY target.ordinal_position),
                       string_agg(format('src.%I', target.column_name), ', ' ORDER BY target.ordinal_position)
                INTO v_column_list, v_select_list
                FROM information_schema.columns target
                JOIN information_schema.columns source
                  ON source.table_schema = 'cts_transactions' AND source.table_name = target.table_name AND source.column_name = target.column_name
                WHERE target.table_schema = 'cts' AND target.table_name = v_row.table_name
                  AND target.is_generated = 'NEVER' AND target.identity_generation IS DISTINCT FROM 'ALWAYS';
                IF v_column_list IS NULL THEN
                    RAISE EXCEPTION 'Table % has no shared writable columns', v_row.table_name;
                END IF;

                -- Lock and validate the source in the same rollback scope as its
                -- move. Missing or no-longer-B rows are errors, not successes.
                EXECUTE format(
                    'SELECT 1 FROM cts_transactions.%I WHERE trans_id = $1 AND trans_type = ''B'' FOR UPDATE',
                    v_row.table_name
                ) USING v_row.transaction_row_id;
                GET DIAGNOSTICS v_affected = ROW_COUNT;
                IF v_affected <> 1 THEN
                    RAISE EXCEPTION 'Expected one bulk source row for %.trans_id %, found %',
                        v_row.table_name, v_row.transaction_row_id, v_affected USING ERRCODE = 'P0002';
                END IF;
                EXECUTE format(
                    'INSERT INTO cts.%I (%s) SELECT %s FROM cts_transactions.%I src '
                    || 'WHERE src.trans_id = $1 AND src.trans_type = ''B''',
                    v_row.table_name, v_column_list, v_select_list, v_row.table_name
                ) USING v_row.transaction_row_id;
                GET DIAGNOSTICS v_affected = ROW_COUNT;
                IF v_affected <> 1 THEN
                    RAISE EXCEPTION 'Expected one inserted row for %.trans_id %, inserted %',
                        v_row.table_name, v_row.transaction_row_id, v_affected;
                END IF;

                IF v_run.delete_source THEN
                    EXECUTE format('DELETE FROM cts_transactions.%I WHERE trans_id = $1 AND trans_type = ''B''', v_row.table_name)
                    USING v_row.transaction_row_id;
                    GET DIAGNOSTICS v_deleted = ROW_COUNT;
                    IF v_deleted <> 1 THEN
                        RAISE EXCEPTION 'Expected one deleted row for %.trans_id %, deleted %',
                            v_row.table_name, v_row.transaction_row_id, v_deleted;
                    END IF;
                ELSE
                    INSERT INTO cads.cts_bulk_import_migrated_rows (table_name, transaction_row_id)
                    VALUES (v_row.table_name, v_row.transaction_row_id) ON CONFLICT DO NOTHING;
                END IF;

                UPDATE cads.cts_parallel_import_deferred_rows
                SET status = 'resolved', retry_count = retry_count + 1,
                    last_attempted_at = clock_timestamp(), resolved_at = clock_timestamp(),
                    error_sqlstate = NULL, error_message = NULL, error_detail = NULL,
                    error_hint = NULL, error_context = NULL
                WHERE run_id = p_run_id AND table_name = v_row.table_name AND transaction_row_id = v_row.transaction_row_id;
                UPDATE cads.cts_parallel_import_status
                SET number_migrated = number_migrated + 1,
                    number_deferred = GREATEST(number_deferred - 1, 0), updated_at = clock_timestamp()
                WHERE run_id = p_run_id AND table_name = v_row.table_name;
                -- Keep range/task totals consistent with the table after repair.
                -- Worker samples remain the historic BULK-worker measurements.
                UPDATE cads.cts_parallel_import_work_queue
                SET rows_migrated = rows_migrated + 1,
                    rows_deferred = GREATEST(rows_deferred - 1, 0)
                WHERE run_id = p_run_id AND table_name = v_row.table_name
                  AND v_row.transaction_row_id BETWEEN minimum_trans_id AND maximum_trans_id;
                v_pass_resolved := v_pass_resolved + 1;
            EXCEPTION WHEN OTHERS THEN
                GET STACKED DIAGNOSTICS v_message = MESSAGE_TEXT, v_detail = PG_EXCEPTION_DETAIL,
                    v_hint = PG_EXCEPTION_HINT, v_context = PG_EXCEPTION_CONTEXT,
                    v_sqlstate = RETURNED_SQLSTATE;
                UPDATE cads.cts_parallel_import_deferred_rows
                SET status = 'error', retry_count = retry_count + 1,
                    last_attempted_at = clock_timestamp(), error_sqlstate = v_sqlstate,
                    error_message = v_message, error_detail = nullif(v_detail, ''),
                    error_hint = nullif(v_hint, ''), error_context = nullif(v_context, '')
                WHERE run_id = p_run_id AND table_name = v_row.table_name AND transaction_row_id = v_row.transaction_row_id;
            END;
            COMMIT;
        END LOOP;
        v_total_resolved := v_total_resolved + v_pass_resolved;
        RAISE NOTICE 'Deferred recovery run %, pass %: resolved % row(s), total %.',
            p_run_id, v_pass, v_pass_resolved, v_total_resolved;
        EXIT WHEN v_pass_resolved = 0;
    END LOOP;

    UPDATE cads.cts_parallel_import_work_queue work
    SET state = CASE WHEN EXISTS (
            SELECT FROM cads.cts_parallel_import_deferred_rows deferred
            WHERE deferred.run_id = work.run_id AND deferred.table_name = work.table_name
              AND deferred.transaction_row_id BETWEEN work.minimum_trans_id AND work.maximum_trans_id
              AND deferred.status <> 'resolved'
        ) THEN 'complete with deferred rows' ELSE 'complete' END
    WHERE work.run_id = p_run_id AND (p_table_name IS NULL OR work.table_name = p_table_name);
    -- Preserve the original bulk error on each deferred record for audit, but
    -- do not present it as a current table/range error after successful repair.
    UPDATE cads.cts_parallel_import_work_queue
    SET last_error_message = NULL
    WHERE run_id = p_run_id AND (p_table_name IS NULL OR table_name = p_table_name)
      AND state = 'complete';

    UPDATE cads.cts_parallel_import_status status
    SET status = CASE WHEN EXISTS (
            SELECT FROM cads.cts_parallel_import_deferred_rows deferred
            WHERE deferred.run_id = status.run_id AND deferred.table_name = status.table_name
              AND deferred.status <> 'resolved'
        ) THEN CASE WHEN p_table_name IS NULL THEN 'complete with errors' ELSE 'bulk complete with deferred rows' END
          ELSE CASE WHEN p_table_name IS NULL THEN 'complete' ELSE 'bulk complete' END END,
        completed_at = CASE WHEN p_table_name IS NULL THEN clock_timestamp() ELSE status.completed_at END,
        updated_at = clock_timestamp()
    WHERE status.run_id = p_run_id AND (p_table_name IS NULL OR status.table_name = p_table_name);
    UPDATE cads.cts_parallel_import_status
    SET last_error_message = NULL
    WHERE run_id = p_run_id AND (p_table_name IS NULL OR table_name = p_table_name)
      AND status IN ('complete', 'bulk complete');
    IF p_table_name IS NULL THEN
        UPDATE cads.cts_parallel_import_runs run
        SET status = CASE WHEN EXISTS (
                SELECT FROM cads.cts_parallel_import_deferred_rows deferred
                WHERE deferred.run_id = run.run_id AND deferred.status <> 'resolved'
            ) THEN 'complete with errors' ELSE 'complete' END,
            completed_at = clock_timestamp()
        WHERE run.run_id = p_run_id;
    END IF;
    COMMIT;
    PERFORM pg_advisory_unlock(
        hashtextextended(format('cads.cts_parallel_retry.%s', p_run_id), 0)
    );
    RAISE NOTICE 'Deferred recovery finished: run %, table %, resolved %.',
        p_run_id, COALESCE(p_table_name, 'ALL'), v_total_resolved;
END;
$procedure$;

-- Preserve the original signature, grants and existing CALL statements.
CREATE OR REPLACE PROCEDURE cads.retry_cts_parallel_deferred_rows(IN p_run_id bigint)
LANGUAGE plpgsql
AS $procedure$
BEGIN
    CALL cads.retry_cts_parallel_deferred_rows(p_run_id, NULL);
END;
$procedure$;

-- Recover ranges abandoned by terminated worker backends. A live PID is never
-- reclaimed regardless of heartbeat age. If an errored psql session remains idle,
-- terminate or close that backend first, then call this procedure.
CREATE OR REPLACE PROCEDURE cads.requeue_cts_parallel_stale_work(
    IN p_run_id bigint,
    IN p_stale_after interval DEFAULT interval '30 minutes'
)
LANGUAGE plpgsql
AS $procedure$
DECLARE
    v_requeued bigint;
BEGIN
    IF p_stale_after IS NULL OR p_stale_after <= interval '0 seconds' THEN
        RAISE EXCEPTION 'p_stale_after must be greater than zero';
    END IF;

    UPDATE cads.cts_parallel_import_work_queue work
    SET state = 'ready', claimed_by = NULL, claimed_backend_pid = NULL,
        claimed_at = NULL, heartbeat_at = clock_timestamp(),
        last_error_message = concat_ws('; ', work.last_error_message,
            'Requeued after terminated worker backend')
    WHERE work.run_id = p_run_id
      AND work.state = 'processing'
      AND work.heartbeat_at < clock_timestamp() - p_stale_after
      AND NOT EXISTS (
          SELECT FROM pg_stat_activity activity
          WHERE activity.pid = work.claimed_backend_pid
      );
    GET DIAGNOSTICS v_requeued = ROW_COUNT;

    UPDATE cads.cts_parallel_import_workers worker
    SET status = 'failed', completed_at = clock_timestamp(),
        heartbeat_at = clock_timestamp(),
        last_error_message = concat_ws('; ', worker.last_error_message,
            'Backend ended before assigned work completed')
    WHERE worker.run_id = p_run_id
      AND worker.status = 'working'
      AND NOT EXISTS (
          SELECT FROM pg_stat_activity activity WHERE activity.pid = worker.backend_pid
      );

    RAISE NOTICE 'Requeued % stale work range(s) for parallel import run %.',
        v_requeued, p_run_id;
END;
$procedure$;

-- Plan-level monitoring function. The plan order is immutable for the life of
-- the run: dependency depth enforces parent-before-child ordering and table name
-- gives tables at the same depth a deterministic display order. Worker counts
-- are reconciled with pg_stat_activity so terminated sessions are not reported
-- as live merely because their durable worker row still says "working".
DROP FUNCTION IF EXISTS cads.get_cts_parallel_import_status(bigint);

CREATE OR REPLACE FUNCTION cads.get_cts_parallel_import_status(p_run_id bigint)
RETURNS TABLE
(
    plan_order bigint,
    dependency_depth integer,
    table_name text,
    status text,
    original_total text,
    number_migrated text,
    number_deferred text,
    remaining text,
    live_workers bigint,
    currently_executing bigint,
    live_but_waiting bigint,
    stale_workers bigint,
    updated_at timestamp with time zone,
    last_error_message text
)
LANGUAGE sql
STABLE
AS $function$
    WITH worker_state AS (
        SELECT
            worker.run_id,
            worker.current_table_name AS table_name,
            count(DISTINCT worker.backend_pid) FILTER (
                WHERE activity.pid IS NOT NULL
                  AND worker.status IN ('starting', 'working', 'waiting')
            )::bigint AS live_workers,
            count(DISTINCT worker.backend_pid) FILTER (
                WHERE activity.pid IS NOT NULL
                  AND activity.state = 'active'
                  AND worker.status IN ('starting', 'working', 'waiting')
            )::bigint AS currently_executing,
            count(DISTINCT worker.backend_pid) FILTER (
                WHERE activity.pid IS NOT NULL
                  AND activity.wait_event IS NOT NULL
                  AND worker.status IN ('starting', 'working', 'waiting')
            )::bigint AS live_but_waiting,
            count(*) FILTER (
                WHERE activity.pid IS NULL
                  AND worker.status IN ('starting', 'working', 'waiting')
            )::bigint AS stale_workers
        FROM cads.cts_parallel_import_workers worker
        LEFT JOIN pg_catalog.pg_stat_activity activity
          ON activity.pid = worker.backend_pid
        WHERE worker.run_id = p_run_id
        GROUP BY worker.run_id, worker.current_table_name
    ),
    plan AS (
        SELECT
            row_number() OVER (
                ORDER BY status.dependency_depth, status.table_name
            )::bigint AS plan_order,
            status.*
        FROM cads.cts_parallel_import_status status
        WHERE status.run_id = p_run_id
    )
    SELECT
        plan.plan_order,
        plan.dependency_depth,
        plan.table_name,
        plan.status,
        to_char(plan.original_total, 'FM999,999,999,999,999,999,999')::text,
        to_char(plan.number_migrated, 'FM999,999,999,999,999,999,999')::text,
        to_char(plan.number_deferred, 'FM999,999,999,999,999,999,999')::text,
        to_char(GREATEST(
            plan.original_total - plan.number_migrated - plan.number_deferred,
            0
        ), 'FM999,999,999,999,999,999,999')::text AS remaining,
        COALESCE(worker_state.live_workers, 0)::bigint,
        COALESCE(worker_state.currently_executing, 0)::bigint,
        COALESCE(worker_state.live_but_waiting, 0)::bigint,
        COALESCE(worker_state.stale_workers, 0)::bigint,
        plan.updated_at,
        plan.last_error_message
    FROM plan
    LEFT JOIN worker_state
      ON worker_state.run_id = plan.run_id
     AND worker_state.table_name = plan.table_name
    ORDER BY plan.plan_order;
$function$;

COMMENT ON FUNCTION cads.get_cts_parallel_import_status(bigint)
IS 'Returns the immutable dependency plan and table progress, with worker counts reconciled against live PostgreSQL backends.';

-- Table-to-date timing, successful-row throughput and assigned worker liveness.
-- Do not require activity.state='active': this is hidden from some monitoring
-- roles, and a live worker waiting for IO/locks still owns its assigned range.
CREATE OR REPLACE FUNCTION cads.get_cts_parallel_import_progress(p_run_id bigint)
RETURNS TABLE
(
    table_name text,
    elapsed_hours numeric,
    elapsed_minutes numeric,
    original_rows text,
    migrated_rows text,
    deferred_rows text,
    remaining_bulk_rows text,
    migrated_rows_per_minute text,
    migrated_rows_per_second text,
    active_workers bigint,
    stale_workers bigint
)
LANGUAGE sql
STABLE
AS $function$
    WITH timings AS (
        SELECT s.*,
               GREATEST(EXTRACT(EPOCH FROM (
                   COALESCE(s.bulk_completed_at, s.completed_at, statement_timestamp())
                   - s.started_at
               )), 0) AS elapsed_seconds
        FROM cads.cts_parallel_import_status s
        WHERE s.run_id = p_run_id AND s.started_at IS NOT NULL
    ), worker_state AS (
        SELECT w.current_table_name AS table_name,
               count(DISTINCT w.backend_pid) FILTER (WHERE a.pid IS NOT NULL) AS active_workers,
               count(*) FILTER (WHERE a.pid IS NULL) AS stale_workers
        FROM cads.cts_parallel_import_workers w
        LEFT JOIN pg_catalog.pg_stat_activity a
          ON a.pid = w.backend_pid
         AND a.datname = current_database()
         -- A newer backend with a recycled PID is not this worker. When start
         -- visibility is restricted, retain the PID-based liveness indication.
         AND (a.backend_start IS NULL OR a.backend_start <= w.started_at)
        WHERE w.run_id = p_run_id
          AND w.status IN ('starting', 'working', 'waiting')
          AND w.current_table_name IS NOT NULL
        GROUP BY w.current_table_name
    )
    SELECT t.table_name,
           round(t.elapsed_seconds / 3600.0, 2),
           round(t.elapsed_seconds / 60.0, 2),
           to_char(t.original_total, 'FM999,999,999,999,999,999,999'),
           to_char(t.number_migrated, 'FM999,999,999,999,999,999,999'),
           to_char(t.number_deferred, 'FM999,999,999,999,999,999,999'),
           to_char(GREATEST(t.original_total - t.number_migrated - t.number_deferred, 0),
                   'FM999,999,999,999,999,999,999'),
           to_char(t.number_migrated * 60.0 / NULLIF(t.elapsed_seconds, 0),
                   'FM999,999,999,999,999,999,999.00'),
           to_char(t.number_migrated / NULLIF(t.elapsed_seconds, 0),
                   'FM999,999,999,999,999,999,999.00'),
           COALESCE(w.active_workers, 0),
           COALESCE(w.stale_workers, 0)
    FROM timings t
    LEFT JOIN worker_state w ON w.table_name = t.table_name
    ORDER BY t.original_total DESC, t.table_name;
$function$;

COMMENT ON FUNCTION cads.get_cts_parallel_import_progress(bigint)
IS 'Started-table elapsed time, formatted successful-row averages and assigned live/stale workers. Active means backend present, not necessarily executing; restricted backend visibility prevents full PID-reuse verification. Rates include waits and are table-to-date, not recent intervals; post-bulk retries change migrated totals but not the frozen bulk duration.';

-- Live-worker monitoring function. An inner join to pg_stat_activity deliberately
-- excludes terminated sessions; those remain visible as failed audit rows after
-- cads.requeue_cts_parallel_stale_work has recovered their ranges.
CREATE OR REPLACE FUNCTION cads.get_cts_parallel_import_workers(p_run_id bigint)
RETURNS TABLE
(
    worker_type text,
    worker_name text,
    status text,
    backend_pid integer,
    current_table_name text,
    current_work_id bigint,
    minimum_trans_id bigint,
    maximum_trans_id bigint,
    range_status text,
    range_rows_migrated bigint,
    range_rows_deferred bigint,
    batches_committed bigint,
    worker_total_migrated bigint,
    worker_total_deferred bigint,
    heartbeat_at timestamp with time zone,
    last_error_message text
)
LANGUAGE sql
STABLE
AS $function$
    SELECT
        CASE
            WHEN worker.worker_name LIKE 'cron-worker-%' THEN 'pg_cron'
            WHEN worker.worker_name LIKE 'terminal-worker-%'
              OR worker.worker_name LIKE 'replacement-worker-%' THEN 'terminal'
            ELSE 'other'
        END::text AS worker_type,
        worker.worker_name,
        worker.status,
        worker.backend_pid,
        worker.current_table_name,
        worker.current_work_id,
        work.minimum_trans_id,
        work.maximum_trans_id,
        work.state AS range_status,
        work.rows_migrated AS range_rows_migrated,
        work.rows_deferred AS range_rows_deferred,
        worker.batches_committed,
        worker.rows_migrated AS worker_total_migrated,
        worker.rows_deferred AS worker_total_deferred,
        worker.heartbeat_at,
        work.last_error_message
    FROM cads.cts_parallel_import_workers worker
    JOIN pg_catalog.pg_stat_activity activity
      ON activity.pid = worker.backend_pid
    LEFT JOIN cads.cts_parallel_import_work_queue work
      ON work.run_id = worker.run_id
     AND work.work_id = worker.current_work_id
    WHERE worker.run_id = p_run_id
      AND worker.status IN ('starting', 'working', 'waiting')
    ORDER BY worker_type, worker.worker_name;
$function$;

COMMENT ON FUNCTION cads.get_cts_parallel_import_workers(bigint)
IS 'Returns only live parallel-import workers and their currently assigned work range.';

-- Task-level monitoring function. This exposes every immutable range generated
-- by the planner, including blocked, ready, processing, completed and failed work.
CREATE OR REPLACE FUNCTION cads.get_cts_parallel_import_tasks(p_run_id bigint)
RETURNS TABLE
(
    task_order bigint,
    work_id bigint,
    dependency_depth integer,
    table_name text,
    minimum_trans_id bigint,
    maximum_trans_id bigint,
    range_status text,
    claimed_by text,
    claimed_backend_pid integer,
    worker_is_live boolean,
    rows_migrated bigint,
    rows_deferred bigint,
    claimed_at timestamp with time zone,
    heartbeat_at timestamp with time zone,
    completed_at timestamp with time zone,
    last_error_message text
)
LANGUAGE sql
STABLE
AS $function$
    SELECT
        row_number() OVER (
            ORDER BY work.dependency_depth, work.table_name,
                     work.minimum_trans_id, work.work_id
        )::bigint AS task_order,
        work.work_id,
        work.dependency_depth,
        work.table_name,
        work.minimum_trans_id,
        work.maximum_trans_id,
        work.state AS range_status,
        work.claimed_by,
        work.claimed_backend_pid,
        activity.pid IS NOT NULL AS worker_is_live,
        work.rows_migrated,
        work.rows_deferred,
        work.claimed_at,
        work.heartbeat_at,
        work.completed_at,
        work.last_error_message
    FROM cads.cts_parallel_import_work_queue work
    LEFT JOIN pg_catalog.pg_stat_activity activity
      ON activity.pid = work.claimed_backend_pid
    WHERE work.run_id = p_run_id
    ORDER BY task_order;
$function$;

COMMENT ON FUNCTION cads.get_cts_parallel_import_tasks(bigint)
IS 'Returns every work-range task in immutable dependency execution order and identifies whether its claimed backend is live.';

-- Persistent worker timings and throughput. The write lock keeps baseline
-- capture atomic with trigger installation; redeployment preserves all samples.
LOCK TABLE cads.cts_parallel_import_workers IN SHARE ROW EXCLUSIVE MODE;

-- Keep history independently of the resettable planner tables. Each execution
-- is identified by run, worker name and the worker's recorded start timestamp.
-- Samples share the worker transaction: rolled-back progress is not logged.
CREATE TABLE IF NOT EXISTS cads.cts_parallel_import_worker_samples
(
    sample_id bigint GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    run_id bigint NOT NULL,
    worker_name text NOT NULL,
    backend_pid integer,
    worker_started_at timestamptz NOT NULL,
    observed_at timestamptz NOT NULL DEFAULT clock_timestamp(),
    status text NOT NULL,
    table_name text,
    work_id bigint,
    batches_committed bigint NOT NULL,
    rows_migrated bigint NOT NULL,
    rows_deferred bigint NOT NULL,
    worker_completed_at timestamptz,
    baseline_only boolean NOT NULL DEFAULT false
);

CREATE INDEX IF NOT EXISTS cts_parallel_worker_samples_execution_idx
    ON cads.cts_parallel_import_worker_samples
       (run_id, worker_name, worker_started_at, sample_id);

CREATE INDEX IF NOT EXISTS cts_parallel_worker_samples_observed_idx
    ON cads.cts_parallel_import_worker_samples (run_id, observed_at);

-- Existing executions begin measurement at installation. Their historic totals
-- may include earlier invocations of the same worker name; do not attribute those
-- totals to the current execution. Later rates use differences from this baseline.
INSERT INTO cads.cts_parallel_import_worker_samples
    (run_id, worker_name, backend_pid, worker_started_at, status,
     table_name, work_id, batches_committed, rows_migrated, rows_deferred,
     worker_completed_at, baseline_only)
SELECT worker.run_id, worker.worker_name, worker.backend_pid, worker.started_at, worker.status,
       worker.current_table_name, worker.current_work_id, worker.batches_committed,
       worker.rows_migrated, worker.rows_deferred, worker.completed_at, true
FROM cads.cts_parallel_import_workers worker
WHERE NOT EXISTS (
    SELECT 1 FROM cads.cts_parallel_import_worker_samples sample
    WHERE sample.run_id = worker.run_id
      AND sample.worker_name = worker.worker_name
      AND sample.worker_started_at = worker.started_at
);

CREATE OR REPLACE FUNCTION cads.log_cts_parallel_worker_progress()
RETURNS trigger
LANGUAGE plpgsql
AS $function$
BEGIN
    -- Do not log every waiting heartbeat. Capture starts, finishes, assignment
    -- changes, committed row progress and deferred-row progress. This avoids
    -- large volumes of idle samples while keeping meaningful throughput history.
    IF TG_OP = 'UPDATE' THEN
        IF ROW(NEW.started_at, NEW.backend_pid, NEW.status,
               NEW.current_work_id, NEW.current_table_name,
               NEW.batches_committed, NEW.rows_migrated, NEW.rows_deferred,
               NEW.completed_at)
           IS NOT DISTINCT FROM
           ROW(OLD.started_at, OLD.backend_pid, OLD.status,
               OLD.current_work_id, OLD.current_table_name,
               OLD.batches_committed, OLD.rows_migrated, OLD.rows_deferred,
               OLD.completed_at) THEN
            RETURN NEW;
        END IF;
    END IF;

    INSERT INTO cads.cts_parallel_import_worker_samples
        (run_id, worker_name, backend_pid, worker_started_at, status,
         table_name, work_id, batches_committed, rows_migrated, rows_deferred,
         worker_completed_at)
    VALUES
        (NEW.run_id, NEW.worker_name, NEW.backend_pid, NEW.started_at, NEW.status,
         NEW.current_table_name, NEW.current_work_id, NEW.batches_committed,
         NEW.rows_migrated, NEW.rows_deferred, NEW.completed_at);
    RETURN NEW;
END;
$function$;

DROP TRIGGER IF EXISTS cts_parallel_worker_progress_log ON cads.cts_parallel_import_workers;
CREATE TRIGGER cts_parallel_worker_progress_log
AFTER INSERT OR UPDATE ON cads.cts_parallel_import_workers
FOR EACH ROW EXECUTE FUNCTION cads.log_cts_parallel_worker_progress();

-- One row per execution, including completed/failed executions that disappear
-- from the live-worker function. Keep metrics numeric for comparisons/averages.
-- elapsed_seconds includes waiting and retry time. For a failed/stale worker
-- the last sample is the last known progress, not the exact disconnect time.
CREATE OR REPLACE VIEW cads.cts_parallel_import_worker_metrics AS
WITH bounds AS (
    SELECT run_id, worker_name, worker_started_at,
           min(sample_id) AS first_sample_id, max(sample_id) AS last_sample_id
    FROM cads.cts_parallel_import_worker_samples
    GROUP BY run_id, worker_name, worker_started_at
), measured AS (
    SELECT first.run_id, first.worker_name, first.worker_started_at AS started_at,
           last.backend_pid, last.status,
           first.observed_at AS measured_from,
           last.observed_at AS last_progress_at,
           last.worker_completed_at AS completed_at,
           first.baseline_only AS partial_history,
           last.rows_migrated - first.rows_migrated AS migrated_rows,
           last.rows_deferred - first.rows_deferred AS deferred_rows,
           last.batches_committed - first.batches_committed AS successful_batches,
           CASE
               WHEN last.status = 'complete' THEN 'complete'
               WHEN last.status = 'failed' THEN 'failed'
               WHEN EXISTS (
                   SELECT 1 FROM cads.cts_parallel_import_workers worker
                   JOIN pg_catalog.pg_stat_activity activity
                     ON activity.pid = worker.backend_pid
                   WHERE worker.run_id = last.run_id
                     AND worker.worker_name = last.worker_name
                     AND worker.started_at = last.worker_started_at
                     AND worker.backend_pid = last.backend_pid
                     AND activity.datname = current_database()
                     AND activity.backend_start <= worker.started_at
               ) THEN 'live'
               WHEN EXISTS (
                   SELECT 1 FROM cads.cts_parallel_import_workers worker
                   JOIN pg_catalog.pg_stat_activity activity ON activity.pid = worker.backend_pid
                   WHERE worker.run_id = last.run_id
                     AND worker.worker_name = last.worker_name
                     AND worker.started_at = last.worker_started_at
                     AND worker.backend_pid = last.backend_pid
                     AND activity.datname = current_database()
                     AND activity.backend_start IS NULL
               ) THEN 'backend present; visibility restricted'
               ELSE 'stale or replaced'
           END AS execution_state
    FROM bounds
    JOIN cads.cts_parallel_import_worker_samples first
      ON first.sample_id = bounds.first_sample_id
    JOIN cads.cts_parallel_import_worker_samples last
      ON last.sample_id = bounds.last_sample_id
), durations AS (
    SELECT measured.*,
           GREATEST(extract(epoch FROM (
               CASE WHEN execution_state IN ('live', 'backend present; visibility restricted') THEN statement_timestamp()
                    WHEN execution_state = 'complete' THEN completed_at
                    ELSE last_progress_at END
               - measured_from
           )), 0) AS elapsed_seconds
    FROM measured
)
SELECT durations.*,
       round(migrated_rows / NULLIF(elapsed_seconds, 0), 2) AS rows_per_second,
       round(migrated_rows * 60::numeric / NULLIF(elapsed_seconds, 0), 2)
           AS rows_per_minute,
       round(elapsed_seconds / NULLIF(successful_batches, 0), 2)
           AS elapsed_seconds_per_successful_batch
FROM durations;

COMMENT ON VIEW cads.cts_parallel_import_worker_metrics IS
'Per-execution progress since logging began; duration includes waits/retries, partial_history flags existing-worker baselines, and failed completion timestamps may reflect later cleanup.';

COMMENT ON TABLE cads.cts_parallel_import_worker_samples IS
'Transactional worker progress history. observed_at is sampled before commit, not the WAL flush completion time. Counter differences support comparisons across time windows.';
