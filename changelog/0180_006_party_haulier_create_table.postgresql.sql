-- liquibase formatted sql

-- changeset codex:0180-006

create table if not exists party_haulier
(
    identifier           text    not null
        primary key,
    party_number         integer not null
        constraint fk_party_haulier_party
            references party,
    authorisation_number text    not null,
    start_date           date    not null,
    end_date             date,
    constraint chk_party_haulier_dates
        check ((end_date IS NULL) OR (end_date >= start_date))
);
create index if not exists idx_party_haulier_party_number
    on party_haulier (party_number);
