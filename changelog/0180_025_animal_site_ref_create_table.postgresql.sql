-- liquibase formatted sql

-- changeset codex:0180-025

create table if not exists animal_site_ref
(
    identifier text not null
        primary key
);
