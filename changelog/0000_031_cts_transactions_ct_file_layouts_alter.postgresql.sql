-- liquibase formatted sql

-- changeset MarkGent1:1787240786898-1 splitStatements:false
ALTER TABLE cts_transactions.ct_file_layouts ADD "flt_record_type" VARCHAR(1);

