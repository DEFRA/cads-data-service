-- liquibase formatted sql

-- changeset codex:0180-049

create table if not exists animal_death
(
    animal_identifier                  text    not null
        primary key
        constraint fk_animal_death_animal
            references animal,
    death_date                         date    not null,
    death_reported_date                date,
    death_site_identifier              text    not null
        constraint fk_animal_death_site
            references animal_site_ref,
    death_reason_species               text,
    death_reason                       text,
    carcass_collection_site_identifier text
        constraint fk_animal_death_carcass_site
            references animal_site_ref,
    tse_test_required_flag             boolean not null,
    death_received_date                date,
    constraint fk_animal_death_reason
        foreign key (death_reason_species, death_reason) references animal_death_reason
);
create index if not exists idx_animal_death_site
    on animal_death (death_site_identifier);
