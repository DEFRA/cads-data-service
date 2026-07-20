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
        ('dtn', 'CTSM_CADS_PROD_BULK_ABC_0001_CT_PARTIES_2026-01-01-012345', 100, NOW(), 1, 1, 0, NULL, NULL),
        ('dtn', 'CTSM_CADS_PROD_BULK_ABC_1001_CT_PARTIES_2026-01-01-012345', 100, NOW(), 1, 1, 0, NULL, NULL),
        ('dtn', 'CTSM_CADS_PROD_BULK_ABC_0002_CT_PARTIES_2026-01-01-012345', 100, NOW(), 2, 1, 0, NOW(), NULL),
        ('dtn', 'CTSM_CADS_PROD_BULK_ABC_1002_CT_PARTIES_2026-01-01-012345', 100, NOW(), 2, 1, 0, NOW(), NULL),
		('dtn', 'CTSM_CADS_PROD_BULK_ABC_0003_CT_PARTIES_2026-01-01-012345', 100, NOW(), 3, 1, 0, NOW(), NOW()),
        ('dtn', 'CTSM_CADS_PROD_BULK_ABC_0004_CT_PARTIES_2026-01-01-012345', 100, NOW(), 4, 1, 0, NOW(), NOW()),
        ('dtn', 'CTSM_CADS_PROD_BULK_ABC_0005_CT_PARTIES_2026-01-01-012345', 100, NOW(), 5, 1, 0, NOW(), NOW()),
		('dtn', 'CTSM_CADS_PROD_BULK_ABC_0007_CT_PARTIES_2026-01-01-012345', 100, NOW(), 1, 1, 0, NOW(), NULL),
        ('dtn', 'CTSM_CADS_PROD_BULK_ABC_0008_CT_PARTIES_2026-01-01-012345', 100, NOW(), 2, 1, 0, NOW(), NULL),
        ('dtn', 'CTSM_CADS_PROD_BULK_ABC_0009_CT_PARTIES_2026-01-01-012345', 100, NOW(), 3, 1, 0, NOW(), NULL),
        ('dtn', 'CTSM_CADS_PROD_BULK_ABC_0010_CT_PARTIES_2026-01-01-012345', 100, NOW(), 1, 1, 0, NOW(), NULL),
        ('dtn', 'CTSM_CADS_PROD_BULK_ABC_0011_CT_PARTIES_2026-01-01-012345', 100, NOW(), 2, 1, 0, NOW(), NULL)
ON CONFLICT DO NOTHING;
