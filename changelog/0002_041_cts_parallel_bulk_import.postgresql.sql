-- liquibase formatted sql

-- changeset MarkGent1:1789533600000-1 splitStatements:false

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

-- Remove old run-1 schedules at changeset application time. This is deliberately
-- outside all procedures and only targets this import's own job-name prefix.
DO $cron_cleanup$
DECLARE
    v_jobid bigint;
BEGIN
    IF to_regclass('cron.job') IS NOT NULL THEN
        FOR v_jobid IN EXECUTE
            'SELECT jobid FROM cron.job WHERE jobname LIKE ''cts-import-run-1-worker-%'''
        LOOP
            EXECUTE 'SELECT cron.unschedule($1)' USING v_jobid;
        END LOOP;
    END IF;
END;
$cron_cleanup$;

-- Remove only the known failing foreign keys.  PostgreSQL implements FK
-- enforcement with system RI triggers, which a normal deployment role cannot
-- disable.  042 supplies missing parent rows as S records; all other
-- constraints remain enabled.  These statements are idempotent and can be
-- run by the table owner.
ALTER TABLE cts.ct_animal_identifiers
    DROP CONSTRAINT IF EXISTS fk_ct_animal_identifiers_aid_aid_id_original,
    DROP CONSTRAINT IF EXISTS fk_ct_animal_identifiers_aid_aid_id_previous,
    DROP CONSTRAINT IF EXISTS fk_ct_animal_identifiers_aid_ran_id;

ALTER TABLE cts.ct_animal_correct_summaries
    DROP CONSTRAINT IF EXISTS fk_ct_animal_correct_summaries_acs_ran_id,
    DROP CONSTRAINT IF EXISTS fk_ct_animal_correct_summaries_acs_san_id;

ALTER TABLE cts.ct_movt_correct_summaries
    DROP CONSTRAINT IF EXISTS fk_ct_movt_correct_summaries_mcs_smo_id,
    DROP CONSTRAINT IF EXISTS fk_ct_movt_correct_summaries_mcs_rmo_id,
    DROP CONSTRAINT IF EXISTS fk_ct_movt_correct_summaries_mcs_mov_id;

-- Migration stubs for the remaining ct_animal_identifiers deferred rows.
-- These location parents are absent from cts_transactions and cts, so they
-- cannot be recovered by the deferred-row resolver.  The S rows are consumed
-- before the dependent B rows by the normal CTS transaction ordering.
INSERT INTO cts_transactions.ct_locations (
    trans_type,
    loc_id,
    fake_data
)
SELECT
    'S',
    v.loc_id,
    1
FROM (
    VALUES
        (62619::numeric),
        (281946::numeric),
        (273056::numeric)
) AS v(loc_id)
WHERE NOT EXISTS (
    SELECT 1
    FROM cts_transactions.ct_locations existing
    WHERE existing.loc_id = v.loc_id
      AND existing.trans_type = 'S'
);

-- Keep the source transaction type on every CTS destination row. Existing CTS
-- rows are treated as ordinary bulk rows; newly generated stub source rows use
-- S and ordinary source rows use B. The worker's dynamic writable-column list
-- copies this value during the normal insert.
-- Destination writes are upserts: a B row replaces a prior S stub, while an S
-- row never overwrites an existing B row.
DO $destination_type$
DECLARE
    v_table record;
BEGIN
    FOR v_table IN
        SELECT c.relname AS table_name
        FROM pg_class c
        JOIN pg_namespace n
          ON n.oid = c.relnamespace
        WHERE n.nspname = 'cts'
          AND c.relkind IN ('r', 'p')
    LOOP
        EXECUTE format(
            'ALTER TABLE cts.%I '
            || 'ADD COLUMN IF NOT EXISTS trans_type text '
            || 'NOT NULL DEFAULT ''B''',
            v_table.table_name
        );
    END LOOP;
END;
$destination_type$;

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

-- These are per-table policies shared by all runs. Missing entries use 25
-- workers and the caller's batch size. A batch_size override is a ceiling, not
-- permission to exceed the caller's requested size. Stale processing claims
-- still consume slots until explicitly recovered; never steal live work.
CREATE TABLE IF NOT EXISTS cads.cts_parallel_import_table_settings
(
    table_name text PRIMARY KEY,
    max_workers integer NOT NULL DEFAULT 30 CHECK (max_workers > 0),
    batch_size integer CHECK (batch_size > 0)
);

