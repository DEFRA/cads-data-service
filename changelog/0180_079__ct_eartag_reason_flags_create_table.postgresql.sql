-- liquibase formatted sql

-- changeset codex:0180-079

create table if not exists _ct_eartag_reason_flags
(
    erf_id                        numeric(12) not null
        primary key,
    erf_eartag_authority          varchar(2),
    erf_etr_id                    numeric(12)
        constraint fk_ct_eartag_reason_flags_erf_etr_id
            references _ct_eartag_reasons,
    erf_manual_entry_default_ind  numeric(1),
    erf_manual_deletion_ind       numeric(1),
    erf_batch_update_amend_flag   numeric(1),
    erf_cts_animal_reg_flag       numeric(1),
    erf_manual_override           numeric(1),
    erf_cts_gen_surr_sire_allowed numeric(1),
    erf_manual_entry_ind          numeric(1),
    erf_backcapture_regn_flag     numeric(1),
    erf_manual_update_flag        numeric(1),
    erf_current_status            varchar(2),
    erf_current_user              varchar(10),
    erf_current_modified_date     date,
    erf_current_pid               numeric(3),
    erf_version                   numeric(6),
    row_number                    numeric
);
