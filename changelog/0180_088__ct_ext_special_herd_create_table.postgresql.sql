-- liquibase formatted sql

-- changeset codex:0180-088

create table if not exists _ct_ext_special_herd
(
    sph_herd_code   varchar(10),
    sph_herd_region varchar(30),
    sph_version     numeric(6),
    row_number      numeric
);
