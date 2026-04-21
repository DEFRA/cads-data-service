-- liquibase formatted sql

-- changeset codex:0180-142

create table if not exists _ct_web_users
(
    wur_current_pid             numeric(3),
    wur_version                 numeric(6),
    wur_id                      numeric(12) not null
        primary key,
    wur_access_number           varchar(12),
    wur_bad_login_reset_count   numeric(3),
    wur_bad_login_per_day_count numeric(3),
    wur_password_issue_flag     varchar(1),
    wur_user_type               varchar(2),
    wur_lpr_id_keeper           numeric(12)
        constraint fk_ct_web_users_wur_lpr_id_keeper
            references _ct_location_party_rels,
    wur_encrypted_password      varchar(30),
    wur_staff_number            varchar(7),
    wur_welsh_indicator         varchar(1),
    wur_issued_to_identifier    varchar(30),
    wur_security_filename       varchar(20),
    wur_mobile_number           varchar(30),
    wur_telephone_number        varchar(30),
    wur_user_name               varchar(60),
    wur_user_location           varchar(35),
    wur_address_2               varchar(35),
    wur_address_3               varchar(35),
    wur_address_4               varchar(35),
    wur_address_5               varchar(35),
    wur_post_code               varchar(10),
    wur_email_address           varchar(100),
    wur_expiry_date             date,
    wur_password_filename       varchar(20),
    wur_current_user            varchar(10),
    wur_current_status          varchar(2),
    wur_current_modified_date   date,
    row_number                  numeric
);
