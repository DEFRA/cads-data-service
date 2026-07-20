-- liquibase formatted sql

-- changeset schema:0001-020-fix-cts-file-imports-set-status-timestamps splitStatements:false

-- Fix: import_end_at should only be set for Complete (4) and Error (5) statuses.
-- Previously included Split (3) which is an intermediate state, not an end state.
CREATE OR REPLACE FUNCTION cads.cts_file_imports_set_status_timestamps()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
BEGIN
    IF NEW.import_status_id = 2
       AND (TG_OP = 'INSERT' OR OLD.import_status_id IS DISTINCT FROM NEW.import_status_id)
       AND NEW.import_start_at IS NULL THEN
        NEW.import_start_at := clock_timestamp();
    END IF;

    IF NEW.import_status_id IN (4, 5)
       AND (TG_OP = 'INSERT' OR OLD.import_status_id IS DISTINCT FROM NEW.import_status_id)
       AND NEW.import_end_at IS NULL THEN
        NEW.import_end_at := clock_timestamp();
    END IF;

    IF NEW.processing_status_id = 2
       AND (TG_OP = 'INSERT' OR OLD.processing_status_id IS DISTINCT FROM NEW.processing_status_id)
       AND NEW.processing_start_at IS NULL THEN
        NEW.processing_start_at := clock_timestamp();
    END IF;

    IF NEW.processing_status_id IN (3, 4)
       AND (TG_OP = 'INSERT' OR OLD.processing_status_id IS DISTINCT FROM NEW.processing_status_id)
       AND NEW.processing_end_at IS NULL THEN
        NEW.processing_end_at := clock_timestamp();
    END IF;

    RETURN NEW;
END;
$function$;
