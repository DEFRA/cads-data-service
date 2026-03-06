-- liquibase formatted sql

-- changeset MarkGent1:1772802524907-1 splitStatements:false
CREATE TABLE "mi_user_report_permission" ("user_id" UUID NOT NULL, "report_id" UUID NOT NULL, "permission_id" UUID NOT NULL, "granted" BOOLEAN NOT NULL, "reason" TEXT, "granted_at" TIMESTAMP WITH TIME ZONE DEFAULT NOW() NOT NULL, CONSTRAINT "mi_user_report_permission_pkey" PRIMARY KEY ("user_id", "report_id", "permission_id"));

-- changeset MarkGent1:1772802524907-2 splitStatements:false
CREATE INDEX "mi_rrp_report_permission_idx" ON "mi_role_report_permission" USING btree("report_id", "permission_id");

-- changeset MarkGent1:1772802524907-3 splitStatements:false
CREATE INDEX "mi_urp_user_report_idx" ON "mi_user_report_permission" USING btree("user_id", "report_id");

-- changeset MarkGent1:1772802524907-4 splitStatements:false
CREATE INDEX "mi_urp_report_idx" ON "mi_user_report_permission" USING btree("report_id");

-- changeset MarkGent1:1772802524907-5 splitStatements:false
CREATE INDEX "mi_urp_permission_idx" ON "mi_user_report_permission" USING btree("permission_id");

-- changeset MarkGent1:1772802524907-6 splitStatements:false
ALTER TABLE "mi_user_report_permission" ADD CONSTRAINT "mi_user_report_permission_permission_id_fkey" FOREIGN KEY ("permission_id") REFERENCES "mi_permission" ("permission_id") ON UPDATE NO ACTION ON DELETE RESTRICT;

-- changeset MarkGent1:1772802524907-7 splitStatements:false
ALTER TABLE "mi_user_report_permission" ADD CONSTRAINT "mi_user_report_permission_report_id_fkey" FOREIGN KEY ("report_id") REFERENCES "mi_report" ("report_id") ON UPDATE NO ACTION ON DELETE CASCADE;

-- changeset MarkGent1:1772802524907-8 splitStatements:false
ALTER TABLE "mi_user_report_permission" ADD CONSTRAINT "mi_user_report_permission_user_id_fkey" FOREIGN KEY ("user_id") REFERENCES "mi_user" ("user_id") ON UPDATE NO ACTION ON DELETE CASCADE;

