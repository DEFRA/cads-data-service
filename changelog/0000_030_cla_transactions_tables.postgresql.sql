-- liquibase formatted sql

-- changeset schema:0000-030-cla-transactions-tables splitStatements:false

-- CLA transaction tables mirror the core columns and carry the CTS-compatible transaction metadata.
-- trans_id identifies the source transaction row; trans_type is B (bulk) or S (stub).

CREATE SCHEMA IF NOT EXISTS cla_transactions;

CREATE TABLE IF NOT EXISTS cla_transactions.aspnetroles
    (LIKE cla.aspnetroles INCLUDING DEFAULTS);

DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM pg_constraint
        WHERE conrelid = 'cla_transactions.aspnetroles'::regclass
          AND contype = 'p'
    ) THEN
        ALTER TABLE cla_transactions.aspnetroles
            ALTER COLUMN trans_id SET NOT NULL;
        ALTER TABLE cla_transactions.aspnetroles
            ADD CONSTRAINT aspnetroles_trans_id_pkey PRIMARY KEY (trans_id);
    END IF;
END
$$;

CREATE TABLE IF NOT EXISTS cla_transactions.aspnetuserroles
    (LIKE cla.aspnetuserroles INCLUDING DEFAULTS);

DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM pg_constraint
        WHERE conrelid = 'cla_transactions.aspnetuserroles'::regclass
          AND contype = 'p'
    ) THEN
        ALTER TABLE cla_transactions.aspnetuserroles
            ALTER COLUMN trans_id SET NOT NULL;
        ALTER TABLE cla_transactions.aspnetuserroles
            ADD CONSTRAINT aspnetuserroles_trans_id_pkey PRIMARY KEY (trans_id);
    END IF;
END
$$;

CREATE TABLE IF NOT EXISTS cla_transactions.tbl_batchmovement
    (LIKE cla.tbl_batchmovement INCLUDING DEFAULTS);

DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM pg_constraint
        WHERE conrelid = 'cla_transactions.tbl_batchmovement'::regclass
          AND contype = 'p'
    ) THEN
        ALTER TABLE cla_transactions.tbl_batchmovement
            ALTER COLUMN trans_id SET NOT NULL;
        ALTER TABLE cla_transactions.tbl_batchmovement
            ADD CONSTRAINT tbl_batchmovement_trans_id_pkey PRIMARY KEY (trans_id);
    END IF;
END
$$;

CREATE TABLE IF NOT EXISTS cla_transactions.tbl_batchmovement_undo
    (LIKE cla.tbl_batchmovement_undo INCLUDING DEFAULTS);

DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM pg_constraint
        WHERE conrelid = 'cla_transactions.tbl_batchmovement_undo'::regclass
          AND contype = 'p'
    ) THEN
        ALTER TABLE cla_transactions.tbl_batchmovement_undo
            ALTER COLUMN trans_id SET NOT NULL;
        ALTER TABLE cla_transactions.tbl_batchmovement_undo
            ADD CONSTRAINT tbl_batchmovement_undo_trans_id_pkey PRIMARY KEY (trans_id);
    END IF;
END
$$;

CREATE TABLE IF NOT EXISTS cla_transactions.tbl_batchnumber
    (LIKE cla.tbl_batchnumber INCLUDING DEFAULTS);

DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM pg_constraint
        WHERE conrelid = 'cla_transactions.tbl_batchnumber'::regclass
          AND contype = 'p'
    ) THEN
        ALTER TABLE cla_transactions.tbl_batchnumber
            ALTER COLUMN trans_id SET NOT NULL;
        ALTER TABLE cla_transactions.tbl_batchnumber
            ADD CONSTRAINT tbl_batchnumber_trans_id_pkey PRIMARY KEY (trans_id);
    END IF;
END
$$;

CREATE TABLE IF NOT EXISTS cla_transactions.tbl_breed
    (LIKE cla.tbl_breed INCLUDING DEFAULTS);

DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM pg_constraint
        WHERE conrelid = 'cla_transactions.tbl_breed'::regclass
          AND contype = 'p'
    ) THEN
        ALTER TABLE cla_transactions.tbl_breed
            ALTER COLUMN trans_id SET NOT NULL;
        ALTER TABLE cla_transactions.tbl_breed
            ADD CONSTRAINT tbl_breed_trans_id_pkey PRIMARY KEY (trans_id);
    END IF;
END
$$;

CREATE TABLE IF NOT EXISTS cla_transactions.tbl_contact
    (LIKE cla.tbl_contact INCLUDING DEFAULTS);

DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM pg_constraint
        WHERE conrelid = 'cla_transactions.tbl_contact'::regclass
          AND contype = 'p'
    ) THEN
        ALTER TABLE cla_transactions.tbl_contact
            ALTER COLUMN trans_id SET NOT NULL;
        ALTER TABLE cla_transactions.tbl_contact
            ADD CONSTRAINT tbl_contact_trans_id_pkey PRIMARY KEY (trans_id);
    END IF;
END
$$;

CREATE TABLE IF NOT EXISTS cla_transactions.tbl_county
    (LIKE cla.tbl_county INCLUDING DEFAULTS);

DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM pg_constraint
        WHERE conrelid = 'cla_transactions.tbl_county'::regclass
          AND contype = 'p'
    ) THEN
        ALTER TABLE cla_transactions.tbl_county
            ALTER COLUMN trans_id SET NOT NULL;
        ALTER TABLE cla_transactions.tbl_county
            ADD CONSTRAINT tbl_county_trans_id_pkey PRIMARY KEY (trans_id);
    END IF;
END
$$;

CREATE TABLE IF NOT EXISTS cla_transactions.tbl_device
    (LIKE cla.tbl_device INCLUDING DEFAULTS);

DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM pg_constraint
        WHERE conrelid = 'cla_transactions.tbl_device'::regclass
          AND contype = 'p'
    ) THEN
        ALTER TABLE cla_transactions.tbl_device
            ALTER COLUMN trans_id SET NOT NULL;
        ALTER TABLE cla_transactions.tbl_device
            ADD CONSTRAINT tbl_device_trans_id_pkey PRIMARY KEY (trans_id);
    END IF;
END
$$;

CREATE TABLE IF NOT EXISTS cla_transactions.tbl_deviceattributes
    (LIKE cla.tbl_deviceattributes INCLUDING DEFAULTS);

DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM pg_constraint
        WHERE conrelid = 'cla_transactions.tbl_deviceattributes'::regclass
          AND contype = 'p'
    ) THEN
        ALTER TABLE cla_transactions.tbl_deviceattributes
            ALTER COLUMN trans_id SET NOT NULL;
        ALTER TABLE cla_transactions.tbl_deviceattributes
            ADD CONSTRAINT tbl_deviceattributes_trans_id_pkey PRIMARY KEY (trans_id);
    END IF;
END
$$;

CREATE TABLE IF NOT EXISTS cla_transactions.tbl_devicelocation
    (LIKE cla.tbl_devicelocation INCLUDING DEFAULTS);

DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM pg_constraint
        WHERE conrelid = 'cla_transactions.tbl_devicelocation'::regclass
          AND contype = 'p'
    ) THEN
        ALTER TABLE cla_transactions.tbl_devicelocation
            ALTER COLUMN trans_id SET NOT NULL;
        ALTER TABLE cla_transactions.tbl_devicelocation
            ADD CONSTRAINT tbl_devicelocation_trans_id_pkey PRIMARY KEY (trans_id);
    END IF;
END
$$;

CREATE TABLE IF NOT EXISTS cla_transactions.tbl_devicelocationduplicate
    (LIKE cla.tbl_devicelocationduplicate INCLUDING DEFAULTS);

DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM pg_constraint
        WHERE conrelid = 'cla_transactions.tbl_devicelocationduplicate'::regclass
          AND contype = 'p'
    ) THEN
        ALTER TABLE cla_transactions.tbl_devicelocationduplicate
            ALTER COLUMN trans_id SET NOT NULL;
        ALTER TABLE cla_transactions.tbl_devicelocationduplicate
            ADD CONSTRAINT tbl_devicelocationduplicate_trans_id_pkey PRIMARY KEY (trans_id);
    END IF;
END
$$;

CREATE TABLE IF NOT EXISTS cla_transactions.tbl_devicelocationduplicate_undo
    (LIKE cla.tbl_devicelocationduplicate_undo INCLUDING DEFAULTS);

DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM pg_constraint
        WHERE conrelid = 'cla_transactions.tbl_devicelocationduplicate_undo'::regclass
          AND contype = 'p'
    ) THEN
        ALTER TABLE cla_transactions.tbl_devicelocationduplicate_undo
            ALTER COLUMN trans_id SET NOT NULL;
        ALTER TABLE cla_transactions.tbl_devicelocationduplicate_undo
            ADD CONSTRAINT tbl_devicelocationduplicate_undo_trans_id_pkey PRIMARY KEY (trans_id);
    END IF;
END
$$;

CREATE TABLE IF NOT EXISTS cla_transactions.tbl_devicelocation_undo
    (LIKE cla.tbl_devicelocation_undo INCLUDING DEFAULTS);

DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM pg_constraint
        WHERE conrelid = 'cla_transactions.tbl_devicelocation_undo'::regclass
          AND contype = 'p'
    ) THEN
        ALTER TABLE cla_transactions.tbl_devicelocation_undo
            ALTER COLUMN trans_id SET NOT NULL;
        ALTER TABLE cla_transactions.tbl_devicelocation_undo
            ADD CONSTRAINT tbl_devicelocation_undo_trans_id_pkey PRIMARY KEY (trans_id);
    END IF;
END
$$;

CREATE TABLE IF NOT EXISTS cla_transactions.tbl_device_undo
    (LIKE cla.tbl_device_undo INCLUDING DEFAULTS);

DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM pg_constraint
        WHERE conrelid = 'cla_transactions.tbl_device_undo'::regclass
          AND contype = 'p'
    ) THEN
        ALTER TABLE cla_transactions.tbl_device_undo
            ALTER COLUMN trans_id SET NOT NULL;
        ALTER TABLE cla_transactions.tbl_device_undo
            ADD CONSTRAINT tbl_device_undo_trans_id_pkey PRIMARY KEY (trans_id);
    END IF;
END
$$;

CREATE TABLE IF NOT EXISTS cla_transactions.tbl_devolvedarea
    (LIKE cla.tbl_devolvedarea INCLUDING DEFAULTS);

DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM pg_constraint
        WHERE conrelid = 'cla_transactions.tbl_devolvedarea'::regclass
          AND contype = 'p'
    ) THEN
        ALTER TABLE cla_transactions.tbl_devolvedarea
            ALTER COLUMN trans_id SET NOT NULL;
        ALTER TABLE cla_transactions.tbl_devolvedarea
            ADD CONSTRAINT tbl_devolvedarea_trans_id_pkey PRIMARY KEY (trans_id);
    END IF;
END
$$;

CREATE TABLE IF NOT EXISTS cla_transactions.tbl_gender
    (LIKE cla.tbl_gender INCLUDING DEFAULTS);

DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM pg_constraint
        WHERE conrelid = 'cla_transactions.tbl_gender'::regclass
          AND contype = 'p'
    ) THEN
        ALTER TABLE cla_transactions.tbl_gender
            ALTER COLUMN trans_id SET NOT NULL;
        ALTER TABLE cla_transactions.tbl_gender
            ADD CONSTRAINT tbl_gender_trans_id_pkey PRIMARY KEY (trans_id);
    END IF;
END
$$;

CREATE TABLE IF NOT EXISTS cla_transactions.tbl_ltaterror
    (LIKE cla.tbl_ltaterror INCLUDING DEFAULTS);

DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM pg_constraint
        WHERE conrelid = 'cla_transactions.tbl_ltaterror'::regclass
          AND contype = 'p'
    ) THEN
        ALTER TABLE cla_transactions.tbl_ltaterror
            ALTER COLUMN trans_id SET NOT NULL;
        ALTER TABLE cla_transactions.tbl_ltaterror
            ADD CONSTRAINT tbl_ltaterror_trans_id_pkey PRIMARY KEY (trans_id);
    END IF;
END
$$;

CREATE TABLE IF NOT EXISTS cla_transactions.tbl_ltaterrorinfo
    (LIKE cla.tbl_ltaterrorinfo INCLUDING DEFAULTS);

DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM pg_constraint
        WHERE conrelid = 'cla_transactions.tbl_ltaterrorinfo'::regclass
          AND contype = 'p'
    ) THEN
        ALTER TABLE cla_transactions.tbl_ltaterrorinfo
            ALTER COLUMN trans_id SET NOT NULL;
        ALTER TABLE cla_transactions.tbl_ltaterrorinfo
            ADD CONSTRAINT tbl_ltaterrorinfo_trans_id_pkey PRIMARY KEY (trans_id);
    END IF;
END
$$;

CREATE TABLE IF NOT EXISTS cla_transactions.tbl_ltaterrortype
    (LIKE cla.tbl_ltaterrortype INCLUDING DEFAULTS);

DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM pg_constraint
        WHERE conrelid = 'cla_transactions.tbl_ltaterrortype'::regclass
          AND contype = 'p'
    ) THEN
        ALTER TABLE cla_transactions.tbl_ltaterrortype
            ALTER COLUMN trans_id SET NOT NULL;
        ALTER TABLE cla_transactions.tbl_ltaterrortype
            ADD CONSTRAINT tbl_ltaterrortype_trans_id_pkey PRIMARY KEY (trans_id);
    END IF;
END
$$;

CREATE TABLE IF NOT EXISTS cla_transactions.tbl_movement
    (LIKE cla.tbl_movement INCLUDING DEFAULTS);

DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM pg_constraint
        WHERE conrelid = 'cla_transactions.tbl_movement'::regclass
          AND contype = 'p'
    ) THEN
        ALTER TABLE cla_transactions.tbl_movement
            ALTER COLUMN trans_id SET NOT NULL;
        ALTER TABLE cla_transactions.tbl_movement
            ADD CONSTRAINT tbl_movement_trans_id_pkey PRIMARY KEY (trans_id);
    END IF;
END
$$;

CREATE TABLE IF NOT EXISTS cla_transactions.tbl_movementattributes
    (LIKE cla.tbl_movementattributes INCLUDING DEFAULTS);

DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM pg_constraint
        WHERE conrelid = 'cla_transactions.tbl_movementattributes'::regclass
          AND contype = 'p'
    ) THEN
        ALTER TABLE cla_transactions.tbl_movementattributes
            ALTER COLUMN trans_id SET NOT NULL;
        ALTER TABLE cla_transactions.tbl_movementattributes
            ADD CONSTRAINT tbl_movementattributes_trans_id_pkey PRIMARY KEY (trans_id);
    END IF;
END
$$;

CREATE TABLE IF NOT EXISTS cla_transactions.tbl_movementdocumentcontact
    (LIKE cla.tbl_movementdocumentcontact INCLUDING DEFAULTS);

DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM pg_constraint
        WHERE conrelid = 'cla_transactions.tbl_movementdocumentcontact'::regclass
          AND contype = 'p'
    ) THEN
        ALTER TABLE cla_transactions.tbl_movementdocumentcontact
            ALTER COLUMN trans_id SET NOT NULL;
        ALTER TABLE cla_transactions.tbl_movementdocumentcontact
            ADD CONSTRAINT tbl_movementdocumentcontact_trans_id_pkey PRIMARY KEY (trans_id);
    END IF;
END
$$;

CREATE TABLE IF NOT EXISTS cla_transactions.tbl_movementdocumentnoncompliantdevice
    (LIKE cla.tbl_movementdocumentnoncompliantdevice INCLUDING DEFAULTS);

DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM pg_constraint
        WHERE conrelid = 'cla_transactions.tbl_movementdocumentnoncompliantdevice'::regclass
          AND contype = 'p'
    ) THEN
        ALTER TABLE cla_transactions.tbl_movementdocumentnoncompliantdevice
            ALTER COLUMN trans_id SET NOT NULL;
        ALTER TABLE cla_transactions.tbl_movementdocumentnoncompliantdevice
            ADD CONSTRAINT tbl_movementdocumentnoncompliantdevice_trans_id_pkey PRIMARY KEY (trans_id);
    END IF;
END
$$;

CREATE TABLE IF NOT EXISTS cla_transactions.tbl_movementdocumenttransportertype
    (LIKE cla.tbl_movementdocumenttransportertype INCLUDING DEFAULTS);

DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM pg_constraint
        WHERE conrelid = 'cla_transactions.tbl_movementdocumenttransportertype'::regclass
          AND contype = 'p'
    ) THEN
        ALTER TABLE cla_transactions.tbl_movementdocumenttransportertype
            ALTER COLUMN trans_id SET NOT NULL;
        ALTER TABLE cla_transactions.tbl_movementdocumenttransportertype
            ADD CONSTRAINT tbl_movementdocumenttransportertype_trans_id_pkey PRIMARY KEY (trans_id);
    END IF;
END
$$;

CREATE TABLE IF NOT EXISTS cla_transactions.tbl_movementdocumentversion
    (LIKE cla.tbl_movementdocumentversion INCLUDING DEFAULTS);

DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM pg_constraint
        WHERE conrelid = 'cla_transactions.tbl_movementdocumentversion'::regclass
          AND contype = 'p'
    ) THEN
        ALTER TABLE cla_transactions.tbl_movementdocumentversion
            ALTER COLUMN trans_id SET NOT NULL;
        ALTER TABLE cla_transactions.tbl_movementdocumentversion
            ADD CONSTRAINT tbl_movementdocumentversion_trans_id_pkey PRIMARY KEY (trans_id);
    END IF;
END
$$;

CREATE TABLE IF NOT EXISTS cla_transactions.tbl_movementdocumentversion_undo
    (LIKE cla.tbl_movementdocumentversion_undo INCLUDING DEFAULTS);

DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM pg_constraint
        WHERE conrelid = 'cla_transactions.tbl_movementdocumentversion_undo'::regclass
          AND contype = 'p'
    ) THEN
        ALTER TABLE cla_transactions.tbl_movementdocumentversion_undo
            ALTER COLUMN trans_id SET NOT NULL;
        ALTER TABLE cla_transactions.tbl_movementdocumentversion_undo
            ADD CONSTRAINT tbl_movementdocumentversion_undo_trans_id_pkey PRIMARY KEY (trans_id);
    END IF;
END
$$;

