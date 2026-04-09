-- liquibase formatted sql

-- changeset codex:0180-147

create table if not exists _ct_animal_identifiers
(
    aid_id                     numeric(12) not null
        primary key,
    aid_identifier             varchar(50),
    aid_identifier_type        varchar(2),
    aid_effective_from_date    date,
    aid_effective_to_date      date,
    aid_loc_id_assigned        numeric(12)
        constraint fk_ct_animal_identifiers_aid_loc_id_assigned
            references _ct_locations,
    aid_current_flag           varchar(1),
    aid_ran_id                 numeric(12)
        constraint fk_ct_animal_identifiers_aid_ran_id
            references _ct_registered_animals,
    aid_etg_id                 numeric(12)
        constraint fk_ct_animal_identifiers_aid_etg_id
            references _ct_eartags,
    aid_eid_id                 numeric(12)
        constraint fk_ct_animal_identifiers_aid_eid_id
            references _ct_electronic_identifiers,
    aid_current_user           varchar(10),
    aid_current_status         varchar(2),
    aid_current_modified_date  date,
    aid_current_pid            numeric(3),
    aid_aid_id_original        numeric(12)
        constraint fk_ct_animal_identifiers_aid_aid_id_original
            references _ct_animal_identifiers,
    aid_aid_id_previous        numeric(12)
        constraint fk_ct_animal_identifiers_aid_aid_id_previous
            references _ct_animal_identifiers,
    aid_version                numeric(6),
    aid_assigned_location_repd varchar(17),
    row_number                 numeric
);
