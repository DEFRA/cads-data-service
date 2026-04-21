-- liquibase formatted sql

-- changeset codex:0180-108

create table if not exists _ct_param_value_group
(
    pvg_id                    numeric(12) not null
        primary key,
    pvg_pgp_id                numeric(12)
        constraint fk_ct_param_value_group_pvg_pgp_id
            references _ct_param_group,
    pvg_pvl_id                numeric(12)
        constraint fk_ct_param_value_group_pvg_pvl_id
            references _ct_param_value,
    pvg_group_value           varchar(15),
    pvg_param                 varchar(30),
    pvg_param_value           varchar(30),
    pvg_current_user          varchar(10),
    pvg_current_status        varchar(2),
    pvg_current_modified_date date,
    pvg_current_pid           numeric(3),
    pvg_version               numeric(6),
    row_number                numeric
);
