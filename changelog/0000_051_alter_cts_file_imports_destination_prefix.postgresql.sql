-- liquibase formatted sql

-- changeset schema:0000-051-alter-cts-file-imports-destination-prefix splitStatements:false
-- The internal bucket prefix the file was copied to (import/{data_source}/{type}, e.g. import/cts/bulk).
-- Existing rows pre-date the layout change and were landed in the flat 'import' folder.
ALTER TABLE cads.cts_file_imports
ADD COLUMN destination_prefix TEXT NOT NULL DEFAULT 'import';
