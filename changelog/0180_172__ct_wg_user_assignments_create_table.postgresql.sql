-- liquibase formatted sql

-- changeset codex:0180-172

create table if not exists _ct_wg_user_assignments
(
    wua_id                    numeric(12) not null
        primary key,
    wua_cus_id                numeric(12)
        constraint fk_ct_wg_user_assignments_wua_cus_id
            references _ct_cts_users,
    wua_wgp_id                numeric(12)
        constraint fk_ct_wg_user_assignments_wua_wgp_id
            references _ct_workgroups,
    wua_wg_contact_ind        char,
    wua_favoured_wg_ind       char,
    wua_current_user          varchar(10),
    wua_current_status        varchar(2),
    wua_current_modified_date date,
    wua_current_pid           numeric(3),
    wua_version               numeric(6),
    row_number                numeric
);
