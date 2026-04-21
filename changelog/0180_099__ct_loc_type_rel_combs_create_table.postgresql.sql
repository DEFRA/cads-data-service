-- liquibase formatted sql

-- changeset codex:0180-099

create table if not exists _ct_loc_type_rel_combs
(
    lrc_id                    numeric(12) not null
        primary key,
    lrc_lty_id_1              numeric(12)
        constraint fk_ct_loc_type_rel_combs_lrc_lty_id_1
            references _ct_location_types,
    lrc_lty_id_2              numeric(12)
        constraint fk_ct_loc_type_rel_combs_lrc_lty_id_2
            references _ct_location_types,
    lrc_lrt_id                numeric(12)
        constraint fk_ct_loc_type_rel_combs_lrc_lrt_id
            references _ct_location_rel_types,
    lrc_current_user          varchar(10),
    lrc_current_modified_date date,
    lrc_current_status        varchar(2),
    lrc_current_pid           numeric(3),
    lrc_version               numeric(6),
    row_number                numeric
);
