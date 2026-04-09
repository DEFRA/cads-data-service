-- liquibase formatted sql

-- changeset codex:0180-174

alter table _ct_registered_movements
    add foreign key (mov_ran_id) references _ct_registered_animals;

alter table _ct_registered_movements
    add foreign key (mov_loc_id) references _ct_locations;

alter table _ct_registered_movements
    add foreign key (mov_cry_id_import) references _ct_countries;