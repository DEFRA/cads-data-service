-- liquibase formatted sql

-- changeset codex:0180-040

create table if not exists animal_breed
(
    species          text    not null
        constraint fk_animal_breed_species
            references animal_species,
    breed_code       text    not null,
    breed            text,
    cross_breed_flag boolean not null,
    state            text    not null
        constraint fk_animal_breed_state
            references animal_breed_state,
    primary key (species, breed_code)
);
create index if not exists idx_animal_breed_species
    on animal_breed (species);
