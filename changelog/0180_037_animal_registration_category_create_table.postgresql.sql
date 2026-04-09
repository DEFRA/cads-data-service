-- liquibase formatted sql

-- changeset codex:0180-037

create table if not exists animal_registration_category
(
    category text not null
        primary key
);
