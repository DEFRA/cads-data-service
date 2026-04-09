-- liquibase formatted sql

-- changeset codex:0180-109

create table if not exists _ct_parties
(
    par_id                    numeric(12) not null
        primary key,
    par_initials              varchar(12),
    par_surname               varchar(30),
    par_title                 varchar(10),
    par_welsh_indicator       varchar(1),
    par_email_address         varchar(50),
    par_effective_from_date   date,
    par_effective_to_date     date,
    par_fax_number            varchar(25),
    par_cessation_reason      varchar(2),
    par_tel_number            varchar(25),
    par_mobile_number         varchar(25),
    par_comments              varchar(400),
    par_current_user          varchar(10),
    par_current_status        varchar(2),
    par_current_modified_date date,
    par_current_pid           numeric(12),
    par_version               numeric(6),
    row_number                numeric
);
