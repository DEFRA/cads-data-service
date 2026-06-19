-- liquibase formatted sql

-- changeset schema:0000-000-create-schemas splitStatements:true

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
