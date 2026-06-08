-- liquibase formatted sql

-- changeset dev_db_build:0003-014 splitStatements:false
INSERT INTO _ct_location_types (
    lty_cii_location_type,
    lty_ownership,
    lty_current_status,
    lty_current_modified_date,
    lty_current_user,
    lty_current_pid,
    lty_version,
    lty_id,
    lty_loc_type,
    lty_lif_id,
    lty_location_type_reqd,
    lty_short_description,
    lty_subloc_type_reqd,
    lty_long_description,
    lty_premises_group,
    lty_hier_link_permitted,
    lty_movement_loc_ind,
    lty_peer_link_permitted,
    lty_perform_anomaly_check,
    row_number,
    record_type,
    record_count,
    imported_date
) VALUES
    ('N', '1', '1', DATE '2005-10-19', 'x905756', 34, 1, 13, 'AG', 5, 1, 'Agency', 0, 'Agency', '4', 'Y', 'N', 'Y', 'N', 1, 'D', 1, CURRENT_TIMESTAMP),
    ('N', '1', '1', DATE '1999-10-22', 'x903052', 34, 1, 1, 'SH', 2, 1, 'S/HOUSE', 0, 'SLAUGHTER HOUSE', '2', 'Y', 'Y', 'Y', 'Y', 2, 'D', 2, CURRENT_TIMESTAMP),
    ('Y', '1', '1', DATE '2001-11-07', 'x902692', 34, 1, 2, 'AH', 1, 1, 'AG. HOLD', 0, 'AGRICULTURAL HOLDING', '1', 'Y', 'Y', 'Y', 'Y', 3, 'D', 3, CURRENT_TIMESTAMP),
    (NULL, '1', '1', DATE '1997-12-16', 'x902652', 34, 1, 3, 'CL', 1, 1, 'C/LAND', 0, 'COMMON LAND', '1', NULL, 'Y', NULL, 'Y', 4, 'D', 4, CURRENT_TIMESTAMP),
    ('N', '1', '1', DATE '2005-10-06', 'x905756', 34, 1, 4, 'CC', 4, 2, 'calf coll.', 2, 'Calf collection', '3', 'Y', 'Y', 'Y', 'Y', 5, 'D', 5, CURRENT_TIMESTAMP)
;
