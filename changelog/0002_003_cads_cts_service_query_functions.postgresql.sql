-- liquibase formatted sql

-- changeset schema:0002-003-cads-cts-service-query-functions splitStatements:false
CREATE SCHEMA IF NOT EXISTS cads;

CREATE OR REPLACE FUNCTION cads.get_sam_cattle_status(
    p_location_id text,
    p_sublocation_id text DEFAULT NULL
)
RETURNS TABLE (
    holding_id text,
    sub_holding_id text,
    full_holding_id text,
    animal_id numeric,
    eartag text,
    breed_code text,
    gender text,
    date_of_birth date,
    animal_status text,
    movement_id numeric,
    movement_direction text,
    movement_type text,
    movement_date date,
    movement_received_date date,
    anomaly_code text
)
LANGUAGE sql
STABLE
AS $function$
WITH holding AS (
    SELECT lid.lid_loc_id,
           lid.lid_identifier,
           lid.lid_sub_identifier,
           lid.lid_full_identifier
    FROM cts.ct_location_identifiers AS lid
    WHERE lid.lid_identifier = p_location_id
      AND (p_sublocation_id IS NULL OR lid.lid_sub_identifier = p_sublocation_id)
      AND lid.lid_current_status IN ('L', '1')
), latest_movement AS (
    SELECT DISTINCT ON (mov.mov_ran_id)
           mov.*
    FROM cts.ct_registered_movements AS mov
    WHERE mov.mov_current_status = 'L'
      AND mov.mov_ran_id IS NOT NULL
    ORDER BY mov.mov_ran_id, mov.mov_movement_date DESC NULLS LAST, mov.mov_id DESC
)
SELECT h.lid_identifier::text,
       h.lid_sub_identifier::text,
       h.lid_full_identifier::text,
       ran.ran_id,
       COALESCE(aid.aid_identifier, mov.mov_reported_eartag)::text,
       brd.brd_code::text,
       ran.ran_sex::text,
       ran.ran_birth_date,
       ran.ran_current_status::text,
       mov.mov_id,
       mov.mov_direction::text,
       mov.mov_movement_type::text,
       mov.mov_movement_date,
       mov.mov_movement_received_date,
       mov.mov_anomaly_code::text
FROM holding AS h
JOIN latest_movement AS mov ON mov.mov_loc_id = h.lid_loc_id
JOIN cts.ct_registered_animals AS ran ON ran.ran_id = mov.mov_ran_id
LEFT JOIN cts.ct_breeds AS brd ON brd.brd_id = ran.ran_brd_id
LEFT JOIN cts.ct_animal_identifiers AS aid
  ON aid.aid_ran_id = ran.ran_id
 AND aid.aid_identifier_type = 'ET'
 AND aid.aid_current_flag = 'Y'
 AND aid.aid_current_status = 'L'
ORDER BY COALESCE(aid.aid_identifier, mov.mov_reported_eartag), ran.ran_id;
$function$;

CREATE OR REPLACE FUNCTION cads.get_sam_animal_details(p_eartag text)
RETURNS TABLE (
    animal_id numeric,
    eartag text,
    breed_code text,
    breed_description text,
    gender text,
    date_of_birth date,
    animal_status text,
    cts_indicator text,
    passport_or_licence text,
    passport_version text,
    country_of_origin text,
    dam_eartag text,
    surrogate_dam_eartag text,
    sire_eartag text,
    movement_id numeric,
    movement_direction text,
    movement_type text,
    movement_date date,
    movement_received_date date,
    holding_id text,
    sub_holding_id text,
    full_holding_id text,
    anomaly_code text
)
LANGUAGE sql
STABLE
AS $function$
WITH animal_match AS (
    SELECT aid.aid_ran_id AS ran_id, aid.aid_identifier::text AS eartag, 1 AS source_order
    FROM cts.ct_animal_identifiers AS aid
    WHERE aid.aid_identifier = p_eartag
      AND aid.aid_identifier_type = 'ET'
      AND aid.aid_current_flag = 'Y'
      AND aid.aid_current_status IN ('L', '1')
    UNION ALL
    SELECT mov.mov_ran_id, mov.mov_reported_eartag::text, 2
    FROM cts.ct_registered_movements AS mov
    WHERE mov.mov_reported_eartag = p_eartag
      AND mov.mov_current_status = 'L'
), animal AS (
    SELECT ran.*, matched.eartag AS aid_identifier
    FROM animal_match AS matched
    JOIN cts.ct_registered_animals AS ran ON ran.ran_id = matched.ran_id
    ORDER BY matched.source_order
    LIMIT 1
)
SELECT a.ran_id,
       a.aid_identifier::text,
       brd.brd_code::text,
       brd.brd_long_description::text,
       a.ran_sex::text,
       a.ran_birth_date,
       a.ran_current_status::text,
       a.ran_cts_indicator::text,
       a.ran_passport_or_licence::text,
       a.ran_passport_version_number::text,
       cry.cry_code::text,
       dam.parent_identifier,
       surrogate.parent_identifier,
       sire.parent_identifier,
       mov.mov_id,
       mov.mov_direction::text,
       mov.mov_movement_type::text,
       mov.mov_movement_date,
       mov.mov_movement_received_date,
       lid.lid_identifier::text,
       lid.lid_sub_identifier::text,
       lid.lid_full_identifier::text,
       mov.mov_anomaly_code::text
