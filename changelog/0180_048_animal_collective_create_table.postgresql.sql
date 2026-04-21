-- liquibase formatted sql

-- changeset codex:0180-048

create table if not exists animal_collective
(
    animal_identifier                  text not null
        constraint fk_animal_collective_animal
            references animal,
    species                            text not null,
    home_collective_site_identifier    text not null,
    current_collective_site_identifier text not null,
    start_date                         date not null,
    end_date                           date,
    primary key (animal_identifier, species, home_collective_site_identifier, start_date),
    constraint fk_animal_collective_home
        foreign key (species, home_collective_site_identifier) references animal_collective_ref,
    constraint fk_animal_collective_current
        foreign key (species, current_collective_site_identifier) references animal_collective_ref,
    constraint chk_animal_collective_dates
        check ((end_date IS NULL) OR (end_date >= start_date))
);
create index if not exists idx_animal_collective_current
    on animal_collective (species, current_collective_site_identifier);
