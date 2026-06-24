-- liquibase formatted sql

-- changeset schema:0000-002-create-schemas-permissions splitStatements:true context:dev,ext-test,perf-test

-- Grant the application role (cads_data_service) full access to all non-public
-- schemas it reads from and writes to. The Liquibase migrations are applied by the
-- 'postgres' superuser, but the running service connects as 'cads_data_service'
-- (Postgres__DefaultConnection), which only has explicitly granted privileges.
-- Without USAGE on these schemas the bulk SQL loader fails with:
--   SQLSTATE 42501 - permission denied for schema cts

-- Schema-level access (USAGE = resolve objects, CREATE = create/replace objects via the SQL loader)
GRANT USAGE, CREATE ON SCHEMA cts, cts_audit, cts_transactions, cads TO cads_data_service;

-- Existing tables
GRANT SELECT, INSERT, UPDATE, DELETE
    ON ALL TABLES IN SCHEMA cts, cts_audit, cts_transactions, cads
    TO cads_data_service;

-- Existing sequences (needed for INSERTs into serial/identity columns)
GRANT USAGE, SELECT, UPDATE
    ON ALL SEQUENCES IN SCHEMA cts, cts_audit, cts_transactions, cads
    TO cads_data_service;

-- Existing functions (e.g. cads.get_imports_summary and other report functions)
GRANT EXECUTE
    ON ALL FUNCTIONS IN SCHEMA cts, cts_audit, cts_transactions, cads
    TO cads_data_service;