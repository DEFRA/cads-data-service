-- liquibase formatted sql

-- changeset schema:0000-040-cla-audit-tables splitStatements:true

-- The CLA source specification supplies no audit-row layout. Create the managed schema now;
-- audit tables can be added in a later migration once the audit contract is defined.
CREATE SCHEMA IF NOT EXISTS cla_audit;
