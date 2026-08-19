-- liquibase formatted sql

-- changeset schema:0001-011-create-cts-file-import-monitoring-tables splitStatements:true

CREATE SCHEMA IF NOT EXISTS cads;

CREATE TABLE IF NOT EXISTS cads."cts_file_import_statuses"
(
    "import_status_id" SMALLINT NOT NULL,
    "status_description" TEXT NOT NULL,
    CONSTRAINT "cts_file_import_statuses_pkey" PRIMARY KEY ("import_status_id"),
    CONSTRAINT "cts_file_import_statuses_status_description_key" UNIQUE ("status_description")
);

CREATE TABLE IF NOT EXISTS cads."cts_file_processing_statuses"
(
    "processing_status_id" SMALLINT NOT NULL,
    "status_description" TEXT NOT NULL,
    CONSTRAINT "cts_file_processing_statuses_pkey" PRIMARY KEY ("processing_status_id"),
    CONSTRAINT "cts_file_processing_statuses_status_description_key" UNIQUE ("status_description")
);

CREATE TABLE IF NOT EXISTS cads."cts_file_imports"
(
    "cts_file_import_id" BIGINT GENERATED ALWAYS AS IDENTITY NOT NULL,
    "destination_table_name" TEXT NOT NULL,
    "file_name" TEXT NOT NULL,
    "total_rows_to_process" BIGINT NOT NULL,
    "added_at" TIMESTAMP WITH TIME ZONE DEFAULT CLOCK_TIMESTAMP() NOT NULL,
    "import_status_id" SMALLINT DEFAULT 1 NOT NULL,
    "processing_status_id" SMALLINT DEFAULT 1 NOT NULL,
    "rows_found" BIGINT DEFAULT 0 NOT NULL,
    "import_start_at" TIMESTAMP WITH TIME ZONE,
    "import_end_at" TIMESTAMP WITH TIME ZONE,
    "processing_start_at" TIMESTAMP WITH TIME ZONE,
    "processing_end_at" TIMESTAMP WITH TIME ZONE,
    "failed_attempts" SMALLINT DEFAULT 0,
    "last_error_reason" TEXT,
    "group_key" TEXT NOT NULL,
    "import_type" TEXT NOT NULL,
    "batch_date" TIMESTAMP WITH TIME ZONE NOT NULL,
    CONSTRAINT "cts_file_imports_pkey" PRIMARY KEY ("cts_file_import_id"),
    CONSTRAINT "cts_file_imports_import_status_id_fkey"
        FOREIGN KEY ("import_status_id") REFERENCES cads."cts_file_import_statuses" ("import_status_id"),
    CONSTRAINT "cts_file_imports_processing_status_id_fkey"
        FOREIGN KEY ("processing_status_id") REFERENCES cads."cts_file_processing_statuses" ("processing_status_id"),
    CONSTRAINT "cts_file_imports_rows_found_check" CHECK ("rows_found" >= 0),
    CONSTRAINT "cts_file_imports_total_rows_to_process_check" CHECK ("total_rows_to_process" >= 0)
);

CREATE TABLE IF NOT EXISTS cads."cts_file_imports_log"
(
    "cts_file_import_log_id" BIGINT GENERATED ALWAYS AS IDENTITY NOT NULL,
    "cts_file_import_id" BIGINT NOT NULL,
    "log_level" TEXT DEFAULT 'info' NOT NULL,
    "log_message" TEXT NOT NULL,
    "error_message" TEXT,
    "expected_records" BIGINT,
    "processed_records" BIGINT,
    "inserted_records" BIGINT DEFAULT 0 NOT NULL,
    "updated_records" BIGINT DEFAULT 0 NOT NULL,
    "deleted_records" BIGINT DEFAULT 0 NOT NULL,
    "processing_started_at" TIMESTAMP WITH TIME ZONE,
    "processing_ended_at" TIMESTAMP WITH TIME ZONE,
    "insert_started_at" TIMESTAMP WITH TIME ZONE,
    "insert_ended_at" TIMESTAMP WITH TIME ZONE,
    "update_started_at" TIMESTAMP WITH TIME ZONE,
    "update_ended_at" TIMESTAMP WITH TIME ZONE,
    "delete_started_at" TIMESTAMP WITH TIME ZONE,
    "delete_ended_at" TIMESTAMP WITH TIME ZONE,
    "logged_at" TIMESTAMP WITH TIME ZONE DEFAULT CLOCK_TIMESTAMP() NOT NULL,
    CONSTRAINT "cts_file_imports_log_pkey" PRIMARY KEY ("cts_file_import_log_id"),
    CONSTRAINT "cts_file_imports_log_file_import_fkey"
        FOREIGN KEY ("cts_file_import_id") REFERENCES cads."cts_file_imports" ("cts_file_import_id"),
    CONSTRAINT "cts_file_imports_log_level_check"
        CHECK ("log_level" IN ('info', 'warning', 'error'))
);

CREATE INDEX IF NOT EXISTS "cts_file_imports_destination_table_name_idx"
    ON cads."cts_file_imports" ("destination_table_name");
CREATE UNIQUE INDEX IF NOT EXISTS "cts_file_imports_file_name_idx"
    ON cads."cts_file_imports" ("file_name");
CREATE INDEX IF NOT EXISTS "cts_file_imports_import_status_id_idx"
    ON cads."cts_file_imports" ("import_status_id");
CREATE INDEX IF NOT EXISTS "cts_file_imports_processing_status_id_idx"
    ON cads."cts_file_imports" ("processing_status_id");
CREATE INDEX IF NOT EXISTS "cts_file_imports_group_key_idx"
    ON cads."cts_file_imports" ("group_key");
CREATE INDEX IF NOT EXISTS "cts_file_imports_log_file_import_id_idx"
    ON cads."cts_file_imports_log" ("cts_file_import_id");
CREATE INDEX IF NOT EXISTS "cts_file_imports_log_logged_at_idx"
    ON cads."cts_file_imports_log" ("logged_at");

INSERT INTO cads."cts_file_import_statuses" ("import_status_id", "status_description")
VALUES
    (1, 'pending'),
    (2, 'transferred'),
    (3, 'split'),
    (4, 'completed'),
    (5, 'error')
ON CONFLICT ("import_status_id") DO UPDATE
SET "status_description" = EXCLUDED."status_description";

INSERT INTO cads."cts_file_processing_statuses" ("processing_status_id", "status_description")
VALUES
    (1, 'pending'),
    (2, 'processing'),
    (3, 'completed'),
    (4, 'error')
ON CONFLICT ("processing_status_id") DO UPDATE
SET "status_description" = EXCLUDED."status_description";
