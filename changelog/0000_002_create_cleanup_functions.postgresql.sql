-- liquibase formatted sql

-- changeset schema:0000-001-create_cleanup_functions endDelimiter:// context:local,dev,ext-test,perf-test

-- Function 1: Truncate tables in the specified or default schemas
-- SELECT public.truncate_schema_tables();
-- SELECT public.truncate_schema_tables(ARRAY['cts', 'cads']);

DROP FUNCTION IF EXISTS public.truncate_schema_tables(text[]);

CREATE OR REPLACE FUNCTION public.truncate_schema_tables(p_schemas text[] DEFAULT NULL)
    RETURNS void
    LANGUAGE plpgsql
AS $$
DECLARE
    v_table_names text;
    v_schemas     text[];
BEGIN
    -- Fall back to the default schema list when none is supplied
    v_schemas := COALESCE(p_schemas, ARRAY['public', 'cts', 'cts_audit', 'cts_transactions', 'cads']);

    -- Build a comma-separated, fully-qualified list of all base tables in the public schema
    SELECT string_agg(format('%I.%I', schemaname, tablename), ', ')
    INTO v_table_names
    FROM pg_tables
    WHERE schemaname = ANY(v_schemas);

    IF v_table_names IS NOT NULL THEN
        EXECUTE format('TRUNCATE TABLE %s RESTART IDENTITY CASCADE', v_table_names);
    END IF;
END;
$$;

-- Function 2: Truncate a data seed group of tables defined within the function
-- SELECT public.truncate_data_seed_tables();
DROP FUNCTION IF EXISTS public.truncate_data_seed_tables();

CREATE OR REPLACE FUNCTION public.truncate_data_seed_tables()
    RETURNS void
    LANGUAGE plpgsql
AS $$
DECLARE
    v_tables text[] := ARRAY[
        'cts.ct_cm_measures_results',
        'cts.ct_condition_markers',
        'cts.ct_susp_condition_markers',
        'cts.ct_received_applications',
        'cts.ct_suspended_movements',
        'cts.ct_suspended_animals',
        'cts.ct_received_movements',
        'cts.ct_issued_documents',
        'cts.ct_animal_statuses',
        'cts.ct_animal_relationships',
        'cts.ct_eartags',
        'cts.ct_registered_movements',
        'cts.ct_registered_animals',
        'cts.ct_valid_applications',
        'cts.ct_location_relationships',
        'cts.ct_location_party_rels',
        'cts.ct_location_identifiers',
        'cts.ct_parties',
        'cts.ct_locations',
        'public.data_seed_ingestion_history'
        ];
    v_qualified text;
    v_parts text[];
BEGIN
    FOREACH v_qualified IN ARRAY v_tables
        LOOP
            v_parts := parse_ident(v_qualified);
            -- Only truncate tables that actually exist to avoid errors
            IF EXISTS (
                SELECT 1
                FROM pg_tables
                WHERE schemaname = v_parts[1]
                  AND tablename = v_parts[2]
            ) THEN
                EXECUTE format('TRUNCATE TABLE %I.%I RESTART IDENTITY CASCADE', v_parts[1], v_parts[2]);
            END IF;
        END LOOP;
END;
$$;

-- Drops all tables, views, and functions (plus sequences, types, materialized views)
-- in a schema, WITHOUT dropping the schema itself.
-- Note: this is a destructive operation, use with caution. If applied to the public schema,
-- it will drop all objects in the database inlcuding this function.
-- SELECT public.clear_schema_objects('cts');
DROP FUNCTION IF EXISTS public.clear_schema_objects(text);

CREATE OR REPLACE FUNCTION public.clear_schema_objects(
    p_schema text
)
    RETURNS void
    LANGUAGE plpgsql
AS $$
DECLARE
    v_protected text[] := ARRAY['pg_catalog', 'information_schema', 'pg_toast'];
    r record;
BEGIN
    -- Guards
    IF p_schema IS NULL OR btrim(p_schema) = '' THEN
        RAISE EXCEPTION 'A schema name must be provided';
    END IF;

    IF p_schema = ANY(v_protected) THEN
        RAISE EXCEPTION 'Refusing to clear protected schema "%"', p_schema;
    END IF;

    IF NOT EXISTS (SELECT 1 FROM pg_namespace WHERE nspname = p_schema) THEN
        RAISE EXCEPTION 'Schema "%" does not exist', p_schema;
    END IF;

    -- 1. Views (drop before tables they may depend on; CASCADE handles ordering anyway)
    FOR r IN
        SELECT table_name
        FROM information_schema.views
        WHERE table_schema = p_schema
        LOOP
            EXECUTE format('DROP VIEW IF EXISTS %I.%I CASCADE', p_schema, r.table_name);
        END LOOP;

    -- 2. Materialized views (not covered by information_schema.views)
    FOR r IN
        SELECT matviewname AS name
        FROM pg_matviews
        WHERE schemaname = p_schema
        LOOP
            EXECUTE format('DROP MATERIALIZED VIEW IF EXISTS %I.%I CASCADE', p_schema, r.name);
        END LOOP;

    -- 3. Tables
    FOR r IN
        SELECT tablename
        FROM pg_tables
        WHERE schemaname = p_schema
        LOOP
            EXECUTE format('DROP TABLE IF EXISTS %I.%I CASCADE', p_schema, r.tablename);
        END LOOP;

    -- 4. Functions and procedures
    FOR r IN
        SELECT p.oid::regprocedure AS signature
        FROM pg_proc p
                 JOIN pg_namespace n ON n.oid = p.pronamespace
        WHERE n.nspname = p_schema
        LOOP
            EXECUTE format('DROP ROUTINE IF EXISTS %s CASCADE', r.signature);
        END LOOP;

    -- 5. Sequences not owned by a table (owned ones are dropped with their table)
    FOR r IN
        SELECT sequencename
        FROM pg_sequences
        WHERE schemaname = p_schema
        LOOP
            EXECUTE format('DROP SEQUENCE IF EXISTS %I.%I CASCADE', p_schema, r.sequencename);
        END LOOP;

    -- 6. User-defined types / domains (e.g. enums) — skip table row-types (typtype 'c' from tables)
    FOR r IN
        SELECT t.typname
        FROM pg_type t
                 JOIN pg_namespace n ON n.oid = t.typnamespace
        WHERE n.nspname = p_schema
          AND t.typtype IN ('e', 'd')          -- enums and domains; extend if needed
        LOOP
            EXECUTE format('DROP TYPE IF EXISTS %I.%I CASCADE', p_schema, r.typname);
        END LOOP;
END;
$$;