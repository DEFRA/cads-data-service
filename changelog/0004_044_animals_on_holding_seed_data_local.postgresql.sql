-- liquibase formatted sql

-- changeset MarkGent1:seed-animals-on-holding-and-animal-detail context:local

--
-- Local/integration-test seed for:
--   cads.get_animals_on_holding(...)   (list, paging, filters, historical)
--   cads.get_animal_detail(...)        (single animal, parentage, state)
--
-- Follows on from 0004_043 (births seed). All ids are in the 93xxxxxxx range so they
-- cannot collide with the 90xxxxxxx / 91xxxxxxx / 92xxxxxxx ids used there.
--
-- Holding 12/345/6789 ("Oakfield Farm")  - loc 930000001
--   30 CURRENT animals   (n = 1..30)
--        sex       : even index = F (15), odd index = M (15)
--        breed     : index % 3 -> 0 HO, 1 AA, 2 LM  (10 each)  => Female + HO = 5
--        n = 1     : UK324537113234  Female, HO, Restricted, born 2023-03-10,
--                    registered 2023-03-14, dam UK324537113243, sire UK324537110987
--        n = 2     : stored with spaces ('UK 324537 120001') to exercise normalisation
--    3 HISTORICAL animals (n = 31..33): on the holding, then moved off
--    1 DEAD animal        (n = 34)    : UK324537113250, no parents
--   => include_historical = false : 30 rows, include_historical = true : 33 rows
--
-- Holding 98/765/4321 ("Elm Farm")     - loc 930000002
--    5 CURRENT animals   (n = 101..105)   (proves the CPH filter isolates results)
 
-- # Locations, location identifiers and addresses
INSERT INTO cts.ct_locations (
    loc_id, loc_cty_id, loc_effective_from, loc_current_status,
    loc_current_user, loc_current_modified_date, loc_version, row_number
) VALUES
    (930000001, 45, '2000-01-01', '1', 'seed', '2026-04-23', 1, 1),
    (930000002, 55, '2000-01-01', '1', 'seed', '2026-04-23', 1, 2);
 
INSERT INTO cts.ct_location_identifiers (
    lid_id, lid_loc_id, lid_effective_from_date, lid_identifier, lid_full_identifier,
    lid_sub_identifier, lid_effective_to_date, lid_current_status,
    lid_current_modified_date, lid_current_user, lid_version, row_number
) VALUES
    (935000001, 930000001, '2000-01-01', '12/345/6789', 'AH-12/345/6789', NULL, NULL, '1', '2026-04-23', 'seed', 1, 1),
    (935000002, 930000002, '2000-01-01', '98/765/4321', 'AH-98/765/4321', NULL, NULL, '1', '2026-04-23', 'seed', 1, 2);
 
INSERT INTO cts.ct_addresses (
    adr_id, adr_loc_id, adr_name, adr_address_2, adr_address_3, adr_address_4,
    adr_post_code, adr_current_modified_date, adr_current_status, adr_current_user,
    adr_version, row_number
) VALUES
    (936000001, 930000001, 'Oakfield Farm', 'Oakfield Lane', 'Hightown', 'Testshire', 'TS1 1AA', '2026-04-23', '1', 'seed', 1, 1),
    (936000002, 930000002, 'Elm Farm',      'Elm Road',      'Lowtown',  'Testshire', 'TS2 2BB', '2026-04-23', '1', 'seed', 1, 2);
 
-- # Animal templates (one row per animal to create)
CREATE TEMPORARY TABLE seed_animals AS
WITH current_animals AS (
    SELECT
        n,
        930000001::numeric AS loc_id,
        'current'::text AS kind,
        CASE n
            WHEN 1 THEN 'UK324537113234'
            WHEN 2 THEN 'UK 324537 120001'          -- spaces: normalisation case
            ELSE 'UK3245371' || (20000 + n - 1)::text
        END AS eartag,
        CASE WHEN (n - 1) % 2 = 0 THEN 'F' ELSE 'M' END AS sex,
        DATE '2023-03-10' + ((n - 1) * 5) AS birth_date,
        (ARRAY[177, 20, 93])[((n - 1) % 3) + 1]::numeric AS brd_id,
        CASE WHEN n = 1 THEN '52' ELSE '12' END AS status     -- 52 = Passport Snaffled -> Restricted
    FROM generate_series(1, 30) AS n
),
historical_animals AS (
    SELECT
        n,
        930000001::numeric,
        'historical'::text,
        'UK3245371' || (20000 + n - 1)::text,
        CASE WHEN n % 2 = 0 THEN 'F' ELSE 'M' END,
        DATE '2022-06-01' + ((n - 31) * 5),
        177::numeric,
        '12'
    FROM generate_series(31, 33) AS n
),
dead_animal AS (
    SELECT 34, 930000001::numeric, 'dead'::text, 'UK324537113250', 'F', DATE '2022-02-01', 177::numeric, '48'
),
other_holding AS (
    SELECT
        n,
        930000002::numeric,
        'current'::text,
        'UK3245379' || (30000 + n)::text,
        CASE WHEN n % 2 = 0 THEN 'F' ELSE 'M' END,
        DATE '2023-05-01' + ((n - 101) * 5),
        177::numeric,
        '12'
    FROM generate_series(101, 105) AS n
)
SELECT * FROM current_animals
UNION ALL SELECT * FROM historical_animals
UNION ALL SELECT * FROM dead_animal
UNION ALL SELECT * FROM other_holding;
 
