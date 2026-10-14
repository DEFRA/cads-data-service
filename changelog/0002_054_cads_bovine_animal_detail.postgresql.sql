-- liquibase formatted sql

-- 054 updates the existing database-backed AnimalDetail function.
-- The function returns the complete relational projection consumed by the API.

-- changeset gary:0002_054_00
DROP FUNCTION IF EXISTS cads.get_bovine_animal_detail(text);
DROP FUNCTION IF EXISTS cads.get_animal_detail(text);
-- rollback -- no-op

-- changeset gary:0002_054_01 runInTransaction:false runOnChange:true
CREATE INDEX CONCURRENTLY IF NOT EXISTS ix_ct_animal_identifiers_detail_exact
    ON cts.ct_animal_identifiers
        (aid_identifier, aid_ran_id, aid_id DESC)
    WHERE aid_identifier IS NOT NULL
        AND aid_identifier_type = 'ET'
        AND aid_current_status IN ('1', 'L')
        AND aid_effective_to_date IS NULL;
-- rollback DROP INDEX CONCURRENTLY IF EXISTS cts.ix_ct_animal_identifiers_detail_exact;

-- changeset gary:0002_054_02 runInTransaction:false
CREATE INDEX CONCURRENTLY IF NOT EXISTS ix_ct_registered_movements_detail
    ON cts.ct_registered_movements
        (mov_ran_id, mov_movement_date DESC, mov_id DESC)
    INCLUDE (mov_loc_id, mov_direction, mov_current_status);
-- rollback DROP INDEX CONCURRENTLY IF EXISTS cts.ix_ct_registered_movements_detail;

-- changeset gary:0002_054_03 runInTransaction:false runOnChange:true
CREATE INDEX CONCURRENTLY IF NOT EXISTS ix_ct_animal_relationships_detail_live
    ON cts.ct_animal_relationships
        (aar_ran_id_child, aar_rel_type, aar_id DESC)
    INCLUDE (aar_parent_identifier)
    WHERE aar_current_status = 'L';
-- rollback DROP INDEX CONCURRENTLY IF EXISTS cts.ix_ct_animal_relationships_detail_live;

-- changeset gary:0002_054_04 runInTransaction:false
CREATE INDEX CONCURRENTLY IF NOT EXISTS ix_ct_location_identifiers_detail
    ON cts.ct_location_identifiers
        (lid_loc_id, lid_current_status, lid_sub_identifier);
-- rollback DROP INDEX CONCURRENTLY IF EXISTS cts.ix_ct_location_identifiers_detail;

-- changeset gary:0002_054_05 runInTransaction:false
CREATE INDEX CONCURRENTLY IF NOT EXISTS ix_ct_param_value_detail
    ON cts.ct_param_value (pvl_param, pvl_param_value)
    INCLUDE (pvl_param_short_desc, pvl_param_long_desc)
    WHERE pvl_current_status = '1';
-- rollback DROP INDEX CONCURRENTLY IF EXISTS cts.ix_ct_param_value_detail;

-- changeset gary:0002_054_06 splitStatements:false runOnChange:true
-- Recreate on rerun because adding output columns changes the return type.
DROP FUNCTION IF EXISTS cads.get_animal_detail(text);
CREATE OR REPLACE FUNCTION cads.get_animal_detail(p_eartag text)
    RETURNS TABLE(resource_type text, identifier character varying, event_datetime text, source_system text, source_schema text, source_schema_version text, animal_resource_type text, identifier_schema text, animal_identifier character varying, species text, sex text, birth_date date, registration_date date, date_on_cph date, breed_schema text, breed_code character varying, breed_display_name_long character varying, breed_display_name character varying, breed_name character varying, state text, restriction_status text, genetic_dam_schema text, genetic_dam_identifier character varying, sire_schema text, sire_identifier character varying, request_loc_id numeric, request_lid_identifier character varying, request_lid_sub_identifier character varying, request_lid_full_identifier character varying, request_loc_name character varying, request_loc_premises_type character varying, reg_lid_identifier character varying, reg_lid_sub_identifier character varying, reg_lid_full_identifier character varying, reg_loc_name character varying, reg_loc_premises_type character varying, death_lid_identifier character varying, death_lid_sub_identifier character varying, death_lid_full_identifier character varying, death_loc_name character varying, death_loc_premises_type character varying)
    LANGUAGE sql
    STABLE
