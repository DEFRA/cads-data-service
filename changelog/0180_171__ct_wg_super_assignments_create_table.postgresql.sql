-- liquibase formatted sql

-- changeset codex:0180-171

create table if not exists _ct_wg_super_assignments
(
    wsa_id                    numeric(12) not null
        primary key,
    wsa_wgp_id_current        numeric(12)
        constraint fk_ct_wg_super_assignments_wsa_wgp_id_current
            references _ct_workgroups,
    wsa_wgp_id_assigned       numeric(12)
        constraint fk_ct_wg_super_assignments_wsa_wgp_id_assigned
            references _ct_workgroups,
    wsa_rou_id                numeric(12)
        constraint fk_ct_wg_super_assignments_wsa_rou_id
            references _ct_alloc_routines,
    wsa_current_user          varchar(10),
    wsa_current_status        varchar(2),
    wsa_current_modified_date date,
    wsa_current_pid           numeric(3),
    wsa_version               numeric(6),
    row_number                numeric
);