-- Default per-table worker cap for the bulk import.
-- Re-deployment must not overwrite operator tuning.
INSERT INTO cads.cts_parallel_import_table_settings (table_name, max_workers, batch_size)
VALUES ('ct_valid_applications', 30, 10000)
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
-- supports the planner's B/S-row count/min/max and each worker's ordered trans_id
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
                        || '_bulk_trans_type_sb_trans_id_'
                        || substr(md5(v_table.table_name), 1, 6)
                        || '_idx';

        EXECUTE format(
            'CREATE INDEX IF NOT EXISTS %I '
            || 'ON cts_transactions.%I (trans_id) '
            || 'WHERE trans_type IN (''B'', ''S'')',
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

    -- Preserve parent-first planning when selected FK constraints have been
    -- removed from CTS. These edges mirror the known relationships used by
    -- the migration-stub workflow and are idempotent with the FK-derived rows.
    INSERT INTO cads.cts_parallel_import_dependencies (run_id, child_table, parent_table)
    SELECT p_run_id, edge.child_table, edge.parent_table
    FROM (VALUES
        ('ct_animal_identifiers', 'ct_registered_animals'),
        ('ct_animal_correct_summaries', 'ct_registered_animals'),
        ('ct_animal_correct_summaries', 'ct_received_applications'),
        ('ct_movt_correct_summaries', 'ct_suspended_movements'),
        ('ct_movt_correct_summaries', 'ct_received_movements'),
        ('ct_movt_correct_summaries', 'ct_registered_movements'),
        ('ct_movt_corr_summ_errors', 'ct_movt_correct_summaries')
    ) AS edge(child_table, parent_table)
    WHERE to_regclass(format('cts_transactions.%I', edge.child_table)) IS NOT NULL
      AND to_regclass(format('cts_transactions.%I', edge.parent_table)) IS NOT NULL
    ON CONFLICT (run_id, child_table, parent_table) DO NOTHING;

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
            || 'FROM cts_transactions.%I WHERE trans_type IN (''B'', ''S'')',
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

        RAISE NOTICE 'Run 1: clearing all CTS destination tables before workers start: %', v_truncate_tables;
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
    IN p_range_size bigint DEFAULT 1000000,
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

-- Source rows with trans_type B (bulk) and S (stub) are both eligible. Each
-- invocation is a long-lived worker. Queue-row locking and non-overlapping
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
    v_upsert_set_list text;
    v_pk_select_list text;
    v_conflict_target text;
    v_order_by_src text;
    v_order_by_x text;
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
        -- competing claimant sees the preceding committed claim. Do not block
        -- indefinitely behind a slow WAL commit: report this worker as waiting,
        -- commit that state, and retry on the next loop.
        IF NOT pg_try_advisory_xact_lock(
            hashtextextended(format('cads.cts_parallel_claim.%s', p_run_id), 0)
        ) THEN
            UPDATE cads.cts_parallel_import_workers
            SET status = 'waiting', current_work_id = NULL,
                current_table_name = NULL, heartbeat_at = clock_timestamp()
            WHERE run_id = p_run_id AND worker_name = p_worker_name;
            COMMIT;
            PERFORM pg_sleep(0.25);
            CONTINUE;
        END IF;
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
              ) THEN 1 ELSE COALESCE(settings.max_workers, 30) END
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

        SELECT string_agg(
                   format(
                       '%I = CASE WHEN EXCLUDED.trans_type = ''S'' '
                       || 'AND target.trans_type = ''B'' '
                       || 'THEN target.%I ELSE EXCLUDED.%I END',
                       target.column_name, target.column_name, target.column_name
                   ),
                   ', ' ORDER BY target.ordinal_position
               )
        INTO v_upsert_set_list
        FROM information_schema.columns target
        WHERE target.table_schema = 'cts'
          AND target.table_name = v_work.table_name
          AND target.is_generated = 'NEVER'
          AND target.identity_generation IS DISTINCT FROM 'ALWAYS'
          AND NOT EXISTS (
              SELECT 1
              FROM pg_index primary_key
              CROSS JOIN LATERAL unnest(primary_key.indkey) AS key_column(attnum)
              JOIN pg_attribute target_pk
                ON target_pk.attrelid = primary_key.indrelid
               AND target_pk.attnum = key_column.attnum
              WHERE primary_key.indrelid = format('cts.%I', v_work.table_name)::regclass
                AND primary_key.indisprimary
                AND target_pk.attname = target.column_name
          );

        -- Use the CTS primary-key equivalent in cts_transactions as the row
        -- ordering key for both B and S rows. trans_id remains the
        -- queue/checkpoint identifier only.
        SELECT string_agg(format('src.%I', target_pk.attname), ', ' ORDER BY key_column.ordinality),
               string_agg(format('x.%I', target_pk.attname), ', ' ORDER BY key_column.ordinality),
               string_agg(format('src.%I', target_pk.attname), ', ' ORDER BY key_column.ordinality),
               string_agg(format('%I', target_pk.attname), ', ' ORDER BY key_column.ordinality)
        INTO v_order_by_src, v_order_by_x, v_pk_select_list, v_conflict_target
        FROM pg_index primary_key
        CROSS JOIN LATERAL unnest(primary_key.indkey)
            WITH ORDINALITY AS key_column(attnum, ordinality)
        JOIN pg_attribute target_pk
          ON target_pk.attrelid = primary_key.indrelid
         AND target_pk.attnum = key_column.attnum
        WHERE primary_key.indrelid = format('cts.%I', v_work.table_name)::regclass
          AND primary_key.indisprimary
          AND EXISTS (
              SELECT FROM pg_attribute source_pk
              WHERE source_pk.attrelid = format('cts_transactions.%I', v_work.table_name)::regclass
                AND source_pk.attname = target_pk.attname
                AND NOT source_pk.attisdropped
          );
        -- A small number of legacy tables have no declared CTS primary key.
        -- They can still be migrated safely in queue order.  Fall back to the
        -- source transaction identifier for deterministic ordering and use
        -- DO NOTHING when there is no safe conflict target for an upsert.
        IF v_order_by_src IS NULL THEN
            v_order_by_src := 'src.trans_id';
            v_order_by_x := 'x.trans_id';
            v_pk_select_list := 'NULL::bigint AS fallback_order_key';
            v_conflict_target := NULL;
        END IF;

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
            -- Queue-range ownership already excludes competing workers. Do not
            -- lock source rows here: legacy source tables can make FOR UPDATE
            -- block or fail before the destination insert is attempted.
            BEGIN
                EXECUTE format(
                    'SELECT array_agg(x.trans_id ORDER BY ' || v_order_by_x || ', x.trans_id) FROM ('
                    || 'SELECT src.trans_id, ' || v_pk_select_list || ' FROM cts_transactions.%I src '
                    || 'WHERE src.trans_type IN (''B'', ''S'') AND src.trans_id BETWEEN $1 AND $2 '
                    || 'AND NOT EXISTS (SELECT FROM cads.cts_parallel_import_deferred_rows d '
                    || 'WHERE d.run_id = $3 AND d.table_name = $4 AND d.transaction_row_id = src.trans_id) '
                    || 'AND NOT EXISTS (SELECT FROM cads.cts_bulk_import_migrated_rows m '
                    || 'WHERE m.table_name = $4 AND m.transaction_row_id = src.trans_id) '
                    || 'AND (%s) '
                    || 'ORDER BY ' || v_order_by_src || ', src.trans_id LIMIT $5) x',
                    v_work.table_name,
                    v_self_reference_predicate
                ) INTO v_ids USING v_work.minimum_trans_id, v_work.maximum_trans_id,
                                   p_run_id, v_work.table_name, v_effective_size;

                IF v_ids IS NOT NULL THEN
                    EXECUTE format(
                        'INSERT INTO cts.%I AS target (%s) SELECT %s FROM cts_transactions.%I src '
                        || 'WHERE src.trans_id = ANY($1) AND src.trans_type IN (''B'', ''S'') '
                        || CASE WHEN v_upsert_set_list IS NULL
                                      OR v_conflict_target IS NULL
                                THEN 'ON CONFLICT DO NOTHING'
                                ELSE 'ON CONFLICT (' || v_conflict_target
                                     || ') DO UPDATE SET ' || v_upsert_set_list
                           END,
                        v_work.table_name, v_column_list, v_select_list, v_work.table_name
                    ) USING v_ids;
                    GET DIAGNOSTICS v_affected = ROW_COUNT;
                    IF v_affected <> cardinality(v_ids) THEN RAISE EXCEPTION 'Selected %, inserted %', cardinality(v_ids), v_affected; END IF;

                    IF v_run.delete_source THEN
                        EXECUTE format('DELETE FROM cts_transactions.%I WHERE trans_id = ANY($1) AND trans_type IN (''B'', ''S'')', v_work.table_name) USING v_ids;
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
                        || 'WHERE src.trans_type IN (''B'', ''S'') AND src.trans_id BETWEEN $1 AND $2 '
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
                    'SELECT 1 FROM cts_transactions.%I WHERE trans_id = $1 AND trans_type IN (''B'', ''S'') FOR UPDATE',
                    v_row.table_name
                ) USING v_row.transaction_row_id;
                GET DIAGNOSTICS v_affected = ROW_COUNT;
                IF v_affected <> 1 THEN
                    RAISE EXCEPTION 'Expected one bulk source row for %.trans_id %, found %',
                        v_row.table_name, v_row.transaction_row_id, v_affected USING ERRCODE = 'P0002';
                END IF;
                EXECUTE format(
                    'INSERT INTO cts.%I (%s) SELECT %s FROM cts_transactions.%I src '
                    || 'WHERE src.trans_id = $1 AND src.trans_type IN (''B'', ''S'')',
                    v_row.table_name, v_column_list, v_select_list, v_row.table_name
                ) USING v_row.transaction_row_id;
                GET DIAGNOSTICS v_affected = ROW_COUNT;
                IF v_affected <> 1 THEN
                    RAISE EXCEPTION 'Expected one inserted row for %.trans_id %, inserted %',
                        v_row.table_name, v_row.transaction_row_id, v_affected;
                END IF;

                IF v_run.delete_source THEN
                    EXECUTE format('DELETE FROM cts_transactions.%I WHERE trans_id = $1 AND trans_type IN (''B'', ''S'')', v_row.table_name)
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

