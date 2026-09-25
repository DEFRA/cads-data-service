-- liquibase formatted sql

-- changeset schema:0000-006-cla-create-schemas-permissions splitStatements:true context:dev,test,ext-test,perf-test

GRANT USAGE, CREATE ON SCHEMA cla, cla_audit, cla_transactions TO cads_data_service;

GRANT SELECT, INSERT, UPDATE, DELETE
    ON ALL TABLES IN SCHEMA cla, cla_audit, cla_transactions
    TO cads_data_service;

GRANT USAGE, SELECT, UPDATE
    ON ALL SEQUENCES IN SCHEMA cla, cla_audit, cla_transactions
    TO cads_data_service;

ALTER DEFAULT PRIVILEGES IN SCHEMA cla, cla_audit, cla_transactions
    GRANT SELECT, INSERT, UPDATE, DELETE ON TABLES TO cads_data_service;

ALTER DEFAULT PRIVILEGES IN SCHEMA cla, cla_audit, cla_transactions
    GRANT USAGE, SELECT, UPDATE ON SEQUENCES TO cads_data_service;
