-- liquibase formatted sql

-- changeset MarkGent1:1772802268658-1 splitStatements:false
CREATE TABLE "mi_report_group" ("group_id" UUID NOT NULL, "group_key" TEXT NOT NULL, "title" TEXT NOT NULL, CONSTRAINT "mi_report_group_pkey" PRIMARY KEY ("group_id"));

-- changeset MarkGent1:1772802268658-2 splitStatements:false
ALTER TABLE "mi_report_group" ADD CONSTRAINT "mi_report_group_group_key_key" UNIQUE ("group_key");

