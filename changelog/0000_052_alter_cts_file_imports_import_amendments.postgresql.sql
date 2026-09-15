-- liquibase formatted sql

-- changeset schema:0000-052-alter-cts-file-imports-amendments-made splitStatements:false
-- We need to flag if we have amended the filed during the import.
ALTER TABLE cads.cts_file_imports
ADD COLUMN amendments_made_flag BOOLEAN NOT NULL DEFAULT FALSE;
