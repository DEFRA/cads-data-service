-- liquibase formatted sql

-- changeset codex:0180-018

create table if not exists location
(
    identifier          varchar(50) not null
        primary key,
    uprn                bigint,
    single_line_address varchar(500),
    postcode            varchar(10)
        constraint fk_location_postcode
            references location_postcode,
    os_map_reference    varchar(30),
    easting             integer,
    northing            integer,
    country_code        varchar(10)
        constraint fk_location_country
            references location_country
);
create index if not exists idx_location_country_code
    on location (country_code);
create index if not exists idx_location_postcode
    on location (postcode);
