-- liquibase formatted sql

-- changeset codex:0180-148

create table if not exists _ct_animal_relationships
(
    aar_current_modified_date  date,
    aar_current_pid            numeric(3),
    aar_version                numeric(6),
    aar_id                     numeric(12) not null
        primary key,
    aar_rel_type               varchar(3),
    aar_loc_id                 numeric(12)
        constraint fk_ct_animal_relationships_aar_loc_id
            references _ct_locations,
    aar_confidence_indicator   numeric(1),
    aar_effective_from_date    date,
    aar_effective_to_date      date,
    aar_ran_id_child           numeric(12)
        constraint fk_ct_animal_relationships_aar_ran_id_child
            references _ct_registered_animals,
    aar_ran_id_parent          numeric(12)
        constraint fk_ct_animal_relationships_aar_ran_id_parent
            references _ct_registered_animals,
    aar_parent_identifier      varchar(20),
    aar_parent_identifier_type varchar(2),
    aar_cancelled_reason       varchar(3),
    aar_current_user           varchar(10),
    aar_current_status         varchar(2),
    row_number                 numeric
);
