-- liquibase formatted sql

-- changeset codex:0180-047

create table if not exists animal_birth
(
    animal_identifier                     text    not null
        primary key
        constraint fk_animal_birth_animal
            references animal,
    birth_site_identifier                 text    not null
        constraint fk_animal_birth_site
            references animal_site_ref,
    birth_date                            date,
    birth_year                            text,
    birth_mark_species                    text,
    birth_mark_collective_site_identifier text,
    birth_mark                            text,
    assisted_birth_flag                   boolean not null,
    multiple_births_flag                  boolean not null,
    embryo_transfer_flag                  boolean not null,
    constraint fk_animal_birth_mark
        foreign key (birth_mark_species, birth_mark_collective_site_identifier, birth_mark) references animal_mark
);
