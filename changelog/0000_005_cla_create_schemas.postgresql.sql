-- liquibase formatted sql

-- changeset schema:0000-005-cla-create-schemas splitStatements:true

CREATE SCHEMA IF NOT EXISTS cla;
CREATE SCHEMA IF NOT EXISTS cla_audit;
CREATE SCHEMA IF NOT EXISTS cla_transactions;

COMMENT ON SCHEMA cla IS
'CLA core tables loaded from the CLA LIS extracts.';
COMMENT ON SCHEMA cla_audit IS
'Audit tables for CLA data changes.';
COMMENT ON SCHEMA cla_transactions IS
'CLA source transaction tables containing bulk (B) and migration stub (S) records.';
