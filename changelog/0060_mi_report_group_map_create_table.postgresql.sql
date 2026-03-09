-- liquibase formatted sql

-- changeset MarkGent1:1772802333432-1 splitStatements:false
CREATE TABLE "mi_report_group_map" ("group_id" UUID NOT NULL, "report_id" UUID NOT NULL, CONSTRAINT "mi_report_group_map_pkey" PRIMARY KEY ("group_id", "report_id"));

-- changeset MarkGent1:1772802333432-2 splitStatements:false
ALTER TABLE "mi_report_group_map" ADD CONSTRAINT "mi_report_group_map_group_id_fkey" FOREIGN KEY ("group_id") REFERENCES "mi_report_group" ("group_id") ON UPDATE NO ACTION ON DELETE CASCADE;

-- changeset MarkGent1:1772802333432-3 splitStatements:false
ALTER TABLE "mi_report_group_map" ADD CONSTRAINT "mi_report_group_map_report_id_fkey" FOREIGN KEY ("report_id") REFERENCES "mi_report" ("report_id") ON UPDATE NO ACTION ON DELETE CASCADE;

