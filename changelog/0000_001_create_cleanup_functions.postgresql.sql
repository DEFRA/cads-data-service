-- liquibase formatted sql

-- changeset schema:0000-001-create_cleanup_functions endDelimiter:// context:local,dev,ext-test,perf-test

-- Function 1: Truncate ALL tables in the public schema
DROP FUNCTION IF EXISTS public.truncate_all_tables();

CREATE OR REPLACE FUNCTION public.truncate_all_tables()
    RETURNS void
    LANGUAGE plpgsql
AS $$
DECLARE
    v_table_names text;
BEGIN
    -- Build a comma-separated, fully-qualified list of all base tables in the public schema
    SELECT string_agg(format('%I.%I', schemaname, tablename), ', ')
    INTO v_table_names
    FROM pg_tables
    WHERE schemaname = 'public';

    IF v_table_names IS NOT NULL THEN
        EXECUTE format('TRUNCATE TABLE %s RESTART IDENTITY CASCADE', v_table_names);
    END IF;
END;
$$;

-- Function 2: Truncate a data seed group of tables defined within the function
DROP FUNCTION IF EXISTS public.truncate_data_seed_tables();

CREATE OR REPLACE FUNCTION public.truncate_data_seed_tables()
    RETURNS void
    LANGUAGE plpgsql
AS $$
DECLARE
    v_tables text[] := ARRAY[
        '_ct_cm_measures_results',
        '_ct_condition_markers',
        '_ct_susp_condition_markers',
        '_ct_received_applications',
        '_ct_suspended_movements',
        '_ct_suspended_animals',
        '_ct_received_movements',
        '_ct_issued_documents',
        '_ct_animal_statuses',
        '_ct_animal_relationships',
        '_ct_eartags',
        '_ct_registered_movements',
        '_ct_registered_animals',
        '_ct_valid_applications',
        '_ct_location_relationships',
        '_ct_location_party_rels',
        '_ct_location_identifiers',
        '_ct_parties',
        '_ct_locations',
        'data_seed_ingestion_history'
        ];
    v_table_name text;
BEGIN
    FOREACH v_table_name IN ARRAY v_tables
        LOOP
            -- Only truncate tables that actually exist to avoid errors
            IF EXISTS (
                SELECT 1
                FROM pg_tables
                WHERE schemaname = 'public'
                  AND tablename = v_table_name
            ) THEN
                EXECUTE format('TRUNCATE TABLE %I.%I RESTART IDENTITY CASCADE', 'public', v_table_name);
            END IF;
        END LOOP;
END;
$$;