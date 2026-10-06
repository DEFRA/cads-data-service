-- liquibase formatted sql

-- changeset MarkGent1:1789532400000-1 splitStatements:false
CREATE SCHEMA IF NOT EXISTS cads;

CREATE SEQUENCE IF NOT EXISTS cads.pseudo_anonymisation_run_id_seq;

DO $migration$
BEGIN
    IF to_regclass('cads.cts_pseudo_anonymisation_progress') IS NULL
       AND to_regclass('cads.pseudo_anonymisation_progress') IS NOT NULL THEN
        ALTER TABLE cads.pseudo_anonymisation_progress
            RENAME TO cts_pseudo_anonymisation_progress;
    END IF;
END;
$migration$;

CREATE TABLE IF NOT EXISTS cads.cts_pseudo_anonymisation_progress (
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

ALTER TABLE cads.cts_pseudo_anonymisation_progress
    ADD COLUMN IF NOT EXISTS failed_at timestamptz,
    ADD COLUMN IF NOT EXISTS error_code text,
    ADD COLUMN IF NOT EXISTS error_message text;

ALTER TABLE cads.cts_pseudo_anonymisation_progress
    DROP CONSTRAINT IF EXISTS pseudo_anonymisation_progress_status_check;

ALTER TABLE cads.cts_pseudo_anonymisation_progress
    DROP CONSTRAINT IF EXISTS cts_pseudo_anonymisation_progress_status_check;

ALTER TABLE cads.cts_pseudo_anonymisation_progress
    ADD CONSTRAINT cts_pseudo_anonymisation_progress_status_check
    CHECK (status IN ('PENDING', 'RUNNING', 'COMPLETED', 'ERROR'));

TRUNCATE TABLE cads.cts_pseudo_anonymisation_progress RESTART IDENTITY;

DROP FUNCTION IF EXISTS cads.set_pseudo_anonymisation_progress(bigint, text, text, text, bigint);

CREATE OR REPLACE FUNCTION cads.set_pseudo_anonymisation_progress(
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
    v_completed_at timestamptz;
    v_elapsed_seconds numeric;
BEGIN
    IF p_status = 'RUNNING' THEN
        UPDATE cads.cts_pseudo_anonymisation_progress
        SET status = 'RUNNING',
            started_at = clock_timestamp(),
            completed_at = NULL,
            failed_at = NULL,
            error_code = NULL,
            error_message = NULL,
            rows_anonymised = NULL,
            elapsed_hours = NULL,
            elapsed_minutes = NULL
        WHERE run_id = p_run_id
          AND schema_name = p_schema_name
          AND table_name = p_table_name;
    ELSIF p_status = 'COMPLETED' THEN
        v_completed_at := clock_timestamp();

        SELECT extract(epoch FROM (v_completed_at - started_at))
        INTO v_elapsed_seconds
        FROM cads.cts_pseudo_anonymisation_progress
        WHERE run_id = p_run_id
          AND schema_name = p_schema_name
          AND table_name = p_table_name;

        UPDATE cads.cts_pseudo_anonymisation_progress
        SET status = 'COMPLETED',
            rows_anonymised = p_rows_anonymised,
            completed_at = v_completed_at,
            elapsed_hours = floor(v_elapsed_seconds / 3600)::integer,
            elapsed_minutes = floor(mod(v_elapsed_seconds, 3600) / 60)::integer
        WHERE run_id = p_run_id
          AND schema_name = p_schema_name
          AND table_name = p_table_name;
    ELSIF p_status = 'ERROR' THEN
        v_completed_at := clock_timestamp();

        SELECT extract(epoch FROM (v_completed_at - started_at))
        INTO v_elapsed_seconds
        FROM cads.cts_pseudo_anonymisation_progress
        WHERE run_id = p_run_id
          AND schema_name = p_schema_name
          AND table_name = p_table_name;

        UPDATE cads.cts_pseudo_anonymisation_progress
        SET status = 'ERROR',
            failed_at = v_completed_at,
            error_code = p_error_code,
            error_message = p_error_message,
            elapsed_hours = floor(v_elapsed_seconds / 3600)::integer,
            elapsed_minutes = floor(mod(v_elapsed_seconds, 3600) / 60)::integer
        WHERE run_id = p_run_id
          AND schema_name = p_schema_name
          AND table_name = p_table_name;
    ELSE
        RAISE EXCEPTION 'Unsupported pseudonymisation status: %', p_status;
    END IF;
END;
$function$;

CREATE OR REPLACE PROCEDURE cads.pseudo_anonymise_data()
LANGUAGE plpgsql
AS $procedure$
DECLARE
    target_schema text;
    target_table text;
    current_run_id bigint;
    rows_anonymised bigint;
BEGIN
current_run_id := nextval('cads.pseudo_anonymisation_run_id_seq');

INSERT INTO cads.cts_pseudo_anonymisation_progress (run_id, schema_name, table_name, status)
SELECT current_run_id, schema_name, table_name, 'PENDING'
FROM unnest(ARRAY['cts', 'cts_transactions', 'cts_audit']) AS schemas(schema_name)
CROSS JOIN unnest(ARRAY[
    'ct_parties',
    'ct_locations',
    'ct_addresses',
    'ct_label_requests',
    'ct_web_users',
    'ct_comms_addresses',
    'ct_cts_users',
    'ct_email_log',
    'ct_location_party_rels',
    'ct_location_relationships',
    'ct_condition_markers',
    'ct_susp_condition_markers'
]) AS table_list(table_name);
COMMIT;

FOREACH target_schema IN ARRAY ARRAY['cts', 'cts_transactions', 'cts_audit']
LOOP
target_table := 'ct_parties';
PERFORM cads.set_pseudo_anonymisation_progress(current_run_id, target_schema, target_table, 'RUNNING');
COMMIT;
rows_anonymised := NULL;
BEGIN
EXECUTE format($sql$
UPDATE %I.ct_parties
SET par_initials = chr(65 + (get_byte(decode(md5(par_id::text), 'hex'), 0) %% 26)) || chr(65 + (get_byte(decode(md5(par_id::text), 'hex'), 1) %% 26)),
    par_surname = 'Surname_' || par_id::text,
    par_title = (ARRAY['MR','MRS','MS','MISS','DR'])[1 + (get_byte(decode(md5(par_id::text), 'hex'), 2) %% 5)],
    par_email_address = 'pa_email_' || par_id::text || '@defra.gov.uk',
    par_fax_number = 'PA-FAX-' || par_id::text,
    par_tel_number = 'PA-TEL-' || par_id::text,
    par_mobile_number = 'PA-MOB-' || par_id::text,
    par_comments = 'PA-COMMENT-' || par_id::text
WHERE par_id IS NOT NULL
$sql$, target_schema);
GET DIAGNOSTICS rows_anonymised = ROW_COUNT;
EXCEPTION WHEN query_canceled THEN
    PERFORM cads.set_pseudo_anonymisation_progress(current_run_id, target_schema, target_table, 'ERROR', NULL, SQLSTATE, SQLERRM);
WHEN OTHERS THEN
    PERFORM cads.set_pseudo_anonymisation_progress(current_run_id, target_schema, target_table, 'ERROR', NULL, SQLSTATE, SQLERRM);
END;
IF rows_anonymised IS NOT NULL THEN
PERFORM cads.set_pseudo_anonymisation_progress(current_run_id, target_schema, target_table, 'COMPLETED', rows_anonymised);
END IF;
COMMIT;

target_table := 'ct_locations';
PERFORM cads.set_pseudo_anonymisation_progress(current_run_id, target_schema, target_table, 'RUNNING');
COMMIT;
rows_anonymised := NULL;
BEGIN
EXECUTE format($sql$
UPDATE %I.ct_locations
SET loc_comments = 'PA-COMMENT-' || loc_id::text,
    loc_tel_number = 'PA-TEL-' || loc_id::text,
    loc_mobile_number = 'PA-MOB-' || loc_id::text,
    loc_fax_number = 'PA-FAX-' || loc_id::text,
    loc_email_address = 'pa_email_' || loc_id::text || '@defra.gov.uk'
WHERE loc_id IS NOT NULL
$sql$, target_schema);
GET DIAGNOSTICS rows_anonymised = ROW_COUNT;
EXCEPTION WHEN query_canceled THEN
    PERFORM cads.set_pseudo_anonymisation_progress(current_run_id, target_schema, target_table, 'ERROR', NULL, SQLSTATE, SQLERRM);
WHEN OTHERS THEN
    PERFORM cads.set_pseudo_anonymisation_progress(current_run_id, target_schema, target_table, 'ERROR', NULL, SQLSTATE, SQLERRM);
END;
IF rows_anonymised IS NOT NULL THEN
PERFORM cads.set_pseudo_anonymisation_progress(current_run_id, target_schema, target_table, 'COMPLETED', rows_anonymised);
END IF;
COMMIT;

target_table := 'ct_addresses';
PERFORM cads.set_pseudo_anonymisation_progress(current_run_id, target_schema, target_table, 'RUNNING');
COMMIT;
rows_anonymised := NULL;
BEGIN
EXECUTE format($sql$
UPDATE %I.ct_addresses
SET adr_name = 'ADR-NAME-' || adr_id::text,
    adr_address_2 = 'ADR-ADDR2-' || adr_id::text,
    adr_address_3 = 'ADR-ADDR3-' || adr_id::text,
    adr_address_4 = 'ADR-ADDR4-' || adr_id::text,
    adr_address_5 = 'ADR-ADDR5-' || adr_id::text,
    adr_post_code = 'PC' || lpad((adr_id %% 100000)::text, 5, '0')
WHERE adr_id IS NOT NULL
$sql$, target_schema);
GET DIAGNOSTICS rows_anonymised = ROW_COUNT;
EXCEPTION WHEN query_canceled THEN
    PERFORM cads.set_pseudo_anonymisation_progress(current_run_id, target_schema, target_table, 'ERROR', NULL, SQLSTATE, SQLERRM);
WHEN OTHERS THEN
    PERFORM cads.set_pseudo_anonymisation_progress(current_run_id, target_schema, target_table, 'ERROR', NULL, SQLSTATE, SQLERRM);
END;
IF rows_anonymised IS NOT NULL THEN
PERFORM cads.set_pseudo_anonymisation_progress(current_run_id, target_schema, target_table, 'COMPLETED', rows_anonymised);
END IF;
COMMIT;

target_table := 'ct_label_requests';
PERFORM cads.set_pseudo_anonymisation_progress(current_run_id, target_schema, target_table, 'RUNNING');
COMMIT;
rows_anonymised := NULL;
BEGIN
EXECUTE format($sql$
UPDATE %I.ct_label_requests
SET lar_keeper_title = (ARRAY['MR','MRS','MS','MISS','DR'])[1 + (get_byte(decode(md5(lar_id::text), 'hex'), 0) %% 5)],
    lar_keeper_initials = 'LK' || lar_id::text,
    lar_keeper_surname = 'Surname_' || lar_id::text,
    lar_label_loc_name = 'LAR-LOC-' || lar_id::text,
    lar_label_address_2 = 'LAR-ADDR2-' || lar_id::text,
    lar_label_address_3 = 'LAR-ADDR3-' || lar_id::text,
    lar_label_address_4 = 'LAR-ADDR4-' || lar_id::text,
    lar_label_address_5 = 'LAR-ADDR5-' || lar_id::text,
    lar_label_post_code = 'PC' || lpad((lar_id %% 100000)::text, 5, '0'),
    lar_corr_title = (ARRAY['MR','MRS','MS','MISS','DR'])[1 + (get_byte(decode(md5(lar_id::text), 'hex'), 1) %% 5)],
    lar_corr_initials = 'LC' || lar_id::text,
    lar_corr_surname = 'Surname_' || lar_id::text,
    lar_corr_loc_name = 'LAR-CORR-' || lar_id::text,
    lar_corr_address_2 = 'LAR-CADDR2-' || lar_id::text,
    lar_corr_address_3 = 'LAR-CADDR3-' || lar_id::text,
    lar_corr_address_4 = 'LAR-CADDR4-' || lar_id::text,
    lar_corr_address_5 = 'LAR-CADDR5-' || lar_id::text,
    lar_corr_post_code = 'PC' || lpad((lar_id %% 100000)::text, 5, '0')
WHERE lar_id IS NOT NULL
$sql$, target_schema);
GET DIAGNOSTICS rows_anonymised = ROW_COUNT;
EXCEPTION WHEN query_canceled THEN
    PERFORM cads.set_pseudo_anonymisation_progress(current_run_id, target_schema, target_table, 'ERROR', NULL, SQLSTATE, SQLERRM);
WHEN OTHERS THEN
    PERFORM cads.set_pseudo_anonymisation_progress(current_run_id, target_schema, target_table, 'ERROR', NULL, SQLSTATE, SQLERRM);
END;
IF rows_anonymised IS NOT NULL THEN
PERFORM cads.set_pseudo_anonymisation_progress(current_run_id, target_schema, target_table, 'COMPLETED', rows_anonymised);
END IF;
COMMIT;

target_table := 'ct_web_users';
PERFORM cads.set_pseudo_anonymisation_progress(current_run_id, target_schema, target_table, 'RUNNING');
COMMIT;
rows_anonymised := NULL;
BEGIN
EXECUTE format($sql$
UPDATE %I.ct_web_users
SET wur_mobile_number = 'WUR-MOB-' || wur_id::text,
    wur_telephone_number = 'WUR-TEL-' || wur_id::text,
    wur_user_name = 'WUR-USER-' || wur_id::text,
    wur_user_location = 'WUR-LOC-' || wur_id::text,
    wur_address_2 = 'WUR-ADDR2-' || wur_id::text,
    wur_address_3 = 'WUR-ADDR3-' || wur_id::text,
    wur_address_4 = 'WUR-ADDR4-' || wur_id::text,
    wur_address_5 = 'WUR-ADDR5-' || wur_id::text,
    wur_post_code = 'PC' || lpad((wur_id %% 100000)::text, 5, '0'),
    wur_email_address = 'wur_email_' || wur_id::text || '@defra.gov.uk'
WHERE wur_id IS NOT NULL
$sql$, target_schema);
GET DIAGNOSTICS rows_anonymised = ROW_COUNT;
EXCEPTION WHEN query_canceled THEN
    PERFORM cads.set_pseudo_anonymisation_progress(current_run_id, target_schema, target_table, 'ERROR', NULL, SQLSTATE, SQLERRM);
WHEN OTHERS THEN
    PERFORM cads.set_pseudo_anonymisation_progress(current_run_id, target_schema, target_table, 'ERROR', NULL, SQLSTATE, SQLERRM);
END;
IF rows_anonymised IS NOT NULL THEN
PERFORM cads.set_pseudo_anonymisation_progress(current_run_id, target_schema, target_table, 'COMPLETED', rows_anonymised);
END IF;
COMMIT;

target_table := 'ct_comms_addresses';
PERFORM cads.set_pseudo_anonymisation_progress(current_run_id, target_schema, target_table, 'RUNNING');
COMMIT;
rows_anonymised := NULL;
BEGIN
EXECUTE format($sql$
UPDATE %I.ct_comms_addresses
SET coa_email_address = 'coa_email_' || coa_id::text || '@defra.gov.uk'
WHERE coa_id IS NOT NULL
$sql$, target_schema);
GET DIAGNOSTICS rows_anonymised = ROW_COUNT;
EXCEPTION WHEN query_canceled THEN
    PERFORM cads.set_pseudo_anonymisation_progress(current_run_id, target_schema, target_table, 'ERROR', NULL, SQLSTATE, SQLERRM);
WHEN OTHERS THEN
    PERFORM cads.set_pseudo_anonymisation_progress(current_run_id, target_schema, target_table, 'ERROR', NULL, SQLSTATE, SQLERRM);
END;
IF rows_anonymised IS NOT NULL THEN
PERFORM cads.set_pseudo_anonymisation_progress(current_run_id, target_schema, target_table, 'COMPLETED', rows_anonymised);
END IF;
COMMIT;

target_table := 'ct_cts_users';
PERFORM cads.set_pseudo_anonymisation_progress(current_run_id, target_schema, target_table, 'RUNNING');
COMMIT;
rows_anonymised := NULL;
BEGIN
EXECUTE format($sql$
UPDATE %I.ct_cts_users
SET cus_room_name = 'CUS-ROOM-' || cus_id::text,
    cus_email_address = 'cus_email_' || cus_id::text || '@defra.gov.uk'
WHERE cus_id IS NOT NULL
$sql$, target_schema);
GET DIAGNOSTICS rows_anonymised = ROW_COUNT;
EXCEPTION WHEN query_canceled THEN
    PERFORM cads.set_pseudo_anonymisation_progress(current_run_id, target_schema, target_table, 'ERROR', NULL, SQLSTATE, SQLERRM);
WHEN OTHERS THEN
    PERFORM cads.set_pseudo_anonymisation_progress(current_run_id, target_schema, target_table, 'ERROR', NULL, SQLSTATE, SQLERRM);
END;
IF rows_anonymised IS NOT NULL THEN
PERFORM cads.set_pseudo_anonymisation_progress(current_run_id, target_schema, target_table, 'COMPLETED', rows_anonymised);
END IF;
COMMIT;

target_table := 'ct_email_log';
PERFORM cads.set_pseudo_anonymisation_progress(current_run_id, target_schema, target_table, 'RUNNING');
COMMIT;
rows_anonymised := NULL;
BEGIN
EXECUTE format($sql$
UPDATE %I.ct_email_log
SET eml_email_addr_recd = 'eml_recv_' || eml_id::text || '@defra.gov.uk',
    eml_email_addr_sent = 'eml_sent_' || eml_id::text || '@defra.gov.uk'
WHERE eml_id IS NOT NULL
$sql$, target_schema);
GET DIAGNOSTICS rows_anonymised = ROW_COUNT;
EXCEPTION WHEN query_canceled THEN
    PERFORM cads.set_pseudo_anonymisation_progress(current_run_id, target_schema, target_table, 'ERROR', NULL, SQLSTATE, SQLERRM);
WHEN OTHERS THEN
    PERFORM cads.set_pseudo_anonymisation_progress(current_run_id, target_schema, target_table, 'ERROR', NULL, SQLSTATE, SQLERRM);
END;
IF rows_anonymised IS NOT NULL THEN
PERFORM cads.set_pseudo_anonymisation_progress(current_run_id, target_schema, target_table, 'COMPLETED', rows_anonymised);
END IF;
COMMIT;

target_table := 'ct_location_party_rels';
PERFORM cads.set_pseudo_anonymisation_progress(current_run_id, target_schema, target_table, 'RUNNING');
COMMIT;
rows_anonymised := NULL;
BEGIN
EXECUTE format($sql$
UPDATE %I.ct_location_party_rels
SET lpr_comments = 'LPR-COMMENT-' || lpr_id::text
WHERE lpr_id IS NOT NULL
$sql$, target_schema);
GET DIAGNOSTICS rows_anonymised = ROW_COUNT;
EXCEPTION WHEN query_canceled THEN
    PERFORM cads.set_pseudo_anonymisation_progress(current_run_id, target_schema, target_table, 'ERROR', NULL, SQLSTATE, SQLERRM);
WHEN OTHERS THEN
    PERFORM cads.set_pseudo_anonymisation_progress(current_run_id, target_schema, target_table, 'ERROR', NULL, SQLSTATE, SQLERRM);
END;
IF rows_anonymised IS NOT NULL THEN
PERFORM cads.set_pseudo_anonymisation_progress(current_run_id, target_schema, target_table, 'COMPLETED', rows_anonymised);
END IF;
COMMIT;

target_table := 'ct_location_relationships';
PERFORM cads.set_pseudo_anonymisation_progress(current_run_id, target_schema, target_table, 'RUNNING');
COMMIT;
rows_anonymised := NULL;
BEGIN
EXECUTE format($sql$
UPDATE %I.ct_location_relationships
SET llr_comments = 'LLR-COMMENT-' || llr_id::text
WHERE llr_id IS NOT NULL
$sql$, target_schema);
GET DIAGNOSTICS rows_anonymised = ROW_COUNT;
EXCEPTION WHEN query_canceled THEN
    PERFORM cads.set_pseudo_anonymisation_progress(current_run_id, target_schema, target_table, 'ERROR', NULL, SQLSTATE, SQLERRM);
WHEN OTHERS THEN
    PERFORM cads.set_pseudo_anonymisation_progress(current_run_id, target_schema, target_table, 'ERROR', NULL, SQLSTATE, SQLERRM);
END;
IF rows_anonymised IS NOT NULL THEN
PERFORM cads.set_pseudo_anonymisation_progress(current_run_id, target_schema, target_table, 'COMPLETED', rows_anonymised);
END IF;
COMMIT;

target_table := 'ct_condition_markers';
PERFORM cads.set_pseudo_anonymisation_progress(current_run_id, target_schema, target_table, 'RUNNING');
COMMIT;
rows_anonymised := NULL;
BEGIN
EXECUTE format($sql$
UPDATE %I.ct_condition_markers
SET com_comments = 'COM-COMMENT-' || com_id::text
WHERE com_id IS NOT NULL
$sql$, target_schema);
GET DIAGNOSTICS rows_anonymised = ROW_COUNT;
EXCEPTION WHEN query_canceled THEN
    PERFORM cads.set_pseudo_anonymisation_progress(current_run_id, target_schema, target_table, 'ERROR', NULL, SQLSTATE, SQLERRM);
WHEN OTHERS THEN
    PERFORM cads.set_pseudo_anonymisation_progress(current_run_id, target_schema, target_table, 'ERROR', NULL, SQLSTATE, SQLERRM);
END;
IF rows_anonymised IS NOT NULL THEN
PERFORM cads.set_pseudo_anonymisation_progress(current_run_id, target_schema, target_table, 'COMPLETED', rows_anonymised);
END IF;
COMMIT;

target_table := 'ct_susp_condition_markers';
PERFORM cads.set_pseudo_anonymisation_progress(current_run_id, target_schema, target_table, 'RUNNING');
COMMIT;
rows_anonymised := NULL;
BEGIN
EXECUTE format($sql$
UPDATE %I.ct_susp_condition_markers
SET scm_comments = 'SCM-COMMENT-' || scm_id::text
WHERE scm_id IS NOT NULL
$sql$, target_schema);
GET DIAGNOSTICS rows_anonymised = ROW_COUNT;
EXCEPTION WHEN query_canceled THEN
    PERFORM cads.set_pseudo_anonymisation_progress(current_run_id, target_schema, target_table, 'ERROR', NULL, SQLSTATE, SQLERRM);
WHEN OTHERS THEN
    PERFORM cads.set_pseudo_anonymisation_progress(current_run_id, target_schema, target_table, 'ERROR', NULL, SQLSTATE, SQLERRM);
END;
IF rows_anonymised IS NOT NULL THEN
PERFORM cads.set_pseudo_anonymisation_progress(current_run_id, target_schema, target_table, 'COMPLETED', rows_anonymised);
END IF;
COMMIT;
END LOOP;
END;
$procedure$;