FROM animal AS a
LEFT JOIN cts.ct_breeds AS brd ON brd.brd_id = a.ran_brd_id
LEFT JOIN cts.ct_countries AS cry ON cry.cry_id = a.ran_cry_id_chr_origin
LEFT JOIN cts.ct_registered_movements AS mov
  ON mov.mov_ran_id = a.ran_id
 AND mov.mov_current_status = 'L'
LEFT JOIN cts.ct_location_identifiers AS lid
  ON lid.lid_loc_id = mov.mov_loc_id
 AND lid.lid_current_status IN ('L', '1')
LEFT JOIN LATERAL (
    SELECT COALESCE(parent_aid.aid_identifier, rel.aar_parent_identifier)::text AS parent_identifier
    FROM cts.ct_animal_relationships AS rel
    LEFT JOIN cts.ct_animal_identifiers AS parent_aid
      ON parent_aid.aid_ran_id = rel.aar_ran_id_parent
     AND parent_aid.aid_identifier_type = 'ET'
     AND parent_aid.aid_current_flag = 'Y'
     AND parent_aid.aid_current_status = 'L'
    WHERE rel.aar_ran_id_child = a.ran_id AND rel.aar_rel_type = 'CB'
    ORDER BY rel.aar_id DESC LIMIT 1
) AS dam ON true
LEFT JOIN LATERAL (
    SELECT COALESCE(parent_aid.aid_identifier, rel.aar_parent_identifier)::text AS parent_identifier
    FROM cts.ct_animal_relationships AS rel
    LEFT JOIN cts.ct_animal_identifiers AS parent_aid
      ON parent_aid.aid_ran_id = rel.aar_ran_id_parent
     AND parent_aid.aid_identifier_type = 'ET'
     AND parent_aid.aid_current_flag = 'Y'
     AND parent_aid.aid_current_status = 'L'
    WHERE rel.aar_ran_id_child = a.ran_id AND rel.aar_rel_type = 'CG'
    ORDER BY rel.aar_id DESC LIMIT 1
) AS surrogate ON true
LEFT JOIN LATERAL (
    SELECT COALESCE(parent_aid.aid_identifier, rel.aar_parent_identifier)::text AS parent_identifier
    FROM cts.ct_animal_relationships AS rel
    LEFT JOIN cts.ct_animal_identifiers AS parent_aid
      ON parent_aid.aid_ran_id = rel.aar_ran_id_parent
     AND parent_aid.aid_identifier_type = 'ET'
     AND parent_aid.aid_current_flag = 'Y'
     AND parent_aid.aid_current_status = 'L'
    WHERE rel.aar_ran_id_child = a.ran_id AND rel.aar_rel_type = 'CS'
    ORDER BY rel.aar_id DESC LIMIT 1
) AS sire ON true
ORDER BY mov.mov_movement_date, mov.mov_id;
$function$;

CREATE OR REPLACE FUNCTION cads.get_sam_livestock_movements(p_eartag text)
RETURNS TABLE (
    movement_id numeric,
    animal_id numeric,
    eartag text,
    movement_direction text,
    movement_type text,
    movement_date date,
    report_received_date date,
    health_certificate_number text,
    anomaly_code text,
    holding_id text,
    sub_holding_id text,
    full_holding_id text
)
LANGUAGE sql
STABLE
AS $function$
WITH animal_match AS (
    SELECT aid.aid_ran_id AS ran_id, aid.aid_identifier::text AS eartag, 1 AS source_order
    FROM cts.ct_animal_identifiers AS aid
    WHERE aid.aid_identifier = p_eartag
      AND aid.aid_identifier_type = 'ET'
      AND aid.aid_current_flag = 'Y'
      AND aid.aid_current_status IN ('L', '1')
    UNION ALL
    SELECT mov.mov_ran_id, mov.mov_reported_eartag::text, 2
    FROM cts.ct_registered_movements AS mov
    WHERE mov.mov_reported_eartag = p_eartag
      AND mov.mov_current_status = 'L'
), animal AS (
    SELECT matched.ran_id, matched.eartag
    FROM animal_match AS matched
    ORDER BY matched.source_order
    LIMIT 1
)
SELECT mov.mov_id,
       mov.mov_ran_id,
       a.eartag,
       mov.mov_direction::text,
       mov.mov_movement_type::text,
       mov.mov_movement_date,
       mov.mov_movement_received_date,
       mov.mov_health_certificate_no::text,
       mov.mov_anomaly_code::text,
       lid.lid_identifier::text,
       lid.lid_sub_identifier::text,
       lid.lid_full_identifier::text
