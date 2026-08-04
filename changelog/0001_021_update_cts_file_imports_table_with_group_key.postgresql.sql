-- liquibase formatted sql

-- changeset schema:0001-021-update-cts-file-imports-table-with-indexed-group-key splitStatements:false

-- Update the cts_file_imports table to add a new column for grouop key
alter table cads.cts_file_imports
add column group_key text,
add column import_type text,
add column batch_date timestamp with time zone;

create index if not exists cts_file_imports_group_key_idx
    on cads.cts_file_imports (group_key);