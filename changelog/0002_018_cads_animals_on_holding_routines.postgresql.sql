-- liquibase formatted sql

-- changeset gary:0004_003_01 endDelimiter://
DROP FUNCTION IF EXISTS cads.get_holding_information(TEXT);

CREATE OR REPLACE FUNCTION cads.get_holding_information(p_cph_number text)
 RETURNS TABLE(cph_number text, holding_name text, business_name text, address_line_1 text, address_line_2 text, address_line_3 text, address_line_4 text, address_line_5 text, postcode text, county text, country text, holding_type text, premises_type_code text, premises_type_description text, registered_keeper text, herd_mark text)
 LANGUAGE sql
 STABLE
AS $function$
WITH selected_holding AS (
    SELECT DISTINCT ON (lid.lid_loc_id)
        lid.lid_loc_id AS loc_id,
        lid.lid_identifier::TEXT AS cph_number
    FROM cts.ct_location_identifiers lid
    WHERE lid.lid_identifier = p_cph_number
      AND lid.lid_current_status = '1'
      AND lid.lid_effective_from_date <= CURRENT_DATE
      AND (
          lid.lid_effective_to_date IS NULL
          OR lid.lid_effective_to_date >= CURRENT_DATE
      )
    ORDER BY
        lid.lid_loc_id,
        lid.lid_effective_from_date DESC,
        lid.lid_id DESC
),
holding_address AS (
    SELECT DISTINCT ON (adr.adr_loc_id)
        adr.adr_loc_id,
        adr.adr_name,
        adr.adr_address_2,
        adr.adr_address_3,
        adr.adr_address_4,
        adr.adr_address_5,
        adr.adr_post_code
    FROM cts.ct_addresses adr
    JOIN selected_holding holding
      ON holding.loc_id = adr.adr_loc_id
    WHERE COALESCE(adr.adr_current_status, '1') = '1'
    ORDER BY
        adr.adr_loc_id,
        adr.adr_current_modified_date DESC NULLS LAST,
        adr.adr_id DESC
),
current_keeper AS (
    SELECT DISTINCT ON (rel.lpr_loc_id)
        rel.lpr_loc_id,
        rel.lpr_par_id
    FROM cts.ct_location_party_rels rel
    JOIN selected_holding holding
      ON holding.loc_id = rel.lpr_loc_id
    JOIN cts.ct_location_party_rel_types rel_type
      ON rel_type.lpt_id = rel.lpr_lpt_id
     AND rel_type.lpt_code = 'KN'
    WHERE rel.lpr_current_status = '1'
      AND rel.lpr_effective_from_date <= CURRENT_DATE
      AND (
          rel.lpr_effective_to_date IS NULL
          OR rel.lpr_effective_to_date >= CURRENT_DATE
      )
    ORDER BY
        rel.lpr_loc_id,
        rel.lpr_effective_from_date DESC,
        rel.lpr_id DESC
),
keeper_address AS (
    SELECT DISTINCT ON (adr.adr_par_id)
        adr.adr_par_id,
        adr.adr_name
    FROM cts.ct_addresses adr
    JOIN current_keeper keeper
      ON keeper.lpr_par_id = adr.adr_par_id
    WHERE COALESCE(adr.adr_current_status, '1') = '1'
    ORDER BY
        adr.adr_par_id,
        adr.adr_current_modified_date DESC NULLS LAST,
        adr.adr_id DESC
),
candidate_animals AS (
    SELECT DISTINCT mov.mov_ran_id
    FROM cts.ct_registered_movements mov
    JOIN selected_holding holding
      ON holding.loc_id = mov.mov_loc_id
    WHERE mov.mov_direction = '1'
      AND mov.mov_current_status <> 'C'
      AND mov.mov_ran_id IS NOT NULL
),
latest_movements AS (
    SELECT DISTINCT ON (mov.mov_ran_id)
        mov.mov_ran_id,
        mov.mov_loc_id,
        mov.mov_direction,
        mov.mov_reported_eartag
    FROM cts.ct_registered_movements mov
    JOIN candidate_animals candidate
      ON candidate.mov_ran_id = mov.mov_ran_id
    WHERE mov.mov_current_status <> 'C'
    ORDER BY
        mov.mov_ran_id,
        mov.mov_movement_date DESC NULLS LAST,
        mov.mov_version_creation_date DESC NULLS LAST,
        mov.mov_id DESC
),
holding_herd_marks AS (
    SELECT
        latest.mov_loc_id,
        string_agg(
            DISTINCT 'UK ' || substring(
                regexp_replace(upper(latest.mov_reported_eartag), '[^A-Z0-9]', '', 'g')
                FROM 3 FOR 6
            ),
            ', '
        ) AS herd_mark
    FROM latest_movements latest
    JOIN cts.ct_registered_animals animal
      ON animal.ran_id = latest.mov_ran_id
    WHERE latest.mov_direction = '1'
      AND animal.ran_mov_id_death IS NULL
      AND regexp_replace(
          upper(latest.mov_reported_eartag),
          '[^A-Z0-9]',
          '',
          'g'
      ) ~ '^UK[0-9]{12}$'
    GROUP BY latest.mov_loc_id
)
SELECT
    holding.cph_number,
    address.adr_name::TEXT AS holding_name,
    keeper_address.adr_name::TEXT AS business_name,
    address.adr_name::TEXT AS address_line_1,
    address.adr_address_2::TEXT AS address_line_2,
    address.adr_address_3::TEXT AS address_line_3,
    address.adr_address_4::TEXT AS address_line_4,
    address.adr_address_5::TEXT AS address_line_5,
    address.adr_post_code::TEXT AS postcode,
    county.cty_name::TEXT AS county,
    CASE
        WHEN county.cty_uk_area = 'S' THEN 'Scotland'
        WHEN county.cty_uk_area = 'W' THEN 'Wales'
        WHEN county.cty_name IS NOT NULL THEN 'England'
        ELSE 'Unknown'
    END::TEXT AS country,
    CASE
        WHEN location.loc_premises_type = 'TH' THEN 'Temporary'
        ELSE 'Permanent'
    END::TEXT AS holding_type,
    location.loc_premises_type::TEXT AS premises_type_code,
    premises_type.pvl_param_long_desc::TEXT AS premises_type_description,
    concat_ws(
        ' ',
        NULLIF(party.par_title, ''),
        NULLIF(party.par_initials, ''),
        NULLIF(party.par_surname, '')
    )::TEXT AS registered_keeper,
    herd_marks.herd_mark::TEXT
