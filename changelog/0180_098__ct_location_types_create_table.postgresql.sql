-- liquibase formatted sql

-- changeset codex:0180-098

create table if not exists _ct_location_types
(
    lty_cii_location_type     varchar(1),
    lty_ownership             varchar(2),
    lty_current_status        varchar(2),
    lty_current_modified_date date,
    lty_current_user          varchar(10),
    lty_current_pid           numeric(3),
    lty_version               numeric(6),
    lty_id                    numeric(12) not null
        primary key,
    lty_loc_type              varchar(2),
    lty_lif_id                numeric(12)
        constraint fk_ct_location_types_lty_lif_id
            references _ct_location_id_formats,
    lty_location_type_reqd    numeric(1),
    lty_short_description     varchar(20),
    lty_subloc_type_reqd      numeric(1),
    lty_long_description      varchar(60),
    lty_premises_group        varchar(2),
    lty_hier_link_permitted   varchar(1),
    lty_movement_loc_ind      varchar(1),
    lty_peer_link_permitted   varchar(1),
    lty_perform_anomaly_check varchar(1),
    row_number                numeric
);
