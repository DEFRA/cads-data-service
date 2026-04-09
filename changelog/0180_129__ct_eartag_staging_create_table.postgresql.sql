-- liquibase formatted sql

-- changeset codex:0180-129

create table if not exists _ct_eartag_staging
(
    est_id                      numeric(12) not null
        primary key,
    est_eartag                  varchar(20),
    est_usage_code              varchar(2),
    est_identifier_availability varchar(2),
    est_order_location_repd     varchar(17),
    est_loc_id_order            numeric(12)
        constraint fk_ct_eartag_staging_est_loc_id_order
            references _ct_locations,
    est_eartag_reason_code      varchar(2),
    est_erf_id                  numeric(12)
        constraint fk_ct_eartag_staging_est_erf_id
            references _ct_eartag_reason_flags,
    est_current_modified_date   date
);
