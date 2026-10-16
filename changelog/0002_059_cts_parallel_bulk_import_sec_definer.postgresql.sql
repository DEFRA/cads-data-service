-- liquibase formatted sql

-- changeset MarkGent1:0002_059_01 splitStatements:false context:dev,test,ext-test,perf-test,prod
-- The service role (cads_data_service) has no access to the pg_cron "cron" schema.
-- Run the worker scheduling step with the owner's rights (the Liquibase DDL user,
-- which does have USAGE on "cron") so the service can start an import without
-- being granted anything on "cron" itself.
-- Only fixed worker jobs for a run in 'ready' / 'processing bulk' can be scheduled.
ALTER PROCEDURE cads.schedule_cts_parallel_bulk_workers(bigint)
    SECURITY DEFINER
    SET search_path = pg_catalog, cads;

REVOKE ALL ON PROCEDURE cads.schedule_cts_parallel_bulk_workers(bigint) FROM PUBLIC;
-- The service role only exists in the deployed environments (not in CI / local scratch databases).
DO $grant$
BEGIN
    IF EXISTS (SELECT 1 FROM pg_roles WHERE rolname = 'cads_data_service') THEN
        GRANT EXECUTE ON PROCEDURE cads.schedule_cts_parallel_bulk_workers(bigint) TO cads_data_service;
    END IF;
END
$grant$;