-- Cancel a run cleanly.  pg_cron jobs are removed through cron.unschedule
-- (rather than updating cron.job, which is not writable by the deployment
-- role), live worker sessions belonging to the run are terminated, and any
-- claimed ranges are returned to ready so a future run can be prepared.
CREATE OR REPLACE PROCEDURE cads.cancel_cts_parallel_bulk_import(
    IN p_run_id bigint
)
LANGUAGE plpgsql
AS $procedure$
DECLARE
    v_job record;
    v_worker record;
    v_jobs_removed bigint := 0;
    v_workers_terminated bigint := 0;
    v_ranges_requeued bigint := 0;
BEGIN
    IF p_run_id IS NULL THEN
        RAISE EXCEPTION 'p_run_id must not be null';
    END IF;

    IF NOT EXISTS (
        SELECT 1
        FROM cads.cts_parallel_import_runs
        WHERE run_id = p_run_id
    ) THEN
        RAISE EXCEPTION 'CTS parallel import run % does not exist', p_run_id;
    END IF;

    -- Unschedule every cron worker for this run.  This prevents a new worker
    -- invocation racing with the cancellation while existing sessions stop.
    FOR v_job IN
        SELECT jobid
        FROM cron.job
        WHERE jobname LIKE format('cts-import-run-%s-worker-%%', p_run_id)
    LOOP
        PERFORM cron.unschedule(v_job.jobid);
        v_jobs_removed := v_jobs_removed + 1;
    END LOOP;

    -- Only terminate backends recorded against this run; unrelated database
    -- sessions are never targeted.
    FOR v_worker IN
        SELECT DISTINCT w.backend_pid
        FROM cads.cts_parallel_import_workers w
        JOIN pg_catalog.pg_stat_activity a
          ON a.pid = w.backend_pid
        WHERE w.run_id = p_run_id
          AND w.backend_pid IS NOT NULL
          AND w.backend_pid <> pg_backend_pid()
    LOOP
        IF pg_terminate_backend(v_worker.backend_pid) THEN
            v_workers_terminated := v_workers_terminated + 1;
        END IF;
    END LOOP;

    -- Make unfinished work restartable.  Completed ranges and migration
    -- counters are preserved.
    UPDATE cads.cts_parallel_import_work_queue
    SET state = 'ready',
        claimed_by = NULL,
        claimed_backend_pid = NULL,
        claimed_at = NULL,
        heartbeat_at = NULL,
        rows_deferred = 0,
        last_error_message = NULL
    WHERE run_id = p_run_id
      AND state = 'processing';
    GET DIAGNOSTICS v_ranges_requeued = ROW_COUNT;

    UPDATE cads.cts_parallel_import_workers
    SET status = 'failed',
        backend_pid = NULL,
        current_work_id = NULL,
        current_table_name = NULL,
        completed_at = clock_timestamp(),
        heartbeat_at = clock_timestamp(),
        last_error_message = 'Run cancelled by cads.cancel_cts_parallel_bulk_import'
    WHERE run_id = p_run_id
      AND status IN ('starting', 'working', 'waiting');

    UPDATE cads.cts_parallel_import_status
    SET status = 'failed',
        updated_at = clock_timestamp(),
        last_error_message = 'Run cancelled by cads.cancel_cts_parallel_bulk_import'
    WHERE run_id = p_run_id
      AND status IN ('processing bulk', 'processing errors');

    UPDATE cads.cts_parallel_import_runs
    SET status = 'failed',
        completed_at = clock_timestamp(),
        last_error_message = 'Run cancelled by cads.cancel_cts_parallel_bulk_import'
    WHERE run_id = p_run_id
      AND status IN ('planning', 'ready', 'processing bulk', 'processing errors');

    RAISE NOTICE 'Cancelled run %: unscheduled %, terminated %, requeued % range(s).',
        p_run_id, v_jobs_removed, v_workers_terminated, v_ranges_requeued;
