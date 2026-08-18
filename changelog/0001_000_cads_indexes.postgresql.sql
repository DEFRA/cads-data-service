-- liquibase formatted sql

-- changeset schema:0001-000-cads-indexes splitStatements:false
create index if not exists idx_party_location_identifier
    on cads.party (location_identifier);
create index if not exists idx_party_haulier_party_number
    on cads.party_haulier (party_number);
create index if not exists idx_location_country_code
    on cads.location (country_code);
create index if not exists idx_location_postcode
    on cads.location (postcode);
create index if not exists idx_location_site_type
    on cads.location_site (site_type);
create index if not exists idx_location_site_location_identifier
    on cads.location_site (location_identifier);
create index if not exists idx_location_site_source
    on cads.location_site (site_source);
create index if not exists idx_location_site_state
    on cads.location_site (state);
create index if not exists idx_location_site_party_identifier
    on cads.location_site_party (party_identifier);
create index if not exists idx_location_site_activity_site_identifier
    on cads.location_site_activity (site_identifier);
create index if not exists idx_location_associated_site_related_site
    on cads.location_associated_site (associated_site_identifier);
create index if not exists idx_animal_breed_species
    on cads.animal_breed (species);
create index if not exists idx_animal_collective_ref_site_identifier
    on cads.animal_collective_ref (site_identifier);
create index if not exists idx_animal_genotype_species
    on cads.animal_genotype (species);
create index if not exists idx_animal_death_reason_species
    on cads.animal_death_reason (species);
create index if not exists idx_animal_species_production_type_species
    on cads.animal_species_production_type (species);
create index if not exists idx_animal_species
    on cads.animal (species);
create index if not exists idx_animal_registration_site_identifier
    on cads.animal (registration_site_identifier);
create index if not exists idx_animal_mark_collective
    on cads.animal_mark (species, collective_site_identifier);
create index if not exists idx_animal_collective_current
    on cads.animal_collective (species, current_collective_site_identifier);
create index if not exists idx_animal_death_site
    on cads.animal_death (death_site_identifier);
create index if not exists idx_animal_party_party_identifier
    on cads.animal_party (party_identifier);
create index if not exists idx_animal_status_animal_identifier
    on cads.animal_status (animal_identifier);
create index if not exists idx_animal_lost_or_stolen_status_home_site_identifier
    on cads.animal_lost_or_stolen_status (home_site_identifier);
create index if not exists idx_animal_collective_registration_collective_ref
    on cads.animal_collective_registration (species, site_identifier);
create index if not exists idx_animal_collective_death_collective_ref
    on cads.animal_collective_death (species, site_identifier);
create index if not exists idx_animal_collective_party_party_identifier
    on cads.animal_collective_party (party_identifier);
create index if not exists idx_animal_notice_to_identify_site_identifier
    on cads.animal_notice_to_identify (site_identifier);
CREATE UNIQUE INDEX "mi_user_external_subject_normalized_key" ON cads."mi_user" USING btree("external_subject_normalized");
CREATE INDEX "mi_user_role_user_idx" ON cads."mi_user_role" USING btree("user_id");
CREATE INDEX "mi_user_role_role_idx" ON cads."mi_user_role" USING btree("role_id");
CREATE INDEX "mi_rrp_role_report_idx" ON cads."mi_role_report_permission" USING btree("role_id", "report_id");
CREATE INDEX "mi_rrp_permission_idx" ON cads."mi_role_report_permission" USING btree("permission_id");
CREATE INDEX "mi_rrp_report_idx" ON cads."mi_role_report_permission" USING btree("report_id");
CREATE INDEX "mi_rrp_report_permission_idx" ON cads."mi_role_report_permission" USING btree("report_id", "permission_id");
CREATE INDEX "mi_urp_user_report_idx" ON cads."mi_user_report_permission" USING btree("user_id", "report_id");
CREATE INDEX "mi_urp_report_idx" ON cads."mi_user_report_permission" USING btree("report_id");
CREATE INDEX "mi_urp_permission_idx" ON cads."mi_user_report_permission" USING btree("permission_id");
create index if not exists cts_file_imports_destination_table_name_idx
    on cads.cts_file_imports (destination_table_name);
create index if not exists cts_file_imports_import_status_id_idx
    on cads.cts_file_imports (import_status_id);
create index if not exists cts_file_imports_processing_status_id_idx
    on cads.cts_file_imports (processing_status_id);
create index if not exists cts_file_imports_log_file_import_id_idx
    on cads.cts_file_imports_log (cts_file_import_id);
create index if not exists cts_file_imports_log_logged_at_idx
    on cads.cts_file_imports_log (logged_at);
CREATE UNIQUE INDEX IF NOT EXISTS cts_file_imports_file_name_idx
    ON cads.cts_file_imports (file_name);
CREATE INDEX IF NOT EXISTS cts_file_imports_group_key_idx
    ON cads.cts_file_imports (group_key);