CREATE TABLE IF NOT EXISTS cla_transactions.tbl_movementreviewstatus
    (LIKE cla.tbl_movementreviewstatus INCLUDING DEFAULTS);

DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM pg_constraint
        WHERE conrelid = 'cla_transactions.tbl_movementreviewstatus'::regclass
          AND contype = 'p'
    ) THEN
        ALTER TABLE cla_transactions.tbl_movementreviewstatus
            ALTER COLUMN trans_id SET NOT NULL;
        ALTER TABLE cla_transactions.tbl_movementreviewstatus
            ADD CONSTRAINT tbl_movementreviewstatus_trans_id_pkey PRIMARY KEY (trans_id);
    END IF;
END
$$;

CREATE TABLE IF NOT EXISTS cla_transactions.tbl_movementreviewstatus_undo
    (LIKE cla.tbl_movementreviewstatus_undo INCLUDING DEFAULTS);

DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM pg_constraint
        WHERE conrelid = 'cla_transactions.tbl_movementreviewstatus_undo'::regclass
          AND contype = 'p'
    ) THEN
        ALTER TABLE cla_transactions.tbl_movementreviewstatus_undo
            ALTER COLUMN trans_id SET NOT NULL;
        ALTER TABLE cla_transactions.tbl_movementreviewstatus_undo
            ADD CONSTRAINT tbl_movementreviewstatus_undo_trans_id_pkey PRIMARY KEY (trans_id);
    END IF;
END
$$;

CREATE TABLE IF NOT EXISTS cla_transactions.tbl_movement_undo
    (LIKE cla.tbl_movement_undo INCLUDING DEFAULTS);

DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM pg_constraint
        WHERE conrelid = 'cla_transactions.tbl_movement_undo'::regclass
          AND contype = 'p'
    ) THEN
        ALTER TABLE cla_transactions.tbl_movement_undo
            ALTER COLUMN trans_id SET NOT NULL;
        ALTER TABLE cla_transactions.tbl_movement_undo
            ADD CONSTRAINT tbl_movement_undo_trans_id_pkey PRIMARY KEY (trans_id);
    END IF;
END
$$;

CREATE TABLE IF NOT EXISTS cla_transactions.tbl_papermovement
    (LIKE cla.tbl_papermovement INCLUDING DEFAULTS);

DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM pg_constraint
        WHERE conrelid = 'cla_transactions.tbl_papermovement'::regclass
          AND contype = 'p'
    ) THEN
        ALTER TABLE cla_transactions.tbl_papermovement
            ALTER COLUMN trans_id SET NOT NULL;
        ALTER TABLE cla_transactions.tbl_papermovement
            ADD CONSTRAINT tbl_papermovement_trans_id_pkey PRIMARY KEY (trans_id);
    END IF;
END
$$;

CREATE TABLE IF NOT EXISTS cla_transactions.tbl_papermovementbatch
    (LIKE cla.tbl_papermovementbatch INCLUDING DEFAULTS);

DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM pg_constraint
        WHERE conrelid = 'cla_transactions.tbl_papermovementbatch'::regclass
          AND contype = 'p'
    ) THEN
        ALTER TABLE cla_transactions.tbl_papermovementbatch
            ALTER COLUMN trans_id SET NOT NULL;
        ALTER TABLE cla_transactions.tbl_papermovementbatch
            ADD CONSTRAINT tbl_papermovementbatch_trans_id_pkey PRIMARY KEY (trans_id);
    END IF;
END
$$;

CREATE TABLE IF NOT EXISTS cla_transactions.tbl_papermovementbatch_undo
    (LIKE cla.tbl_papermovementbatch_undo INCLUDING DEFAULTS);

DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM pg_constraint
        WHERE conrelid = 'cla_transactions.tbl_papermovementbatch_undo'::regclass
          AND contype = 'p'
    ) THEN
        ALTER TABLE cla_transactions.tbl_papermovementbatch_undo
            ALTER COLUMN trans_id SET NOT NULL;
        ALTER TABLE cla_transactions.tbl_papermovementbatch_undo
            ADD CONSTRAINT tbl_papermovementbatch_undo_trans_id_pkey PRIMARY KEY (trans_id);
    END IF;
END
$$;

CREATE TABLE IF NOT EXISTS cla_transactions.tbl_papermovementdevice
    (LIKE cla.tbl_papermovementdevice INCLUDING DEFAULTS);

DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM pg_constraint
        WHERE conrelid = 'cla_transactions.tbl_papermovementdevice'::regclass
          AND contype = 'p'
    ) THEN
        ALTER TABLE cla_transactions.tbl_papermovementdevice
            ALTER COLUMN trans_id SET NOT NULL;
        ALTER TABLE cla_transactions.tbl_papermovementdevice
            ADD CONSTRAINT tbl_papermovementdevice_trans_id_pkey PRIMARY KEY (trans_id);
    END IF;
