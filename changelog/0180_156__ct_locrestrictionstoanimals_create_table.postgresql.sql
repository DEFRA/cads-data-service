-- liquibase formatted sql

-- changeset codex:0180-156

create table if not exists _ct_locrestrictionstoanimals
(
    lra_com_id             numeric(12)
        constraint fk_ct_locrestrictionstoanimals_lra_com_id
            references _ct_condition_markers,
    lra_last_probity_date  date,
    lra_com_effective_from date,
    lra_com_effective_to   date,
    lra_loc_id             numeric(12)
        constraint fk_ct_locrestrictionstoanimals_lra_loc_id
            references _ct_locations,
    lra_ran_id             numeric(12)
        constraint fk_ct_locrestrictionstoanimals_lra_ran_id
            references _ct_registered_animals,
    row_number             numeric
);
