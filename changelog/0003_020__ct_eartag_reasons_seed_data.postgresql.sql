-- liquibase formatted sql

-- changeset dev_db_build:0003-020 splitStatements:false
INSERT INTO _ct_eartag_reasons (
    etr_id,
    etr_eartag_reason_code,
    etr_reason_code_type,
    etr_short_description,
    etr_long_description,
    etr_current_status,
    etr_current_user,
    etr_current_modified_date,
    etr_current_pid,
    etr_version,
    row_number,
    record_type,
    record_count,
    imported_date
) VALUES
    (1, 'BC', 'A', 'BC Available', 'Ear Tag available for Back Capture', '1', NULL, NULL, NULL, 1, 1, 'D', 1, CURRENT_TIMESTAMP),
    (2, 'MD', 'N', 'Damaged - CTS', 'Ear Tag Cancelled via CTS - Damaged', '1', NULL, NULL, NULL, 1, 2, 'D', 2, CURRENT_TIMESTAMP),
    (3, 'M', 'N', 'Lost', 'Ear tag cancelled (Lost) - May reissue', '1', NULL, NULL, NULL, 1, 3, 'D', 3, CURRENT_TIMESTAMP),
    (4, 'US', 'N', 'Ear Tag Used', 'Ear Tag has been used', '1', NULL, NULL, NULL, 1, 4, 'D', 4, CURRENT_TIMESTAMP),
    (5, 'KE', 'N', 'Canc Keeper Err', 'Ear Tag cancelled - keeper error', '1', NULL, NULL, NULL, 1, 5, 'D', 5, CURRENT_TIMESTAMP),
    (6, 'AU', 'A', 'Ordered', 'Ear tag ordered', '1', 'x902791', DATE '2000-03-20', NULL, 1, 6, 'D', 6, CURRENT_TIMESTAMP),
    (7, 'CA', 'N', 'Canc Office Err', 'Ear Tag cancelled - office error', '1', NULL, NULL, NULL, 1, 7, 'D', 7, CURRENT_TIMESTAMP),
    (8, 'S', 'N', 'Stolen', 'Ear tag cancelled(Stolen) Cannot reissue', '1', NULL, NULL, NULL, 1, 8, 'D', 8, CURRENT_TIMESTAMP),
    (9, 'T', 'N', 'Stolen', 'Ear tag cancelled (Stolen) - May reissue', '1', NULL, NULL, NULL, 1, 9, 'D', 9, CURRENT_TIMESTAMP),
    (10, 'C', 'N', 'Canc User Error', 'Ear tag cancelled - May reissue', '1', NULL, NULL, NULL, 1, 10, 'D', 10, CURRENT_TIMESTAMP),
    (11, 'D', 'N', 'Damaged', 'Ear tag cancelled (Damaged) -May reissue', '1', NULL, NULL, NULL, 1, 11, 'D', 11, CURRENT_TIMESTAMP),
    (12, 'ML', 'N', 'Lost - CTS', 'Ear Tag Cancelled via CTS - Lost', '1', NULL, NULL, NULL, 1, 12, 'D', 12, CURRENT_TIMESTAMP),
    (13, 'E', 'N', 'HQ Issued Tag', 'Ear tag cancelled - Cannot reissue', '1', NULL, NULL, NULL, 1, 13, 'D', 13, CURRENT_TIMESTAMP),
    (14, 'F', 'N', 'HQ Issued Tag', 'Ear tag cancelled - May re-issue', '1', NULL, NULL, NULL, 1, 14, 'D', 14, CURRENT_TIMESTAMP),
    (15, 'Y', 'N', 'Other', 'Ear tag cancelled - Cannot reissue', '1', NULL, NULL, NULL, 1, 15, 'D', 15, CURRENT_TIMESTAMP),
    (16, 'Z', 'N', 'Other', 'Ear tag cancelled - May re-issue', '1', NULL, NULL, NULL, 1, 16, 'D', 16, CURRENT_TIMESTAMP),
    (17, 'MS', 'N', 'Stolen - CTS', 'Ear Tag Cancelled via CTS - Stolen', '1', NULL, NULL, NULL, 1, 17, 'D', 17, CURRENT_TIMESTAMP),
    (18, 'MC', 'N', 'Cancel - CTS', 'Ear Tag Cancelled via CTS', '1', NULL, NULL, NULL, 1, 18, 'D', 18, CURRENT_TIMESTAMP),
    (19, 'L', 'N', 'Lost', 'Ear tag cancelled (Lost) -Cannot reissue', '1', NULL, NULL, NULL, 1, 19, 'D', 19, CURRENT_TIMESTAMP)
;
