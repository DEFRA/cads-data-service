-- liquibase formatted sql

-- changeset gary:0001_010_01 runInTransaction:false

CREATE INDEX CONCURRENTLY IF NOT EXISTS ix_ct_location_identifiers_cph ON cts.ct_location_identifiers USING btree (lid_identifier, lid_current_status, lid_effective_to_date, lid_effective_from_date DESC, lid_id DESC) INCLUDE (lid_loc_id);
-- rollback DROP INDEX CONCURRENTLY IF EXISTS cts.ix_ct_location_identifiers_cph;

-- changeset gary:0001_010_02 runInTransaction:false
CREATE INDEX CONCURRENTLY IF NOT EXISTS ix_ct_registered_movements_location ON cts.ct_registered_movements USING btree (mov_loc_id, mov_direction, mov_ran_id) WHERE (((mov_current_status)::text <> 'C'::text) AND (mov_ran_id IS NOT NULL) AND (mov_loc_id IS NOT NULL));
-- rollback DROP INDEX CONCURRENTLY IF EXISTS cts.ix_ct_registered_movements_location;

-- changeset gary:0001_010_03 runInTransaction:false
CREATE INDEX CONCURRENTLY IF NOT EXISTS ix_ct_registered_movements_animal_latest ON cts.ct_registered_movements USING btree (mov_ran_id, mov_movement_date DESC NULLS LAST, mov_version_creation_date DESC NULLS LAST, mov_id DESC) INCLUDE (mov_loc_id, mov_direction, mov_reported_eartag) WHERE (((mov_current_status)::text <> 'C'::text) AND (mov_ran_id IS NOT NULL));
-- rollback DROP INDEX CONCURRENTLY IF EXISTS cts.ix_ct_registered_movements_animal_latest;

-- changeset gary:0001_010_04 runInTransaction:false
CREATE INDEX CONCURRENTLY IF NOT EXISTS ix_ct_condition_markers_active_animal ON cts.ct_condition_markers USING btree (com_ran_id, com_effective_to_date, com_effective_from_date) INCLUDE (com_cov_id, com_cac_id, com_comments) WHERE (((com_current_status)::text = '1'::text) AND (com_ran_id IS NOT NULL));
-- rollback DROP INDEX CONCURRENTLY IF EXISTS cts.ix_ct_condition_markers_active_animal;

-- changeset gary:0001_010_05 runInTransaction:false
CREATE INDEX CONCURRENTLY IF NOT EXISTS ix_ct_location_party_rels_current_location ON cts.ct_location_party_rels USING btree (lpr_loc_id, lpr_lpt_id, lpr_current_status, lpr_effective_to_date, lpr_effective_from_date DESC, lpr_id DESC) INCLUDE (lpr_par_id);
-- rollback DROP INDEX CONCURRENTLY IF EXISTS cts.ix_ct_location_party_rels_current_location;

-- changeset gary:0001_010_06 runInTransaction:false
CREATE INDEX CONCURRENTLY IF NOT EXISTS ix_ct_addresses_location ON cts.ct_addresses USING btree (adr_loc_id, adr_current_modified_date DESC NULLS LAST, adr_id DESC) WHERE (adr_loc_id IS NOT NULL);
-- rollback DROP INDEX CONCURRENTLY IF EXISTS cts.ix_ct_addresses_location;

-- changeset gary:0001_010_07 runInTransaction:false
CREATE INDEX CONCURRENTLY IF NOT EXISTS ix_ct_addresses_party ON cts.ct_addresses USING btree (adr_par_id, adr_current_modified_date DESC NULLS LAST, adr_id DESC) WHERE (adr_par_id IS NOT NULL);
-- rollback DROP INDEX CONCURRENTLY IF EXISTS cts.ix_ct_addresses_party;

-- changeset gary:0001_010_08 runInTransaction:false
CREATE INDEX CONCURRENTLY IF NOT EXISTS ix_ct_param_value_lookup ON cts.ct_param_value USING btree (pvl_param, pvl_param_value);
-- rollback DROP INDEX CONCURRENTLY IF EXISTS cts.ix_ct_param_value_lookup;

-- changeset gary:0001_010_09 runInTransaction:false
CREATE INDEX CONCURRENTLY IF NOT EXISTS ix_ct_location_party_rel_types_code ON cts.ct_location_party_rel_types USING btree (lpt_code);
-- rollback DROP INDEX CONCURRENTLY IF EXISTS cts.ix_ct_location_party_rel_types_code;
