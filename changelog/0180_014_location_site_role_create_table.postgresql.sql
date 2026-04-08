-- liquibase formatted sql

-- changeset codex:0180-014

create table if not exists location_site_role
(
    role varchar(20) not null
        primary key
);