END;
$procedure$;

COMMENT ON PROCEDURE cads.cancel_cts_parallel_bulk_import(bigint)
IS 'Unschedules run workers, terminates live worker sessions, requeues unfinished ranges, and marks the run failed.';

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

-- Deferred-row error breakdown, grouped by table and effective error message.
DROP FUNCTION IF EXISTS cads.get_cts_parallel_import_deferred_errors(bigint);

CREATE OR REPLACE FUNCTION cads.get_cts_parallel_import_deferred_errors(p_run_id bigint)
RETURNS TABLE
(
    table_name text,
    error text,
    rows bigint
)
LANGUAGE sql
STABLE
AS $function$
    SELECT
        deferred.table_name,
        COALESCE(deferred.error_message, deferred.bulk_error_message)::text AS error,
        COUNT(*)::bigint AS rows
    FROM cads.cts_parallel_import_deferred_rows deferred
    WHERE deferred.run_id = p_run_id
    GROUP BY deferred.table_name,
             COALESCE(deferred.error_message, deferred.bulk_error_message)
    ORDER BY rows DESC;
$function$;

COMMENT ON FUNCTION cads.get_cts_parallel_import_deferred_errors(bigint)
IS 'Counts deferred rows by table and effective error message for a parallel import run.';

-- Detailed immutable dependency-plan monitoring with elapsed time, remaining
-- percentage, throughput and live/stale worker counts.
DROP FUNCTION IF EXISTS cads.get_cts_parallel_import_plan(bigint);

