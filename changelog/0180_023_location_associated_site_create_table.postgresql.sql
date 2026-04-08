-- liquibase formatted sql

-- changeset codex:0180-023

create table if not exists location_associated_site
(
    site_identifier            varchar(50) not null
        constraint fk_location_associated_site_site
            references location_site,
    associated_site_identifier varchar(50) not null
        constraint fk_location_associated_site_related_site
            references location_site,
    associated_site_type       varchar(50) not null
        constraint fk_location_associated_site_type
            references location_associated_site_type,
    start_date                 date        not null,
    end_date                   date,
    primary key (site_identifier, associated_site_identifier, associated_site_type, start_date),
    constraint chk_location_associated_site_dates
        check ((end_date IS NULL) OR (end_date >= start_date))
);
create index if not exists idx_location_associated_site_related_site
    on location_associated_site (associated_site_identifier);
