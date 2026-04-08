-- liquibase formatted sql

-- changeset codex:0180-173

alter table _ct_registered_animals
    add foreign key (ran_mov_id_death) references _ct_registered_movements;

alter table _ct_registered_animals
    add foreign key (ran_mov_id_registration) references _ct_registered_movements;