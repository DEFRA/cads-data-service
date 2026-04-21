-- liquibase formatted sql

-- changeset codex:0180-063

create table if not exists _ct_cla_extract_detail
(
    cld_id                    numeric(12) not null
        primary key,
    cld_cle_id                numeric(12)
        constraint fk_ct_cla_extract_detail_cld_cle_id
            references _ct_cla_extract,
    cld_batch_id              numeric(12),
    cld_table_name            varchar(30),
    cld_record_count          numeric(12),
    cld_run_start             date,
    cld_run_end               date,
    cld_current_modified_date date
);
