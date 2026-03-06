-- liquibase formatted sql

-- changeset MarkGent1:1772802393661-1 splitStatements:false
CREATE TABLE "mi_permission" ("permission_id" UUID NOT NULL, "permission_key" TEXT NOT NULL, "description" TEXT, CONSTRAINT "mi_permission_pkey" PRIMARY KEY ("permission_id"));

-- changeset MarkGent1:1772802393661-2 splitStatements:false
ALTER TABLE "mi_permission" ADD CONSTRAINT "mi_permission_permission_key_key" UNIQUE ("permission_key");

