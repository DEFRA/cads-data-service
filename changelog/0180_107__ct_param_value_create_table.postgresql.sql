-- liquibase formatted sql

-- changeset codex:0180-107

create table if not exists _ct_param_value
(
    pvl_id                    numeric(12) not null
        primary key,
    pvl_param                 varchar(30),
    pvl_phd_id                numeric(12)
        constraint fk_ct_param_value_pvl_phd_id
            references _ct_param_header,
    pvl_param_value           varchar(30),
    pvl_param_short_desc      varchar(20),
    pvl_param_long_desc       varchar(100),
    pvl_sequence              numeric(4),
    pvl_current_user          varchar(10),
    pvl_current_status        varchar(2),
    pvl_current_modified_date date,
    pvl_current_pid           numeric(3),
    pvl_version               numeric(6),
    row_number                numeric
);
