-- liquibase formatted sql

-- changeset codex:0180-030

create table if not exists animal_role
(
    role text not null
        primary key
);
