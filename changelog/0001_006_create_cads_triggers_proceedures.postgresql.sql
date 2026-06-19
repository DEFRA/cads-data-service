-- liquibase formatted sql

-- changeset schema:0001-006-cads-triggers-proceedures splitStatements:false

ALTER TABLE cts.ct_received_applications
    ADD COLUMN IF NOT EXISTS trans_id bigint;

CREATE OR REPLACE FUNCTION cads.cts_file_imports_prevent_bulk_processing()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
DECLARE
    v_processing_start_count integer;
BEGIN
    IF NEW.processing_status_id = 2 THEN
        v_processing_start_count :=
            COALESCE(NULLIF(current_setting('cts.processing_start_count', true), ''), '0')::integer + 1;

        PERFORM set_config('cts.processing_start_count', v_processing_start_count::text, true);

        IF v_processing_start_count > 1 THEN
            RAISE EXCEPTION
                'Only one cts_file_imports row can be moved to processing at a time in a single statement';
        END IF;
    END IF;

    RETURN NEW;
END;
$function$;

CREATE OR REPLACE FUNCTION cads.cts_file_imports_process_received_applications()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
BEGIN
    IF COALESCE(current_setting('cts.processing_received_applications', true), 'off') <> 'on'
       AND NEW.destination_table_name = 'cts_transactions.ct_received_applications'
       AND NEW.processing_status_id = 2
       AND OLD.processing_status_id IS DISTINCT FROM NEW.processing_status_id THEN
        CALL cads.process_ct_trans_received_applications(NEW.cts_file_import_id);
    END IF;

    RETURN NEW;
END;
$function$;

CREATE OR REPLACE FUNCTION cads.cts_file_imports_reset_processing_counter()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
BEGIN
    PERFORM set_config('cts.processing_start_count', '0', true);
    RETURN NULL;
END;
$function$;

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

    IF NEW.import_status_id IN (3, 4)
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

CREATE OR REPLACE PROCEDURE cads.demo_import_received_applications()
 LANGUAGE plpgsql
