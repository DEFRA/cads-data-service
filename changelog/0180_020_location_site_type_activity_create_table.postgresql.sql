-- liquibase formatted sql

-- changeset codex:0180-020

create table if not exists location_site_type_activity
(
    site_type varchar(10) not null
        constraint fk_location_site_type_activity_site_type
            references location_site_type,
    activity  varchar(10) not null
        constraint fk_location_site_type_activity_activity
            references location_activity,
    primary key (site_type, activity)
);
