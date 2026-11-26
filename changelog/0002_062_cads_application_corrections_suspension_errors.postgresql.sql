-- liquibase formatted sql

-- changeset gary:0002_062_01 runInTransaction:false
CREATE INDEX CONCURRENTLY ix_rap_eartag_error_lookup
    ON cts.ct_received_applications (rap_eartag) INCLUDE (rap_id)
    WHERE rap_eartag IS NOT NULL;
-- rollback DROP INDEX CONCURRENTLY IF EXISTS cts.ix_rap_eartag_error_lookup;

-- changeset gary:0002_062_02 runInTransaction:false
CREATE INDEX CONCURRENTLY ix_rap_new_eartag_error_lookup
    ON cts.ct_received_applications (rap_new_eartag) INCLUDE (rap_id)
    WHERE rap_new_eartag IS NOT NULL;
-- rollback DROP INDEX CONCURRENTLY IF EXISTS cts.ix_rap_new_eartag_error_lookup;

-- changeset gary:0002_062_03 runInTransaction:false
CREATE INDEX CONCURRENTLY ix_san_eartag_error_lookup
    ON cts.ct_suspended_animals (san_eartag) INCLUDE (san_id)
    WHERE san_eartag IS NOT NULL;
-- rollback DROP INDEX CONCURRENTLY IF EXISTS cts.ix_san_eartag_error_lookup;

-- changeset gary:0002_062_04 runInTransaction:false
CREATE INDEX CONCURRENTLY ix_san_new_eartag_error_lookup
    ON cts.ct_suspended_animals (san_new_eartag) INCLUDE (san_id)
    WHERE san_new_eartag IS NOT NULL;
-- rollback DROP INDEX CONCURRENTLY IF EXISTS cts.ix_san_new_eartag_error_lookup;

-- changeset gary:0002_062_05 runInTransaction:false
CREATE INDEX CONCURRENTLY ix_acs_init_eartag_error_lookup
    ON cts.ct_animal_correct_summaries (acs_init_eartag) INCLUDE (acs_id)
    WHERE acs_init_eartag IS NOT NULL;
-- rollback DROP INDEX CONCURRENTLY IF EXISTS cts.ix_acs_init_eartag_error_lookup;

-- changeset gary:0002_062_06 runInTransaction:false
CREATE INDEX CONCURRENTLY ix_acs_new_eartag_error_lookup
    ON cts.ct_animal_correct_summaries (acs_new_eartag) INCLUDE (acs_id)
    WHERE acs_new_eartag IS NOT NULL;
-- rollback DROP INDEX CONCURRENTLY IF EXISTS cts.ix_acs_new_eartag_error_lookup;

-- changeset gary:0002_062_07 runInTransaction:false
CREATE INDEX CONCURRENTLY ix_acs_init_previous_eartag_error_lookup
    ON cts.ct_animal_correct_summaries (acs_init_previous_eartag) INCLUDE (acs_id)
    WHERE acs_init_previous_eartag IS NOT NULL;
-- rollback DROP INDEX CONCURRENTLY IF EXISTS cts.ix_acs_init_previous_eartag_error_lookup;


-- changeset gary:0002_062_08 splitStatements:false
CREATE FUNCTION cads.application_corrections_suspension_errors(p_eartag text)
RETURNS TABLE (
    searched_eartag text,
    source_table text,
    error_pk_column text,
    error_id numeric,
    parent_table text,
    parent_pk_column text,
    parent_id numeric,
    recorded_eartag text,
    new_eartag text,
    previous_eartag text,
    error_code text,
    field_name text,
    error_description text
)
LANGUAGE sql
STABLE
AS $function$
WITH search_tags(ear_tag_number) AS MATERIALIZED (
    VALUES (p_eartag)
),
errors (
    searched_eartag, source_table, error_pk_column, error_id,
    parent_table, parent_pk_column, parent_id,
    recorded_eartag, new_eartag, previous_eartag, field_name, error_code
) AS (
    SELECT h.ear_tag_number, 'cts.ct_recd_application_errors', 'rae_id', e.rae_id,
           'cts.ct_received_applications', 'rap_id', a.rap_id,
           a.rap_eartag, a.rap_new_eartag, NULL::varchar,
           e.rae_attribute_name, e.rae_error_code
    FROM search_tags h
    CROSS JOIN LATERAL (
        SELECT rap_id FROM cts.ct_received_applications
        WHERE rap_eartag = h.ear_tag_number
        UNION
        SELECT rap_id FROM cts.ct_received_applications
        WHERE rap_new_eartag = h.ear_tag_number
    ) p
    JOIN cts.ct_received_applications a ON a.rap_id = p.rap_id
    JOIN cts.ct_recd_application_errors e ON e.rae_rap_id = p.rap_id

    UNION ALL

    SELECT h.ear_tag_number, 'cts.ct_susp_animal_errors', 'sae_id', e.sae_id,
           'cts.ct_suspended_animals', 'san_id', a.san_id,
           a.san_eartag, a.san_new_eartag, NULL::varchar,
           e.sae_attribute_name, e.sae_error_code
    FROM search_tags h
    CROSS JOIN LATERAL (
        SELECT san_id FROM cts.ct_suspended_animals
        WHERE san_eartag = h.ear_tag_number
        UNION
        SELECT san_id FROM cts.ct_suspended_animals
        WHERE san_new_eartag = h.ear_tag_number
    ) p
    JOIN cts.ct_suspended_animals a ON a.san_id = p.san_id
    JOIN cts.ct_susp_animal_errors e ON e.sae_san_id = p.san_id

    UNION ALL

    SELECT h.ear_tag_number, 'cts.ct_animal_corr_summ_errors', 'ase_id', e.ase_id,
           'cts.ct_animal_correct_summaries', 'acs_id', a.acs_id,
           a.acs_init_eartag, a.acs_new_eartag, a.acs_init_previous_eartag,
           e.ase_attribute_name, e.ase_error_code
    FROM search_tags h
    CROSS JOIN LATERAL (
        SELECT acs_id FROM cts.ct_animal_correct_summaries
        WHERE acs_init_eartag = h.ear_tag_number
        UNION
        SELECT acs_id FROM cts.ct_animal_correct_summaries
        WHERE acs_new_eartag = h.ear_tag_number
        UNION
        SELECT acs_id FROM cts.ct_animal_correct_summaries
        WHERE acs_init_previous_eartag = h.ear_tag_number
    ) p
    JOIN cts.ct_animal_correct_summaries a ON a.acs_id = p.acs_id
    JOIN cts.ct_animal_corr_summ_errors e ON e.ase_acs_id = p.acs_id
)
SELECT e.searched_eartag,
       e.source_table, e.error_pk_column, e.error_id,
       e.parent_table, e.parent_pk_column, e.parent_id,
       e.recorded_eartag, e.new_eartag, e.previous_eartag,
       e.error_code, e.field_name,
       v.pvl_param_long_desc AS error_description
FROM errors e
LEFT JOIN cts.ct_param_value v
       ON v.pvl_param = 'CP.ERR_STATUS' AND v.pvl_param_value = e.error_code
ORDER BY e.searched_eartag, e.source_table, e.error_id;
$function$;
-- rollback DROP FUNCTION IF EXISTS cads.application_corrections_suspension_errors(text);
