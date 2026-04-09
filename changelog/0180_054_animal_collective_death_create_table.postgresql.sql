-- liquibase formatted sql

-- changeset codex:0180-054

create table if not exists animal_collective_death
(
    identifier                         integer not null
        primary key,
    species                            text    not null,
    site_identifier                    text    not null,
    quantity                           integer not null,
    death_date                         date    not null,
    death_reason_species               text,
    death_reason                       text,
    carcass_collection_site_identifier text
        constraint fk_animal_collective_death_carcass_site
            references animal_site_ref,
    mark_species                       text,
    mark_collective_site_identifier    text,
    mark                               text,
    constraint fk_animal_collective_death_collective_ref
        foreign key (species, site_identifier) references animal_collective_ref,
    constraint fk_animal_collective_death_reason
        foreign key (death_reason_species, death_reason) references animal_death_reason,
    constraint fk_animal_collective_death_mark
        foreign key (mark_species, mark_collective_site_identifier, mark) references animal_mark
);
create index if not exists idx_animal_collective_death_collective_ref
    on animal_collective_death (species, site_identifier);