AS $procedure$ DECLARE v_cts_file_import_id bigint; v_rows_inserted integer; BEGIN ALTER TABLE cts.ct_received_applications DROP CONSTRAINT IF EXISTS ct_received_applications_trans_id_fkey; DELETE FROM cts.ct_received_applications WHERE rap_id IN (167143113, 167231259); TRUNCATE cts_transactions.ct_received_applications, cts_audit.ct_received_applications RESTART IDENTITY; ALTER TABLE cts.ct_received_applications ADD CONSTRAINT ct_received_applications_trans_id_fkey FOREIGN KEY (trans_id) REFERENCES cts_transactions.ct_received_applications (trans_id); INSERT INTO cts.ct_received_applications (rap_id, rap_current_user, rap_current_status, rap_current_modified_date, rap_current_pid, rap_application_type, rap_applic_receipt_date, rap_applic_target_date, rap_intended_action, row_number, record_type, record_count, imported_date) VALUES (167143113, 'ORIGINAL', 'M', CURRENT_DATE - 1, 1, 'B', CURRENT_DATE - 1, CURRENT_DATE + 7, 'I', 1, 'B', 1, CURRENT_TIMESTAMP), (167231259, 'ORIGINAL', 'M', CURRENT_DATE - 1, 1, 'B', CURRENT_DATE - 1, CURRENT_DATE + 7, 'I', 2, 'B', 2, CURRENT_TIMESTAMP); INSERT INTO cads.cts_file_imports (destination_table_name, file_name, rows_found, total_rows_to_process, import_status_id, processing_status_id) VALUES ('cts_transactions.ct_received_applications', 'CTSM_UKV_PROD_BULK_123456_CT_RECEIVED_APPLICATIONS_2026-02-22-074604.csv', 2, 2, 2, 1) RETURNING cts_file_import_id INTO v_cts_file_import_id; WITH requested_rap_ids AS ( SELECT * FROM (VALUES (167143113::numeric, 1::numeric), (167231259::numeric, 2::numeric)) AS ids(rap_id, demo_row_number) ), existing_row AS ( SELECT r.demo_row_number, a.* FROM requested_rap_ids r JOIN cts.ct_received_applications a ON a.rap_id = r.rap_id ), current_trans_count AS ( SELECT count(*) AS existing_count FROM cts_transactions.ct_received_applications ) INSERT INTO cts_transactions.ct_received_applications (cts_file_import_id, trans_type, rap_id, rap_current_user, rap_current_status, rap_current_modified_date, rap_current_pid, rap_application_type, rap_applic_receipt_date, rap_applic_target_date, rap_intended_action, rap_amended_by, rap_amended_datetime, row_number, record_type, record_count, imported_date) SELECT v_cts_file_import_id, 'U', r.rap_id, 'GAZZA', 'UP', CURRENT_DATE, r.rap_current_pid, r.rap_application_type, r.rap_applic_receipt_date, r.rap_applic_target_date, 'U', 'GAZZA', CURRENT_TIMESTAMP, r.row_number, 'U', c.existing_count + r.demo_row_number, CURRENT_TIMESTAMP FROM existing_row r CROSS JOIN current_trans_count c; GET DIAGNOSTICS v_rows_inserted = ROW_COUNT; UPDATE cads.cts_file_imports SET rows_found = v_rows_inserted, total_rows_to_process = v_rows_inserted WHERE cts_file_import_id = v_cts_file_import_id; IF v_rows_inserted = 0 THEN UPDATE cads.cts_file_imports SET import_status_id = 4, processing_status_id = 4 WHERE cts_file_import_id = v_cts_file_import_id; RAISE NOTICE 'No transaction rows inserted; cts_file_import_id % marked as error', v_cts_file_import_id; RETURN; END IF; UPDATE cads.cts_file_imports SET import_status_id = 3 WHERE cts_file_import_id = v_cts_file_import_id; UPDATE cads.cts_file_imports SET processing_status_id = 2 WHERE cts_file_import_id = v_cts_file_import_id; RAISE NOTICE 'Inserted % transaction row(s); cts_file_import_id % queued for processing', v_rows_inserted, v_cts_file_import_id; END; $procedure$;

CREATE OR REPLACE PROCEDURE cads.process_ct_trans_received_applications(IN p_cts_file_import_id bigint)
 LANGUAGE plpgsql
AS $procedure$
DECLARE
    v_expected_rows bigint;
    v_actual_rows bigint;
    v_inserted_rows bigint := 0;
    v_updated_rows bigint := 0;
    v_deleted_rows bigint := 0;
    v_processing_started_at timestamp with time zone;
    v_processing_ended_at timestamp with time zone;
    v_insert_started_at timestamp with time zone;
    v_insert_ended_at timestamp with time zone;
    v_update_started_at timestamp with time zone;
    v_update_ended_at timestamp with time zone;
    v_delete_started_at timestamp with time zone;
    v_delete_ended_at timestamp with time zone;
    v_error_message text;
    v_error_detail text;
    v_error_hint text;
    v_error_context text;
    v_full_error_message text;
