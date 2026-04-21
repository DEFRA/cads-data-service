-- liquibase formatted sql

-- changeset codex:0180-008

create table if not exists location_country
(
    code                             varchar(10)           not null
        primary key,
    name                             varchar(100)          not null,
    european_union_trade_member_flag boolean default false not null,
    devolved_authority_flag          boolean default false not null,
    home_country_flag                boolean default false not null
);
