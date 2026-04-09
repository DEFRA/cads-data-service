-- liquibase formatted sql

-- changeset codex:0180-097

create table if not exists _ct_location_rel_types
(
    lrt_id                    numeric(12) not null
        primary key,
    lrt_code                  varchar(2),
    lrt_description           varchar(30),
    lrt_second_single_link    varchar(1),
    lrt_mandatory             varchar(1),
    lrt_gaps_allowed          varchar(1),
    lrt_primary_single_link   varchar(1),
    lrt_hierarchical_link     varchar(1),
    lrt_relship_text_down     varchar(30),
    lrt_relship_text_up       varchar(30),
    lrt_current_modified_date date,
    lrt_current_status        varchar(2),
    lrt_current_user          varchar(10),
    lrt_current_pid           numeric(3),
    lrt_version               numeric(6),
    row_number                numeric
);
