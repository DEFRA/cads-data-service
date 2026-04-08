-- liquibase formatted sql

-- changeset codex:0180-144

create table if not exists _ct_applic_statuses
(
    aps_id              numeric(12) not null
        primary key,
    aps_vap_id          numeric(12)
        constraint fk_ct_applic_statuses_aps_vap_id
            references _ct_valid_applications,
    aps_user            varchar(10),
    aps_status          varchar(2),
    aps_modified_date   date,
    aps_pid             numeric(3),
    aps_intended_action varchar(2),
    aps_version         numeric(6),
    row_number          numeric
);
