-- liquibase formatted sql

-- changeset codex:0180-076

create table if not exists _ct_cts_users
(
    cus_id              numeric(12) not null
        primary key,
    cus_user_identifier varchar(10),
    cus_colon_flag      varchar(1),
    cus_grade           varchar(3),
    cus_team_reference  varchar(4),
    cus_access_group    varchar(3),
    cus_room_name       varchar(20),
    cus_email_address   varchar(200),
    cus_version         numeric(6),
    row_number          numeric
);
