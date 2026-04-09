-- liquibase formatted sql

-- changeset codex:0180-022

create table if not exists location_site_activity
(
    site_identifier varchar(50) not null
        constraint fk_location_site_activity_site
            references location_site,
    site_type       varchar(10) not null,
    activity        varchar(10) not null,
    start_date      date        not null,
    end_date        date,
    primary key (site_identifier, site_type, activity, start_date),
    constraint fk_location_site_activity_site_type_activity
        foreign key (site_type, activity) references location_site_type_activity,
    constraint chk_location_site_activity_dates
        check ((end_date IS NULL) OR (end_date >= start_date))
);
create index if not exists idx_location_site_activity_site_identifier
    on location_site_activity (site_identifier);
