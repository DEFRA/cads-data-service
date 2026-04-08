-- liquibase formatted sql

-- changeset codex:0180-086

create table if not exists _ct_ext_cetd_eartag
(
    cet_key     varchar(20),
    cet_herd    varchar(10),
    cet_rsc     numeric(3),
    cet_date    date,
    cet_bsps    varchar(6),
    cet_cid     varchar(14),
    cet_scps    varchar(9),
    cet_version numeric(6),
    row_number  numeric
);
