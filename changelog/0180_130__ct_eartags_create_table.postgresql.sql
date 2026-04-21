-- liquibase formatted sql

-- changeset codex:0180-130

create table if not exists _ct_eartags
(
    etg_id                      numeric(12) not null
        primary key,
    etg_ett_id                  numeric(12)
        constraint fk_ct_eartags_etg_ett_id
            references _ct_eartag_types,
    etg_erf_id                  numeric(12)
        constraint fk_ct_eartags_etg_erf_id
            references _ct_eartag_reason_flags,
    etg_eartag                  varchar(20),
    etg_usage_code              varchar(2),
    etg_eartag_authority        varchar(2),
    etg_source                  varchar(2),
    etg_identifier_availability varchar(2),
    etg_species                 varchar(240),
    etg_fuzzy_eartag_1          varchar(20),
    etg_fuzzy_eartag_2          varchar(20),
    etg_eartag_defra_format     varchar(20),
    etg_type_defra_format       varchar(10),
    etg_current_user            varchar(10),
    etg_current_modified_date   date,
    etg_current_status          varchar(2),
    etg_current_pid             numeric(3),
    etg_version                 numeric(6),
    etg_loc_id_order            numeric(12)
        constraint fk_ct_eartags_etg_loc_id_order
            references _ct_locations,
    etg_order_location_repd     varchar(17),
    etg_ppaf_indicator          char,
    row_number                  numeric
);
