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
	, import_end_at
    , batch_date
    , group_key
    , import_type
    , failed_attempts
    , last_error_reason)
	VALUES
        -- Fixed scenarios
        ('dtn', 'CTSM_CADS_PROD_BULK_ABC_0001_CT_PARTIES_2026-01-01-012345', 100, NOW(), 1, 1, 0, NULL, NULL, NOW(), 'ABC', 'BULK', 0, NULL),
        ('dtn', 'CTSM_CADS_PROD_BULK_ABC_0002_CT_PARTIES_2026-01-01-012345', 100, NOW(), 2, 1, 0, NOW(), NULL, NOW(), 'ABC', 'BULK', 0, NULL),
        ('dtn', 'CTSM_CADS_PROD_BULK_ABC_0003_CT_PARTIES_2026-01-01-012345', 100, NOW(), 3, 1, 0, NOW(), NULL, NOW(), 'ABC', 'BULK', 0, NULL),
        ('dtn', 'CTSM_CADS_PROD_BULK_ABC_0004_CT_PARTIES_2026-01-01-012345', 100, NOW(), 4, 1, 0, NOW(), NOW(), NOW(), 'ABC', 'BULK', 0, NULL),
        ('dtn', 'CTSM_CADS_PROD_BULK_ABC_0005_CT_PARTIES_2026-01-01-012345', 100, NOW(), 5, 1, 0, NOW(), NOW(), NOW(), 'ABC', 'BULK', 1, 'import failed'),
        -- Mutable scenarios
        ('dtn', 'CTSM_CADS_PROD_BULK_ABC_0007_CT_PARTIES_2026-01-01-012345', 100, NOW(), 1, 1, 0, NULL, NULL, NOW(), 'ABC', 'BULK', 0, NULL),
        ('dtn', 'CTSM_CADS_PROD_BULK_ABC_0008_CT_PARTIES_2026-01-01-012345', 100, NOW(), 2, 1, 0, NOW(), NULL, NOW(), 'ABC', 'BULK', 0, NULL),
        ('dtn', 'CTSM_CADS_PROD_BULK_ABC_0009_CT_PARTIES_2026-01-01-012345', 100, NOW(), 3, 1, 0, NOW(), NULL, NOW(), 'ABC', 'BULK', 0, NULL),
        ('dtn', 'CTSM_CADS_PROD_BULK_ABC_0010_CT_PARTIES_2026-01-01-012345', 100, NOW(), 2, 1, 0, NOW(), NULL, NOW(), 'ABC', 'BULK', 0, NULL),
        ('dtn', 'CTSM_CADS_PROD_BULK_ABC_0011_CT_PARTIES_2026-01-01-012345', 100, NOW(), 5, 1, 0, NOW(), NOW(), NOW(), 'ABC', 'BULK', 1, 'import failed'),
        ('dtn', 'CTSM_CADS_PROD_BULK_ABC_0012_CT_PARTIES_2026-01-01-012345', 100, NOW(), 1, 1, 0, NULL, NULL, NOW(), 'ABC', 'BULK', 0, NULL),
        ('dtn', 'CTSM_CADS_PROD_BULK_ABC_0013_CT_PARTIES_2026-01-01-012345', 100, NOW(), 2, 1, 0, NOW(), NULL, NOW(), 'ABC', 'BULK', 0, NULL),
        ('dtn', 'CTSM_CADS_PROD_BULK_ABC_0014_CT_PARTIES_2026-01-01-012345', 100, NOW(), 2, 1, 0, NOW(), NULL, NOW(), 'ABC', 'BULK', 0, NULL)
ON CONFLICT DO NOTHING;
