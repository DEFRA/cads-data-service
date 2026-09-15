-- liquibase formatted sql

-- changeset schema:0000-000-drop-all-schema-1 objects splitStatements:false
DROP SCHEMA IF EXISTS cads CASCADE;

-- changeset schema:0000-000-drop-all-schema-2 objects splitStatements:false
DROP SCHEMA IF EXISTS cts CASCADE;

-- changeset schema:0000-000-drop-all-schema-3 objects splitStatements:false
DROP SCHEMA IF EXISTS cts_audit CASCADE;

-- changeset schema:0000-000-drop-all-schema-4 objects splitStatements:false
DROP SCHEMA IF EXISTS cts_transactions CASCADE;