BEGIN
    PERFORM set_config('cts.processing_received_applications', 'on', true);

    SELECT total_rows_to_process
    INTO v_expected_rows
    FROM cads.cts_file_imports
    WHERE cts_file_import_id = p_cts_file_import_id;

    IF v_expected_rows IS NULL THEN
        RAISE EXCEPTION 'cts_file_imports row % does not exist', p_cts_file_import_id;
    END IF;

    IF EXISTS (
        SELECT 1
        FROM cads.cts_file_imports
        WHERE processing_status_id = 2
          AND cts_file_import_id <> p_cts_file_import_id
    ) THEN
        INSERT INTO cads.cts_file_imports_log (
            cts_file_import_id,
            log_level,
            log_message,
            expected_records,
            processed_records
        )
        VALUES (
            p_cts_file_import_id,
            'warning',
            'Processing request ignored because another cts_file_imports batch is already processing.',
            v_expected_rows,
            0
        );

        UPDATE cads.cts_file_imports
        SET processing_status_id = 1
        WHERE cts_file_import_id = p_cts_file_import_id;

        RETURN;
    END IF;

    SELECT count(*)
    INTO v_actual_rows
    FROM cts_transactions.ct_received_applications
    WHERE cts_file_import_id = p_cts_file_import_id;

    IF v_actual_rows <> v_expected_rows THEN
        UPDATE cads.cts_file_imports
        SET processing_status_id = 4
        WHERE cts_file_import_id = p_cts_file_import_id;

        INSERT INTO cads.cts_file_imports_log (
            cts_file_import_id,
            log_level,
            log_message,
            error_message,
            expected_records,
            processed_records
        )
        VALUES (
            p_cts_file_import_id,
            'error',
            'Batch row-count validation failed before processing started.',
            format('Expected %s cts_transactions.ct_received_applications row(s), found %s.', v_expected_rows, v_actual_rows),
            v_expected_rows,
            v_actual_rows
        );

        RETURN;
    END IF;

    v_processing_started_at := clock_timestamp();

    UPDATE cads.cts_file_imports
    SET processing_status_id = 2
    WHERE cts_file_import_id = p_cts_file_import_id
      AND processing_status_id IS DISTINCT FROM 2;

    BEGIN
        v_insert_started_at := clock_timestamp();

        INSERT INTO cts.ct_received_applications (
            "rap_id",
            "rap_current_user",
            "rap_current_status",
            "rap_current_modified_date",
            "rap_current_pid",
            "rap_application_type",
            "rap_applic_receipt_date",
            "rap_applic_target_date",
            "rap_cts_indicator",
            "rap_eartag_type",
            "rap_eartag",
            "rap_source_type",
            "rap_source_reference",
            "rap_request_loc_type",
            "rap_request_loc_identifier",
            "rap_request_subloc_identifier",
            "rap_genetic_dam_et_type",
            "rap_genetic_dam_eartag",
            "rap_surr_dam_et_type",
            "rap_surr_dam_eartag",
            "rap_sire_et_type",
            "rap_sire_eartag",
            "rap_birth_date",
            "rap_placement_date",
            "rap_breed",
            "rap_sex",
            "rap_initial_loc_type",
            "rap_initial_loc_identifier",
            "rap_initial_subloc_identifier",
            "rap_country_of_origin",
            "rap_health_certificate_no",
            "rap_import_identifier",
            "rap_electronic_identifier",
            "rap_new_eartag_type",
            "rap_new_eartag",
            "rap_number_calf_movts",
            "rap_wgp_id",
            "rap_interface_file_name",
            "rap_interface_file_txn",
            "rap_orig_if_file_name",
            "rap_orig_if_file_txn",
            "rap_chr_correction_type",
            "rap_chr_location_ind",
            "rap_created_date",
            "rap_intended_action",
            "rap_amended_by",
            "rap_amended_datetime",
            "rap_submit_datetime",
            "rap_originator",
            "rap_ran_id_reserved",
            "rap_version",
            "rap_request_letter",
            "rap_reminder_letter",
            "rap_refused_letter",
            "row_number",
            "record_type",
            "record_count",
            "imported_date",
            "trans_id"
        )
        SELECT
            src."rap_id",
            src."rap_current_user",
            src."rap_current_status",
            src."rap_current_modified_date",
            src."rap_current_pid",
            src."rap_application_type",
            src."rap_applic_receipt_date",
            src."rap_applic_target_date",
            src."rap_cts_indicator",
            src."rap_eartag_type",
            src."rap_eartag",
            src."rap_source_type",
            src."rap_source_reference",
            src."rap_request_loc_type",
            src."rap_request_loc_identifier",
            src."rap_request_subloc_identifier",
            src."rap_genetic_dam_et_type",
            src."rap_genetic_dam_eartag",
            src."rap_surr_dam_et_type",
            src."rap_surr_dam_eartag",
            src."rap_sire_et_type",
            src."rap_sire_eartag",
            src."rap_birth_date",
            src."rap_placement_date",
            src."rap_breed",
            src."rap_sex",
            src."rap_initial_loc_type",
            src."rap_initial_loc_identifier",
            src."rap_initial_subloc_identifier",
            src."rap_country_of_origin",
            src."rap_health_certificate_no",
            src."rap_import_identifier",
            src."rap_electronic_identifier",
            src."rap_new_eartag_type",
            src."rap_new_eartag",
            src."rap_number_calf_movts",
            src."rap_wgp_id",
            src."rap_interface_file_name",
            src."rap_interface_file_txn",
            src."rap_orig_if_file_name",
            src."rap_orig_if_file_txn",
            src."rap_chr_correction_type",
            src."rap_chr_location_ind",
            src."rap_created_date",
            src."rap_intended_action",
            src."rap_amended_by",
            src."rap_amended_datetime",
            src."rap_submit_datetime",
            src."rap_originator",
            src."rap_ran_id_reserved",
            src."rap_version",
            src."rap_request_letter",
            src."rap_reminder_letter",
            src."rap_refused_letter",
            src."row_number",
            src."record_type",
            src."record_count",
            src."imported_date",
            src.trans_id
        FROM cts_transactions.ct_received_applications AS src
        WHERE src.cts_file_import_id = p_cts_file_import_id
          AND src.trans_type = 'I';

        GET DIAGNOSTICS v_inserted_rows = ROW_COUNT;
        v_insert_ended_at := clock_timestamp();

        v_update_started_at := clock_timestamp();

        WITH ordered_updates AS (
            SELECT
                src.*,
                row_number() OVER (
                    PARTITION BY src.rap_id
                    ORDER BY src.row_number DESC NULLS LAST
                ) AS update_rank
            FROM cts_transactions.ct_received_applications AS src
            WHERE src.cts_file_import_id = p_cts_file_import_id
              AND src.trans_type = 'U'
        ),
        latest_update AS (
            SELECT *
            FROM ordered_updates
            WHERE update_rank = 1
        )
        UPDATE cts.ct_received_applications AS target
        SET "rap_current_user" = src."rap_current_user",
            "rap_current_status" = src."rap_current_status",
            "rap_current_modified_date" = src."rap_current_modified_date",
            "rap_current_pid" = src."rap_current_pid",
            "rap_application_type" = src."rap_application_type",
            "rap_applic_receipt_date" = src."rap_applic_receipt_date",
            "rap_applic_target_date" = src."rap_applic_target_date",
            "rap_cts_indicator" = src."rap_cts_indicator",
            "rap_eartag_type" = src."rap_eartag_type",
            "rap_eartag" = src."rap_eartag",
            "rap_source_type" = src."rap_source_type",
            "rap_source_reference" = src."rap_source_reference",
            "rap_request_loc_type" = src."rap_request_loc_type",
            "rap_request_loc_identifier" = src."rap_request_loc_identifier",
            "rap_request_subloc_identifier" = src."rap_request_subloc_identifier",
            "rap_genetic_dam_et_type" = src."rap_genetic_dam_et_type",
            "rap_genetic_dam_eartag" = src."rap_genetic_dam_eartag",
            "rap_surr_dam_et_type" = src."rap_surr_dam_et_type",
            "rap_surr_dam_eartag" = src."rap_surr_dam_eartag",
            "rap_sire_et_type" = src."rap_sire_et_type",
            "rap_sire_eartag" = src."rap_sire_eartag",
            "rap_birth_date" = src."rap_birth_date",
            "rap_placement_date" = src."rap_placement_date",
            "rap_breed" = src."rap_breed",
            "rap_sex" = src."rap_sex",
            "rap_initial_loc_type" = src."rap_initial_loc_type",
            "rap_initial_loc_identifier" = src."rap_initial_loc_identifier",
            "rap_initial_subloc_identifier" = src."rap_initial_subloc_identifier",
            "rap_country_of_origin" = src."rap_country_of_origin",
            "rap_health_certificate_no" = src."rap_health_certificate_no",
            "rap_import_identifier" = src."rap_import_identifier",
            "rap_electronic_identifier" = src."rap_electronic_identifier",
            "rap_new_eartag_type" = src."rap_new_eartag_type",
            "rap_new_eartag" = src."rap_new_eartag",
            "rap_number_calf_movts" = src."rap_number_calf_movts",
            "rap_wgp_id" = src."rap_wgp_id",
            "rap_interface_file_name" = src."rap_interface_file_name",
            "rap_interface_file_txn" = src."rap_interface_file_txn",
            "rap_orig_if_file_name" = src."rap_orig_if_file_name",
            "rap_orig_if_file_txn" = src."rap_orig_if_file_txn",
            "rap_chr_correction_type" = src."rap_chr_correction_type",
            "rap_chr_location_ind" = src."rap_chr_location_ind",
            "rap_created_date" = src."rap_created_date",
            "rap_intended_action" = src."rap_intended_action",
            "rap_amended_by" = src."rap_amended_by",
            "rap_amended_datetime" = src."rap_amended_datetime",
            "rap_submit_datetime" = src."rap_submit_datetime",
            "rap_originator" = src."rap_originator",
            "rap_ran_id_reserved" = src."rap_ran_id_reserved",
            "rap_version" = src."rap_version",
            "rap_request_letter" = src."rap_request_letter",
            "rap_reminder_letter" = src."rap_reminder_letter",
            "rap_refused_letter" = src."rap_refused_letter",
            "row_number" = src."row_number",
            "record_type" = src."record_type",
            "record_count" = src."record_count",
            "imported_date" = src."imported_date",
            "trans_id" = src.trans_id
        FROM latest_update AS src
        WHERE target.rap_id = src.rap_id;

        GET DIAGNOSTICS v_updated_rows = ROW_COUNT;
        v_update_ended_at := clock_timestamp();

        v_delete_started_at := clock_timestamp();

        DELETE FROM cts.ct_received_applications AS target
        USING (
            SELECT DISTINCT rap_id
            FROM cts_transactions.ct_received_applications
            WHERE cts_file_import_id = p_cts_file_import_id
              AND trans_type = 'D'
        ) AS src
        WHERE target.rap_id = src.rap_id;

        GET DIAGNOSTICS v_deleted_rows = ROW_COUNT;
        v_delete_ended_at := clock_timestamp();

    EXCEPTION
        WHEN OTHERS THEN
            GET STACKED DIAGNOSTICS
                v_error_message = MESSAGE_TEXT,
                v_error_detail = PG_EXCEPTION_DETAIL,
                v_error_hint = PG_EXCEPTION_HINT,
                v_error_context = PG_EXCEPTION_CONTEXT;

            v_processing_ended_at := clock_timestamp();
            v_full_error_message := concat_ws(E'',
                v_error_message,
                NULLIF('DETAIL: ' || COALESCE(v_error_detail, ''), 'DETAIL: '),
                NULLIF('HINT: ' || COALESCE(v_error_hint, ''), 'HINT: '),
                NULLIF('CONTEXT: ' || COALESCE(v_error_context, ''), 'CONTEXT: ')
            );

            UPDATE cads.cts_file_imports
            SET processing_status_id = 4
            WHERE cts_file_import_id = p_cts_file_import_id;

            INSERT INTO cads.cts_file_imports_log (
                cts_file_import_id,
                log_level,
                log_message,
                error_message,
                expected_records,
                processed_records,
                inserted_records,
                updated_records,
                deleted_records,
                processing_started_at,
                processing_ended_at,
                insert_started_at,
                insert_ended_at,
                update_started_at,
                update_ended_at,
                delete_started_at,
                delete_ended_at
            )
            VALUES (
                p_cts_file_import_id,
                'error',
                'Batch processing failed. Processing data changes were rolled back.',
                v_full_error_message,
                v_expected_rows,
                v_inserted_rows + v_updated_rows + v_deleted_rows,
                v_inserted_rows,
                v_updated_rows,
                v_deleted_rows,
                v_processing_started_at,
                v_processing_ended_at,
                v_insert_started_at,
                v_insert_ended_at,
                v_update_started_at,
                v_update_ended_at,
                v_delete_started_at,
                v_delete_ended_at
            );

            RETURN;
    END;

    v_processing_ended_at := clock_timestamp();

    UPDATE cads.cts_file_imports
    SET processing_status_id = 3
    WHERE cts_file_import_id = p_cts_file_import_id;

    INSERT INTO cads.cts_file_imports_log (
        cts_file_import_id,
        log_level,
        log_message,
        expected_records,
        processed_records,
        inserted_records,
        updated_records,
        deleted_records,
        processing_started_at,
        processing_ended_at,
        insert_started_at,
        insert_ended_at,
        update_started_at,
        update_ended_at,
        delete_started_at,
        delete_ended_at
    )
    VALUES (
        p_cts_file_import_id,
        'info',
        'Batch processing completed.',
        v_expected_rows,
        v_inserted_rows + v_updated_rows + v_deleted_rows,
        v_inserted_rows,
        v_updated_rows,
        v_deleted_rows,
        v_processing_started_at,
        v_processing_ended_at,
        v_insert_started_at,
        v_insert_ended_at,
        v_update_started_at,
        v_update_ended_at,
        v_delete_started_at,
        v_delete_ended_at
    );

    RAISE NOTICE 'Processed batch %: inserted %, updated %, deleted %',
        p_cts_file_import_id,
        v_inserted_rows,
        v_updated_rows,
        v_deleted_rows;
