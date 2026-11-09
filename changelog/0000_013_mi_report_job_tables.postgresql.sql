-- liquibase formatted sql

-- changeset schema:0000-013-mi-report-job-tables splitStatements:false

-- Report generation job requests and status tracking for async report generation.

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

CREATE INDEX "mi_report_job_status_idx" ON cads."mi_report_job" ("status");
CREATE INDEX "mi_report_job_expires_at_idx" ON cads."mi_report_job" ("expires_at")
    WHERE "expires_at" IS NOT NULL;

-- Transactional outbox for async report generation job status notifications.
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
                                                 CHECK ("status" IN ('pending', 'dispatching', 'dispatched', 'failed')),
                                             CONSTRAINT "mi_report_job_outbox_report_job_id_fkey"
                                                 FOREIGN KEY ("report_job_id") REFERENCES cads."mi_report_job" ("report_job_id")
                                                     ON UPDATE NO ACTION ON DELETE CASCADE
);

CREATE INDEX "mi_report_job_outbox_pending_idx"
    ON cads."mi_report_job_outbox" ("created_at")
    WHERE "status" = 'pending';

-- changeset schema:0000-013-mi-report-job-outbox-claim-function endDelimiter://

DROP FUNCTION IF EXISTS cads.claim_pending_report_job_outbox_messages(INTEGER);

CREATE OR REPLACE FUNCTION cads.claim_pending_report_job_outbox_messages(
    p_batch_size INTEGER
)
    RETURNS SETOF cads."mi_report_job_outbox"
    LANGUAGE sql
AS $$
    WITH claimed AS (
        SELECT "outbox_id"
        FROM cads."mi_report_job_outbox"
        WHERE "status" = 'pending'
        ORDER BY "created_at"
        LIMIT p_batch_size
        FOR UPDATE SKIP LOCKED
    )
    UPDATE cads."mi_report_job_outbox" AS o
    SET "status" = 'dispatching'
    FROM claimed AS c
    WHERE o."outbox_id" = c."outbox_id"
    RETURNING o.*;
$$
//

