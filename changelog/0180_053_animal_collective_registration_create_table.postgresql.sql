-- liquibase formatted sql

-- changeset codex:0180-053

create table if not exists animal_collective_registration
(
    identifier                      integer not null
        primary key,
    species                         text    not null,
    site_identifier                 text    not null,
    quantity                        integer not null,
    birth_year                      text    not null,
    registration_date               date    not null,
    identification_date             date    not null,
    genotype_species                text,
    genotype                        text,
    mark_species                    text,
    mark_collective_site_identifier text,
    mark                            text,
    breed_species                   text,
    breed_code                      text,
    constraint fk_animal_collective_registration_collective_ref
        foreign key (species, site_identifier) references animal_collective_ref,
    constraint fk_animal_collective_registration_genotype
        foreign key (genotype_species, genotype) references animal_genotype,
    constraint fk_animal_collective_registration_mark
        foreign key (mark_species, mark_collective_site_identifier, mark) references animal_mark,
    constraint fk_animal_collective_registration_breed
        foreign key (breed_species, breed_code) references animal_breed
);
create index if not exists idx_animal_collective_registration_collective_ref
    on animal_collective_registration (species, site_identifier);
