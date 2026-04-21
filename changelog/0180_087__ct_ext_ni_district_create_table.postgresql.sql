-- liquibase formatted sql

-- changeset codex:0180-087

create table if not exists _ct_ext_ni_district
(
    nid_electoral_district varchar(16),
    nid_version            numeric(6),
    nid_herd_code          varchar(2),
    row_number             numeric
);
