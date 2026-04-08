-- liquibase formatted sql

-- changeset codex:0180-010

create table if not exists location_site_type
(
    type        varchar(10)  not null
        primary key,
    description varchar(100) not null
);
