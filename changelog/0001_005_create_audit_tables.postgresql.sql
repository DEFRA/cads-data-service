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

create index if not exists cts_file_imports_destination_table_name_idx
    on cads.cts_file_imports (destination_table_name);
create index if not exists cts_file_imports_file_name_idx
    on cads.cts_file_imports (file_name);
create index if not exists cts_file_imports_import_status_id_idx
    on cads.cts_file_imports (import_status_id);
create index if not exists cts_file_imports_processing_status_id_idx
    on cads.cts_file_imports (processing_status_id);
create index if not exists cts_file_imports_log_file_import_id_idx
    on cads.cts_file_imports_log (cts_file_import_id);
create index if not exists cts_file_imports_log_logged_at_idx
    on cads.cts_file_imports_log (logged_at);

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

alter table cts_transactions.ct_addresses
    add constraint ct_addresses_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_animal_changes
    add constraint ct_animal_changes_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_animal_claims
    add constraint ct_animal_claims_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_animal_corr_summ_errors
    add constraint ct_animal_corr_summ_errors_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_animal_correct_summaries
    add constraint ct_animal_correct_summaries_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_animal_identifiers
    add constraint ct_animal_identifiers_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_animal_relationships
    add constraint ct_animal_relationships_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_animal_statuses
    add constraint ct_animal_statuses_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_applic_statuses
    add constraint ct_applic_statuses_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_application_late_days
    add constraint ct_application_late_days_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_cla_extract
    add constraint ct_cla_extract_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_cla_extract_detail
    add constraint ct_cla_extract_detail_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_cla_extract_dm
    add constraint ct_cla_extract_dm_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_cla_mini_detail
    add constraint ct_cla_mini_detail_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_cla_mini_extract
    add constraint ct_cla_mini_extract_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_cm_measures_results
    add constraint ct_cm_measures_results_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_comms_addresses
    add constraint ct_comms_addresses_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_condition_marker_errors
    add constraint ct_condition_marker_errors_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_condition_markers
    add constraint ct_condition_markers_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_cps167_report
    add constraint ct_cps167_report_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_cts_users
    add constraint ct_cts_users_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_eartag_staging
    add constraint ct_eartag_staging_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_eartags
    add constraint ct_eartags_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_electronic_identifiers
    add constraint ct_electronic_identifiers_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_email_log
    add constraint ct_email_log_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_ereport_files
    add constraint ct_ereport_files_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_ereport_load_messages
    add constraint ct_ereport_load_messages_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_ereport_locks
    add constraint ct_ereport_locks_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_ereport_process_messages
    add constraint ct_ereport_process_messages_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_ext_cetd_eartag
    add constraint ct_ext_cetd_eartag_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_insert_update_log
    add constraint ct_insert_update_log_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_issued_documents
    add constraint ct_issued_documents_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_label_requests
    add constraint ct_label_requests_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_label_summaries
    add constraint ct_label_summaries_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_letters
    add constraint ct_letters_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_location_identifiers
    add constraint ct_location_identifiers_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_location_party_rels
    add constraint ct_location_party_rels_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_location_relationships
    add constraint ct_location_relationships_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_locations
    add constraint ct_locations_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_locations_faker
    add constraint ct_locations_faker_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_locrestrictionstoanimals
    add constraint ct_locrestrictionstoanimals_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_mgt_control_errors
    add constraint ct_mgt_control_errors_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_mhs_to_cph
    add constraint ct_mhs_to_cph_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_mov_hst
    add constraint ct_mov_hst_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_movt_corr_summ_errors
    add constraint ct_movt_corr_summ_errors_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_movt_correct_summaries
    add constraint ct_movt_correct_summaries_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_parties
    add constraint ct_parties_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_parties_faker
    add constraint ct_parties_faker_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_ppaf_groupings
    add constraint ct_ppaf_groupings_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_preprinted_appn_forms
    add constraint ct_preprinted_appn_forms_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_ps9999_ahdb_data
    add constraint ct_ps9999_ahdb_data_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_ps9999_ahdb_mov_history
    add constraint ct_ps9999_ahdb_mov_history_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_recd_application_errors
    add constraint ct_recd_application_errors_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_recd_movement_errors
    add constraint ct_recd_movement_errors_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_received_applications
    add constraint ct_received_applications_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_received_movements
    add constraint ct_received_movements_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_registered_animals
    add constraint ct_registered_animals_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_registered_movements
    add constraint ct_registered_movements_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_reset_to_extract
    add constraint ct_reset_to_extract_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_sbcs_ext
    add constraint ct_sbcs_ext_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_stage_files
    add constraint ct_stage_files_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_stage_locks
    add constraint ct_stage_locks_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_stage_messages
    add constraint ct_stage_messages_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_susp_animal_errors
    add constraint ct_susp_animal_errors_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_susp_cm_measure_results
    add constraint ct_susp_cm_measure_results_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_susp_condition_markers
    add constraint ct_susp_condition_markers_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_susp_movement_errors
    add constraint ct_susp_movement_errors_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_suspended_animals
    add constraint ct_suspended_animals_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_suspended_movements
    add constraint ct_suspended_movements_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_valid_applications
    add constraint ct_valid_applications_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_web_users
    add constraint ct_web_users_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_wg_autoallocations
    add constraint ct_wg_autoallocations_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_wg_super_assignments
    add constraint ct_wg_super_assignments_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_wg_user_assignments
    add constraint ct_wg_user_assignments_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;

alter table cts_transactions.ct_workgroups
    add constraint ct_workgroups_cts_file_import_id_fkey
        foreign key (cts_file_import_id) references cads.cts_file_imports;
