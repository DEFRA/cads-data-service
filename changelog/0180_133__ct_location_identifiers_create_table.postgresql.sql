-- liquibase formatted sql

-- changeset codex:0180-133

create table if not exists _ct_location_identifiers
(
    lid_id                    numeric(12) not null
        primary key,
    lid_loc_id                numeric(12)
        constraint fk_ct_location_identifiers_lid_loc_id
            references _ct_locations,
    lid_effective_from_date   date,
    lid_identifier            varchar(14),
    lid_full_identifier       varchar(17),
    lid_sub_identifier        varchar(2),
    lid_effective_to_date     date,
    lid_current_status        varchar(2),
    lid_current_modified_date date,
    lid_current_user          varchar(10),
    lid_current_pid           numeric(3),
    lid_current_amend_reason  varchar(2),
    lid_version               numeric(6),
    row_number                numeric
);
