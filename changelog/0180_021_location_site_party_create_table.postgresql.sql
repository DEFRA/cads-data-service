-- liquibase formatted sql

-- changeset codex:0180-021

create table if not exists location_site_party
(
    site_identifier  varchar(50) not null
        constraint fk_location_site_party_site
            references location_site,
    party_identifier bigint      not null
        constraint fk_location_site_party_party
            references location_party_ref,
    site_role        varchar(20) not null
        constraint fk_location_site_party_role
            references location_site_role,
    start_date       date        not null,
    end_date         date,
    primary key (site_identifier, party_identifier, site_role, start_date),
    constraint chk_location_site_party_dates
        check ((end_date IS NULL) OR (end_date >= start_date))
);
create index if not exists idx_location_site_party_identifier
    on location_site_party (party_identifier);
