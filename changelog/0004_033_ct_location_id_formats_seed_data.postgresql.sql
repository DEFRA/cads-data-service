-- liquibase formatted sql

-- changeset dev_db_build:0003-013 splitStatements:false
INSERT INTO cts.ct_location_id_formats (
    lif_version,
    lif_id,
    lif_subloc_type_reqd,
    lif_description,
    lif_loc_type_reqd,
    lif_format_pattern,
    lif_current_user,
    lif_current_status,
    lif_current_modified_date,
    lif_current_pid,
    row_number,
    record_type,
    record_count,
    imported_date
) VALUES
    (1, 1, '0', 'CPH format', '1', '99/999/9999', '1', '1', DATE '1997-01-01', 33, 1, 'D', 1, CURRENT_TIMESTAMP),
    (1, 2, '0', 'Meat Hygiene No./Priv. res fmt', '1', '9999', '1', '1', DATE '1997-01-01', 33, 2, 'D', 2, CURRENT_TIMESTAMP),
    (1, 3, '2', 'letter plus CPH', '2', 'X999999999', 'C900000', '1', DATE '1997-12-10', 33, 3, 'D', 3, CURRENT_TIMESTAMP),
    (1, 4, '2', 'Collection Centre format', '0', 'XXXXX', 'x901190', '1', DATE '1998-07-24', 33, 4, 'D', 4, CURRENT_TIMESTAMP),
    (1, 5, '2', 'Agency', '1', '9999999', 'x905756', '1', DATE '2005-10-19', NULL, 5, 'D', 5, CURRENT_TIMESTAMP)
;