FROM selected_holding holding
JOIN cts.ct_locations location
  ON location.loc_id = holding.loc_id
LEFT JOIN holding_address address
  ON address.adr_loc_id = holding.loc_id
LEFT JOIN cts.ct_counties county
  ON county.cty_id = location.loc_cty_id
LEFT JOIN cts.ct_param_value premises_type
  ON premises_type.pvl_param = 'CP.PTYPE'
 AND premises_type.pvl_param_value = location.loc_premises_type
LEFT JOIN current_keeper keeper
  ON keeper.lpr_loc_id = holding.loc_id
LEFT JOIN cts.ct_parties party
  ON party.par_id = keeper.lpr_par_id
LEFT JOIN keeper_address
  ON keeper_address.adr_par_id = party.par_id
LEFT JOIN holding_herd_marks herd_marks
  ON herd_marks.mov_loc_id = holding.loc_id;
$function$

-- changeset gary:0004_003_02 endDelimiter://
DROP FUNCTION IF EXISTS cads.get_animals_on_holding(TEXT);

CREATE OR REPLACE FUNCTION cads.get_animals_on_holding(p_cph_number text)
 RETURNS TABLE(cph_number text, animal_id numeric, ear_tag_number text, ear_tag_url_identifier text, date_of_birth date, date_registered date, sex text, breed_code text, breed text, animal_status_code text, animal_status text)
 LANGUAGE sql
 STABLE
AS $function$
WITH selected_holding AS (
    SELECT DISTINCT ON (lid.lid_loc_id)
        lid.lid_loc_id AS loc_id,
        lid.lid_identifier::TEXT AS cph_number
    FROM cts.ct_location_identifiers lid
    WHERE lid.lid_identifier = p_cph_number
      AND lid.lid_current_status = '1'
      AND lid.lid_effective_from_date <= CURRENT_DATE
      AND (
          lid.lid_effective_to_date IS NULL
          OR lid.lid_effective_to_date >= CURRENT_DATE
      )
    ORDER BY
        lid.lid_loc_id,
        lid.lid_effective_from_date DESC,
        lid.lid_id DESC
),
candidate_animals AS (
    SELECT DISTINCT mov.mov_ran_id
    FROM cts.ct_registered_movements mov
    JOIN selected_holding holding
      ON holding.loc_id = mov.mov_loc_id
    WHERE mov.mov_direction = '1'
      AND mov.mov_current_status <> 'C'
      AND mov.mov_ran_id IS NOT NULL
),
latest_movements AS (
    SELECT DISTINCT ON (mov.mov_ran_id)
        mov.mov_ran_id,
        mov.mov_loc_id,
        mov.mov_direction,
        mov.mov_reported_eartag
    FROM cts.ct_registered_movements mov
    JOIN candidate_animals candidate
      ON candidate.mov_ran_id = mov.mov_ran_id
    WHERE mov.mov_current_status <> 'C'
    ORDER BY
        mov.mov_ran_id,
        mov.mov_movement_date DESC NULLS LAST,
        mov.mov_version_creation_date DESC NULLS LAST,
        mov.mov_id DESC
)
SELECT
    holding.cph_number,
    animal.ran_id AS animal_id,
    latest.mov_reported_eartag::TEXT AS ear_tag_number,
    regexp_replace(
        latest.mov_reported_eartag,
        '[^A-Za-z0-9]',
        '',
        'g'
    )::TEXT AS ear_tag_url_identifier,
    animal.ran_birth_date AS date_of_birth,
    registration.mov_movement_received_date AS date_registered,
    animal.ran_sex::TEXT AS sex,
    breed.brd_code::TEXT AS breed_code,
    coalesce(
        breed.brd_long_description,
        breed.brd_short_description,
        breed.brd_code
    )::TEXT AS breed,
    animal.ran_current_status::TEXT AS animal_status_code,
    animal_status.pvl_param_long_desc::TEXT AS animal_status