AS $function$
WITH animal_key AS (
    SELECT aid.aid_ran_id AS ran_id
    FROM cts.ct_animal_identifiers aid
    WHERE NULLIF(btrim(p_eartag), '') IS NOT NULL
      AND aid.aid_identifier = upper(btrim(p_eartag))
      AND aid.aid_identifier_type = 'ET'
      AND aid.aid_current_status IN ('1', 'L')
      AND aid.aid_effective_to_date IS NULL
        FETCH FIRST 1 ROW ONLY
),
     core AS (
         SELECT
             ran.ran_id,
             aid.aid_identifier AS eartag,
             ran.ran_sex,
             ran.ran_birth_date,
             ran.ran_current_status,
             ran.ran_mov_id_registration,
             ran.ran_mov_id_death,
             brd.brd_code AS breed_code,
             brd.brd_long_description AS breed_display_name_long,
             brd.brd_short_description AS breed_display_name_short,
             COALESCE(brd.brd_long_description, brd.brd_short_description, brd.brd_code) AS breed_name
         FROM animal_key k
                  JOIN cts.ct_registered_animals ran
                       ON ran.ran_id = k.ran_id
                  JOIN cts.ct_animal_identifiers aid
                       ON aid.aid_ran_id = ran.ran_id
                           AND aid.aid_identifier_type = 'ET'
                           AND aid.aid_current_status IN ('1', 'L')
                           AND aid.aid_effective_to_date IS NULL
                  LEFT JOIN cts.ct_breeds brd
                            ON brd.brd_id = ran.ran_brd_id
     ),
     request_location AS (
         SELECT *
         FROM (
                  SELECT
                      lid.lid_loc_id,
                      lid.lid_identifier,
                      lid.lid_sub_identifier,
                      lid.lid_full_identifier,
                      adr.adr_name AS loc_name,
                      loc.loc_premises_type,
                      ROW_NUMBER() OVER (
                          ORDER BY COALESCE(lid.lid_sub_identifier, ' ')
                          ) AS rn
                  FROM cts.ct_location_identifiers lid
                           LEFT JOIN cts.ct_locations loc
                                     ON loc.loc_id = lid.lid_loc_id
                           LEFT JOIN LATERAL (
                      SELECT a.adr_name
                      FROM cts.ct_addresses AS a
                      WHERE a.adr_loc_id = lid.lid_loc_id
                        AND a.adr_current_status = '1'
                      ORDER BY a.adr_id DESC
                      LIMIT 1
                      ) AS adr ON true
                  WHERE lid.lid_current_status = '1'
                    AND lid.lid_loc_id = (
                      SELECT m.mov_loc_id
                      FROM cts.ct_registered_movements AS m
                               JOIN animal_key AS k
                                    ON k.ran_id = m.mov_ran_id
                      WHERE m.mov_loc_id IS NOT NULL
                      ORDER BY m.mov_movement_date DESC NULLS LAST, m.mov_id DESC
                      LIMIT 1
                  )
              ) AS ranked
         WHERE rn = 1
     ),
     date_on_cph AS (
         SELECT
             m.mov_ran_id AS ran_id,
             MIN(m.mov_movement_date) AS date_on_cph
         FROM cts.ct_registered_movements m
                  JOIN request_location rl
                       ON rl.lid_loc_id = m.mov_loc_id
         WHERE m.mov_current_status IN ('1', 'L')
           AND m.mov_direction = '1'  -- ON
         GROUP BY m.mov_ran_id
     ),
     registration_movement AS (
         SELECT
             c.ran_id,
             reg.mov_movement_date AS registration_date,
             reg.mov_loc_id        AS registration_loc_id
         FROM core c
                  LEFT JOIN cts.ct_registered_movements reg
                            ON reg.mov_id = c.ran_mov_id_registration
     ),
     death_movement AS (
         SELECT
             c.ran_id,
             dth.mov_movement_date AS death_date,
             dth.mov_loc_id        AS death_loc_id
         FROM core c
                  LEFT JOIN cts.ct_registered_movements dth
                            ON dth.mov_id = c.ran_mov_id_death
     ),
     registration_location AS (
         SELECT *
         FROM (
                  SELECT
                      rm.ran_id,
                      lid.lid_identifier      AS reg_lid_identifier,
                      lid.lid_sub_identifier  AS reg_lid_sub_identifier,
                      lid.lid_full_identifier AS reg_lid_full_identifier,
                      adr.adr_name            AS reg_loc_name,
                      loc.loc_premises_type   AS reg_loc_premises_type,
                      ROW_NUMBER() OVER (
                          PARTITION BY rm.ran_id
                          ORDER BY COALESCE(lid.lid_sub_identifier, ' ')
                          ) AS rn
                  FROM registration_movement rm
                           LEFT JOIN cts.ct_location_identifiers lid
                                     ON lid.lid_loc_id = rm.registration_loc_id
                                         AND lid.lid_current_status = '1'
                           LEFT JOIN cts.ct_locations loc
                                     ON loc.loc_id = rm.registration_loc_id
                           LEFT JOIN LATERAL (
                      SELECT a.adr_name
                      FROM cts.ct_addresses AS a
                      WHERE a.adr_loc_id = rm.registration_loc_id
                        AND a.adr_current_status = '1'
                      ORDER BY a.adr_id DESC
                      LIMIT 1
                      ) AS adr ON true
              ) AS ranked
         WHERE rn = 1
     ),
     death_location AS (
         SELECT *
         FROM (
                  SELECT
                      dm.ran_id,
                      lid.lid_identifier      AS death_lid_identifier,
                      lid.lid_sub_identifier  AS death_lid_sub_identifier,
                      lid.lid_full_identifier AS death_lid_full_identifier,
                      adr.adr_name            AS death_loc_name,
                      loc.loc_premises_type   AS death_loc_premises_type,
                      ROW_NUMBER() OVER (
                          PARTITION BY dm.ran_id
                          ORDER BY COALESCE(lid.lid_sub_identifier, ' ')
                          ) AS rn
                  FROM death_movement dm
                           LEFT JOIN cts.ct_location_identifiers lid
                                     ON lid.lid_loc_id = dm.death_loc_id
                                         AND lid.lid_current_status = '1'
                           LEFT JOIN cts.ct_locations loc
                                     ON loc.loc_id = dm.death_loc_id
                           LEFT JOIN LATERAL (
                      SELECT a.adr_name
                      FROM cts.ct_addresses AS a
                      WHERE a.adr_loc_id = dm.death_loc_id
                        AND a.adr_current_status = '1'
                      ORDER BY a.adr_id DESC
                      LIMIT 1
                      ) AS adr ON true
              ) AS ranked
         WHERE rn = 1
     ),
     parentage_pivot AS (
         SELECT
             aar.aar_ran_id_child AS ran_id,
             MAX(CASE
                     WHEN aar.aar_rel_type = 'CG' -- Calf-Genetic Dam (CP.AA_CG_REL)
                         THEN aar.aar_parent_identifier
                 END) AS genetic_dam_identifier,
             MAX(CASE
                     WHEN aar.aar_rel_type = 'CS' -- Calf-Sire (CP.AA_CS_REL)
                         THEN aar.aar_parent_identifier
                 END) AS sire_identifier
         FROM cts.ct_animal_relationships aar
                  JOIN animal_key k
                       ON k.ran_id = aar.aar_ran_id_child
         WHERE aar.aar_current_status = 'L'
           AND aar.aar_rel_type IN ('CG', 'CS')
         GROUP BY aar.aar_ran_id_child
     ),
     status_ref AS (
         SELECT
             pvl.pvl_param_value::numeric AS status_code,
             UPPER(COALESCE(pvl.pvl_param_long_desc, pvl.pvl_param_short_desc, pvl.pvl_param_value)) AS status_desc
         FROM cts.ct_param_value pvl
         WHERE pvl.pvl_param = 'CP.EARSTATUS'
           AND pvl.pvl_current_status = '1'
           AND pvl.pvl_param_value ~ '^[0-9]+$'
     )
