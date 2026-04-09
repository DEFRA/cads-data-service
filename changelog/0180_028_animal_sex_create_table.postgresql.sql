-- liquibase formatted sql

-- changeset codex:0180-028

create table if not exists animal_sex
(
    sex text not null
        primary key
);
