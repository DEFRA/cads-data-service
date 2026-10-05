-- liquibase formatted sql

-- Indexes supporting the CPH animal detail query. The location and movement
-- indexes are deliberately partial/current-row indexes so they remain small
-- relative to the historical CTS tables.
-- changeset gary:0002_052_01 runInTransaction:false
CREATE INDEX CONCURRENTLY IF NOT EXISTS ix_ct_animal_identifiers_current_animal
    ON cts.ct_animal_identifiers
       (aid_ran_id, aid_identifier_type, aid_current_flag, aid_current_status,
        aid_effective_from_date DESC, aid_id DESC)
    INCLUDE (aid_identifier, aid_effective_to_date)
    WHERE aid_ran_id IS NOT NULL;
-- rollback DROP INDEX CONCURRENTLY IF EXISTS cts.ix_ct_animal_identifiers_current_animal;

-- changeset gary:0002_052_02 runInTransaction:false
CREATE INDEX CONCURRENTLY IF NOT EXISTS ix_ct_animal_relationships_child_type_latest
    ON cts.ct_animal_relationships
       (aar_ran_id_child, aar_rel_type, aar_id DESC)
    INCLUDE (aar_ran_id_parent, aar_parent_identifier, aar_parent_identifier_type)
    WHERE aar_ran_id_child IS NOT NULL;
-- rollback DROP INDEX CONCURRENTLY IF EXISTS cts.ix_ct_animal_relationships_child_type_latest;

-- changeset gary:0002_052_03 runInTransaction:false
CREATE INDEX CONCURRENTLY IF NOT EXISTS ix_ct_issued_documents_animal_latest
    ON cts.ct_issued_documents
       (ido_ran_id, ido_passport_version_number DESC, ido_creation_date DESC, ido_id DESC)
    INCLUDE (ido_current_status, ido_loc_id)
    WHERE ido_ran_id IS NOT NULL;
-- rollback DROP INDEX CONCURRENTLY IF EXISTS cts.ix_ct_issued_documents_animal_latest;

-- changeset gary:0002_052_04 runInTransaction:false
CREATE INDEX CONCURRENTLY IF NOT EXISTS ix_ct_applic_statuses_application_latest
    ON cts.ct_applic_statuses
       (aps_vap_id, aps_modified_date DESC, aps_id DESC)
    INCLUDE (aps_status, aps_intended_action)
    WHERE aps_vap_id IS NOT NULL;
-- rollback DROP INDEX CONCURRENTLY IF EXISTS cts.ix_ct_applic_statuses_application_latest;

-- changeset gary:0002_052_08 splitStatements:false
-- Preserve the original holding-selection logic and only add the contract
-- fields. This prevents the enrichment query from changing the 21-row result
-- set produced by get_animals_on_holding(text).
DO $rename$
BEGIN
    IF to_regprocedure('cads.get_animals_on_holding(text)') IS NOT NULL
       AND to_regprocedure('cads.get_animals_on_holding_legacy(text)') IS NULL THEN
        ALTER FUNCTION cads.get_animals_on_holding(text)
            RENAME TO get_animals_on_holding_legacy;
    END IF;
END
$rename$;

DROP FUNCTION IF EXISTS cads.get_animals_on_holding(text);

