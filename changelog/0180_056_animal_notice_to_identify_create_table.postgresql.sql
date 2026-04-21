-- liquibase formatted sql

-- changeset codex:0180-056

create table if not exists animal_notice_to_identify
(
    notice_reference                text    not null
        primary key,
    species                         text    not null
        constraint fk_animal_notice_to_identify_species
            references animal_species,
    animal_identifier               text
        constraint fk_animal_notice_to_identify_animal
            references animal,
    dna_proven_flag                 boolean not null,
    original_animal_identifier      text    not null
        constraint fk_animal_notice_to_identify_original_animal
            references animal,
    original_animal_identifier_type text    not null
        constraint fk_animal_notice_to_identify_original_type
            references animal_original_identifier_type,
    breed_species                   text,
    breed_code                      text,
    sex                             text    not null
        constraint fk_animal_notice_to_identify_sex
            references animal_sex,
    issue_date                      date    not null,
    end_date                        date,
    inspection_year                 text,
    additional_details              text,
    inspection_reference            text,
    site_identifier                 text    not null
        constraint fk_animal_notice_to_identify_site
            references animal_site_ref,
    resolution                      text
        constraint fk_animal_notice_to_identify_resolution
            references animal_resolution_type,
    constraint fk_animal_notice_to_identify_breed
        foreign key (breed_species, breed_code) references animal_breed,
    constraint chk_animal_notice_to_identify_dates
        check ((end_date IS NULL) OR (end_date >= issue_date))
);
create index if not exists idx_animal_notice_to_identify_site_identifier
    on animal_notice_to_identify (site_identifier);
