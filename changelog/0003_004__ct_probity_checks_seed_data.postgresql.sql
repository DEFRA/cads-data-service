-- liquibase formatted sql

-- changeset dev_db_build:0003-004 splitStatements:false
INSERT INTO _ct_probity_checks (
    pch_id,
    pch_long_description,
    pch_short_description,
    pch_checked_to_date,
    pch_check_period,
    pch_next_check_date,
    pch_current_user,
    pch_current_status,
    pch_current_modified_date,
    pch_current_pid,
    pch_version,
    row_number,
    record_type,
    record_count,
    imported_date
) VALUES
    (1, 'Standard', 'Standard', DATE '1998-09-10', 30, DATE '1998-12-17', 'f800705', '1', DATE '1998-11-03', 69, 1, 1, 'D', 1, CURRENT_TIMESTAMP)
;