SELECT
    /* top-level */
    'AnimalDetail' AS resource_type,
    c.eartag AS identifier,
    TO_CHAR(CURRENT_TIMESTAMP AT TIME ZONE 'UTC', 'YYYY-MM-DD"T"HH24:MI:SS"Z"') AS event_datetime,
    'CTS' AS source_system,
    'animal_details' AS source_schema,
    '1.0' AS source_schema_version,

    /* animalDetail */
    'Animal' AS animal_resource_type,
    'uk.gov.defra.ear-tag.conventional' AS identifier_schema,
    c.eartag AS animal_identifier,
    'Cattle' AS species,
    CASE c.ran_sex
        WHEN 'F' THEN 'Female'
        WHEN 'M' THEN 'Male'
        ELSE 'Unknown'
        END AS sex,
    c.ran_birth_date AS birth_date,
    rm.registration_date,
    doc.date_on_cph,
    'cts.breed' AS breed_schema,
    c.breed_code,
    c.breed_display_name_long,
    COALESCE(
        c.breed_display_name_long,
        c.breed_display_name_short
    ) AS breed_display_name,
    c.breed_name,
    CASE
        WHEN dm.death_date IS NULL THEN 'Alive'
        ELSE 'Dead'
        END AS state,
    CASE
        WHEN c.ran_current_status IN ('52', '53', '54') THEN 'Restricted'
        WHEN sr.status_desc LIKE '%LOST%' THEN 'Restricted'
        WHEN sr.status_desc LIKE '%STOLEN%' THEN 'Restricted'
        WHEN sr.status_desc LIKE '%SNAFF%' THEN 'Restricted'
        WHEN sr.status_desc LIKE '%REFUS%' THEN 'Restricted'
        WHEN sr.status_desc LIKE '%INCOM%' THEN 'Restricted'
        ELSE 'Unrestricted'
        END AS restriction_status,

    /* parentage as single-row columns */
    'uk.gov.defra.ear-tag.conventional' AS genetic_dam_schema,
    pp.genetic_dam_identifier,
    'uk.gov.defra.ear-tag.conventional' AS sire_schema,
    pp.sire_identifier,

    /* requested CPH location */
    rl.lid_loc_id          AS request_loc_id,
    rl.lid_identifier      AS request_lid_identifier,
    rl.lid_sub_identifier  AS request_lid_sub_identifier,
    rl.lid_full_identifier AS request_lid_full_identifier,
    rl.loc_name            AS request_loc_name,
    rl.loc_premises_type   AS request_loc_premises_type,

    /* registration location */
    regloc.reg_lid_identifier,
    regloc.reg_lid_sub_identifier,
    regloc.reg_lid_full_identifier,
    regloc.reg_loc_name,
    regloc.reg_loc_premises_type,

    /* death location */
    dloc.death_lid_identifier,
    dloc.death_lid_sub_identifier,
    dloc.death_lid_full_identifier,
    dloc.death_loc_name,
    dloc.death_loc_premises_type

FROM core c
         LEFT JOIN registration_movement rm
                   ON rm.ran_id = c.ran_id
         LEFT JOIN death_movement dm
                   ON dm.ran_id = c.ran_id
         LEFT JOIN date_on_cph doc
                   ON doc.ran_id = c.ran_id
         LEFT JOIN request_location rl
                   ON 1 = 1
         LEFT JOIN registration_location regloc
                   ON regloc.ran_id = c.ran_id
         LEFT JOIN death_location dloc
                   ON dloc.ran_id = c.ran_id
         LEFT JOIN parentage_pivot pp
                   ON pp.ran_id = c.ran_id
         LEFT JOIN status_ref sr
                   ON sr.status_code = c.ran_current_status::numeric;
$function$;

-- changeset gary:0002_052_07
COMMENT ON FUNCTION cads.get_animal_detail(text) IS
    'Returns the complete AnimalDetail relational projection for an exact ear-tag lookup, including breed, parentage, CPH, registration and death locations.';