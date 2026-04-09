-- liquibase formatted sql

-- changeset codex:0180-051

create table if not exists animal_status
(
    animal_identifier text not null
        constraint fk_animal_status_animal
            references animal,
    animal_state      text not null
        constraint fk_animal_status_state
            references animal_state,
    start_date        date not null,
    end_date          date,
    primary key (animal_identifier, animal_state, start_date),
    constraint chk_animal_status_dates
        check ((end_date IS NULL) OR (end_date >= start_date))
);
create index if not exists idx_animal_status_animal_identifier
    on animal_status (animal_identifier);
