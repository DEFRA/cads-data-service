-- liquibase formatted sql

-- changeset codex:0180-153

create table if not exists _ct_animal_changes
(
    ach_id                      numeric(12) not null
        primary key,
    ach_current_status          varchar(2),
    ach_current_user            varchar(10),
    ach_current_modified_date   date,
    ach_current_pid             numeric(3),
    ach_ran_id_doc_issued       numeric(12)
        constraint fk_ct_animal_changes_ach_ran_id_doc_issued
            references _ct_registered_animals,
    ach_loc_id_doc_issued       numeric(12)
        constraint fk_ct_animal_changes_ach_loc_id_doc_issued
            references _ct_locations,
    ach_doc_issued_date         date,
    ach_passport_version_number varchar(3),
    ach_mov_id_death_cancel     numeric(12)
        constraint fk_ct_animal_changes_ach_mov_id_death_cancel
            references _ct_registered_movements,
    ach_breed_original          varchar(5),
    ach_breed_new               varchar(5),
    ach_sex_original            char,
    ach_sex_new                 char,
    ach_birth_date_original     date,
    ach_birth_date_new          date,
    ach_eartag_original         varchar(14),
    ach_eartag_new              varchar(14),
    row_number                  numeric
);
