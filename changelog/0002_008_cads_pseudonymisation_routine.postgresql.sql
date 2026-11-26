-- liquibase formatted sql

-- changeset dev_db_build:0002-008-cads-pseudonymisation splitStatements:false
CREATE OR REPLACE PROCEDURE cads.pseudonymise_data()
LANGUAGE plpgsql
AS $procedure$
DECLARE
    v_table_name text;
    v_rows_updated bigint;
BEGIN
    v_table_name := 'cads.party';
    UPDATE cads.party
    SET first_name = CASE WHEN first_name IS NULL THEN NULL ELSE 'First_' || number::text END,
        last_name = CASE WHEN last_name IS NULL THEN NULL ELSE 'Last_' || number::text END,
        name = 'Party_' || number::text,
        mobile = CASE WHEN mobile IS NULL THEN NULL ELSE '07000' || lpad((number % 1000000)::text, 6, '0') END,
        landline = CASE WHEN landline IS NULL THEN NULL ELSE '01000' || lpad((number % 1000000)::text, 6, '0') END,
        email = CASE WHEN email IS NULL THEN NULL ELSE 'party_' || number::text || '@example.invalid' END;
    GET DIAGNOSTICS v_rows_updated = ROW_COUNT;
    RAISE NOTICE '%: % records updated', v_table_name, v_rows_updated;

    v_table_name := 'cads.location';
    UPDATE cads.location
    SET single_line_address = CASE WHEN single_line_address IS NULL THEN NULL ELSE 'Address_' || substr(md5(identifier), 1, 16) END,
        postcode = CASE WHEN postcode IS NULL THEN NULL ELSE 'ZZ0 0ZZ' END;
    GET DIAGNOSTICS v_rows_updated = ROW_COUNT;
    RAISE NOTICE '%: % records updated', v_table_name, v_rows_updated;

    v_table_name := 'cads.mi_user';
    UPDATE cads.mi_user
    SET external_subject = 'user_' || replace(user_id::text, '-', ''),
        display_name = 'User ' || substr(user_id::text, 1, 8),
        email = CASE WHEN email IS NULL THEN NULL ELSE 'user_' || substr(user_id::text, 1, 8) || '@example.invalid' END;
    GET DIAGNOSTICS v_rows_updated = ROW_COUNT;
    RAISE NOTICE '%: % records updated', v_table_name, v_rows_updated;
EXCEPTION
    WHEN OTHERS THEN
        RAISE EXCEPTION 'cads pseudonymisation failed while updating %: %', v_table_name, SQLERRM
            USING ERRCODE = SQLSTATE;
END;
$procedure$;

-- changeset dev_db_build:0002-008-cads-pseudonymisation-all splitStatements:false
CREATE OR REPLACE PROCEDURE cads.pseudonymise_all_data()
LANGUAGE plpgsql
AS $procedure$
BEGIN
    CALL cads.pseudonymise_data();
    CALL cads.cts_pseudonymise_data();
    CALL cads.cts_transactions_pseudonymise_data();
    CALL cads.cts_audit_pseudonymise_data();
EXCEPTION
    WHEN OTHERS THEN
        RAISE EXCEPTION 'All-schema pseudonymisation failed: %', SQLERRM
            USING ERRCODE = SQLSTATE;
END;
$procedure$;
