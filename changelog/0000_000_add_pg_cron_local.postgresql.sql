-- liquibase formatted sql

-- changeset schema:0000-000-add-pg-cron-local context:local,integration
-- comment: pg_cron is only provisioned locally and in CI via the cads-tools Postgres image.
CREATE EXTENSION IF NOT EXISTS pg_cron;