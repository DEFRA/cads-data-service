-- liquibase formatted sql

-- changeset codex:0180-135

create table if not exists _ct_location_relationships
(
    llr_id                    numeric(12) not null
        primary key,
    llr_loc_id_parent         numeric(12)
        constraint fk_ct_location_relationships_llr_loc_id_parent
            references _ct_locations,
    llr_loc_id_child          numeric(12)
        constraint fk_ct_location_relationships_llr_loc_id_child
            references _ct_locations,
    llr_effective_from_date   date,
    llr_cessation_reason      varchar(2),
    llr_comments              varchar(200),
    llr_lrt_id                numeric(12)
        constraint fk_ct_location_relationships_llr_lrt_id
            references _ct_location_rel_types,
    llr_effective_to_date     date,
    llr_current_status        varchar(2),
    llr_current_modified_date date,
    llr_current_user          varchar(10),
    llr_current_pid           numeric(3),
    llr_version               numeric(6),
    row_number                numeric
);
