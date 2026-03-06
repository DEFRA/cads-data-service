-- liquibase formatted sql

-- changeset MarkGent1:1772802465073-1 splitStatements:false
CREATE TABLE "mi_role_report_permission" ("role_id" UUID NOT NULL, "report_id" UUID NOT NULL, "permission_id" UUID NOT NULL, "granted" BOOLEAN DEFAULT TRUE NOT NULL, CONSTRAINT "mi_role_report_permission_pkey" PRIMARY KEY ("role_id", "report_id", "permission_id"));

-- changeset MarkGent1:1772802465073-2 splitStatements:false
CREATE INDEX "mi_rrp_role_report_idx" ON "mi_role_report_permission" USING btree("role_id", "report_id");

-- changeset MarkGent1:1772802465073-3 splitStatements:false
CREATE INDEX "mi_rrp_permission_idx" ON "mi_role_report_permission" USING btree("permission_id");

-- changeset MarkGent1:1772802465073-4 splitStatements:false
CREATE INDEX "mi_rrp_report_idx" ON "mi_role_report_permission" USING btree("report_id");

-- changeset MarkGent1:1772802465073-5 splitStatements:false
ALTER TABLE "mi_role_report_permission" ADD CONSTRAINT "mi_role_report_permission_permission_id_fkey" FOREIGN KEY ("permission_id") REFERENCES "mi_permission" ("permission_id") ON UPDATE NO ACTION ON DELETE NO ACTION;

-- changeset MarkGent1:1772802465073-6 splitStatements:false
ALTER TABLE "mi_role_report_permission" ADD CONSTRAINT "mi_role_report_permission_report_id_fkey" FOREIGN KEY ("report_id") REFERENCES "mi_report" ("report_id") ON UPDATE NO ACTION ON DELETE CASCADE;

-- changeset MarkGent1:1772802465073-7 splitStatements:false
ALTER TABLE "mi_role_report_permission" ADD CONSTRAINT "mi_role_report_permission_role_id_fkey" FOREIGN KEY ("role_id") REFERENCES "mi_role" ("role_id") ON UPDATE NO ACTION ON DELETE CASCADE;

