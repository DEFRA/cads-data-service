-- liquibase formatted sql

-- changeset codex:0180-154

create table if not exists _ct_condition_markers
(
    com_id                    numeric(12) not null
        primary key,
    com_ran_id                numeric(12)
        references _ct_registered_animals,
    com_cma_id                numeric(12)
        references _ct_cm_authorities,
    com_cac_id                numeric(12)
        references _ct_condition_activities,
    com_effective_from_date   date,
    com_marker_type           varchar(1),
    com_amendment_reason_code varchar(3),
    com_last_used_bud_number  numeric(12),
    com_autotag_wave_number   numeric(2),
    com_comments              varchar(1000),
    com_amendment_reason_text varchar(60),
    com_effective_to_date     date,
    com_loc_id                numeric(12)
        references _ct_locations,
    com_cov_id                numeric(12)
        references _ct_condition_variants,
    com_grouping_reference    varchar(16),
    com_branch_number         numeric(1),
    com_mov_id                numeric(12)
        references _ct_registered_movements,
    com_document_refs         varchar(60),
    com_last_probity_date     date,
    com_source                varchar(2),
    com_current_pid           numeric(3),
    com_current_status        varchar(2),
    com_current_user          varchar(10),
    com_current_modified_date date,
    com_version               numeric(6),
    row_number                numeric
);
