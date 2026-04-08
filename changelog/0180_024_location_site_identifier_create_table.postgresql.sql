-- liquibase formatted sql

-- changeset codex:0180-024

create table if not exists location_site_identifier
(
    site_identifier  varchar(50)  not null
        constraint fk_location_site_identifier_site
            references location_site,
    identifier_type  varchar(50)  not null
        constraint fk_location_site_identifier_type
            references location_site_identifier_type,
    identifier_value varchar(100) not null,
    primary key (site_identifier, identifier_type, identifier_value)
);
