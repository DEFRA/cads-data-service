-- liquibase formatted sql

-- changeset codex:0180-127

create table if not exists _ct_locations
(
    loc_receive_ppaf_flag     varchar(1),
    loc_id                    numeric(12) not null
        primary key,
    loc_slt_id                numeric(12)
        constraint fk_ct_locations_loc_slt_id
            references _ct_sublocation_types,
    loc_lty_id                numeric(12)
        constraint fk_ct_locations_loc_lty_id
            references _ct_location_types,
    loc_cty_id                numeric(12)
        constraint fk_ct_locations_loc_cty_id
            references _ct_counties,
    loc_receive_labels_flag   varchar(1),
    loc_effective_from        date,
    loc_effective_to          date,
    loc_cessation_reason      varchar(2),
    loc_premises_type         varchar(4),
    loc_comments              varchar(400),
    loc_map_reference         varchar(12),
    loc_source_identifier     varchar(2),
    loc_source_reference      varchar(20),
    loc_tel_number            varchar(25),
    loc_mobile_number         varchar(25),
    loc_fax_number            varchar(25),
    loc_email_address         varchar(50),
    loc_current_status        varchar(2),
    loc_current_user          varchar(10),
    loc_current_modified_date date,
    loc_current_pid           numeric(3),
    loc_reason_code           varchar(2),
    loc_version               numeric(6),
    row_number                numeric
);
