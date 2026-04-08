-- liquibase formatted sql

-- changeset codex:0180-050

create table if not exists animal_party
(
    animal_identifier text    not null
        constraint fk_animal_party_animal
            references animal,
    party_identifier  integer not null
        constraint fk_animal_party_party
            references animal_party_ref,
    animal_role       text    not null
        constraint fk_animal_party_role
            references animal_role,
    start_date        date    not null,
    end_date          date,
    primary key (animal_identifier, party_identifier, animal_role, start_date),
    constraint chk_animal_party_dates
        check ((end_date IS NULL) OR (end_date >= start_date))
);
create index if not exists idx_animal_party_party_identifier
    on animal_party (party_identifier);
