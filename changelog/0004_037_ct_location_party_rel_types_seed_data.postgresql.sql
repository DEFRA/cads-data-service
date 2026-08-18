-- liquibase formatted sql

-- changeset dev_db_build:0003-017 splitStatements:false
INSERT INTO cts.ct_location_party_rel_types (
    lpt_id,
    lpt_code,
    lpt_description,
    lpt_gaps_allowed,
    lpt_mandatory,
    lpt_primary_single_link,
    lpt_second_single_link,
    lpt_hierarchical_link,
    lpt_relship_text_down,
    lpt_relship_text_up,
    lpt_current_user,
    lpt_current_status,
    lpt_current_modified_date,
    lpt_current_pid,
    lpt_version,
    row_number,
    record_type,
    record_count,
    imported_date
) VALUES
    (4, 'KN', 'KEEPER NAME', 'N', 'Y', 'Y', 'Y', 'Y', 'HAS THE KEEPER AT', 'IS THE KEEPER FOR', 'f800702', '1', DATE '1999-04-15', 37, 1, 1, 'D', 1, CURRENT_TIMESTAMP),
    (5, 'CA', 'CORRES. ADDRESS', 'Y', 'N', 'Y', 'Y', 'Y', 'HAS CORRESPONDENCE SENT TO', 'IS THE CORRES. ADDRESS FOR', 'f800702', '1', DATE '1999-04-15', NULL, 1, 2, 'D', 2, CURRENT_TIMESTAMP),
    (8, 'CO', 'CONTACT (OTHER)', 'Y', 'N', 'N', 'N', 'Y', 'HAS ALTERNATIVE CONTACT ADDR.', 'IS ANOTHER CONTACT ADDRESS FOR', 'f800702', '1', DATE '1999-04-15', 37, 1, 3, 'D', 3, CURRENT_TIMESTAMP)
;
