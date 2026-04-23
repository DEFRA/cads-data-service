-- liquibase formatted sql

-- changeset andy:1776876267450-1 splitStatements:false
CREATE INDEX "idx_ran_birth_date" ON "_ct_registered_animals" USING btree("ran_birth_date");

-- changeset andy:1776876267450-2 splitStatements:false
CREATE INDEX "idx_ran_covering" ON "_ct_registered_animals" USING btree("ran_birth_date", "ran_brd_id", "ran_vap_id", "ran_loc_id_passport", "ran_cry_id_chr_origin", "ran_sex");

