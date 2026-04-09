-- liquibase formatted sql

-- changeset codex:0180-175

alter table _ct_movt_corr_summ_errors
    add foreign key (mse_mcs_id) references _ct_movt_correct_summaries;