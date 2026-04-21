-- liquibase formatted sql

-- changeset MarkGent1:1772802208494-1 splitStatements:false
CREATE TABLE "mi_report" ("report_id" UUID NOT NULL, "report_key" TEXT NOT NULL, "title" TEXT NOT NULL, "description" TEXT, "is_active" BOOLEAN DEFAULT TRUE NOT NULL, "created_at" TIMESTAMP WITH TIME ZONE DEFAULT NOW() NOT NULL, CONSTRAINT "mi_report_pkey" PRIMARY KEY ("report_id"));

-- changeset MarkGent1:1772802208494-2 splitStatements:false
ALTER TABLE "mi_report" ADD CONSTRAINT "mi_report_report_key_key" UNIQUE ("report_key");

