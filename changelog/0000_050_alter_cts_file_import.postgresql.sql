-- liquibase formatted sql

-- changeset schema:0000-050-alter-cts-file-import splitStatements:false
ALTER TABLE cads.cts_file_imports 
ADD COLUMN rows_imported BIGINT NOT NULL DEFAULT 0,
ADD COLUMN last_file_part_imported TEXT;