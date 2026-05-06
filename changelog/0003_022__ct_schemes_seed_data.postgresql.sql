-- liquibase formatted sql

-- changeset dev_db_build:0003-022 splitStatements:false
INSERT INTO _ct_schemes (
    sch_expiry_date,
    sch_short_description,
    sch_id,
    sch_current_status,
    sch_current_user,
    sch_current_modified_date,
    sch_current_pid,
    sch_scheme,
    sch_long_description,
    sch_version,
    row_number,
    record_type,
    record_count,
    imported_date
) VALUES
    (NULL, 'BSPS', 1, 'L', 'f803572', DATE '2004-09-05', 999, 'B', 'Beef Special Premium Scheme', NULL, 1, 'D', 1, CURRENT_TIMESTAMP)
;
