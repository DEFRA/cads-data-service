-- liquibase formatted sql

-- changeset codex:0180-041

create table if not exists animal_collective_ref
(
    species         text not null
        constraint fk_animal_collective_ref_species
            references animal_species,
    site_identifier text not null
        constraint fk_animal_collective_ref_site
            references animal_site_ref,
    state           text not null
        constraint fk_animal_collective_ref_state
            references animal_collective_state,
    primary key (species, site_identifier)
);
create index if not exists idx_animal_collective_ref_site_identifier
    on animal_collective_ref (site_identifier);
