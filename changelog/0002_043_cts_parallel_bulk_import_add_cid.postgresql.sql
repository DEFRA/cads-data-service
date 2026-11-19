-- liquibase formatted sql

-- changeset MarkGent1:0002_043_01 splitStatements:false

-- Record the optional correlation_id to supplied by the requesting party.
-- Doesn't change the behaviour of existing runs.
ALTER TABLE cads.cts_parallel_import_runs
    ADD COLUMN IF NOT EXISTS correlation_id uuid NULL;
