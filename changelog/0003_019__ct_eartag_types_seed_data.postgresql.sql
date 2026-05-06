-- liquibase formatted sql

-- changeset dev_db_build:0003-019 splitStatements:false
INSERT INTO _ct_eartag_types (
    ett_id,
    ett_eartag_type,
    ett_cr_export,
    ett_description,
    ett_etf_id,
    ett_short_description,
    ett_current_user,
    ett_current_status,
    ett_current_modified_date,
    ett_current_pid,
    ett_version,
    row_number,
    record_type,
    record_count,
    imported_date
) VALUES
    (1, 'NT', 'Y', 'Numeric', 1, 'Numeric', 'x903916', '1', DATE '1999-07-19', 1, 1, 1, 'D', 1, CURRENT_TIMESTAMP),
    (2, 'FT', 'Y', 'Free Text', 4, 'Free Text', 'x901187', '1', DATE '1999-05-13', 1, 1, 2, 'D', 2, CURRENT_TIMESTAMP),
    (3, 'BA', 'Y', 'Barimo', 2, 'Barimo', 'x903916', '1', DATE '1999-07-19', 1, 1, 3, 'D', 3, CURRENT_TIMESTAMP),
    (4, 'PB', 'Y', 'Pre-Barimo', 3, 'Pre-Barimo', 'x901189', '1', DATE '1999-10-13', 1, 1, 4, 'D', 4, CURRENT_TIMESTAMP)
;
