-- liquibase formatted sql

-- changeset schema:0001-019-update-cts-file-imports-table-and-status-seed-data splitStatements:false

-- Update the cts_file_imports table to add a new column for import status
alter table cads.cts_file_imports
add column failed_attempts int default 0;

alter table cads.cts_file_imports
add column last_error_reason text;

-- Update the cts_file_import_status table to add a new status for failed imports
update cads.cts_file_import_statuses
set status_description =  'transferred'
where import_status_id  = 2;

update cads.cts_file_import_statuses
set status_description =  'split'
where import_status_id  = 3;

update cads.cts_file_import_statuses
set status_description =  'completed'
where import_status_id  = 4;

insert into cads.cts_file_import_statuses (import_status_id, status_description)
values (5, 'error')
on conflict (import_status_id) do update
    set status_description = excluded.status_description;