FROM animal AS a
JOIN cts.ct_registered_movements AS mov ON mov.mov_ran_id = a.ran_id
LEFT JOIN cts.ct_location_identifiers AS lid
  ON lid.lid_loc_id = mov.mov_loc_id
 AND lid.lid_current_status IN ('L', '1')
WHERE mov.mov_current_status = 'L'
ORDER BY mov.mov_movement_date DESC, mov.mov_direction, mov.mov_id DESC;
$function$;

CREATE OR REPLACE FUNCTION cads.get_sam_animal_cohort(
    p_eartag text,
    p_months integer DEFAULT 12
)
RETURNS TABLE (
    animal_id numeric,
    eartag text,
    gender text,
    date_of_birth date,
    animal_status text,
    breed_code text,
    breed_description text,
    birth_holding text,
    dam_eartag text,
    genetic_dam_eartag text
)
LANGUAGE sql
STABLE
AS $function$
WITH animal_match AS (
    SELECT aid.aid_ran_id AS ran_id, 1 AS source_order
    FROM cts.ct_animal_identifiers AS aid
    WHERE aid.aid_identifier = p_eartag
      AND aid.aid_identifier_type = 'ET'
      AND aid.aid_current_flag = 'Y'
      AND aid.aid_current_status IN ('L', '1')
    UNION ALL
    SELECT mov.mov_ran_id, 2
    FROM cts.ct_registered_movements AS mov
    WHERE mov.mov_reported_eartag = p_eartag
      AND mov.mov_current_status = 'L'
), index_animal AS (
    SELECT ran.ran_id, ran.ran_birth_date, reg.mov_loc_id
    FROM animal_match AS matched
    JOIN cts.ct_registered_animals AS ran ON ran.ran_id = matched.ran_id
    LEFT JOIN cts.ct_registered_movements AS reg ON reg.mov_id = ran.ran_mov_id_registration
    ORDER BY matched.source_order LIMIT 1
)
SELECT ran.ran_id,
       COALESCE(aid.aid_identifier, reported.mov_reported_eartag)::text,
       ran.ran_sex::text,
       ran.ran_birth_date,
       ran.ran_current_status::text,
       brd.brd_code::text,
       brd.brd_long_description::text,
       lid.lid_full_identifier::text,
       dam.parent_identifier,
       genetic.parent_identifier
FROM index_animal AS ia
JOIN cts.ct_registered_animals AS ran
  ON ran.ran_id <> ia.ran_id
 AND ran.ran_birth_date BETWEEN
     ia.ran_birth_date - make_interval(months => p_months)
     AND ia.ran_birth_date + make_interval(months => p_months)
JOIN cts.ct_registered_movements AS reg
  ON reg.mov_id = ran.ran_mov_id_registration
 AND reg.mov_loc_id = ia.mov_loc_id
LEFT JOIN cts.ct_animal_identifiers AS aid
  ON aid.aid_ran_id = ran.ran_id
 AND aid.aid_identifier_type = 'ET'
 AND aid.aid_current_flag = 'Y'
 AND aid.aid_current_status = 'L'
LEFT JOIN LATERAL (
    SELECT mov.mov_reported_eartag
    FROM cts.ct_registered_movements AS mov
    WHERE mov.mov_ran_id = ran.ran_id
      AND mov.mov_reported_eartag IS NOT NULL
    ORDER BY mov.mov_movement_date DESC NULLS LAST, mov.mov_id DESC
    LIMIT 1
) AS reported ON true
LEFT JOIN cts.ct_breeds AS brd ON brd.brd_id = ran.ran_brd_id
LEFT JOIN cts.ct_location_identifiers AS lid
  ON lid.lid_loc_id = reg.mov_loc_id
 AND lid.lid_current_status IN ('L', '1')
