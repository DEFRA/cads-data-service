-- liquibase formatted sql

-- changeset schema:0000-003-truncate-all-tables
CALL cads.cts_audit_truncate_all_tables();
CALL cads.cts_transactions_truncate_all_tables();
CALL cads.cts_truncate_all_tables();
CALL cads.truncate_all_tables();
