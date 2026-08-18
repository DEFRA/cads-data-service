-- liquibase formatted sql

-- changeset dev_db_build:0003-018 splitStatements:false
INSERT INTO cts.ct_eartag_formats (
    etf_id,
    etf_description,
    etf_format_pattern,
    etf_max_input_length,
    etf_extra_chars_allowed,
    etf_current_user,
    etf_current_status,
    etf_current_modified_date,
    etf_current_pid,
    etf_version,
    row_number,
    record_type,
    record_count,
    imported_date
) VALUES
    (3, 'Pre-Barimo', 'XXXXXX XXXXX', 14, NULL, 'x901189', '1', DATE '1999-10-13', 1, 1, 1, 'D', 1, CURRENT_TIMESTAMP),
    (1, 'Numeric', 'AA-XXNNNNNNNNNN', 14, NULL, 'x903916', '1', DATE '1999-07-21', 1, 1, 2, 'D', 2, CURRENT_TIMESTAMP),
    (4, 'Free Text', 'XXXXXXXXXXXXXX', 14, NULL, 'x901187', '1', DATE '1999-05-13', 1, 1, 3, 'D', 3, CURRENT_TIMESTAMP),
    (2, 'Barimo', 'AAXXNNNN NNNNN', 14, NULL, 'x901187', '1', DATE '1999-05-13', 1, 1, 4, 'D', 4, CURRENT_TIMESTAMP)
;
