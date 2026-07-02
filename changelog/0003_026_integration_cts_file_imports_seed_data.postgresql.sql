-- liquibase formatted sql

-- changeset dev_db_build:0003-026-inttest-cts-file-imports splitStatements:false context:integration
INSERT INTO cads.cts_file_imports(
	destination_table_name
	, file_name
	, total_rows_to_process
	, added_at
	, import_status_id
	, processing_status_id
	, rows_found
	, import_start_at
	, import_end_at)
	VALUES 
		('dtn', 'CTSM_CLA_PROD_BULK_ABC_CT_PARTIES_2026-01-01-012345-part1', 100, NOW(), 1, 1, 0, NULL, NULL),
		('dtn', 'CTSM_CLA_PROD_BULK_ABC_CT_PARTIES_2026-01-01-012345-part2', 100, NOW(), 2, 1, 0, NOW(), NULL),
		('dtn', 'CTSM_CLA_PROD_BULK_ABC_CT_PARTIES_2026-01-01-012345-part3', 100, NOW(), 3, 1, 0, NOW(), NOW()),
		('dtn', 'CTSM_CLA_PROD_BULK_ABC_CT_PARTIES_2026-01-01-012345-part4', 100, NOW(), 4, 1, 0, NOW(), NOW()),
		('dtn', 'CTSM_CLA_PROD_BULK_ABC_CT_PARTIES_2026-01-01-012345-part6', 100, NOW(), 1, 1, 0, NULL, NULL),
		('dtn', 'CTSM_CLA_PROD_BULK_ABC_CT_PARTIES_2026-01-01-012345-part7', 100, NOW(), 2, 1, 0, NOW(), NULL),
		('dtn', 'CTSM_CLA_PROD_BULK_ABC_CT_PARTIES_2026-01-01-012345-part8', 100, NOW(), 2, 1, 0, NOW(), NULL),
		('dtn', 'CTSM_CLA_PROD_BULK_ABC_CT_PARTIES_2026-01-01-012345-part9', 100, NOW(), 4, 1, 0, NOW(), NOW())
    ON CONFLICT DO NOTHING;

