-- liquibase formatted sql

-- changeset codex:0180-046

create table if not exists animal_mark
(
    species                    text not null,
    collective_site_identifier text not null,
    mark                       text not null,
    start_date                 date not null,
    end_date                   date,
    primary key (species, collective_site_identifier, mark),
    constraint fk_animal_mark_collective_ref
        foreign key (species, collective_site_identifier) references animal_collective_ref,
    constraint chk_animal_mark_dates
        check ((end_date IS NULL) OR (end_date >= start_date))
);
create index if not exists idx_animal_mark_collective
    on animal_mark (species, collective_site_identifier);
