-- liquibase formatted sql

-- changeset schema:0002-029-cts-transactions-truncate-tables-procedure splitStatements:false
CREATE OR REPLACE PROCEDURE cads.cts_transactions_truncate_all_tables()
LANGUAGE plpgsql
AS $procedure$
DECLARE
    v_tables text;
BEGIN
    SELECT string_agg(format('%I.%I', schemaname, tablename), ', ' ORDER BY tablename)
    INTO v_tables
    FROM pg_tables
    WHERE schemaname = 'cts_transactions';

    IF v_tables IS NOT NULL THEN
        EXECUTE 'TRUNCATE TABLE ' || v_tables || ' RESTART IDENTITY CASCADE';
    END IF;
END;
$procedure$;
