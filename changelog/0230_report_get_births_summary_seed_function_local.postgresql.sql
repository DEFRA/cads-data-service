-- liquibase formatted sql
    
--changeset andy:seed-births-by-date context:local
-- Seed data for the births-by-date aggregation report, modelled on
-- WL1131 - Cattle Registrations Jan to June 2024.xlsx.
INSERT INTO cts.ct_locations (
    loc_id,
    loc_cty_id,
    loc_effective_from,
    loc_current_status,
    loc_current_user,
    loc_current_modified_date,
    loc_version,
    row_number
) VALUES
      (910000001, 45, '2024-01-01', '1', 'seed', '2026-04-23', 1, 1),
      (910000002, 55, '2024-01-01', '1', 'seed', '2026-04-23', 1, 2),
      (910000003, 6, '2024-01-01', '1', 'seed', '2026-04-23', 1, 3),
      (910000004, 46, '2024-01-01', '1', 'seed', '2026-04-23', 1, 4),
      (910000005, 55, '2024-01-01', '1', 'seed', '2026-04-23', 1, 5),
      (910000006, 66, '2024-01-01', '1', 'seed', '2026-04-23', 1, 6),
      (910000007, 7, '2024-01-01', '1', 'seed', '2026-04-23', 1, 7),
      (910000008, 23, '2024-01-01', '1', 'seed', '2026-04-23', 1, 8),
      (910000009, 16, '2024-01-01', '1', 'seed', '2026-04-23', 1, 9),
      (910000010, 11, '2024-01-01', '1', 'seed', '2026-04-23', 1, 10),
      (910000011, 38, '2024-01-01', '1', 'seed', '2026-04-23', 1, 11),
      (910000012, 6, '2024-01-01', '1', 'seed', '2026-04-23', 1, 12),
      (910000013, 84, '2024-01-01', '1', 'seed', '2026-04-23', 1, 13),
      (910000014, 86, '2024-01-01', '1', 'seed', '2026-04-23', 1, 14),
      (910000015, 19, '2024-01-01', '1', 'seed', '2026-04-23', 1, 15);

INSERT INTO cts.ct_valid_applications (
    vap_id,
    vap_current_status,
    vap_current_user,
    vap_current_modified_date,
    vap_application_type,
    vap_receipt_date,
    vap_cts_indicator,
    vap_version,
    row_number
) VALUES (
             900000001,
             '1',
             'seed',
             '2026-04-23',
             'B',
             '2024-01-01',
             'Y',
             1,
             1
         );

WITH template_WL1131_Cattle_Registrations (
                                           group_id,
                                           birth_date,
                                           sex,
                                           number_of_births
    ) AS (
    VALUES
        (1, '2024-01-01'::date, 'F', 19),
        (2, '2024-01-01'::date, 'M', 4),
        (3, '2024-01-01'::date, 'M', 59),
        (4, '2024-02-01'::date, 'F', 80),
        (5, '2024-02-01'::date, 'F', 17),
        (6, '2024-02-01'::date, 'F', 2),
        (7, '2024-03-01'::date, 'F', 57),
        (8, '2024-03-01'::date, 'M', 218),
        (9, '2024-03-01'::date, 'F', 20),
        (10, '2024-04-01'::date, 'M', 17),
        (11, '2024-04-01'::date, 'M', 11),
        (12, '2024-04-01'::date, 'F', 178),
        (13, '2024-05-01'::date, 'F', 17),
        (14, '2024-05-01'::date, 'M', 35),
        (15, '2024-05-01'::date, 'F', 11)
)
INSERT INTO cts.ct_registered_animals (
    ran_id,
    ran_current_user,
    ran_current_status,
    ran_current_modified_date,
    ran_cts_indicator,
    ran_passport_or_licence,
    ran_sex,
    ran_birth_date,
    ran_brd_id,
    ran_loc_id_passport,
    ran_vap_id,
    ran_version,
    row_number
)
SELECT
    920000000 + (group_id * 1000) + generated_birth.birth_number,
    'seed',
    '1',
    '2026-04-23',
    'Y',
    'P',
    sex,
    birth_date,
    20,
    910000000 + group_id,
    900000001,
    1,
    (group_id * 1000) + generated_birth.birth_number
FROM template_WL1131_Cattle_Registrations
         CROSS JOIN LATERAL generate_series(1, number_of_births) AS generated_birth(birth_number);