FROM selected_holding holding
JOIN latest_movements latest
  ON latest.mov_loc_id = holding.loc_id
 AND latest.mov_direction = '1'
JOIN cts.ct_registered_animals animal
  ON animal.ran_id = latest.mov_ran_id
LEFT JOIN cts.ct_registered_movements registration
  ON registration.mov_id = animal.ran_mov_id_registration
LEFT JOIN cts.ct_breeds breed
  ON breed.brd_id = animal.ran_brd_id
LEFT JOIN cts.ct_param_value animal_status
  ON animal_status.pvl_param = 'CP.EARSTATUS'
 AND animal_status.pvl_param_value = animal.ran_current_status
WHERE animal.ran_mov_id_death IS NULL
  AND coalesce(animal.ran_current_status, '') <> '48'
ORDER BY
    animal.ran_birth_date,
    latest.mov_reported_eartag;
$function$

-- changeset gary:0004_003_03 endDelimiter://
DROP FUNCTION IF EXISTS cads.get_animal_error_record(TEXT);

CREATE OR REPLACE FUNCTION cads.get_animal_error_record(p_cph_number text)
 RETURNS TABLE(cph_number text, animal_id numeric, ear_tag_number text, ear_tag_url_identifier text, date_of_birth date, date_registered date, reason_for_error text, animal_status_code text, condition_error_count bigint, total_animals_needing_attention bigint)
 LANGUAGE sql
 STABLE
AS $function$
WITH animals_on_holding AS MATERIALIZED (
    SELECT *
    FROM cads.get_animals_on_holding(p_cph_number)
),
active_condition_errors AS (
    SELECT
        marker.com_ran_id AS animal_id,
        count(*) AS condition_error_count,
        string_agg(
            DISTINCT coalesce(
                variant.cov_long_description,
                condition.con_long_description,
                marker.com_comments
            ),
            '; '
        ) AS condition_error_reasons
    FROM cts.ct_condition_markers marker
    LEFT JOIN cts.ct_condition_variants variant
      ON variant.cov_id = marker.com_cov_id
    LEFT JOIN cts.ct_condition_activities activity
      ON activity.cac_id = marker.com_cac_id
    LEFT JOIN cts.ct_conditions condition
      ON condition.con_id = coalesce(
          variant.cov_con_id,
          activity.cac_con_id
      )
    JOIN animals_on_holding animal
      ON animal.animal_id = marker.com_ran_id
    WHERE marker.com_current_status = '1'
      AND marker.com_effective_from_date <= CURRENT_DATE
      AND (
          marker.com_effective_to_date IS NULL
          OR marker.com_effective_to_date >= CURRENT_DATE
      )
      AND coalesce(variant.cov_scope, 'A') = 'A'
      AND coalesce(
          variant.cov_condition_variant,
          ''
      ) <> 'REMOVE_FOR_DEAD'
    GROUP BY marker.com_ran_id
),
error_records AS (
    SELECT
        animal.cph_number,
        animal.animal_id,
        animal.ear_tag_number,
        animal.ear_tag_url_identifier,
        animal.date_of_birth,
        animal.date_registered,
        CASE
            WHEN animal.date_registered > animal.date_of_birth + 27
                THEN 'Date of registration is more than 27 days after date of birth.'
            WHEN animal.animal_status_code = '30'
                THEN 'Sex does not match the number recorded at birth notification.'
            WHEN animal.animal_status_code = '41'
                THEN 'Dam ear tag number recorded does not exist on the holding register.'
            WHEN animal.animal_status_code = '46'
                THEN 'Dam sex does not match the recorded animal details.'
            WHEN condition_error.condition_error_reasons IS NOT NULL
                THEN condition_error.condition_error_reasons
            WHEN animal.animal_status IS NOT NULL
                THEN animal.animal_status
            ELSE 'Animal record requires attention.'
        END::TEXT AS reason_for_error,
        animal.animal_status_code,
        coalesce(condition_error.condition_error_count, 0)::BIGINT
            AS condition_error_count
    FROM animals_on_holding animal
    LEFT JOIN active_condition_errors condition_error
      ON condition_error.animal_id = animal.animal_id
    WHERE animal.animal_status_code IN (
              '14', '30', '41', '46', '51', '52', '53', '54'
          )
       OR condition_error.condition_error_count > 0
       OR animal.date_registered > animal.date_of_birth + 27
)
SELECT
    error.cph_number,
    error.animal_id,
    error.ear_tag_number,
    error.ear_tag_url_identifier,
    error.date_of_birth,
    error.date_registered,
    error.reason_for_error,
    error.animal_status_code,
    error.condition_error_count,
    count(*) OVER ()::BIGINT AS total_animals_needing_attention
FROM error_records error
ORDER BY
    error.date_of_birth,
    error.ear_tag_number;
$function$
