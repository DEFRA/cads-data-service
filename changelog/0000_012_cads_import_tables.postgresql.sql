-- liquibase formatted sql

-- changeset schema:0001-005-audit-tables splitStatements:false

create table if not exists cads.cts_file_import_statuses
(
    import_status_id smallint not null
        primary key,
    status_description text not null
        unique
);

create table if not exists cads.cts_file_processing_statuses
(
    processing_status_id smallint not null
        primary key,
    status_description text not null
        unique
);

create table if not exists cads.cts_file_imports
(
    cts_file_import_id bigint generated always as identity (
        start with 1
        increment by 1
        no minvalue
        no maxvalue
        cache 1
    ) not null
        primary key,
    destination_table_name text not null,
    file_name text not null,
    total_rows_to_process bigint not null,
    added_at timestamp with time zone default clock_timestamp() not null,
    import_status_id smallint default 1 not null
        constraint cts_file_imports_import_status_id_fkey
            references cads.cts_file_import_statuses,
    processing_status_id smallint default 1 not null
        constraint cts_file_imports_processing_status_id_fkey
            references cads.cts_file_processing_statuses,
    rows_found bigint default 0 not null,
    import_start_at timestamp with time zone,
    import_end_at timestamp with time zone,
    processing_start_at timestamp with time zone,
    processing_end_at timestamp with time zone,
    failed_attempts integer default 0,
    last_error_reason text,
    batch_date timestamp with time zone not null,
    group_key text not null,
    import_type text not null,
    constraint cts_file_imports_rows_found_check
        check (rows_found >= 0),
    constraint cts_file_imports_total_rows_to_process_check
        check (total_rows_to_process >= 0)
);

create table if not exists cads.cts_file_imports_log
(
    cts_file_import_log_id bigint generated always as identity (
        start with 1
        increment by 1
        no minvalue
        no maxvalue
        cache 1
    ) not null
        primary key,
    cts_file_import_id bigint not null
        constraint cts_file_imports_log_file_import_fkey
            references cads.cts_file_imports,
    log_level text default 'info'::text not null,
    log_message text not null,
    error_message text,
    expected_records bigint,
    processed_records bigint,
    inserted_records bigint default 0 not null,
    updated_records bigint default 0 not null,
    deleted_records bigint default 0 not null,
    processing_started_at timestamp with time zone,
    processing_ended_at timestamp with time zone,
    insert_started_at timestamp with time zone,
    insert_ended_at timestamp with time zone,
    update_started_at timestamp with time zone,
    update_ended_at timestamp with time zone,
    delete_started_at timestamp with time zone,
    delete_ended_at timestamp with time zone,
    logged_at timestamp with time zone default clock_timestamp() not null,
    constraint cts_file_imports_log_level_check
        check (log_level = any (array['info'::text, 'warning'::text, 'error'::text]))
);
