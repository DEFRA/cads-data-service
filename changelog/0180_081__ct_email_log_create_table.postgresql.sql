-- liquibase formatted sql

-- changeset codex:0180-081

create table if not exists _ct_email_log
(
    eml_id                    numeric(12) not null
        primary key,
    eml_sent_datetime         date,
    eml_email_addr_recd       varchar(70),
    eml_file_name             varchar(50),
    eml_received_datetime     date,
    eml_send_return_code      varchar(100),
    eml_email_addr_sent       varchar(70),
    eml_current_user          varchar(10),
    eml_current_modified_date date,
    eml_current_pid           numeric(3),
    eml_current_status        varchar(2),
    eml_version               numeric(6),
    row_number                numeric
);
