-- liquibase formatted sql

-- changeset andy-defra:0002_051_01 splitStatements:false
CREATE OR REPLACE FUNCTION cads.cts_parallel_import_admin_exec_command(command_name text, args jsonb DEFAULT '{}'::jsonb)
    RETURNS jsonb
    LANGUAGE plpgsql
AS $$
DECLARE
    v_run_id bigint;
BEGIN
    v_run_id := (args->>'run_id')::bigint;

    IF command_name <> 'runs' AND v_run_id IS NULL THEN
        RAISE EXCEPTION 'run_id is required in args';
    END IF;

    CASE command_name

        -- 0. All import runs, newest first
        WHEN 'runs' THEN
            RETURN (
                SELECT COALESCE(jsonb_agg(row_to_json(t)), '[]'::jsonb)
                FROM (
                         SELECT run_id, status, created_at, bulk_completed_at, completed_at
                         FROM cads.cts_parallel_import_runs
                         ORDER BY run_id DESC
                     ) t
            );

        -- 1. Status-level totals summary
        WHEN 'summary' THEN
            RETURN (
                SELECT COALESCE(jsonb_agg(row_to_json(t)), '[]'::jsonb)
                FROM cads.get_cts_parallel_import_summary(v_run_id) t
            );

        -- 2. Per-table dependency plan progress
        WHEN 'plan' THEN
            RETURN (
                SELECT COALESCE(jsonb_agg(row_to_json(t)), '[]'::jsonb)
                FROM cads.get_cts_parallel_import_plan(v_run_id) t
            );

        -- 3. Per-table status
        WHEN 'status' THEN
            RETURN (
                SELECT COALESCE(jsonb_agg(row_to_json(t)), '[]'::jsonb)
                FROM cads.get_cts_parallel_import_status(v_run_id) t
            );

        -- 4. Progress
        WHEN 'progress' THEN
            RETURN (
                SELECT COALESCE(jsonb_agg(row_to_json(t)), '[]'::jsonb)
                FROM cads.get_cts_parallel_import_progress(v_run_id) t
            );

        -- 5. Workers
        WHEN 'workers' THEN
            RETURN (
                SELECT COALESCE(jsonb_agg(row_to_json(t)), '[]'::jsonb)
                FROM cads.get_cts_parallel_import_workers(v_run_id) t
            );

        -- 6. Work tasks / ranges
        WHEN 'tasks' THEN
            RETURN (
                SELECT COALESCE(jsonb_agg(row_to_json(t)), '[]'::jsonb)
                FROM cads.get_cts_parallel_import_tasks(v_run_id) t
            );

        -- 7. Deferred-row error breakdown
        WHEN 'deferred_errors' THEN
            RETURN (
                SELECT COALESCE(jsonb_agg(row_to_json(t)), '[]'::jsonb)
                FROM cads.get_cts_parallel_import_deferred_errors(v_run_id) t
            );

        ELSE
            RAISE EXCEPTION 'Unknown admin command: %', command_name;
    END CASE;
END;
$$;