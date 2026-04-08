-- liquibase formatted sql

-- changeset codex:0180-042

create table if not exists animal_genotype
(
    species  text not null
        constraint fk_animal_genotype_species
            references animal_species,
    genotype text not null,
    primary key (species, genotype)
);
create index if not exists idx_animal_genotype_species
    on animal_genotype (species);