DROP FUNCTION IF EXISTS cads.get_animals_on_holding(text);
CREATE FUNCTION cads.get_animals_on_holding(p_cph_number text)
RETURNS TABLE (
    cph_number text,
    animal_id numeric,
    ear_tag_number text,
    ear_tag_url_identifier text,
    date_of_birth date,
    date_registered date,
    sex text,
    breed_code text,
    breed text,
    animal_status text,
    resource_type text,
    cph_schema text,
    location_name text,
    identifier_schema text,
    date_on_cph date,
    date_off_cph date,
    species text,
    breed_schema text,
    breed_name text,
    breed_identifier text
)
LANGUAGE sql
STABLE
AS $function$
WITH base AS (
    SELECT *
    FROM cads.get_animals_on_holding_legacy(p_cph_number)
), selected_location AS (
    SELECT DISTINCT ON (lid.lid_loc_id)
        lid.lid_loc_id AS loc_id
    FROM cts.ct_location_identifiers lid
    WHERE lid.lid_identifier = p_cph_number
      AND lid.lid_current_status = '1'
      AND lid.lid_effective_from_date <= CURRENT_DATE
      AND (lid.lid_effective_to_date IS NULL OR lid.lid_effective_to_date >= CURRENT_DATE)
    ORDER BY lid.lid_loc_id, lid.lid_effective_from_date DESC, lid.lid_id DESC
), holding_name AS (
    SELECT COALESCE(
               MAX(NULLIF(btrim(a.adr_name), '')),
               MAX(NULLIF(btrim(l.loc_comments), ''))
           ) AS location_name
    FROM selected_location sl
    JOIN cts.ct_locations l ON l.loc_id = sl.loc_id
    LEFT JOIN cts.ct_addresses a
      ON a.adr_loc_id = l.loc_id
     AND COALESCE(a.adr_current_status, '1') = '1'
), current_entry AS (
    SELECT DISTINCT ON (m.mov_ran_id)
        m.mov_ran_id AS animal_id,
        m.mov_movement_date::date AS date_on_cph
    FROM cts.ct_registered_movements m
    JOIN selected_location sl ON sl.loc_id = m.mov_loc_id
    WHERE m.mov_ran_id IS NOT NULL
      AND m.mov_direction = '1'
      AND m.mov_current_status <> 'C'
    ORDER BY m.mov_ran_id,
             m.mov_movement_date DESC NULLS LAST,
             m.mov_version_creation_date DESC NULLS LAST,
             m.mov_id DESC
)
SELECT
    b.cph_number,
    b.animal_id,
    b.ear_tag_number,
    b.ear_tag_url_identifier,
    b.date_of_birth,
    b.date_registered,
    b.sex,
    b.breed_code,
    b.breed,
    status_lookup.pvl_param_long_desc::text,
    'AnimalCollection'::text,
    'uk.gov.defra.cph'::text,
    (SELECT location_name FROM holding_name),
    'uk.gov.defra.ear-tag.conventional'::text,
    ce.date_on_cph,
    NULL::date,
    'Cattle'::text,
    'cts.breed'::text,
    b.breed,
    b.breed_code
FROM base b
LEFT JOIN current_entry ce ON ce.animal_id = b.animal_id
LEFT JOIN cts.ct_param_value status_lookup
  ON status_lookup.pvl_param = 'CP.EARSTATUS'
 AND status_lookup.pvl_param_value = b.animal_status_code
ORDER BY b.date_of_birth, b.ear_tag_number;
$function$;

COMMENT ON FUNCTION cads.get_animals_on_holding(text) IS
'Returns the original current-animal result set with the AnimalCollection contract fields added as columns. This one-argument form is equivalent to include_historical = false and preserves the original row selection.';

