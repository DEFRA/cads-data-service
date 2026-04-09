-- liquibase formatted sql

-- changeset codex:0180-115

create table if not exists _ct_ps9999_ahdb_data
(
    ran_id        numeric not null
        primary key,
    current_cph   varchar(14),
    animal_eartag varchar(50),
    birth_date    date,
    breed_code    varchar(5),
    sex_of_animal varchar(1),
    row_number    numeric
);
