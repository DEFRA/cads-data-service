-- liquibase formatted sql

-- changeset schema:0000-002-create-schemas-permissions splitStatements:true context:dev,test,ext-test,perf-test

GRANT USAGE, CREATE ON SCHEMA cts, cts_audit, cts_transactions, cads TO cads_data_service;

GRANT SELECT, INSERT, UPDATE, DELETE
    ON ALL TABLES IN SCHEMA cts, cts_audit, cts_transactions, cads
    TO cads_data_service;

GRANT USAGE, SELECT, UPDATE
    ON ALL SEQUENCES IN SCHEMA cts, cts_audit, cts_transactions, cads
    TO cads_data_service;

GRANT EXECUTE
    ON ALL FUNCTIONS IN SCHEMA cts, cts_audit, cts_transactions, cads
    TO cads_data_service;

ALTER DEFAULT PRIVILEGES IN SCHEMA cts, cts_audit, cts_transactions, cads
    GRANT SELECT, INSERT, UPDATE, DELETE ON TABLES TO cads_data_service;

ALTER DEFAULT PRIVILEGES IN SCHEMA cts, cts_audit, cts_transactions, cads
    GRANT USAGE, SELECT, UPDATE ON SEQUENCES TO cads_data_service;

ALTER DEFAULT PRIVILEGES IN SCHEMA cts, cts_audit, cts_transactions, cads
    GRANT EXECUTE ON FUNCTIONS TO cads_data_service;
