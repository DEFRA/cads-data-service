-- liquibase formatted sql

-- changeset codex:0180-102

create table if not exists _ct_mov_hst
(
    hst_ondate    date,
    hst_ontype    numeric,
    hst_onsource  varchar(3),
    hst_offkey    numeric,
    hst_offdate   date,
    hst_offtype   numeric,
    hst_offsource varchar(3),
    hst_pairind   varchar(1),
    hst_splitflg  varchar(1),
    hst_key       numeric,
    hst_lkey      numeric,
    hst_onkey     numeric,
    row_number    numeric
);
