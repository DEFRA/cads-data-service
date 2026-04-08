-- liquibase formatted sql

-- changeset codex:0180-013

create table if not exists location_site_state
(
    state varchar(20) not null
        primary key
);