END
$$;

CREATE TABLE IF NOT EXISTS cla_transactions.tbl_papermovementdevice_undo
    (LIKE cla.tbl_papermovementdevice_undo INCLUDING DEFAULTS);

DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM pg_constraint
        WHERE conrelid = 'cla_transactions.tbl_papermovementdevice_undo'::regclass
          AND contype = 'p'
    ) THEN
        ALTER TABLE cla_transactions.tbl_papermovementdevice_undo
            ALTER COLUMN trans_id SET NOT NULL;
        ALTER TABLE cla_transactions.tbl_papermovementdevice_undo
            ADD CONSTRAINT tbl_papermovementdevice_undo_trans_id_pkey PRIMARY KEY (trans_id);
    END IF;
END
$$;

CREATE TABLE IF NOT EXISTS cla_transactions.tbl_papermovement_undo
    (LIKE cla.tbl_papermovement_undo INCLUDING DEFAULTS);

DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM pg_constraint
        WHERE conrelid = 'cla_transactions.tbl_papermovement_undo'::regclass
          AND contype = 'p'
    ) THEN
        ALTER TABLE cla_transactions.tbl_papermovement_undo
            ALTER COLUMN trans_id SET NOT NULL;
        ALTER TABLE cla_transactions.tbl_papermovement_undo
            ADD CONSTRAINT tbl_papermovement_undo_trans_id_pkey PRIMARY KEY (trans_id);
    END IF;
END
$$;

CREATE TABLE IF NOT EXISTS cla_transactions.tbl_pic
    (LIKE cla.tbl_pic INCLUDING DEFAULTS);

DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM pg_constraint
        WHERE conrelid = 'cla_transactions.tbl_pic'::regclass
          AND contype = 'p'
    ) THEN
        ALTER TABLE cla_transactions.tbl_pic
            ALTER COLUMN trans_id SET NOT NULL;
        ALTER TABLE cla_transactions.tbl_pic
            ADD CONSTRAINT tbl_pic_trans_id_pkey PRIMARY KEY (trans_id);
    END IF;
END
$$;

CREATE TABLE IF NOT EXISTS cla_transactions.tbl_picattributes
    (LIKE cla.tbl_picattributes INCLUDING DEFAULTS);

DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM pg_constraint
        WHERE conrelid = 'cla_transactions.tbl_picattributes'::regclass
          AND contype = 'p'
    ) THEN
        ALTER TABLE cla_transactions.tbl_picattributes
            ALTER COLUMN trans_id SET NOT NULL;
        ALTER TABLE cla_transactions.tbl_picattributes
            ADD CONSTRAINT tbl_picattributes_trans_id_pkey PRIMARY KEY (trans_id);
    END IF;
END
$$;

CREATE TABLE IF NOT EXISTS cla_transactions.tbl_pictoabattoirmhs
    (LIKE cla.tbl_pictoabattoirmhs INCLUDING DEFAULTS);

DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM pg_constraint
        WHERE conrelid = 'cla_transactions.tbl_pictoabattoirmhs'::regclass
          AND contype = 'p'
    ) THEN
        ALTER TABLE cla_transactions.tbl_pictoabattoirmhs
            ALTER COLUMN trans_id SET NOT NULL;
        ALTER TABLE cla_transactions.tbl_pictoabattoirmhs
            ADD CONSTRAINT tbl_pictoabattoirmhs_trans_id_pkey PRIMARY KEY (trans_id);
    END IF;
END
$$;

CREATE TABLE IF NOT EXISTS cla_transactions.tbl_pictoabattoirmhsheader
    (LIKE cla.tbl_pictoabattoirmhsheader INCLUDING DEFAULTS);

DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM pg_constraint
        WHERE conrelid = 'cla_transactions.tbl_pictoabattoirmhsheader'::regclass
          AND contype = 'p'
    ) THEN
        ALTER TABLE cla_transactions.tbl_pictoabattoirmhsheader
            ALTER COLUMN trans_id SET NOT NULL;
        ALTER TABLE cla_transactions.tbl_pictoabattoirmhsheader
            ADD CONSTRAINT tbl_pictoabattoirmhsheader_trans_id_pkey PRIMARY KEY (trans_id);
    END IF;
END
$$;

CREATE TABLE IF NOT EXISTS cla_transactions.tbl_pictype
    (LIKE cla.tbl_pictype INCLUDING DEFAULTS);

DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM pg_constraint
        WHERE conrelid = 'cla_transactions.tbl_pictype'::regclass
          AND contype = 'p'
    ) THEN
        ALTER TABLE cla_transactions.tbl_pictype
            ALTER COLUMN trans_id SET NOT NULL;
        ALTER TABLE cla_transactions.tbl_pictype
            ADD CONSTRAINT tbl_pictype_trans_id_pkey PRIMARY KEY (trans_id);
    END IF;
END
$$;

CREATE TABLE IF NOT EXISTS cla_transactions.tbl_processingflag
    (LIKE cla.tbl_processingflag INCLUDING DEFAULTS);

DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM pg_constraint
        WHERE conrelid = 'cla_transactions.tbl_processingflag'::regclass
          AND contype = 'p'
    ) THEN
        ALTER TABLE cla_transactions.tbl_processingflag
            ALTER COLUMN trans_id SET NOT NULL;
        ALTER TABLE cla_transactions.tbl_processingflag
            ADD CONSTRAINT tbl_processingflag_trans_id_pkey PRIMARY KEY (trans_id);
    END IF;
END
$$;

CREATE TABLE IF NOT EXISTS cla_transactions.tbl_region
    (LIKE cla.tbl_region INCLUDING DEFAULTS);

DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM pg_constraint
        WHERE conrelid = 'cla_transactions.tbl_region'::regclass
          AND contype = 'p'
    ) THEN
        ALTER TABLE cla_transactions.tbl_region
            ALTER COLUMN trans_id SET NOT NULL;
        ALTER TABLE cla_transactions.tbl_region
            ADD CONSTRAINT tbl_region_trans_id_pkey PRIMARY KEY (trans_id);
    END IF;
END
$$;

CREATE TABLE IF NOT EXISTS cla_transactions.tbl_request
    (LIKE cla.tbl_request INCLUDING DEFAULTS);

DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM pg_constraint
        WHERE conrelid = 'cla_transactions.tbl_request'::regclass
          AND contype = 'p'
    ) THEN
        ALTER TABLE cla_transactions.tbl_request
            ALTER COLUMN trans_id SET NOT NULL;
        ALTER TABLE cla_transactions.tbl_request
            ADD CONSTRAINT tbl_request_trans_id_pkey PRIMARY KEY (trans_id);
    END IF;
END
$$;

CREATE TABLE IF NOT EXISTS cla_transactions.tbl_requestprocessingflag
    (LIKE cla.tbl_requestprocessingflag INCLUDING DEFAULTS);

DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM pg_constraint
        WHERE conrelid = 'cla_transactions.tbl_requestprocessingflag'::regclass
          AND contype = 'p'
    ) THEN
        ALTER TABLE cla_transactions.tbl_requestprocessingflag
            ALTER COLUMN trans_id SET NOT NULL;
        ALTER TABLE cla_transactions.tbl_requestprocessingflag
            ADD CONSTRAINT tbl_requestprocessingflag_trans_id_pkey PRIMARY KEY (trans_id);
    END IF;
END
$$;

CREATE TABLE IF NOT EXISTS cla_transactions.tbl_sessioninfo
    (LIKE cla.tbl_sessioninfo INCLUDING DEFAULTS);

DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM pg_constraint
        WHERE conrelid = 'cla_transactions.tbl_sessioninfo'::regclass
          AND contype = 'p'
    ) THEN
        ALTER TABLE cla_transactions.tbl_sessioninfo
            ALTER COLUMN trans_id SET NOT NULL;
        ALTER TABLE cla_transactions.tbl_sessioninfo
            ADD CONSTRAINT tbl_sessioninfo_trans_id_pkey PRIMARY KEY (trans_id);
    END IF;
END
$$;

CREATE TABLE IF NOT EXISTS cla_transactions.tbl_sessiontorequest
    (LIKE cla.tbl_sessiontorequest INCLUDING DEFAULTS);

DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM pg_constraint
        WHERE conrelid = 'cla_transactions.tbl_sessiontorequest'::regclass
          AND contype = 'p'
    ) THEN
        ALTER TABLE cla_transactions.tbl_sessiontorequest
            ALTER COLUMN trans_id SET NOT NULL;
        ALTER TABLE cla_transactions.tbl_sessiontorequest
            ADD CONSTRAINT tbl_sessiontorequest_trans_id_pkey PRIMARY KEY (trans_id);
    END IF;
END
$$;

CREATE TABLE IF NOT EXISTS cla_transactions.tbl_software
    (LIKE cla.tbl_software INCLUDING DEFAULTS);

DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM pg_constraint
        WHERE conrelid = 'cla_transactions.tbl_software'::regclass
          AND contype = 'p'
    ) THEN
        ALTER TABLE cla_transactions.tbl_software
            ALTER COLUMN trans_id SET NOT NULL;
        ALTER TABLE cla_transactions.tbl_software
            ADD CONSTRAINT tbl_software_trans_id_pkey PRIMARY KEY (trans_id);
    END IF;
