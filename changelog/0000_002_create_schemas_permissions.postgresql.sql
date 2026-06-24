-- liquibase formatted sql

-- changeset schema:0000-002-create-schemas-permissions splitStatements:true context:dev,test,ext-test,perf-test

-- Grant the application role (cads_data_service) full access to all non-public
-- schemas it reads from and writes to. The Liquibase migrations are applied by the
-- migration role (e.g. 'postgres' locally, a managed least-privilege role in CI),
-- but the running service connects as 'cads_data_service'
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

-- Ensure objects created by FUTURE migrations are also accessible.
-- ALTER DEFAULT PRIVILEGES with no "FOR ROLE" clause implicitly targets the current
-- migration role (current_user), so it works in every environment without requiring
-- membership of a hard-coded 'postgres' role — which CI is not a member of and which
-- previously caused SQLSTATE 42501 (insufficient privilege).
ALTER DEFAULT PRIVILEGES IN SCHEMA cts, cts_audit, cts_transactions, cads
    GRANT SELECT, INSERT, UPDATE, DELETE ON TABLES TO cads_data_service;

ALTER DEFAULT PRIVILEGES IN SCHEMA cts, cts_audit, cts_transactions, cads
    GRANT USAGE, SELECT, UPDATE ON SEQUENCES TO cads_data_service;

ALTER DEFAULT PRIVILEGES IN SCHEMA cts, cts_audit, cts_transactions, cads
    GRANT EXECUTE ON FUNCTIONS TO cads_data_service;