-- changeset gary:0002_052_09 splitStatements:false
DROP FUNCTION IF EXISTS cads.get_animals_on_holding(text, boolean);
CREATE FUNCTION cads.get_animals_on_holding(
    p_cph_number text,
    p_include_historical boolean
)
RETURNS TABLE (
    cph_number text,
    animal_id numeric,
    ear_tag_number text,
    ear_tag_url_identifier text,
    date_of_birth date,
    date_registered date,
    sex text,
    breed_code text,
    breed text,
    animal_status text,
    resource_type text,
    cph_schema text,
    location_name text,
    identifier_schema text,
    date_on_cph date,
    date_off_cph date,
    species text,
    breed_schema text,
    breed_name text,
    breed_identifier text
)
LANGUAGE sql
STABLE
AS $function$
WITH selected_location AS (
    SELECT DISTINCT ON (lid.lid_loc_id)
        lid.lid_loc_id AS loc_id,
        lid.lid_identifier::text AS cph_number
    FROM cts.ct_location_identifiers lid
    WHERE lid.lid_identifier = p_cph_number
      AND lid.lid_current_status = '1'
      AND lid.lid_effective_from_date <= CURRENT_DATE
      AND (lid.lid_effective_to_date IS NULL OR lid.lid_effective_to_date >= CURRENT_DATE)
    ORDER BY lid.lid_loc_id, lid.lid_effective_from_date DESC, lid.lid_id DESC
), holding_name AS (
    SELECT COALESCE(MAX(NULLIF(btrim(a.adr_name), '')), MAX(NULLIF(btrim(l.loc_comments), ''))) AS location_name
    FROM selected_location sl
    JOIN cts.ct_locations l ON l.loc_id = sl.loc_id
    LEFT JOIN cts.ct_addresses a
      ON a.adr_loc_id = l.loc_id
     AND COALESCE(a.adr_current_status, '1') = '1'
), inbound AS (
    SELECT m.*,
           ROW_NUMBER() OVER (
               PARTITION BY m.mov_ran_id, m.mov_id
               ORDER BY m.mov_movement_date DESC NULLS LAST, m.mov_id DESC
           ) AS rn
    FROM cts.ct_registered_movements m
    JOIN selected_location sl ON sl.loc_id = m.mov_loc_id
    WHERE m.mov_direction = '1'
      AND m.mov_current_status <> 'C'
      AND m.mov_ran_id IS NOT NULL
), episodes AS (
    SELECT
        i.mov_ran_id,
        i.mov_reported_eartag,
        i.mov_movement_date::date AS date_on_cph,
        off_movement.date_off_cph
    FROM inbound i
    LEFT JOIN LATERAL (
        SELECT o.mov_movement_date::date AS date_off_cph
        FROM cts.ct_registered_movements o
        WHERE o.mov_ran_id = i.mov_ran_id
          AND o.mov_current_status <> 'C'
          AND o.mov_direction <> '1'
          AND (o.mov_movement_date, o.mov_id) > (i.mov_movement_date, i.mov_id)
        ORDER BY o.mov_movement_date, o.mov_id
        LIMIT 1
    ) off_movement ON true
    WHERE i.rn = 1
), historical_rows AS (
    SELECT
        sl.cph_number,
        animal.ran_id AS animal_id,
        COALESCE(e.mov_reported_eartag, identifier.aid_identifier)::text AS ear_tag_number,
        regexp_replace(COALESCE(e.mov_reported_eartag, identifier.aid_identifier), '[^A-Za-z0-9]', '', 'g')::text AS ear_tag_url_identifier,
        animal.ran_birth_date AS date_of_birth,
        registration.mov_movement_received_date::date AS date_registered,
        animal.ran_sex::text AS sex,
        breed.brd_code::text AS breed_code,
        COALESCE(breed.brd_long_description, breed.brd_short_description, breed.brd_code)::text AS breed,
        animal_status.pvl_param_long_desc::text AS animal_status,
        'AnimalCollection'::text AS resource_type,
        'uk.gov.defra.cph'::text AS cph_schema,
        (SELECT location_name FROM holding_name) AS location_name,
        'uk.gov.defra.ear-tag.conventional'::text AS identifier_schema,
        e.date_on_cph,
        e.date_off_cph,
        'Cattle'::text AS species,
        'cts.breed'::text AS breed_schema,
        COALESCE(breed.brd_long_description, breed.brd_short_description, breed.brd_code)::text AS breed_name,
    breed.brd_code::text AS breed_identifier
    FROM episodes e
    JOIN selected_location sl ON true
    JOIN cts.ct_registered_animals animal ON animal.ran_id = e.mov_ran_id
    LEFT JOIN LATERAL (
        SELECT aid.aid_identifier
        FROM cts.ct_animal_identifiers aid
        WHERE aid.aid_ran_id = animal.ran_id
          AND aid.aid_identifier_type = 'ET'
          AND aid.aid_current_flag = 'Y'
        ORDER BY aid.aid_effective_from_date DESC NULLS LAST, aid.aid_id DESC
        LIMIT 1
    ) identifier ON true
    LEFT JOIN cts.ct_registered_movements registration
      ON registration.mov_id = animal.ran_mov_id_registration
    LEFT JOIN cts.ct_breeds breed ON breed.brd_id = animal.ran_brd_id
    LEFT JOIN cts.ct_param_value animal_status
      ON animal_status.pvl_param = 'CP.EARSTATUS'
     AND animal_status.pvl_param_value = animal.ran_current_status
    WHERE animal.ran_mov_id_death IS NULL
)
SELECT * FROM cads.get_animals_on_holding(p_cph_number)
WHERE NOT p_include_historical
UNION ALL
SELECT * FROM historical_rows
WHERE p_include_historical
ORDER BY date_of_birth, ear_tag_number, date_on_cph;
$function$;

COMMENT ON FUNCTION cads.get_animals_on_holding(text, boolean) IS
'Returns the tabular AnimalCollection fields. When p_include_historical is false, preserves the original current-animal result set; when true, returns one row per inbound holding episode and populates date_off_cph from the next outbound movement.';

