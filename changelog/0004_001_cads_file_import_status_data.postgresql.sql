-- liquibase formatted sql

-- changeset schema:0004-001-cads-file-import-status-data splitStatements:false
insert into cads.cts_file_import_statuses (import_status_id, status_description)
values
    (1, 'pending'),
    (2, 'processing'),
    (3, 'completed'),
    (4, 'error')
on conflict (import_status_id) do update
set status_description = excluded.status_description;

insert into cads.cts_file_processing_statuses (processing_status_id, status_description)
values
    (1, 'pending'),
    (2, 'processing'),
    (3, 'completed'),
    (4, 'error')
on conflict (processing_status_id) do update
set status_description = excluded.status_description;

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
