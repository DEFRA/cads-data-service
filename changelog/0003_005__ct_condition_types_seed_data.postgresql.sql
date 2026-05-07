-- liquibase formatted sql

-- changeset dev_db_build:0003-005 splitStatements:false
INSERT INTO _ct_condition_types (
    cot_id,
    cot_condition_type,
    cot_short_description,
    cot_effective_from_date,
    cot_long_description,
    cot_effective_to_date,
    cot_cessation_reason,
    cot_access_group,
    cot_current_user,
    cot_current_status,
    cot_current_modified_date,
    cot_current_pid,
    cot_version,
    row_number,
    record_type,
    record_count,
    imported_date
) VALUES
    (1, 'CIENF', 'CI Enforcement', DATE '2001-06-20', 'Cattle Identification Enforcement', NULL, NULL, 'CPS', 'f800702', '1', DATE '2001-06-29', 66, 1, 1, 'D', 1, CURRENT_TIMESTAMP),
    (2, 'AHVG', 'AHVG Condition', DATE '2001-04-26', 'AHVG Condition', NULL, NULL, 'BDA', 'x902926', '1', DATE '2001-04-26', 40, 1, 2, 'D', 2, CURRENT_TIMESTAMP),
    (3, 'CII', 'CII', DATE '1998-01-01', 'Cattle Identification Inspections', NULL, NULL, 'ADM', 'f801344', '1', DATE '1999-01-28', 66, 1, 3, 'D', 3, CURRENT_TIMESTAMP),
    (4, 'ENF', 'Enf Referral', DATE '1999-01-01', 'Enforcement Referral', NULL, NULL, 'ADM', 'f800702', '1', DATE '2000-01-18', 66, 1, 4, 'D', 4, CURRENT_TIMESTAMP),
    (5, 'M_ANO', 'Movem''t Anomaly', DATE '2001-01-01', 'Movement Anomaly', NULL, NULL, 'CPS', 'f800702', '1', DATE '2001-09-19', 66, 1, 5, 'D', 5, CURRENT_TIMESTAMP),
    (6, 'SDET', 'SDET', DATE '1998-01-01', 'Scottish Dam Upload', NULL, NULL, 'CPS', 'x903052', '1', DATE '1999-03-09', 66, 1, 6, 'D', 6, CURRENT_TIMESTAMP),
    (7, 'ADMIN', 'Administration', DATE '1999-09-01', 'Administration', NULL, NULL, 'CPS', 'f800702', '1', DATE '1999-11-16', 66, 1, 7, 'D', 7, CURRENT_TIMESTAMP),
    (8, 'DIS', 'Disease', DATE '1991-01-01', 'Disease Notification', NULL, NULL, 'ADM', 'f801344', '1', DATE '1999-01-28', 66, 1, 8, 'D', 8, CURRENT_TIMESTAMP),
    (9, 'CHR', 'CHR', DATE '2000-05-01', 'Cattle Herd Registration', NULL, NULL, 'CPS', 'x903124', '1', DATE '2000-05-15', 66, 1, 9, 'D', 9, CURRENT_TIMESTAMP),
    (10, 'AH', 'Animal Health', DATE '2001-11-01', 'Animal Health', NULL, NULL, NULL, 'x905756', '1', DATE '2001-11-22', 66, 1, 10, 'D', 10, CURRENT_TIMESTAMP),
    (11, 'EXNTF', 'Ext Notificatn', DATE '2003-08-13', 'External Notification', NULL, NULL, NULL, 'f800702', '1', DATE '2003-08-22', 66, 1, 11, 'D', 11, CURRENT_TIMESTAMP)
;
