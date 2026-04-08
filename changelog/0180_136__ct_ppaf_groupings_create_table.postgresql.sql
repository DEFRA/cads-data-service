-- liquibase formatted sql

-- changeset codex:0180-136

create table if not exists _ct_ppaf_groupings
(
    ppg_id                    numeric(12) not null
        primary key,
    ppg_loc_id_birth          numeric(12)
        constraint fk_ct_ppaf_groupings_ppg_loc_id_birth
            references _ct_locations,
    ppg_loc_id_corres         numeric(12)
        constraint fk_ct_ppaf_groupings_ppg_loc_id_corres
            references _ct_locations,
    ppg_form_identifier       varchar(30),
    ppg_welsh_indicator       varchar(1),
    ppg_interface_filename    varchar(25),
    ppg_interface_txn_number  numeric(10),
    ppg_printing_date         date,
    ppg_ppaf_added_date       date,
    ppg_current_status        varchar(2),
    ppg_current_user          varchar(10),
    ppg_current_modified_date date,
    ppg_current_pid           numeric(3),
    ppg_version               numeric(6),
    ppg_corres_location_repd  varchar(17),
    ppg_birth_location_repd   varchar(17),
    row_number                numeric
);
