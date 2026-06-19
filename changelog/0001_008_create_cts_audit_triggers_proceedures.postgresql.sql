-- liquibase formatted sql

-- changeset schema:0001-008-cts-audit-triggers-proceedures splitStatements:false

CREATE OR REPLACE FUNCTION cts_audit.ct_audit_bulk_row_before_change()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
DECLARE
    audit_table regclass := TG_ARGV[0]::regclass;
    source_columns text;
    audit_trans_id bigint;
BEGIN
    IF COALESCE(BTRIM(OLD.record_type), '') <> 'B' THEN
        IF TG_OP = 'DELETE' THEN
            RETURN OLD;
        END IF;
        RETURN NEW;
    END IF;

    SELECT string_agg(quote_ident(a.attname), ', ' ORDER BY a.attnum)
    INTO source_columns
    FROM pg_attribute AS a
    WHERE a.attrelid = TG_RELID
      AND a.attnum > 0
      AND NOT a.attisdropped;

    audit_trans_id := CASE
        WHEN TG_OP = 'UPDATE' THEN NEW.trans_id
        ELSE OLD.trans_id
    END;

    EXECUTE format(
        'INSERT INTO %s (audit_action, audit_trans_id, audited_at, %s)
         SELECT $1, $2, clock_timestamp(), %s
         FROM jsonb_populate_record(NULL::%s, $3)',
        audit_table,
        source_columns,
        source_columns,
        TG_RELID::regclass
    )
    USING TG_OP, audit_trans_id, to_jsonb(OLD);

    IF TG_OP = 'DELETE' THEN
        RETURN OLD;
    END IF;
    RETURN NEW;
END;
$function$;

