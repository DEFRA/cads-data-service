-- liquibase formatted sql

-- changeset schema:0000-013-mi-report-job-tables splitStatements:false

-- Async report generation: job state table and transactional outbox (Option B).

CREATE TABLE cads."mi_report_job" (
                                      "report_job_id"    UUID NOT NULL,
                                      "report_key"       TEXT NOT NULL,
                                      "external_subject" TEXT NOT NULL,
                                      "request_payload"  JSONB NOT NULL,
                                      "status"           TEXT DEFAULT 'pending' NOT NULL,
                                      "s3_key"           TEXT,
                                      "row_count"        INTEGER,
                                      "error"            TEXT,
                                      "created_at"       TIMESTAMP WITH TIME ZONE DEFAULT NOW() NOT NULL,
                                      "updated_at"       TIMESTAMP WITH TIME ZONE DEFAULT NOW() NOT NULL,
                                      "started_at"       TIMESTAMP WITH TIME ZONE,
                                      "completed_at"     TIMESTAMP WITH TIME ZONE,
                                      "expires_at"       TIMESTAMP WITH TIME ZONE,
                                      CONSTRAINT "mi_report_job_pkey" PRIMARY KEY ("report_job_id"),
                                      CONSTRAINT "mi_report_job_status_chk"
                                          CHECK ("status" IN ('pending', 'processing', 'complete', 'failed', 'expired'))
);

-- Lookup unexpired jobs ready to be cleaned up / served.
CREATE INDEX "mi_report_job_status_idx" ON cads."mi_report_job" ("status");
CREATE INDEX "mi_report_job_expires_at_idx" ON cads."mi_report_job" ("expires_at")
    WHERE "expires_at" IS NOT NULL;

-- Transactional outbox: one row written atomically with the job on submit,
-- deleted after successful SQS dispatch.
CREATE TABLE cads."mi_report_job_outbox" (
                                             "outbox_id"      UUID NOT NULL,
                                             "report_job_id"  UUID NOT NULL,
                                             "subject"        TEXT NOT NULL,
                                             "payload"        JSONB NOT NULL,
                                             "status"         TEXT DEFAULT 'pending' NOT NULL,
                                             "attempts"       INTEGER DEFAULT 0 NOT NULL,
                                             "last_error"     TEXT,
                                             "created_at"     TIMESTAMP WITH TIME ZONE DEFAULT NOW() NOT NULL,
                                             "dispatched_at"  TIMESTAMP WITH TIME ZONE,
                                             CONSTRAINT "mi_report_job_outbox_pkey" PRIMARY KEY ("outbox_id"),
                                             CONSTRAINT "mi_report_job_outbox_status_chk"
                                                 CHECK ("status" IN ('pending', 'dispatched', 'failed')),
                                             CONSTRAINT "mi_report_job_outbox_report_job_id_fkey"
                                                 FOREIGN KEY ("report_job_id") REFERENCES cads."mi_report_job" ("report_job_id")
                                                     ON UPDATE NO ACTION ON DELETE CASCADE
);

-- Partial index keeps the dispatcher poll ("WHERE status = 'pending'") cheap
-- regardless of how many dispatched/historical rows exist.
CREATE INDEX "mi_report_job_outbox_pending_idx"
    ON cads."mi_report_job_outbox" ("created_at")
    WHERE "status" = 'pending';