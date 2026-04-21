-- liquibase formatted sql

-- changeset codex:0180-128

create table if not exists _ct_addresses
(
    adr_id                    numeric(12) not null
        primary key,
    adr_loc_id                numeric(12)
        constraint fk_ct_addresses_adr_loc_id
            references _ct_locations,
    adr_par_id                numeric(12)
        constraint fk_ct_addresses_adr_par_id
            references _ct_parties,
    adr_name                  varchar(35),
    adr_address_2             varchar(35),
    adr_address_3             varchar(35),
    adr_address_4             varchar(35),
    adr_address_5             varchar(35),
    adr_post_code             varchar(8),
    adr_current_modified_date date,
    adr_current_status        varchar(2),
    adr_current_user          varchar(10),
    adr_current_pid           numeric(3),
    adr_version               numeric(6),
    row_number                numeric
);