LEFT JOIN LATERAL (
    SELECT COALESCE(parent_aid.aid_identifier, rel.aar_parent_identifier)::text AS parent_identifier
    FROM cts.ct_animal_relationships AS rel
    LEFT JOIN cts.ct_animal_identifiers AS parent_aid
      ON parent_aid.aid_ran_id = rel.aar_ran_id_parent
     AND parent_aid.aid_identifier_type = 'ET'
     AND parent_aid.aid_current_flag = 'Y'
    WHERE rel.aar_ran_id_child = ran.ran_id AND rel.aar_rel_type = 'CB'
    ORDER BY rel.aar_id DESC LIMIT 1
) AS dam ON true
LEFT JOIN LATERAL (
    SELECT COALESCE(parent_aid.aid_identifier, rel.aar_parent_identifier)::text AS parent_identifier
    FROM cts.ct_animal_relationships AS rel
    LEFT JOIN cts.ct_animal_identifiers AS parent_aid
      ON parent_aid.aid_ran_id = rel.aar_ran_id_parent
     AND parent_aid.aid_identifier_type = 'ET'
     AND parent_aid.aid_current_flag = 'Y'
    WHERE rel.aar_ran_id_child = ran.ran_id AND rel.aar_rel_type = 'CG'
    ORDER BY rel.aar_id DESC LIMIT 1
) AS genetic ON true
ORDER BY ran.ran_birth_date, COALESCE(aid.aid_identifier, reported.mov_reported_eartag);
$function$;

CREATE OR REPLACE FUNCTION cads.get_sam_passport_and_animal_details(p_eartag text)
RETURNS TABLE (
    animal_id numeric,
    eartag text,
    date_of_birth date,
    gender text,
    animal_status text,
    cts_indicator text,
    breed_code text,
    breed_description text,
    country_of_origin text,
    passport_or_licence text,
    current_passport_version text,
    passport_modified_flag text,
    passport_holding text,
    issued_document_id numeric,
    passport_print_date date,
    passport_version_issued numeric,
    passport_layout_version text,
    passport_reason_code text,
    document_status text,
    issued_to_holding text,
    interface_file_name text,
    interface_transaction_number numeric
)
LANGUAGE sql
STABLE
AS $function$
WITH animal_match AS (
    SELECT aid.aid_ran_id AS ran_id, aid.aid_identifier::text AS eartag, 1 AS source_order
    FROM cts.ct_animal_identifiers AS aid
    WHERE aid.aid_identifier = p_eartag
      AND aid.aid_identifier_type = 'ET'
      AND aid.aid_current_flag = 'Y'
      AND aid.aid_current_status IN ('L', '1')
    UNION ALL
    SELECT mov.mov_ran_id, mov.mov_reported_eartag::text, 2
    FROM cts.ct_registered_movements AS mov
    WHERE mov.mov_reported_eartag = p_eartag
      AND mov.mov_current_status = 'L'
), animal AS (
    SELECT ran.*, matched.eartag AS aid_identifier
    FROM animal_match AS matched
    JOIN cts.ct_registered_animals AS ran ON ran.ran_id = matched.ran_id
    ORDER BY matched.source_order LIMIT 1
)
SELECT a.ran_id,
       a.aid_identifier::text,
       a.ran_birth_date,
       a.ran_sex::text,
       a.ran_current_status::text,
       a.ran_cts_indicator::text,
       brd.brd_code::text,
       brd.brd_long_description::text,
       cry.cry_code::text,
       a.ran_passport_or_licence::text,
       a.ran_passport_version_number::text,
       a.ran_passport_mod_flag::text,
       passport_lid.lid_full_identifier::text,
       ido.ido_id,
       ido.ido_creation_date,
       ido.ido_passport_version_number,
       ido.ido_passpt_layout_ver_number::text,
       ido.ido_reason_code::text,
       ido.ido_current_status::text,
       issued_lid.lid_full_identifier::text,
       ido.ido_interface_file_name::text,
       ido.ido_interface_txn_number
FROM animal AS a
LEFT JOIN cts.ct_breeds AS brd ON brd.brd_id = a.ran_brd_id
LEFT JOIN cts.ct_countries AS cry ON cry.cry_id = a.ran_cry_id_chr_origin
LEFT JOIN cts.ct_location_identifiers AS passport_lid
  ON passport_lid.lid_loc_id = a.ran_loc_id_passport
 AND passport_lid.lid_current_status IN ('L', '1')
LEFT JOIN cts.ct_issued_documents AS ido ON ido.ido_ran_id = a.ran_id
LEFT JOIN cts.ct_location_identifiers AS issued_lid
  ON issued_lid.lid_loc_id = ido.ido_loc_id
 AND issued_lid.lid_current_status IN ('L', '1')
ORDER BY ido.ido_passport_version_number DESC, ido.ido_id DESC;
$function$;
