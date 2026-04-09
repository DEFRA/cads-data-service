-- liquibase formatted sql

-- changeset codex:0180-016

create table if not exists location_site_identifier_type
(
    type varchar(50) not null
        primary key
);
