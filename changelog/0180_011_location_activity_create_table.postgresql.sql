-- liquibase formatted sql

-- changeset codex:0180-011

create table if not exists location_activity
(
    type        varchar(10)  not null
        primary key,
    description varchar(100) not null
);
