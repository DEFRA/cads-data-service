-- liquibase formatted sql

-- changeset cla:0005-002-pseudo-anonymisation splitStatements:false
-- CLA pseudonymisation progress and helper routine.

CREATE SCHEMA IF NOT EXISTS cads;
CREATE SCHEMA IF NOT EXISTS cla;
CREATE SCHEMA IF NOT EXISTS cla_transactions;
CREATE SCHEMA IF NOT EXISTS cla_audit;

CREATE SEQUENCE IF NOT EXISTS cads.cla_pseudo_anonymisation_run_id_seq;

CREATE TABLE IF NOT EXISTS cads.cla_pseudo_anonymisation_progress (
    progress_id bigint GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    run_id bigint NOT NULL,
    schema_name text NOT NULL,
    table_name text NOT NULL,
    status text NOT NULL CHECK (status IN ('PENDING', 'RUNNING', 'COMPLETED', 'ERROR')),
    rows_anonymised bigint,
    started_at timestamptz,
    completed_at timestamptz,
    failed_at timestamptz,
    error_code text,
    error_message text,
    elapsed_hours integer,
    elapsed_minutes integer,
    UNIQUE (run_id, schema_name, table_name)
);

CREATE OR REPLACE FUNCTION cads.set_cla_pseudo_anonymisation_progress(
    p_run_id bigint,
    p_schema_name text,
    p_table_name text,
    p_status text,
    p_rows_anonymised bigint DEFAULT NULL,
    p_error_code text DEFAULT NULL,
    p_error_message text DEFAULT NULL
)
RETURNS void
LANGUAGE plpgsql
AS $function$
DECLARE
    v_finished_at timestamptz;
    v_elapsed_seconds numeric;
BEGIN
    IF p_status = 'RUNNING' THEN
        UPDATE cads.cla_pseudo_anonymisation_progress
        SET status = 'RUNNING', started_at = clock_timestamp(),
            completed_at = NULL, failed_at = NULL, error_code = NULL,
            error_message = NULL, rows_anonymised = NULL,
            elapsed_hours = NULL, elapsed_minutes = NULL
        WHERE run_id = p_run_id AND schema_name = p_schema_name AND table_name = p_table_name;
    ELSIF p_status IN ('COMPLETED', 'ERROR') THEN
        v_finished_at := clock_timestamp();
        SELECT extract(epoch FROM (v_finished_at - started_at))
        INTO v_elapsed_seconds
        FROM cads.cla_pseudo_anonymisation_progress
        WHERE run_id = p_run_id AND schema_name = p_schema_name AND table_name = p_table_name;

        UPDATE cads.cla_pseudo_anonymisation_progress
        SET status = p_status,
            rows_anonymised = CASE WHEN p_status = 'COMPLETED' THEN p_rows_anonymised ELSE NULL END,
            completed_at = CASE WHEN p_status = 'COMPLETED' THEN v_finished_at ELSE NULL END,
            failed_at = CASE WHEN p_status = 'ERROR' THEN v_finished_at ELSE NULL END,
            error_code = CASE WHEN p_status = 'ERROR' THEN p_error_code ELSE NULL END,
            error_message = CASE WHEN p_status = 'ERROR' THEN p_error_message ELSE NULL END,
            elapsed_hours = floor(COALESCE(v_elapsed_seconds, 0) / 3600)::integer,
            elapsed_minutes = floor(mod(COALESCE(v_elapsed_seconds, 0), 3600) / 60)::integer
        WHERE run_id = p_run_id AND schema_name = p_schema_name AND table_name = p_table_name;
    ELSE
        RAISE EXCEPTION 'Unsupported CLA pseudonymisation status: %', p_status;
    END IF;
END;
$function$;