-- # Registered animals (movement links are filled in below, once the movements exist)
INSERT INTO cts.ct_registered_animals (
    ran_id, ran_current_user, ran_current_status, ran_current_modified_date,
    ran_cts_indicator, ran_passport_or_licence, ran_sex, ran_birth_date,
    ran_brd_id, ran_loc_id_passport, ran_version, row_number
)
SELECT
    931000000 + n, 'seed', status, '2026-04-23',
    'Y', 'P', sex, birth_date,
    brd_id, loc_id, 1, n
FROM seed_animals;
 
-- # Registered movements
-- ON / registration (direction '1'): movement date = date of birth, received 4 days later
INSERT INTO cts.ct_registered_movements (
    mov_id, mov_current_user, mov_current_status, mov_current_modified_date,
    mov_ran_id, mov_loc_id, mov_movement_type, mov_direction, mov_movement_date,
    mov_movement_received_date, mov_version_creation_date, mov_reported_eartag,
    mov_version, row_number
)
SELECT
    932000000 + n, 'seed', '1', '2026-04-23',
    931000000 + n, loc_id, 'BI', '1', birth_date,
    birth_date + 4, birth_date + 4, eartag,
    1, n
FROM seed_animals;
 
-- OFF (direction '2') for the historical animals: moved off the holding 2023-01-15
INSERT INTO cts.ct_registered_movements (
    mov_id, mov_current_user, mov_current_status, mov_current_modified_date,
    mov_ran_id, mov_loc_id, mov_movement_type, mov_direction, mov_movement_date,
    mov_movement_received_date, mov_version_creation_date, mov_reported_eartag,
    mov_version, row_number
)
SELECT
    932500000 + n, 'seed', '1', '2026-04-23',
    931000000 + n, loc_id, 'OF', '2', DATE '2023-01-15',
    DATE '2023-01-16', DATE '2023-01-16', eartag,
    1, n
FROM seed_animals
WHERE kind = 'historical';
 
-- DEATH (direction '2') for the dead animal: 2023-02-01
INSERT INTO cts.ct_registered_movements (
    mov_id, mov_current_user, mov_current_status, mov_current_modified_date,
    mov_ran_id, mov_loc_id, mov_movement_type, mov_direction, mov_movement_date,
    mov_movement_received_date, mov_version_creation_date, mov_reported_eartag,
    mov_version, row_number
)
SELECT
    932600000 + n, 'seed', '1', '2026-04-23',
    931000000 + n, loc_id, 'DE', '2', DATE '2023-02-01',
    DATE '2023-02-02', DATE '2023-02-02', eartag,
    1, n
FROM seed_animals
WHERE kind = 'dead';
 
-- Link animals to their registration movement (and death movement where relevant)
UPDATE cts.ct_registered_animals ran
SET ran_mov_id_registration = 932000000 + s.n,
    ran_mov_id_death = CASE WHEN s.kind = 'dead' THEN 932600000 + s.n END
FROM seed_animals s
WHERE ran.ran_id = 931000000 + s.n;
 
-- # Animal identifiers (current ear tag per animal)
INSERT INTO cts.ct_animal_identifiers (
    aid_id, aid_identifier, aid_identifier_type, aid_effective_from_date,
    aid_effective_to_date, aid_loc_id_assigned, aid_current_flag, aid_ran_id,
    aid_current_user, aid_current_status, aid_current_modified_date,
    aid_version, row_number
)
SELECT
    933000000 + n, eartag, 'ET', birth_date,
    NULL, loc_id, 'Y', 931000000 + n,
    'seed', '1', '2026-04-23',
    1, n
FROM seed_animals;
 
-- # Parentage for UK324537113234 (n = 1): genetic dam and sire, held by identifier only
INSERT INTO cts.ct_animal_relationships (
    aar_id, aar_rel_type, aar_loc_id, aar_effective_from_date, aar_effective_to_date,
    aar_ran_id_child, aar_ran_id_parent, aar_parent_identifier, aar_parent_identifier_type,
    aar_current_user, aar_current_status, aar_current_modified_date, aar_version, row_number
) VALUES
    (934000001, 'G', 930000001, '2023-03-10', NULL, 931000001, NULL, 'UK324537113243', 'ET', 'seed', '1', '2026-04-23', 1, 1),
    (934000002, 'S', 930000001, '2023-03-10', NULL, 931000001, NULL, 'UK324537110987', 'ET', 'seed', '1', '2026-04-23', 1, 2);
 
DROP TABLE seed_animals;
