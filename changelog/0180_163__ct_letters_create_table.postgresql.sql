-- liquibase formatted sql

-- changeset codex:0180-163

create table if not exists _ct_letters
(
    let_id                    numeric(12) not null
        primary key,
    let_type                  varchar(3),
    let_description           varchar(30),
    let_wgp_id                numeric(12)
        constraint fk_ct_letters_let_wgp_id
            references _ct_workgroups,
    let_program_name          varchar(10),
    let_wgp_id_sent           numeric(12)
        constraint fk_ct_letters_let_wgp_id_sent
            references _ct_workgroups,
    let_current_user          varchar(10),
    let_current_status        varchar(2),
    let_current_modified_date date,
    let_current_pid           numeric(3),
    let_version               numeric(6),
    row_number                numeric
);
