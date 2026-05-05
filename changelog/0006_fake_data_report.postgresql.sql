-- liquibase formatted sql

-- changeset report:0006-fake-data-report splitStatements:false
DROP VIEW IF EXISTS report;

CREATE VIEW report AS
SELECT
    '_ct_eartag_reason_flags' AS table_name,
    COUNT(*) AS total_rows,
    SUM(CASE WHEN fake_data = 1 THEN 1 ELSE 0 END) AS fake_rows,
    SUM(CASE WHEN fake_data = 0 THEN 1 ELSE 0 END) AS real_rows
FROM _ct_eartag_reason_flags
UNION ALL
SELECT
    '_ct_locations' AS table_name,
    COUNT(*) AS total_rows,
    SUM(CASE WHEN fake_data = 1 THEN 1 ELSE 0 END) AS fake_rows,
    SUM(CASE WHEN fake_data = 0 THEN 1 ELSE 0 END) AS real_rows
FROM _ct_locations
UNION ALL
SELECT
    '_ct_valid_applications' AS table_name,
    COUNT(*) AS total_rows,
    SUM(CASE WHEN fake_data = 1 THEN 1 ELSE 0 END) AS fake_rows,
    SUM(CASE WHEN fake_data = 0 THEN 1 ELSE 0 END) AS real_rows
FROM _ct_valid_applications
UNION ALL
SELECT
    '_ct_registered_animals' AS table_name,
    COUNT(*) AS total_rows,
    SUM(CASE WHEN fake_data = 1 THEN 1 ELSE 0 END) AS fake_rows,
    SUM(CASE WHEN fake_data = 0 THEN 1 ELSE 0 END) AS real_rows
FROM _ct_registered_animals
UNION ALL
SELECT
    '_ct_registered_movements' AS table_name,
    COUNT(*) AS total_rows,
    SUM(CASE WHEN fake_data = 1 THEN 1 ELSE 0 END) AS fake_rows,
    SUM(CASE WHEN fake_data = 0 THEN 1 ELSE 0 END) AS real_rows
FROM _ct_registered_movements
UNION ALL
SELECT
    '_ct_condition_markers' AS table_name,
    COUNT(*) AS total_rows,
    SUM(CASE WHEN fake_data = 1 THEN 1 ELSE 0 END) AS fake_rows,
    SUM(CASE WHEN fake_data = 0 THEN 1 ELSE 0 END) AS real_rows
FROM _ct_condition_markers
UNION ALL
SELECT
    '_ct_workgroups' AS table_name,
    COUNT(*) AS total_rows,
    SUM(CASE WHEN fake_data = 1 THEN 1 ELSE 0 END) AS fake_rows,
    SUM(CASE WHEN fake_data = 0 THEN 1 ELSE 0 END) AS real_rows
FROM _ct_workgroups
;
