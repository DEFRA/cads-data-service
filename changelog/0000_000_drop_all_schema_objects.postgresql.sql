-- liquibase formatted sql

-- Ensure you manually run:
-- DELETE FROM databasechangeloglock;
-- DELETE FROM databasechangelog;

-- Remove old run-1 schedules at changeset application time. This is deliberately
-- outside all procedures and only targets this import's own job-name prefix.
DO $cron_cleanup$
DECLARE
    v_jobid bigint;
BEGIN
    IF to_regclass('cron.job') IS NOT NULL THEN
        FOR v_jobid IN EXECUTE
            'SELECT jobid FROM cron.job WHERE jobname LIKE ''cts-import-run-1-worker-%'''
        LOOP
            EXECUTE 'SELECT cron.unschedule($1)' USING v_jobid;
        END LOOP;
    END IF;
END;
$cron_cleanup$;

-- changeset schema:0000-000-drop-all-schema-1 objects splitStatements:false
DROP SCHEMA IF EXISTS cads CASCADE;

-- changeset schema:0000-000-drop-all-schema-2 objects splitStatements:false
DROP SCHEMA IF EXISTS cts CASCADE;

-- changeset schema:0000-000-drop-all-schema-3 objects splitStatements:false
DROP SCHEMA IF EXISTS cts_audit CASCADE;

-- changeset schema:0000-000-drop-all-schema-4 objects splitStatements:false
DROP SCHEMA IF EXISTS cts_transactions CASCADE;
