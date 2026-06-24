-- liquibase formatted sql

-- changeset schema:0000-002-create-schemas-permissions splitStatements:true context:dev,ext-test,perf-test

CREATE SCHEMA IF NOT EXISTS cts;
CREATE SCHEMA IF NOT EXISTS cts_audit;
CREATE SCHEMA IF NOT EXISTS cts_transactions;
CREATE SCHEMA IF NOT EXISTS cads;

COMMENT ON SCHEMA cts IS
'Original CTS bulk-load tables. These hold the baseline CTS records loaded from source extracts.';

COMMENT ON SCHEMA cts_audit IS
'Audit copies of original CTS bulk-load rows before they are updated or deleted by transaction processing.';

COMMENT ON SCHEMA cts_transactions IS
'Daily CTS transaction tables containing inserts, updates, and deletes to be applied to the CTS bulk-load data.';

COMMENT ON SCHEMA cads IS
'CADS application tables, separate from CTS source, audit, and transaction structures.';

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

-- Ensure objects created by FUTURE migrations (run as 'postgres') are also accessible.
-- ALTER DEFAULT PRIVILEGES only applies to the creating role, hence FOR ROLE postgres.
ALTER DEFAULT PRIVILEGES FOR ROLE postgres IN SCHEMA cts, cts_audit, cts_transactions, cads
    GRANT SELECT, INSERT, UPDATE, DELETE ON TABLES TO cads_data_service;

ALTER DEFAULT PRIVILEGES FOR ROLE postgres IN SCHEMA cts, cts_audit, cts_transactions, cads
    GRANT USAGE, SELECT, UPDATE ON SEQUENCES TO cads_data_service;