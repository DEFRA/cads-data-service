-- liquibase formatted sql

-- changeset codex:0180-055

create table if not exists animal_collective_party
(
    species          text    not null,
    site_identifier  text    not null,
    party_identifier integer not null
        constraint fk_animal_collective_party_party
            references animal_party_ref,
    collective_role  text    not null
        constraint fk_animal_collective_party_role
            references animal_collective_role,
    start_date       date    not null,
    end_date         date,
    primary key (species, site_identifier, party_identifier, collective_role, start_date),
    constraint fk_animal_collective_party_collective_ref
        foreign key (species, site_identifier) references animal_collective_ref,
    constraint chk_animal_collective_party_dates
        check ((end_date IS NULL) OR (end_date >= start_date))
);
create index if not exists idx_animal_collective_party_party_identifier
    on animal_collective_party (party_identifier);
