-- liquibase formatted sql

-- changeset MarkGent1:1777294246566-2 splitStatements:false
ALTER TABLE "mi_user" ADD "external_subject_normalized" TEXT GENERATED ALWAYS AS (lower(external_subject)) STORED;

-- changeset MarkGent1:1777294246566-1 splitStatements:false
CREATE UNIQUE INDEX "mi_user_external_subject_normalized_key" ON "mi_user" USING btree("external_subject_normalized");

-- changeset MarkGent1:1777294246566-3 splitStatements:false
ALTER TABLE "mi_user" DROP CONSTRAINT "mi_user_external_subject_key";

