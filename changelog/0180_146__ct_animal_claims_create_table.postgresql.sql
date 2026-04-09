-- liquibase formatted sql

-- changeset codex:0180-146

create table if not exists _ct_animal_claims
(
    anc_id                       numeric(12) not null
        primary key,
    anc_ran_id                   numeric(12)
        constraint fk_ct_animal_claims_anc_ran_id
            references _ct_registered_animals,
    anc_claim_sequence           numeric(3),
    anc_current_modified_date    date,
    anc_current_pid              numeric(3),
    anc_current_user             varchar(10),
    anc_cls_id                   numeric(12)
        constraint fk_ct_animal_claims_anc_cls_id
            references _ct_claim_statuses,
    anc_clt_id                   numeric(12)
        constraint fk_ct_animal_claims_anc_clt_id
            references _ct_claim_types,
    anc_claim_reference          varchar(20),
    anc_retention_start_date     date,
    anc_retention_end_date       date,
    anc_office                   varchar(2),
    anc_scheme_year              numeric(4),
    anc_scheme_modified_datetime date,
    anc_version                  numeric(6),
    anc_current_status           varchar(2),
    row_number                   numeric
);
