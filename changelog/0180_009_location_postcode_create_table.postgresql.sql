-- liquibase formatted sql

-- changeset codex:0180-009

create table if not exists location_postcode
(
    postcode varchar(10) not null
        primary key
);
