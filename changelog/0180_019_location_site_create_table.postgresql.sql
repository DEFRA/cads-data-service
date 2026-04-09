-- liquibase formatted sql

-- changeset codex:0180-019

create table if not exists location_site
(
    identifier                      varchar(50)           not null
        primary key,
    site_type                       varchar(10)           not null
        constraint fk_location_site_type
            references location_site_type,
    name                            varchar(255)          not null,
    location_identifier             varchar(50)           not null
        constraint fk_location_site_location
            references location,
    site_source                     varchar(20)           not null
        constraint fk_location_site_source
            references location_site_source,
    destroy_identity_documents_flag boolean default false not null,
    state                           varchar(20)           not null
        constraint fk_location_site_state
            references location_site_state,
    start_date                      date                  not null,
    end_date                        date,
    constraint chk_location_site_dates
        check ((end_date IS NULL) OR (end_date >= start_date))
);
create index if not exists idx_location_site_type
    on location_site (site_type);
create index if not exists idx_location_site_location_identifier
    on location_site (location_identifier);
create index if not exists idx_location_site_source
    on location_site (site_source);
create index if not exists idx_location_site_state
    on location_site (state);
