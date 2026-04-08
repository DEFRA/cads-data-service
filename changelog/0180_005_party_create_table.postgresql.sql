-- liquibase formatted sql

-- changeset codex:0180-005

create table if not exists party
(
    number              integer     not null
        primary key,
    party_type          varchar(30) not null
        constraint fk_party_type
            references party_type,
    first_name          text,
    last_name           text,
    name                text        not null,
    mobile              text,
    landline            text,
    email               text,
    location_identifier varchar(50)
        constraint fk_party_location
            references party_location,
    party_state         varchar(20) not null
        constraint fk_party_state
            references party_state
);
create index if not exists idx_party_location_identifier
    on party (location_identifier);
