-- liquibase formatted sql

-- changeset codex:0180-134

create table if not exists _ct_location_party_rels
(
    lpr_id                    numeric(12) not null
        primary key,
    lpr_loc_id                numeric(12)
        constraint fk_ct_location_party_rels_lpr_loc_id
            references _ct_locations,
    lpr_lpt_id                numeric(12)
        constraint fk_ct_location_party_rels_lpr_lpt_id
            references _ct_location_party_rel_types,
    lpr_par_id                numeric(12)
        constraint fk_ct_location_party_rels_lpr_par_id
            references _ct_parties,
    lpr_effective_from_date   date,
    lpr_effective_to_date     date,
    lpr_cessation_reason      varchar(3),
    lpr_comments              varchar(250),
    lpr_current_user          varchar(10),
    lpr_current_modified_date date,
    lpr_current_status        varchar(2),
    lpr_current_pid           numeric(3),
    lpr_version               numeric(6),
    row_number                numeric
);
