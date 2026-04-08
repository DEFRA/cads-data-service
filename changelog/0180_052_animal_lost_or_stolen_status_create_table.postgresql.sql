-- liquibase formatted sql

-- changeset codex:0180-052

create table if not exists animal_lost_or_stolen_status
(
    animal_identifier      text    not null
        constraint fk_animal_lost_or_stolen_status_animal
            references animal,
    event_date             date    not null,
    state                  text    not null
        constraint fk_animal_lost_or_stolen_status_state
            references animal_lost_or_stolen_state,
    crime_reference_number text,
    home_site_identifier   text    not null
        constraint fk_animal_lost_or_stolen_status_home_site
            references animal_site_ref,
    found_dead_flag        boolean not null,
    received_date          date,
    primary key (animal_identifier, event_date)
);
create index if not exists idx_animal_lost_or_stolen_status_home_site_identifier
    on animal_lost_or_stolen_status (home_site_identifier);