END;
$procedure$;

DROP TRIGGER IF EXISTS cts_file_imports_prevent_bulk_processing_trg ON cads.cts_file_imports;
CREATE TRIGGER cts_file_imports_prevent_bulk_processing_trg
    BEFORE UPDATE OF processing_status_id ON cads.cts_file_imports
    FOR EACH ROW
    EXECUTE FUNCTION cads.cts_file_imports_prevent_bulk_processing();

DROP TRIGGER IF EXISTS cts_file_imports_process_received_applications_trg ON cads.cts_file_imports;
CREATE TRIGGER cts_file_imports_process_received_applications_trg
    AFTER UPDATE OF processing_status_id ON cads.cts_file_imports
    FOR EACH ROW
    WHEN (
        COALESCE(current_setting('cts.processing_received_applications'::text, true), 'off'::text) <> 'on'::text
        AND new.destination_table_name = 'cts_transactions.ct_received_applications'::text
        AND new.processing_status_id = 2
        AND old.processing_status_id IS DISTINCT FROM new.processing_status_id
    )
    EXECUTE FUNCTION cads.cts_file_imports_process_received_applications();

DROP TRIGGER IF EXISTS cts_file_imports_reset_processing_counter_trg ON cads.cts_file_imports;
CREATE TRIGGER cts_file_imports_reset_processing_counter_trg
    BEFORE UPDATE OF processing_status_id ON cads.cts_file_imports
    FOR EACH STATEMENT
    EXECUTE FUNCTION cads.cts_file_imports_reset_processing_counter();

DROP TRIGGER IF EXISTS cts_file_imports_set_status_timestamps_trg ON cads.cts_file_imports;
CREATE TRIGGER cts_file_imports_set_status_timestamps_trg
    BEFORE INSERT OR UPDATE OF import_status_id, processing_status_id ON cads.cts_file_imports
    FOR EACH ROW
    EXECUTE FUNCTION cads.cts_file_imports_set_status_timestamps();
