-- liquibase formatted sql

-- changeset codex:0180-106

create table if not exists _ct_param_group
(
    pgp_version               numeric(6),
    pgp_id                    numeric(12) not null
        primary key,
    pgp_param                 varchar(30),
    pgp_group_value           varchar(15),
    pgp_phd_id                numeric(12)
        constraint fk_ct_param_group_pgp_phd_id
            references _ct_param_header,
    pgp_short_desc            varchar(30),
    pgp_long_desc             varchar(100),
    pgp_current_user          varchar(10),
    pgp_current_status        varchar(2),
    pgp_current_modified_date date,
    pgp_current_pid           numeric(3),
    row_number                numeric
);
