-- liquibase formatted sql

-- changeset dev_db_build:0003-000 splitStatements:false
INSERT INTO _ct_sublocation_types (
    slt_id,
    slt_subloc_type,
    slt_short_description,
    slt_long_description,
    slt_peer_link_permitted,
    slt_hier_link_permitted,
    slt_movement_subloc_ind,
    slt_use_subloc_address,
    slt_current_user,
    slt_current_status,
    slt_current_modified_date,
    slt_current_pid,
    slt_version,
    row_number,
    record_type,
    record_count,
    imported_date
) VALUES
    (1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'dev_build', '1', DATE '2026-05-01', 1, 1, 1, 'D', 1, CURRENT_TIMESTAMP)
;
