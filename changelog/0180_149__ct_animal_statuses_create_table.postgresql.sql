-- liquibase formatted sql

-- changeset codex:0180-149

create table if not exists _ct_animal_statuses
(
    ast_id                   numeric(12) not null
        primary key,
    ast_ran_id               numeric(12)
        constraint fk_ct_animal_statuses_ast_ran_id
            references _ct_registered_animals,
    ast_status               varchar(2),
    ast_user                 varchar(10),
    ast_modified_date        date,
    ast_pid                  numeric(3),
    ast_intended_action      varchar(2),
    ast_change_received_date date,
    ast_traced_moves         numeric(4),
    ast_add_moves            numeric(4),
    ast_version              numeric(6),
    row_number               numeric
);
