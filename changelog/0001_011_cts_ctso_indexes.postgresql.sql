-- liquibase formatted sql

-- changeset schema:0001-011-cts-ctso-indexes splitStatements:false
CREATE INDEX IF NOT EXISTS ct_breeds_brd_current_status_idx
    ON cts.ct_breeds (brd_current_status);
CREATE INDEX IF NOT EXISTS ct_breeds_brd_type_brd_code_idx
    ON cts.ct_breeds (brd_type, brd_code);
CREATE INDEX IF NOT EXISTS ct_countries_cry_current_status_idx
    ON cts.ct_countries (cry_current_status);
CREATE INDEX IF NOT EXISTS ct_locations_loc_current_status_idx
    ON cts.ct_locations (loc_current_status);
CREATE INDEX IF NOT EXISTS ct_location_types_lty_current_status_idx
    ON cts.ct_location_types (lty_current_status);
CREATE INDEX IF NOT EXISTS ct_schemes_sch_current_status_idx
    ON cts.ct_schemes (sch_current_status);
CREATE INDEX IF NOT EXISTS ct_registered_animals_ran_current_status_idx
    ON cts.ct_registered_animals (ran_current_status);
CREATE INDEX IF NOT EXISTS ct_animal_identifiers_aid_identifier_idx
    ON cts.ct_animal_identifiers (aid_identifier);
CREATE INDEX IF NOT EXISTS ct_animal_identifiers_aid_current_status_idx
    ON cts.ct_animal_identifiers (aid_current_status);
CREATE INDEX IF NOT EXISTS ct_registered_movements_mov_loc_id_idx
    ON cts.ct_registered_movements (mov_loc_id);
CREATE INDEX IF NOT EXISTS ct_registered_movements_mov_ran_id_idx
    ON cts.ct_registered_movements (mov_ran_id);
CREATE INDEX IF NOT EXISTS ct_registered_movements_mov_current_status_idx
    ON cts.ct_registered_movements (mov_current_status);
CREATE INDEX IF NOT EXISTS ct_registered_movements_mov_anomaly_code_idx
    ON cts.ct_registered_movements (mov_anomaly_code);
CREATE INDEX IF NOT EXISTS ct_registered_movements_mov_movement_type_idx
    ON cts.ct_registered_movements (mov_movement_type);
CREATE INDEX IF NOT EXISTS ct_registered_movements_mov_loc_id_anomaly_code_idx
    ON cts.ct_registered_movements (mov_loc_id, mov_anomaly_code);
CREATE INDEX IF NOT EXISTS ct_registered_movements_mov_loc_id_status_date_idx
    ON cts.ct_registered_movements (mov_loc_id, mov_current_status, mov_movement_date DESC);
CREATE INDEX IF NOT EXISTS ct_registered_movements_mov_loc_id_movement_type_idx
    ON cts.ct_registered_movements (mov_loc_id, mov_movement_type);
CREATE INDEX IF NOT EXISTS ct_animal_claims_anc_ran_id_scheme_year_idx
    ON cts.ct_animal_claims (anc_ran_id, anc_scheme_year);
CREATE INDEX IF NOT EXISTS ct_animal_claims_anc_ran_id_scheme_year_sequence_idx
    ON cts.ct_animal_claims (anc_ran_id, anc_scheme_year, anc_claim_sequence);
CREATE INDEX IF NOT EXISTS ct_param_header_phd_current_status_idx
    ON cts.ct_param_header (phd_current_status);
CREATE INDEX IF NOT EXISTS ct_param_value_pvl_param_idx
    ON cts.ct_param_value (pvl_param);
CREATE INDEX IF NOT EXISTS ct_param_value_pvl_current_status_idx
    ON cts.ct_param_value (pvl_current_status);
CREATE INDEX IF NOT EXISTS ct_param_group_pgp_current_status_idx
    ON cts.ct_param_group (pgp_current_status);
CREATE INDEX IF NOT EXISTS ct_param_group_pgp_param_idx
    ON cts.ct_param_group (pgp_param);
CREATE INDEX IF NOT EXISTS ct_param_group_pgp_param_group_value_idx
    ON cts.ct_param_group (pgp_param, pgp_group_value);
CREATE INDEX IF NOT EXISTS ct_param_value_group_pvg_param_idx
    ON cts.ct_param_value_group (pvg_param);
CREATE INDEX IF NOT EXISTS ct_param_value_group_pvg_current_status_idx
    ON cts.ct_param_value_group (pvg_current_status);
CREATE INDEX IF NOT EXISTS ct_param_value_group_pvg_param_value_group_idx
    ON cts.ct_param_value_group (pvg_param, pvg_param_value, pvg_group_value);
