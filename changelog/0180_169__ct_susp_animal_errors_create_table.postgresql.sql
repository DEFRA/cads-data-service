-- liquibase formatted sql

-- changeset codex:0180-169

create table if not exists _ct_susp_animal_errors
(
    sae_id                    numeric(12) not null
        primary key,
    sae_san_id                numeric(12)
        constraint fk_ct_susp_animal_errors_sae_san_id
            references _ct_suspended_animals,
    sae_error_code            varchar(4),
    sae_attribute_name        varchar(30),
    sae_current_modified_date date,
    sae_current_user          varchar(10),
    sae_current_status        varchar(2),
    sae_current_pid           numeric(3),
    sae_version               numeric(6),
    row_number                numeric
);
