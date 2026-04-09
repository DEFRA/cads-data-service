-- liquibase formatted sql

-- changeset codex:0180-007

create table if not exists party_haulier_species
(
    species            varchar(50) not null
        constraint fk_party_haulier_species_species
            references party_species,
    haulier_identifier text        not null
        constraint fk_party_haulier_species_haulier
            references party_haulier,
    primary key (species, haulier_identifier)
);