END
$$;

CREATE TABLE IF NOT EXISTS cla_transactions.tbl_species
    (LIKE cla.tbl_species INCLUDING DEFAULTS);

DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM pg_constraint
        WHERE conrelid = 'cla_transactions.tbl_species'::regclass
          AND contype = 'p'
    ) THEN
        ALTER TABLE cla_transactions.tbl_species
            ALTER COLUMN trans_id SET NOT NULL;
        ALTER TABLE cla_transactions.tbl_species
            ADD CONSTRAINT tbl_species_trans_id_pkey PRIMARY KEY (trans_id);
    END IF;
END
$$;

CREATE TABLE IF NOT EXISTS cla_transactions.tbl_titemattributes
    (LIKE cla.tbl_titemattributes INCLUDING DEFAULTS);

DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM pg_constraint
        WHERE conrelid = 'cla_transactions.tbl_titemattributes'::regclass
          AND contype = 'p'
    ) THEN
        ALTER TABLE cla_transactions.tbl_titemattributes
            ALTER COLUMN trans_id SET NOT NULL;
        ALTER TABLE cla_transactions.tbl_titemattributes
            ADD CONSTRAINT tbl_titemattributes_trans_id_pkey PRIMARY KEY (trans_id);
    END IF;
END
$$;

CREATE TABLE IF NOT EXISTS cla_transactions.tbl_titem_device
    (LIKE cla.tbl_titem_device INCLUDING DEFAULTS);

DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM pg_constraint
        WHERE conrelid = 'cla_transactions.tbl_titem_device'::regclass
          AND contype = 'p'
    ) THEN
        ALTER TABLE cla_transactions.tbl_titem_device
            ALTER COLUMN trans_id SET NOT NULL;
        ALTER TABLE cla_transactions.tbl_titem_device
            ADD CONSTRAINT tbl_titem_device_trans_id_pkey PRIMARY KEY (trans_id);
    END IF;
END
$$;

CREATE TABLE IF NOT EXISTS cla_transactions.tbl_title
    (LIKE cla.tbl_title INCLUDING DEFAULTS);

DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM pg_constraint
        WHERE conrelid = 'cla_transactions.tbl_title'::regclass
          AND contype = 'p'
    ) THEN
        ALTER TABLE cla_transactions.tbl_title
            ALTER COLUMN trans_id SET NOT NULL;
        ALTER TABLE cla_transactions.tbl_title
            ADD CONSTRAINT tbl_title_trans_id_pkey PRIMARY KEY (trans_id);
    END IF;
END
$$;

CREATE TABLE IF NOT EXISTS cla_transactions.tbl_movementgroup
    (LIKE cla.tbl_movementgroup INCLUDING DEFAULTS);

DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM pg_constraint
        WHERE conrelid = 'cla_transactions.tbl_movementgroup'::regclass
          AND contype = 'p'
    ) THEN
        ALTER TABLE cla_transactions.tbl_movementgroup
            ALTER COLUMN trans_id SET NOT NULL;
        ALTER TABLE cla_transactions.tbl_movementgroup
            ADD CONSTRAINT tbl_movementgroup_trans_id_pkey PRIMARY KEY (trans_id);
    END IF;
END
$$;

CREATE TABLE IF NOT EXISTS cla_transactions.tbl_movementgroupdevice
    (LIKE cla.tbl_movementgroupdevice INCLUDING DEFAULTS);

DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM pg_constraint
        WHERE conrelid = 'cla_transactions.tbl_movementgroupdevice'::regclass
          AND contype = 'p'
    ) THEN
        ALTER TABLE cla_transactions.tbl_movementgroupdevice
            ALTER COLUMN trans_id SET NOT NULL;
        ALTER TABLE cla_transactions.tbl_movementgroupdevice
            ADD CONSTRAINT tbl_movementgroupdevice_trans_id_pkey PRIMARY KEY (trans_id);
    END IF;
END
$$;

CREATE TABLE IF NOT EXISTS cla_transactions.tbl_movementgroup_undo
    (LIKE cla.tbl_movementgroup_undo INCLUDING DEFAULTS);

DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM pg_constraint
        WHERE conrelid = 'cla_transactions.tbl_movementgroup_undo'::regclass
          AND contype = 'p'
    ) THEN
        ALTER TABLE cla_transactions.tbl_movementgroup_undo
            ALTER COLUMN trans_id SET NOT NULL;
        ALTER TABLE cla_transactions.tbl_movementgroup_undo
            ADD CONSTRAINT tbl_movementgroup_undo_trans_id_pkey PRIMARY KEY (trans_id);
    END IF;
END
$$;
