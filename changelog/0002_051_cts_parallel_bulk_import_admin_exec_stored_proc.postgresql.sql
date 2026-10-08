-- liquibase formatted sql

-- changeset andy-defra:1790000000100-1 splitStatements:false
CREATE OR REPLACE FUNCTION cads.cts_parallel_import_admin_exec_command(command_name text, args jsonb DEFAULT '{}'::jsonb)
    RETURNS jsonb
    LANGUAGE plpgsql
AS $$
DECLARE
    result jsonb;
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
                SELECT jsonb_agg(row_to_json(t))
                FROM (
                         SELECT
                             run_id,
                             status,
                             created_at,
                             bulk_completed_at,
                             completed_at
                         FROM cads.cts_parallel_import_runs
                         ORDER BY run_id DESC
                     ) t
            );

        -- 1. Deferred-row error breakdown for a run
        WHEN 'deferred_errors' THEN
            RETURN (
                SELECT jsonb_agg(row_to_json(t))
                FROM cads.get_cts_parallel_import_deferred_errors(v_run_id) t
            );

        -- 2. Dependency-plan progress for a run
        WHEN 'plan' THEN
            RETURN (
                SELECT jsonb_agg(row_to_json(t))
                FROM cads.get_cts_parallel_import_plan(v_run_id) t
            );

        -- 3. Status-level totals summary for a run
        WHEN 'summary' THEN
            RETURN (
                SELECT jsonb_agg(row_to_json(t))
                FROM cads.get_cts_parallel_import_summary(v_run_id) t
            );

        ELSE
            RAISE EXCEPTION 'Unknown admin command: %', command_name;
        END CASE;
END;
$$;