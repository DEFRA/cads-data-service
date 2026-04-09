-- liquibase formatted sql

-- changeset codex:0180-137

create table if not exists _ct_preprinted_appn_forms
(
    paf_id                    numeric(12) not null
        primary key,
    paf_etg_id                numeric(12)
        constraint fk_ct_preprinted_appn_forms_paf_etg_id
            references _ct_eartags,
    paf_ppg_id                numeric(12)
        constraint fk_ct_preprinted_appn_forms_paf_ppg_id
            references _ct_ppaf_groupings,
    paf_reason_for_issue      varchar(1),
    paf_interface_txn_number  numeric(10),
    paf_interface_filename    varchar(25),
    paf_date_issued           date,
    paf_current_status        varchar(2),
    paf_current_modified_date date,
    paf_current_user          varchar(10),
    paf_current_pid           numeric(3),
    paf_version               numeric(6),
    row_number                numeric
);
