-- liquibase formatted sql

-- changeset schema:0000-000-add-pg-cron-local context:local,integration splitStatements:false runOnChange:true
-- comment: pg_cron is provisioned locally and in CI via the cads-tools Postgres image. Plain Postgres images (e.g. Testcontainers) don't ship it, so skip rather than fail.
DO $$
BEGIN
    IF EXISTS (SELECT 1 FROM pg_available_extensions WHERE name = 'pg_cron') THEN
        CREATE EXTENSION IF NOT EXISTS pg_cron;
    ELSE
        RAISE NOTICE 'pg_cron is not available in this PostgreSQL image; skipping.';
    END IF;
END
$$;
