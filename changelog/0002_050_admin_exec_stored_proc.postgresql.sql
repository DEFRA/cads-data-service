-- liquibase formatted sql

-- changeset andy-defra:1790000000000-1 splitStatements:false
CREATE OR REPLACE FUNCTION cads.db_admin_exec_command(command_name text, args jsonb DEFAULT '{}'::jsonb)
    RETURNS jsonb
    LANGUAGE plpgsql
AS $$
DECLARE
    result jsonb;
    has_pg_stat_statements boolean;
BEGIN
    CASE command_name

        -- 1. Current sessions by state
        WHEN 'sessions_by_state' THEN
            RETURN (
                SELECT jsonb_agg(row_to_json(t))
                FROM (
                         SELECT
                             state,
                             COUNT(*) AS sessions
                         FROM pg_stat_activity
                         GROUP BY state
                         ORDER BY sessions DESC
                     ) t
            );

        -- 2. Active / long-running queries
        WHEN 'active_queries' THEN
            RETURN (
                SELECT jsonb_agg(row_to_json(t))
                FROM (
                         SELECT
                             pid,
                             usename,
                             datname,
                             application_name,
                             client_addr,
                             state,
                             wait_event_type,
                             wait_event,
                             now() - query_start AS duration,
                             query
                         FROM pg_stat_activity
                         WHERE state <> 'idle'
                         ORDER BY duration DESC
                     ) t
            );

        -- 3. Cancel a running query
        WHEN 'cancel_query' THEN
            RETURN to_jsonb(pg_cancel_backend((args->>'pid')::int));

        -- 4. Terminate a backend connection
        WHEN 'terminate_query' THEN
            RETURN to_jsonb(pg_terminate_backend((args->>'pid')::int));

        -- 5. Transactions: commits and rollbacks by database
        WHEN 'transactions' THEN
            RETURN (
                SELECT jsonb_agg(row_to_json(t))
                FROM (
                         SELECT
                             datname,
                             xact_commit,
                             xact_rollback,
                             xact_commit + xact_rollback AS total_transactions
                         FROM pg_stat_database
                         ORDER BY total_transactions DESC
                     ) t
            );

        -- 6. Rows read / returned / modified
        WHEN 'rows_activity' THEN
            RETURN (
                SELECT jsonb_agg(row_to_json(t))
                FROM (
                         SELECT
                             datname,
                             tup_returned,
                             tup_fetched,
                             tup_inserted,
                             tup_updated,
                             tup_deleted
                         FROM pg_stat_database
                         ORDER BY tup_returned DESC
                     ) t
            );

        -- 7. Buffer cache hit ratio
        WHEN 'buffer_cache_hit_ratio' THEN
            RETURN (
                SELECT jsonb_agg(row_to_json(t))
                FROM (
                         SELECT datname,
                                blks_hit,
                                blks_read,
                                ROUND(
                                    100.0 * blks_hit / NULLIF(blks_hit + blks_read, 0),
                                    2
                                ) AS cache_hit_ratio_percent
                         FROM pg_stat_database
                         ORDER BY cache_hit_ratio_percent ASC
                     ) t
            );

        -- 8. Locks and blocking
        WHEN 'locks_and_blocking' THEN
            RETURN (
                SELECT jsonb_agg(row_to_json(t))
                FROM (
                         SELECT
                             blocked.pid AS blocked_pid,
                             blocked.query AS blocked_query,
                             blocking.pid AS blocking_pid,
                             blocking.query AS blocking_query,
                             now() - blocked.query_start AS blocked_duration
                         FROM pg_stat_activity blocked
                                  JOIN pg_locks blocked_locks
                                       ON blocked_locks.pid = blocked.pid
                                  JOIN pg_locks blocking_locks
                                       ON blocking_locks.locktype = blocked_locks.locktype
                                           AND blocking_locks.database IS NOT DISTINCT FROM blocked_locks.database
                                           AND blocking_locks.relation IS NOT DISTINCT FROM blocked_locks.relation
                                           AND blocking_locks.page IS NOT DISTINCT FROM blocked_locks.page
                                           AND blocking_locks.tuple IS NOT DISTINCT FROM blocked_locks.tuple
                                           AND blocking_locks.virtualxid IS NOT DISTINCT FROM blocked_locks.virtualxid
                                           AND blocking_locks.transactionid IS NOT DISTINCT FROM blocked_locks.transactionid
                                           AND blocking_locks.classid IS NOT DISTINCT FROM blocked_locks.classid
                                           AND blocking_locks.objid IS NOT DISTINCT FROM blocked_locks.objid
                                           AND blocking_locks.objsubid IS NOT DISTINCT FROM blocked_locks.objsubid
                                           AND blocking_locks.pid <> blocked_locks.pid
                                  JOIN pg_stat_activity blocking
                                       ON blocking.pid = blocking_locks.pid
                         WHERE NOT blocked_locks.granted
                           AND blocking_locks.granted
                         ORDER BY blocked_duration DESC
                     ) t
            );

        -- 9. Idle in transaction sessions
        WHEN 'idle_in_transaction' THEN
            RETURN (
                SELECT jsonb_agg(row_to_json(t))
                FROM (
                         SELECT
                             pid,
                             usename,
                             datname,
                             client_addr,
                             now() - xact_start AS transaction_age,
                             now() - state_change AS idle_duration,
                             query
                         FROM pg_stat_activity
                         WHERE state = 'idle in transaction'
                         ORDER BY transaction_age DESC
                     ) t
            );

        -- 10. Prepared transactions
        WHEN 'prepared_transactions' THEN
            RETURN (
                SELECT jsonb_agg(row_to_json(t))
                FROM (
                         SELECT gid,
                                database,
                                owner,
                                prepared,
                                now() - prepared AS age
                         FROM pg_prepared_xacts
                         ORDER BY prepared ASC
                     ) t
            );

        -- 11. Table activity
        WHEN 'table_activity' THEN
            RETURN (
                SELECT jsonb_agg(row_to_json(t))
                FROM (
                         SELECT
                             schemaname,
                             relname AS table_name,
                             seq_scan,
                             seq_tup_read,
                             idx_scan,
                             idx_tup_fetch,
                             n_tup_ins,
                             n_tup_upd,
                             n_tup_del,
                             n_live_tup,
                             n_dead_tup,
                             last_vacuum,
                             last_autovacuum,
                             last_analyze,
                             last_autoanalyze
                         FROM pg_stat_user_tables
                         ORDER BY n_live_tup DESC
                     ) t
            );

        -- 12. Index usage
        WHEN 'index_usage' THEN
            RETURN (
                SELECT jsonb_agg(row_to_json(t))
                FROM (
                         SELECT
                             schemaname,
                             relname AS table_name,
                             indexrelname AS index_name,
                             idx_scan,
                             idx_tup_read,
                             idx_tup_fetch
                         FROM pg_stat_user_indexes
                         ORDER BY idx_scan ASC
                     ) t
            );

        -- 13. Largest tables
        WHEN 'largest_tables' THEN
            RETURN (
                SELECT jsonb_agg(row_to_json(t))
                FROM (
                         SELECT
                             schemaname,
                             relname AS table_name,
                             pg_size_pretty(pg_total_relation_size(relid)) AS total_size,
                             pg_size_pretty(pg_relation_size(relid)) AS table_size,
                             pg_size_pretty(pg_total_relation_size(relid) - pg_relation_size(relid)) AS index_size,
                             n_live_tup,
                             n_dead_tup
                         FROM pg_stat_user_tables
                         ORDER BY pg_total_relation_size(relid) DESC
                         LIMIT 25
                     ) t
            );

        -- 14. Slow / expensive queries (requires pg_stat_statements)
        WHEN 'slow_queries' THEN
            SELECT EXISTS (
                SELECT 1 FROM pg_extension WHERE extname = 'pg_stat_statements'
            ) INTO has_pg_stat_statements;

            IF NOT has_pg_stat_statements THEN
                RAISE EXCEPTION 'pg_stat_statements extension is not installed';
            END IF;

            RETURN (
                SELECT jsonb_agg(row_to_json(t))
                FROM (
                         SELECT
                             queryid,
                             calls,
                             ROUND(total_exec_time::numeric, 2) AS total_exec_time_ms,
                             ROUND(mean_exec_time::numeric, 2) AS mean_exec_time_ms,
                             ROUND(max_exec_time::numeric, 2) AS max_exec_time_ms,
                             rows,
                             query
                         FROM pg_stat_statements
                         ORDER BY total_exec_time DESC
                         LIMIT 20
                     ) t
            );

        -- 15. Queries with most shared block reads (requires pg_stat_statements)
        WHEN 'shared_block_reads' THEN
            SELECT EXISTS (
                SELECT 1 FROM pg_extension WHERE extname = 'pg_stat_statements'
            ) INTO has_pg_stat_statements;

            IF NOT has_pg_stat_statements THEN
                RAISE EXCEPTION 'pg_stat_statements extension is not installed';
            END IF;

            RETURN (
                SELECT jsonb_agg(row_to_json(t))
                FROM (
                         SELECT
                             queryid,
                             calls,
                             shared_blks_read,
                             shared_blks_hit,
                             ROUND(
                                 100.0 * shared_blks_hit / NULLIF(shared_blks_hit + shared_blks_read, 0),
                                 2
                             ) AS hit_ratio_percent,
                             query
                         FROM pg_stat_statements
                         ORDER BY shared_blks_read DESC
                         LIMIT 20
                     ) t
            );

        ELSE
            RAISE EXCEPTION 'Unknown admin command: %', command_name;
        END CASE;
END;
$$;