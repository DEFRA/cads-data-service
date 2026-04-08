-- liquibase formatted sql

-- changeset codex:0180-057

create table if not exists animal_unregistered_parents
(
    animal_identifier             text not null
        primary key
        constraint fk_animal_unregistered_parents_animal
            references animal,
    sire_animal_identifier        text,
    genetic_dam_animal_identifier text,
    birth_dam_animal_identifier   text
);
