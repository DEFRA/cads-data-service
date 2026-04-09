-- liquibase formatted sql

-- changeset codex:0180-170

create table if not exists _ct_wg_autoallocations
(
    wga_id                    numeric(12) not null
        primary key,
    wga_rou_id                numeric(12)
        constraint fk_ct_wg_autoallocations_wga_rou_id
            references _ct_alloc_routines,
    wga_wgp_id                numeric(12)
        constraint fk_ct_wg_autoallocations_wga_wgp_id
            references _ct_workgroups,
    wga_allocation            varchar(10),
    wga_assignment            varchar(10),
    wga_current_user          varchar(10),
    wga_current_pid           numeric(3),
    wga_current_status        varchar(2),
    wga_current_modified_date date,
    wga_version               numeric(6),
    row_number                numeric
);