-- changeset gary:0002_052_10 splitStatements:false
DROP FUNCTION IF EXISTS cads.get_animals_on_holding(text, boolean, bigint, bigint, text, text, text, text);
CREATE FUNCTION cads.get_animals_on_holding(
    p_cph_number text,
    p_include_historical boolean,
    p_row_from bigint,
    p_row_to bigint,
    p_sort_field text,
    p_sort_direction text,
    p_breed_code text,
    p_sex text
)
RETURNS TABLE (
    cph_number text,
    animal_id numeric,
    ear_tag_number text,
    ear_tag_url_identifier text,
    date_of_birth date,
    date_registered date,
    sex text,
    breed_code text,
    breed text,
    animal_status text,
    resource_type text,
    cph_schema text,
    location_name text,
    identifier_schema text,
    date_on_cph date,
    date_off_cph date,
    species text,
    breed_schema text,
    breed_name text,
    breed_identifier text,
    total_count bigint
)
LANGUAGE sql
STABLE
AS $function$
WITH filtered AS MATERIALIZED (
    SELECT *
    FROM cads.get_animals_on_holding(p_cph_number, p_include_historical) a
    WHERE (NULLIF(btrim(p_breed_code), '') IS NULL
           OR a.breed_identifier = p_breed_code)
      AND (NULLIF(btrim(p_sex), '') IS NULL
           OR upper(a.sex) = upper(p_sex))
), numbered AS (
    SELECT
        f.*,
        ROW_NUMBER() OVER (
            ORDER BY
                CASE WHEN lower(p_sort_direction) = 'asc'
                          AND lower(p_sort_field) = 'date_on_cph'
                     THEN f.date_on_cph END ASC NULLS LAST,
                CASE WHEN lower(p_sort_direction) = 'desc'
                          AND lower(p_sort_field) = 'date_on_cph'
                     THEN f.date_on_cph END DESC NULLS LAST,
                CASE WHEN lower(p_sort_direction) = 'asc'
                          AND lower(p_sort_field) = 'date_of_birth'
                     THEN f.date_of_birth END ASC NULLS LAST,
                CASE WHEN lower(p_sort_direction) = 'desc'
                          AND lower(p_sort_field) = 'date_of_birth'
                     THEN f.date_of_birth END DESC NULLS LAST,
                CASE WHEN lower(p_sort_direction) = 'asc'
                          AND lower(p_sort_field) = 'breed_code'
                     THEN f.breed_identifier END ASC NULLS LAST,
                CASE WHEN lower(p_sort_direction) = 'desc'
                          AND lower(p_sort_field) = 'breed_code'
                     THEN f.breed_identifier END DESC NULLS LAST,
                CASE WHEN lower(p_sort_direction) = 'asc'
                          AND lower(p_sort_field) = 'ear_tag_number'
                     THEN f.ear_tag_number END ASC NULLS LAST,
                CASE WHEN lower(p_sort_direction) = 'desc'
                          AND lower(p_sort_field) = 'ear_tag_number'
                     THEN f.ear_tag_number END DESC NULLS LAST,                
                CASE WHEN lower(p_sort_direction) = 'asc'
                          AND lower(p_sort_field) = 'sex'
                     THEN f.sex END ASC NULLS LAST,
                CASE WHEN lower(p_sort_direction) = 'desc'
                          AND lower(p_sort_field) = 'sex'
                     THEN f.sex END DESC NULLS LAST,
                f.date_on_cph ASC NULLS LAST,
                f.ear_tag_number ASC,
                f.animal_id ASC
        ) AS result_row,
        COUNT(*) OVER () AS total_count
    FROM filtered f
)
SELECT
    n.cph_number,
    n.animal_id,
    n.ear_tag_number,
    n.ear_tag_url_identifier,
    n.date_of_birth,
    n.date_registered,
    n.sex,
    n.breed_code,
    n.breed,
    n.animal_status,
    n.resource_type,
    n.cph_schema,
    n.location_name,
    n.identifier_schema,
    n.date_on_cph,
    n.date_off_cph,
    n.species,
    n.breed_schema,
    n.breed_name,
    n.breed_identifier,
    n.total_count
FROM numbered n
WHERE n.result_row BETWEEN GREATEST(p_row_from, 1)
                        AND GREATEST(p_row_to, GREATEST(p_row_from, 1))
ORDER BY n.result_row;
$function$;

COMMENT ON FUNCTION cads.get_animals_on_holding(text, boolean, bigint, bigint, text, text, text, text) IS
'Returns paginated CPH animal rows. Use row_from=1 and row_to=1000 for the default first page; sort_field defaults to date_on_cph, sort_direction to asc, and null/blank breed or sex for no filter.';
