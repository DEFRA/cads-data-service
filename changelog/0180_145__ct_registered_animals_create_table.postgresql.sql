-- liquibase formatted sql

-- changeset codex:0180-145

create table if not exists _ct_registered_animals
(
    ran_id                       numeric(12) not null
        primary key,
    ran_current_user             varchar(10),
    ran_current_status           varchar(2),
    ran_current_modified_date    date,
    ran_current_pid              numeric(3),
    ran_current_intended_action  varchar(2),
    ran_current_change_rcvd_date date,
    ran_current_traced_moves     numeric(4),
    ran_current_add_moves        numeric(4),
    ran_cts_indicator            varchar(1),
    ran_passport_or_licence      varchar(1),
    ran_sex                      varchar(1),
    ran_birth_date               date,
    ran_applic_line              numeric(2),
    ran_brd_id                   numeric(12)
        references _ct_breeds,
    ran_loc_id_passport          numeric(12)
        references _ct_locations,
    ran_vap_id                   numeric(12)
        references _ct_valid_applications,
    ran_mov_id_registration      numeric(12),
    ran_passport_mod_flag        char,
    ran_passport_version_number  varchar(3),
    ran_version                  numeric(6),
    ran_mov_id_death             numeric(12),
    ran_cry_id_chr_origin        numeric(12)
        references _ct_countries,
    ran_passport_location_repd   varchar(17),
    row_number                   numeric
);