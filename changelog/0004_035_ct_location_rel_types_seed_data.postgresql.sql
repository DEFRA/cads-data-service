-- liquibase formatted sql

-- changeset dev_db_build:0003-015 splitStatements:false
INSERT INTO cts.ct_location_rel_types (
    lrt_id,
    lrt_code,
    lrt_description,
    lrt_second_single_link,
    lrt_mandatory,
    lrt_gaps_allowed,
    lrt_primary_single_link,
    lrt_hierarchical_link,
    lrt_relship_text_down,
    lrt_relship_text_up,
    lrt_current_modified_date,
    lrt_current_status,
    lrt_current_user,
    lrt_current_pid,
    lrt_version,
    row_number,
    record_type,
    record_count,
    imported_date
) VALUES
    (47, 'AF', 'Agent For', 'N', 'N', 'Y', 'N', 'N', 'IS AGENT FOR', 'HAS AGENT', DATE '2005-10-19', '1', 'x905756', NULL, 1, 1, 'D', 1, CURRENT_TIMESTAMP),
    (1, 'SU', 'SUB-LOCATION', 'N', 'N', 'Y', 'N', 'Y', 'IS THE PARENT LOCATION FOR', 'IS A SUB-LOCATION OF', DATE '1999-03-30', '1', 'x903053', 37, 1, 2, 'D', 2, CURRENT_TIMESTAMP),
    (2, 'SF', 'SHARED/FAC', 'N', 'N', 'Y', 'N', 'N', 'SHARES THE FACILITIES OF', 'SHARES ITS FACILITIES WITH', DATE '1999-03-30', '1', 'x903053', 37, 1, 3, 'D', 3, CURRENT_TIMESTAMP),
    (3, 'AL', 'ADDITIONAL LAND', 'N', 'N', 'Y', 'N', 'N', 'USES LAND AT', 'HAS ITS LAND USED BY', DATE '1999-03-30', '1', 'x903053', NULL, 1, 4, 'D', 4, CURRENT_TIMESTAMP),
    (6, 'OW', 'OWNER', 'N', 'N', 'Y', 'Y', 'N', 'IS OWNED BY', 'IS THE OWNER OF', DATE '1999-03-30', '1', 'x903053', 37, 1, 5, 'D', 5, CURRENT_TIMESTAMP),
    (7, 'SP', 'SPLIT', 'Y', 'N', 'Y', 'N', 'N', 'PREVIOUSLY CONTAINED', 'WAS PART OF', DATE '1999-03-30', '1', 'x903053', NULL, 1, 6, 'D', 6, CURRENT_TIMESTAMP),
    (9, 'UL', 'Uses Land', 'N', 'N', 'Y', 'N', 'N', 'KEEPS CATTLE AT', 'CONTAINS CATTLE FROM', DATE '1999-03-30', '1', 'x903053', 37, 1, 7, 'D', 7, CURRENT_TIMESTAMP),
    (10, 'ME', 'MERGED', 'N', 'N', 'Y', 'Y', 'N', 'IS NOW PART OF', 'HAS SUBSUMED', DATE '1999-03-30', '1', 'x903053', NULL, 1, 8, 'D', 8, CURRENT_TIMESTAMP),
    (48, 'TH', 'Temp Holding', 'Y', 'N', 'Y', 'N', 'N', 'HAS A TEMP HOLDING AT', 'IS A TEMP HOLDING OF', DATE '2015-12-03', '1', 'x912716', NULL, 1, 9, 'D', 9, CURRENT_TIMESTAMP)
;