CREATE OR REPLACE FUNCTION cads.get_cts_parallel_import_plan(p_run_id bigint)
RETURNS TABLE
(
    plan_order bigint,
    dependency_depth integer,
    table_name text,
    status text,
    remaining_percent numeric,
    elapsed_hours numeric,
    elapsed_minutes numeric,
    original_rows text,
    migrated_rows text,
    deferred_rows text,
    remaining_bulk_rows text,
    migrated_rows_per_minute text,
    migrated_rows_per_second text,
    active_workers bigint,
    stale_workers bigint,
    updated_at timestamp with time zone,
    last_error_message text
)
LANGUAGE sql
STABLE
AS $function$
    WITH plan AS (
        SELECT
            row_number() OVER (
                ORDER BY status.dependency_depth, status.table_name
            )::bigint AS plan_order,
            status.*,
            CASE
                WHEN status.started_at IS NULL THEN NULL::numeric
                ELSE GREATEST(
                    EXTRACT(EPOCH FROM (
                        COALESCE(
                            status.bulk_completed_at,
                            status.completed_at,
                            statement_timestamp()
                        ) - status.started_at
                    )),
                    0
                )
            END AS elapsed_seconds
        FROM cads.cts_parallel_import_status status
        WHERE status.run_id = p_run_id
    ),
    worker_state AS (
        SELECT
            worker.current_table_name AS table_name,
            COUNT(DISTINCT worker.backend_pid) FILTER (
                WHERE activity.pid IS NOT NULL
            )::bigint AS active_workers,
            COUNT(*) FILTER (
                WHERE activity.pid IS NULL
            )::bigint AS stale_workers
        FROM cads.cts_parallel_import_workers worker
        LEFT JOIN pg_catalog.pg_stat_activity activity
          ON activity.pid = worker.backend_pid
         AND activity.datname = current_database()
        WHERE worker.run_id = p_run_id
          AND worker.status IN ('starting', 'working', 'waiting')
          AND worker.current_table_name IS NOT NULL
        GROUP BY worker.current_table_name
    )
    SELECT
        plan.plan_order,
        plan.dependency_depth,
        plan.table_name,
        plan.status,
        ROUND(
            100.0 * GREATEST(
                plan.original_total
                - plan.number_migrated
                - plan.number_deferred,
                0
            ) / NULLIF(plan.original_total, 0),
            2
        ),
        ROUND(plan.elapsed_seconds / 3600.0, 2),
        ROUND(plan.elapsed_seconds / 60.0, 2),
        TO_CHAR(plan.original_total, 'FM999,999,999,999,999,999,999'),
        TO_CHAR(plan.number_migrated, 'FM999,999,999,999,999,999,999'),
        TO_CHAR(plan.number_deferred, 'FM999,999,999,999,999,999,999'),
        TO_CHAR(
            GREATEST(
                plan.original_total
                - plan.number_migrated
                - plan.number_deferred,
                0
            ),
            'FM999,999,999,999,999,999,999'
        ),
        TO_CHAR(
            plan.number_migrated * 60.0
            / NULLIF(plan.elapsed_seconds, 0),
            'FM999,999,999,999,999,999,999.00'
        ),
        TO_CHAR(
            plan.number_migrated
            / NULLIF(plan.elapsed_seconds, 0),
            'FM999,999,999,999,999,999,999.00'
        ),
        COALESCE(worker_state.active_workers, 0),
        COALESCE(worker_state.stale_workers, 0),
        plan.updated_at,
        plan.last_error_message
    FROM plan
    LEFT JOIN worker_state
      ON worker_state.table_name = plan.table_name
    ORDER BY plan.plan_order;
$function$;

COMMENT ON FUNCTION cads.get_cts_parallel_import_plan(bigint)
IS 'Returns dependency-plan order, progress percentages, elapsed time, throughput and active/stale worker counts.';

-- Status-level totals for a compact run summary.
DROP FUNCTION IF EXISTS cads.get_cts_parallel_import_summary(bigint);

CREATE OR REPLACE FUNCTION cads.get_cts_parallel_import_summary(p_run_id bigint)
RETURNS TABLE
(
    status text,
    table_count bigint,
    percent_remaining numeric,
    total_source_rows text,
    total_migrated_rows text,
    total_deferred_rows text,
    total_remaining_rows text
)
LANGUAGE sql
STABLE
AS $function$
    SELECT
        status.status,
        COUNT(*)::bigint,
        ROUND(
            100.0 * SUM(
                GREATEST(
                    status.original_total
                    - status.number_migrated
                    - status.number_deferred,
                    0
                )
            ) / NULLIF(SUM(status.original_total), 0),
            2
        ),
        TO_CHAR(
            SUM(status.original_total),
            'FM999,999,999,999,999,999,999'
        ),
        TO_CHAR(
            SUM(status.number_migrated),
            'FM999,999,999,999,999,999,999'
        ),
        TO_CHAR(
            SUM(status.number_deferred),
            'FM999,999,999,999,999,999,999'
        ),
        TO_CHAR(
            SUM(
                GREATEST(
                    status.original_total
                    - status.number_migrated
                    - status.number_deferred,
                    0
                )
            ),
            'FM999,999,999,999,999,999,999'
        )
    FROM cads.cts_parallel_import_status status
    WHERE status.run_id = p_run_id
    GROUP BY status.status
    ORDER BY CASE status.status
        WHEN 'bulk complete' THEN 1
        WHEN 'bulk complete with deferred rows' THEN 2
        WHEN 'processing bulk' THEN 3
        WHEN 'ready' THEN 4
        WHEN 'blocked by parents' THEN 5
        ELSE 6
    END;
$function$;

