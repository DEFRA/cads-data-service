-- liquibase formatted sql

-- changeset schema:0001-022-alter-cts-file-imports-table-with-indexed-group-key splitStatements:false

TRUNCATE TABLE cads.cts_file_imports CASCADE;

ALTER TABLE cads.cts_file_imports
    ALTER COLUMN group_key SET NOT NULL,
    ALTER COLUMN import_type SET NOT NULL,
    ALTER COLUMN batch_date SET NOT NULL;

-- Drop old group_key index
DROP INDEX IF EXISTS cads.cts_file_imports_group_key_idx;

-- Recreate group_key index as NON-UNIQUE
CREATE INDEX cts_file_imports_group_key_idx
    ON cads.cts_file_imports (group_key);

-- Drop old file_name index
DROP INDEX IF EXISTS cads.cts_file_imports_file_name_idx;

-- Create UNIQUE index on file_name
CREATE UNIQUE INDEX cts_file_imports_file_name_idx
    ON cads.cts_file_imports (file_name);