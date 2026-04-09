-- liquibase formatted sql

-- changeset codex:0180-044

create table if not exists animal_species_production_type
(
    species         text not null
        constraint fk_animal_species_production_type_species
            references animal_species,
    production_type text not null
        constraint fk_animal_species_production_type_production_type
            references animal_production_type,
    primary key (species, production_type)
);
create index if not exists idx_animal_species_production_type_species
    on animal_species_production_type (species);
