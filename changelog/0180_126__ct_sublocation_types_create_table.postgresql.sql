-- liquibase formatted sql

-- changeset codex:0180-126

create table if not exists _ct_sublocation_types
(
    slt_id                    numeric(12) not null
        primary key,
    slt_subloc_type           varchar(2),
    slt_short_description     varchar(20),
    slt_long_description      varchar(60),
    slt_peer_link_permitted   varchar(1),
    slt_hier_link_permitted   varchar(1),
    slt_movement_subloc_ind   varchar(1),
    slt_use_subloc_address    varchar(1),
    slt_current_user          varchar(10),
    slt_current_status        varchar(2),
    slt_current_modified_date date,
    slt_current_pid           numeric(3),
    slt_version               numeric(6),
    row_number                numeric
);
