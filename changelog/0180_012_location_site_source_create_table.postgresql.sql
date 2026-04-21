-- liquibase formatted sql

-- changeset codex:0180-012

create table if not exists location_site_source
(
    source varchar(20) not null
        primary key
);
