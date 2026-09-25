-- liquibase formatted sql

-- changeset cla:0005-003-pseudo-anonymisation splitStatements:false
-- Pseudonymises likely personal/contact text columns in CLA core and transaction tables.

CREATE OR REPLACE PROCEDURE cads.pseudo_anonymise_cla_data()
LANGUAGE plpgsql
AS $procedure$
DECLARE
    v_run_id bigint := nextval('cads.cla_pseudo_anonymisation_run_id_seq');
    t record;
    c record;
    v_sql text;
    v_rows bigint;
    v_total bigint;
BEGIN
    FOR t IN
        SELECT table_schema, table_name
        FROM information_schema.columns
        WHERE table_schema IN ('cla', 'cla_transactions')
        GROUP BY table_schema, table_name
        HAVING bool_or(
            data_type IN ('text', 'character varying', 'character')
            AND lower(column_name) ~ '(name|email|phone|mobile|address|postcode|post_code|comment|reference|username|title)'
            AND column_name <> 'trans_type'
        )
        ORDER BY table_schema, table_name
    LOOP
        INSERT INTO cads.cla_pseudo_anonymisation_progress (run_id, schema_name, table_name, status)
        VALUES (v_run_id, t.table_schema, t.table_name, 'PENDING')
        ON CONFLICT (run_id, schema_name, table_name) DO NOTHING;

        PERFORM cads.set_cla_pseudo_anonymisation_progress(v_run_id, t.table_schema, t.table_name, 'RUNNING');
        COMMIT;
        v_total := 0;

        BEGIN
            FOR c IN
                SELECT column_name, character_maximum_length
                FROM information_schema.columns
                WHERE table_schema = t.table_schema
                  AND table_name = t.table_name
                  AND data_type IN ('text', 'character varying', 'character')
                  AND column_name <> 'trans_type'
                  AND lower(column_name) ~ '(name|email|phone|mobile|address|postcode|post_code|comment|reference|username|title)'
                ORDER BY ordinal_position
            LOOP
                v_sql := format(
                    'UPDATE %I.%I SET %I = %s WHERE %I IS NOT NULL',
                    t.table_schema,
                    t.table_name,
                    c.column_name,
                    CASE
                        WHEN c.character_maximum_length IS NULL THEN
                            format('''CLA-'' || md5(coalesce(%I::text, ''''))', c.column_name)
                        ELSE
                            format('left(''CLA-'' || md5(coalesce(%I::text, '''')), %s)', c.column_name, c.character_maximum_length)
                    END,
                    c.column_name
                );
                EXECUTE v_sql;
                GET DIAGNOSTICS v_rows = ROW_COUNT;
                v_total := v_total + COALESCE(v_rows, 0);
            END LOOP;

            PERFORM cads.set_cla_pseudo_anonymisation_progress(
                v_run_id, t.table_schema, t.table_name, 'COMPLETED', v_total
            );
        EXCEPTION WHEN query_canceled THEN
            PERFORM cads.set_cla_pseudo_anonymisation_progress(
                v_run_id, t.table_schema, t.table_name, 'ERROR', NULL, SQLSTATE, SQLERRM
            );
        WHEN OTHERS THEN
            PERFORM cads.set_cla_pseudo_anonymisation_progress(
                v_run_id, t.table_schema, t.table_name, 'ERROR', NULL, SQLSTATE, SQLERRM
            );
        END;
        COMMIT;
    END LOOP;
END;
$procedure$;

