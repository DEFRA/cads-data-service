-- liquibase formatted sql

-- changeset codex:0180-003

create table if not exists party_state
(
    state varchar(20) not null
        primary key
);