COMMENT ON FUNCTION cads.get_cts_parallel_import_summary(bigint)
IS 'Returns table counts and source, migrated, deferred and remaining totals grouped by status.';

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

-- Persistent worker timings and throughput. The baseline capture and trigger
-- installation are idempotent so this changeset can also be run directly with
-- psql in autocommit mode; do not use a standalone LOCK TABLE here because
-- PostgreSQL only permits it inside an explicit transaction.

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

-- Migration stubs generated directly from cts-deferred-parent-details-run-1.csv.
-- Only unique parents marked absent from both source and destination are inserted.
-- These S rows are created before the next import plan so dependency ordering can
-- place the parent stub before its dependent B rows.
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (28223153, -28223153, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (28223192, -28223192, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (28223221, -28223221, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (28223250, -28223250, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (28221583, -28221583, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (28803305, -28803305, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (28803341, -28803341, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (10714125, -10714125, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (28803388, -28803388, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (28803412, -28803412, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (28803517, -28803517, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (28803970, -28803970, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (28804002, -28804002, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (10713849, -10713849, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (10712739, -10712739, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (20690128, -20690128, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (20691850, -20691850, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (20693623, -20693623, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (20693687, -20693687, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (20704554, -20704554, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (20704903, -20704903, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (20706771, -20706771, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (20707231, -20707231, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (20707353, -20707353, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (20707477, -20707477, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (20711158, -20711158, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (20711224, -20711224, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (20711442, -20711442, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (20714451, -20714451, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (20715622, -20715622, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (20717850, -20717850, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (20718715, -20718715, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (20722643, -20722643, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (20723090, -20723090, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (20724968, -20724968, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (20727582, -20727582, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (20727681, -20727681, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (20730620, -20730620, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (20694392, -20694392, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (20694485, -20694485, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (20695553, -20695553, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (20697215, -20697215, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (20701626, -20701626, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (20701657, -20701657, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (20703298, -20703298, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (20731581, -20731581, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (20733989, -20733989, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (20737290, -20737290, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (20726858, -20726858, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (20726195, -20726195, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (10719270, -10719270, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (10719255, -10719255, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (10715090, -10715090, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (30431998, -30431998, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (10701747, -10701747, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (30431591, -30431591, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (10719175, -10719175, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (40689045, -40689045, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (1, -1, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (10715380, -10715380, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (10714779, -10714779, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (26746121, -26746121, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (26746180, -26746180, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (26746084, -26746084, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (26752441, -26752441, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (26751725, -26751725, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (26751797, -26751797, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (26749296, -26749296, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (26746016, -26746016, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (26746223, -26746223, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (26751851, -26751851, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (26751898, -26751898, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (26751948, -26751948, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (26751985, -26751985, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (26752342, -26752342, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (26752579, -26752579, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (26752629, -26752629, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (26752380, -26752380, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (26752416, -26752416, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (26752753, -26752753, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (26752802, -26752802, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (40698270, -40698270, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (26752504, -26752504, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (26752549, -26752549, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (10716299, -10716299, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (13631743, -13631743, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (13631030, -13631030, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (10711342, -10711342, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (13632206, -13632206, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (13632342, -13632342, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (13632308, -13632308, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (13630632, -13630632, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (13629330, -13629330, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (13632444, -13632444, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (13632449, -13632449, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (27994325, -27994325, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (27994283, -27994283, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (27993552, -27993552, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (27994505, -27994505, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (27994542, -27994542, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (10711728, -10711728, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (10715479, -10715479, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (26393163, -26393163, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (10712757, -10712757, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (10703896, -10703896, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (74021065, -74021065, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (72422554, -72422554, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (72404316, -72404316, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (81805369, -81805369, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (78525445, -78525445, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (80574117, -80574117, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (78486139, -78486139, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188045431, -188045431, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (180975454, -180975454, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (182898751, -182898751, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188114001, -188114001, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (187341436, -187341436, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (178226220, -178226220, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188302207, -188302207, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (184799962, -184799962, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (181558379, -181558379, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188169068, -188169068, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188186507, -188186507, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188186497, -188186497, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188324219, -188324219, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188324230, -188324230, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (184200753, -184200753, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (157854046, -157854046, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (182136238, -182136238, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (182822782, -182822782, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (187281520, -187281520, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188273408, -188273408, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188026070, -188026070, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188061093, -188061093, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188217974, -188217974, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188145983, -188145983, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188237979, -188237979, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188324229, -188324229, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188107144, -188107144, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (107156872, -107156872, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188320415, -188320415, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188162841, -188162841, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188328622, -188328622, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188316147, -188316147, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188174053, -188174053, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188348933, -188348933, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188317812, -188317812, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188316600, -188316600, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188163217, -188163217, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188029125, -188029125, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188224611, -188224611, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188317296, -188317296, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188108467, -188108467, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188320628, -188320628, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188137421, -188137421, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188138888, -188138888, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188226903, -188226903, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188226927, -188226927, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188228244, -188228244, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188020017, -188020017, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188253414, -188253414, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188256078, -188256078, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188011787, -188011787, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188011806, -188011806, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188012295, -188012295, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188256177, -188256177, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188307027, -188307027, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188113799, -188113799, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188040216, -188040216, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188339399, -188339399, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188339427, -188339427, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188339458, -188339458, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188353600, -188353600, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188353603, -188353603, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188182011, -188182011, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188183041, -188183041, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188353608, -188353608, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188353609, -188353609, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188070922, -188070922, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188353612, -188353612, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188353615, -188353615, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188353616, -188353616, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188353617, -188353617, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188353931, -188353931, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188114062, -188114062, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (187711849, -187711849, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188139569, -188139569, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188284453, -188284453, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188146997, -188146997, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188068017, -188068017, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188298299, -188298299, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188304194, -188304194, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188133274, -188133274, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188072709, -188072709, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (187438178, -187438178, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188251263, -188251263, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188167561, -188167561, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188182807, -188182807, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188211763, -188211763, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188211775, -188211775, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188211872, -188211872, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188211906, -188211906, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188211921, -188211921, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188218021, -188218021, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188218033, -188218033, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188229252, -188229252, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188251591, -188251591, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188112755, -188112755, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188344026, -188344026, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188195855, -188195855, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188109328, -188109328, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188133996, -188133996, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188225216, -188225216, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188121975, -188121975, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188136710, -188136710, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188251386, -188251386, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188300913, -188300913, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188136955, -188136955, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188301125, -188301125, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188177263, -188177263, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188290176, -188290176, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188301202, -188301202, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188301206, -188301206, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188302506, -188302506, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188129518, -188129518, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188129575, -188129575, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188129599, -188129599, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188201702, -188201702, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188201711, -188201711, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341371, -188341371, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341381, -188341381, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341391, -188341391, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341395, -188341395, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341403, -188341403, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188210632, -188210632, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188210659, -188210659, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188210691, -188210691, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341412, -188341412, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341415, -188341415, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188211410, -188211410, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341425, -188341425, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341430, -188341430, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341439, -188341439, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341550, -188341550, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341446, -188341446, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341450, -188341450, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341463, -188341463, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341468, -188341468, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341473, -188341473, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341482, -188341482, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341487, -188341487, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341494, -188341494, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341501, -188341501, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341512, -188341512, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341518, -188341518, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341525, -188341525, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341534, -188341534, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341541, -188341541, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341557, -188341557, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341565, -188341565, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341572, -188341572, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341581, -188341581, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341588, -188341588, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341592, -188341592, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341601, -188341601, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341605, -188341605, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341613, -188341613, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341617, -188341617, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341623, -188341623, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341629, -188341629, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341631, -188341631, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341638, -188341638, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341644, -188341644, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341649, -188341649, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341650, -188341650, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341656, -188341656, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341657, -188341657, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341662, -188341662, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341666, -188341666, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341669, -188341669, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341676, -188341676, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341681, -188341681, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188089486, -188089486, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341686, -188341686, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341695, -188341695, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341702, -188341702, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341710, -188341710, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341718, -188341718, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341727, -188341727, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341731, -188341731, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341929, -188341929, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341739, -188341739, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341743, -188341743, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341747, -188341747, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341750, -188341750, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341758, -188341758, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341766, -188341766, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341769, -188341769, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341776, -188341776, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341784, -188341784, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341792, -188341792, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341796, -188341796, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341806, -188341806, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341814, -188341814, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341823, -188341823, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341833, -188341833, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341842, -188341842, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341845, -188341845, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341851, -188341851, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341856, -188341856, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341862, -188341862, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341869, -188341869, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341877, -188341877, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341880, -188341880, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341887, -188341887, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341892, -188341892, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341899, -188341899, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341906, -188341906, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341914, -188341914, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341921, -188341921, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341934, -188341934, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341935, -188341935, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341944, -188341944, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341950, -188341950, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341960, -188341960, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341966, -188341966, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188341995, -188341995, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188342000, -188342000, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188342007, -188342007, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188342015, -188342015, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188342023, -188342023, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188342030, -188342030, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188342037, -188342037, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188352645, -188352645, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188352656, -188352656, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188352664, -188352664, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188352671, -188352671, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188352702, -188352702, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188352711, -188352711, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188352724, -188352724, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188352729, -188352729, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188352740, -188352740, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188352761, -188352761, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188352770, -188352770, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188352778, -188352778, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188352787, -188352787, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188056345, -188056345, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188147256, -188147256, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188056348, -188056348, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188347746, -188347746, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188057655, -188057655, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188147397, -188147397, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188350957, -188350957, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188351405, -188351405, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188244449, -188244449, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188260816, -188260816, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (187963331, -187963331, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (187963332, -187963332, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (187963333, -187963333, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188058396, -188058396, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188251019, -188251019, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188166316, -188166316, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188165087, -188165087, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188166321, -188166321, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188166483, -188166483, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188229376, -188229376, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188183712, -188183712, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188212109, -188212109, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188090453, -188090453, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188098199, -188098199, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188098215, -188098215, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188098223, -188098223, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188343281, -188343281, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188238633, -188238633, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188195968, -188195968, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188195986, -188195986, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188238655, -188238655, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188343774, -188343774, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188196022, -188196022, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188053492, -188053492, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188053507, -188053507, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188272819, -188272819, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188273184, -188273184, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188273215, -188273215, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188273227, -188273227, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188273251, -188273251, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188273270, -188273270, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188273287, -188273287, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188273295, -188273295, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188273398, -188273398, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188273436, -188273436, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188273687, -188273687, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188273708, -188273708, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188274042, -188274042, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188290256, -188290256, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188142018, -188142018, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188144967, -188144967, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188160224, -188160224, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188145010, -188145010, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188285938, -188285938, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188287993, -188287993, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188287668, -188287668, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188069009, -188069009, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188069018, -188069018, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188287711, -188287711, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188287804, -188287804, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188166788, -188166788, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188287816, -188287816, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188287827, -188287827, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188287892, -188287892, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188145022, -188145022, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188126887, -188126887, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188287935, -188287935, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188181916, -188181916, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188181917, -188181917, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188181929, -188181929, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188181930, -188181930, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188190787, -188190787, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188287972, -188287972, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188287982, -188287982, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188321147, -188321147, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188288029, -188288029, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188288030, -188288030, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188288034, -188288034, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188347095, -188347095, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188291357, -188291357, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188321301, -188321301, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188291384, -188291384, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188291457, -188291457, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188347108, -188347108, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188347119, -188347119, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188347127, -188347127, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188349231, -188349231, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188251458, -188251458, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188197142, -188197142, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188285205, -188285205, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188308836, -188308836, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188348365, -188348365, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188183385, -188183385, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188351753, -188351753, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188270291, -188270291, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188057609, -188057609, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188308971, -188308971, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188191168, -188191168, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188124387, -188124387, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188098678, -188098678, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188197133, -188197133, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188202857, -188202857, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188309018, -188309018, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188322761, -188322761, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188302426, -188302426, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188328978, -188328978, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188222240, -188222240, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188330992, -188330992, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188322833, -188322833, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188040170, -188040170, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188322895, -188322895, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188060117, -188060117, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188322919, -188322919, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188333556, -188333556, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188333889, -188333889, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188151933, -188151933, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188203297, -188203297, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188164380, -188164380, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188333987, -188333987, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188217703, -188217703, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188343062, -188343062, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188060180, -188060180, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188270470, -188270470, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188270685, -188270685, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188065948, -188065948, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188131441, -188131441, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188308900, -188308900, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188286752, -188286752, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188218823, -188218823, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188291949, -188291949, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188220102, -188220102, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188123089, -188123089, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188325951, -188325951, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188333704, -188333704, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188345453, -188345453, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188225271, -188225271, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188098310, -188098310, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188123104, -188123104, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188352957, -188352957, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188123880, -188123880, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188299372, -188299372, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188352971, -188352971, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188227612, -188227612, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188352988, -188352988, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188352998, -188352998, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188353007, -188353007, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188353022, -188353022, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188353034, -188353034, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188353042, -188353042, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188315838, -188315838, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188353055, -188353055, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188353068, -188353068, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188186180, -188186180, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188353089, -188353089, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188214337, -188214337, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188216189, -188216189, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188353106, -188353106, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188353116, -188353116, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188353137, -188353137, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188353153, -188353153, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188353163, -188353163, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188353179, -188353179, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188353194, -188353194, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188186419, -188186419, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188353206, -188353206, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188353223, -188353223, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188186520, -188186520, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188353236, -188353236, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188353253, -188353253, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188353261, -188353261, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188353272, -188353272, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188353286, -188353286, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188353291, -188353291, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188353298, -188353298, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188353307, -188353307, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188353313, -188353313, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188353319, -188353319, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188253329, -188253329, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188353324, -188353324, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188353327, -188353327, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188353330, -188353330, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188353333, -188353333, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188353337, -188353337, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188353338, -188353338, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188353341, -188353341, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188353344, -188353344, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188353346, -188353346, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188353350, -188353350, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188353354, -188353354, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188353356, -188353356, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188353361, -188353361, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188353365, -188353365, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188017139, -188017139, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188318075, -188318075, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188321091, -188321091, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188322797, -188322797, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188301387, -188301387, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188014232, -188014232, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188149628, -188149628, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188284540, -188284540, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188301398, -188301398, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188243196, -188243196, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188237757, -188237757, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188226908, -188226908, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188166195, -188166195, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188237790, -188237790, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188237796, -188237796, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188254015, -188254015, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188238209, -188238209, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188333296, -188333296, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188284563, -188284563, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188284603, -188284603, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188324242, -188324242, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188333316, -188333316, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188335742, -188335742, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188335753, -188335753, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188225948, -188225948, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188043553, -188043553, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188255318, -188255318, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188229325, -188229325, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188114087, -188114087, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188070720, -188070720, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188334803, -188334803, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188307182, -188307182, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188078673, -188078673, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188307391, -188307391, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188308721, -188308721, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (188079524, -188079524, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_registered_animals (ran_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (75076180, -75076180, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_valid_applications (vap_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (96066474, -96066474, 'S') ON CONFLICT (trans_id) DO NOTHING;
INSERT INTO cts_transactions.ct_location_party_rels (lpr_id, trans_id, trans_type) OVERRIDING SYSTEM VALUE VALUES (1135364, -1135364, 'S') ON CONFLICT (trans_id) DO NOTHING;
