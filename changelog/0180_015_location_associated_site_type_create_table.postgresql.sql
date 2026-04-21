-- liquibase formatted sql

-- changeset codex:0180-015

create table if not exists location_associated_site_type
(
    type varchar(50) not null
        primary key
);
