-- liquibase formatted sql

-- changeset codex:0180-045

create table if not exists animal
(
    identifier                   text not null
        primary key,
    animal_identifier_identifier text
        constraint fk_animal_identifier
            references animal_identifier,
    original_identifier          text,
    species                      text not null
        constraint fk_animal_species
            references animal_species,
    breed_species                text,
    breed_code                   text,
    genotype_species             text,
    genotype                     text,
    name                         text,
    sex                          text not null
        constraint fk_animal_sex
            references animal_sex,
    production_type              text,
    identification_date          date,
    received_date                date,
    genetic_dam_identifier       text
        constraint fk_animal_genetic_dam
            references animal,
    sire_identifier              text
        constraint fk_animal_sire
            references animal,
    birth_dam_identifier         text
        constraint fk_animal_birth_dam
            references animal,
    registration_site_identifier text not null
        constraint fk_animal_registration_site
            references animal_site_ref,
    registration_date            date not null,
    registration_category        text not null
        constraint fk_animal_registration_category
            references animal_registration_category,
    constraint fk_animal_breed
        foreign key (breed_species, breed_code) references animal_breed,
    constraint fk_animal_genotype
        foreign key (genotype_species, genotype) references animal_genotype,
    constraint fk_animal_species_production_type
        foreign key (species, production_type) references animal_species_production_type
);
create index if not exists idx_animal_species
    on animal (species);
create index if not exists idx_animal_registration_site_identifier
    on animal (registration_site_identifier);
