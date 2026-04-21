-- liquibase formatted sql

-- changeset codex:0180-043

create table if not exists animal_death_reason
(
    species text not null
        constraint fk_animal_death_reason_species
            references animal_species,
    reason  text not null,
    primary key (species, reason)
);
create index if not exists idx_animal_death_reason_species
    on animal_death_reason (species);
