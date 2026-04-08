-- liquibase formatted sql

-- changeset codex:0180-058

create table if not exists _ct_alloc_routines
(
    rou_id                    numeric(12) not null
        primary key,
    rou_routine               varchar(6),
    rou_allocation_type       varchar(1),
    rou_long_description      varchar(40),
    rou_current_user          varchar(10),
    rou_current_status        varchar(2),
    rou_current_modified_date date,
    rou_current_pid           numeric(3),
    rou_version               numeric(6),
    row_number                numeric
);
