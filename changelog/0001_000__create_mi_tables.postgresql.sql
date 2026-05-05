-- liquibase formatted sql

-- changeset schema:0001-000-mi-tables splitStatements:false


CREATE TABLE "mi_user" ("user_id" UUID NOT NULL, "external_subject" TEXT NOT NULL, "display_name" TEXT NOT NULL, "email" TEXT, "is_active" BOOLEAN DEFAULT TRUE NOT NULL, "created_at" TIMESTAMP WITH TIME ZONE DEFAULT NOW() NOT NULL, CONSTRAINT "mi_user_pkey" PRIMARY KEY ("user_id"));

-- changeset MarkGent1:1777294246566-2 splitStatements:false
ALTER TABLE "mi_user" ADD "external_subject_normalized" TEXT GENERATED ALWAYS AS (lower(external_subject)) STORED;

-- changeset MarkGent1:1777294246566-1 splitStatements:false
CREATE UNIQUE INDEX "mi_user_external_subject_normalized_key" ON "mi_user" USING btree("external_subject_normalized");

CREATE TABLE "mi_role" ("role_id" UUID NOT NULL, "role_key" TEXT NOT NULL, "description" TEXT, "created_at" TIMESTAMP WITH TIME ZONE DEFAULT NOW() NOT NULL, CONSTRAINT "mi_role_pkey" PRIMARY KEY ("role_id"));

ALTER TABLE "mi_role" ADD CONSTRAINT "mi_role_role_key_key" UNIQUE ("role_key");


CREATE TABLE "mi_user_role" ("user_id" UUID NOT NULL, "role_id" UUID NOT NULL, "granted_at" TIMESTAMP WITH TIME ZONE DEFAULT NOW() NOT NULL, CONSTRAINT "mi_user_role_pkey" PRIMARY KEY ("user_id", "role_id"));

CREATE INDEX "mi_user_role_user_idx" ON "mi_user_role" USING btree("user_id");

CREATE INDEX "mi_user_role_role_idx" ON "mi_user_role" USING btree("role_id");

ALTER TABLE "mi_user_role" ADD CONSTRAINT "mi_user_role_role_id_fkey" FOREIGN KEY ("role_id") REFERENCES "mi_role" ("role_id") ON UPDATE NO ACTION ON DELETE CASCADE;

ALTER TABLE "mi_user_role" ADD CONSTRAINT "mi_user_role_user_id_fkey" FOREIGN KEY ("user_id") REFERENCES "mi_user" ("user_id") ON UPDATE NO ACTION ON DELETE CASCADE;


CREATE TABLE "mi_report" ("report_id" UUID NOT NULL, "report_key" TEXT NOT NULL, "title" TEXT NOT NULL, "description" TEXT, "is_active" BOOLEAN DEFAULT TRUE NOT NULL, "created_at" TIMESTAMP WITH TIME ZONE DEFAULT NOW() NOT NULL, CONSTRAINT "mi_report_pkey" PRIMARY KEY ("report_id"));

ALTER TABLE "mi_report" ADD CONSTRAINT "mi_report_report_key_key" UNIQUE ("report_key");


CREATE TABLE "mi_report_group" ("group_id" UUID NOT NULL, "group_key" TEXT NOT NULL, "title" TEXT NOT NULL, CONSTRAINT "mi_report_group_pkey" PRIMARY KEY ("group_id"));

ALTER TABLE "mi_report_group" ADD CONSTRAINT "mi_report_group_group_key_key" UNIQUE ("group_key");


CREATE TABLE "mi_report_group_map" ("group_id" UUID NOT NULL, "report_id" UUID NOT NULL, CONSTRAINT "mi_report_group_map_pkey" PRIMARY KEY ("group_id", "report_id"));

ALTER TABLE "mi_report_group_map" ADD CONSTRAINT "mi_report_group_map_group_id_fkey" FOREIGN KEY ("group_id") REFERENCES "mi_report_group" ("group_id") ON UPDATE NO ACTION ON DELETE CASCADE;

ALTER TABLE "mi_report_group_map" ADD CONSTRAINT "mi_report_group_map_report_id_fkey" FOREIGN KEY ("report_id") REFERENCES "mi_report" ("report_id") ON UPDATE NO ACTION ON DELETE CASCADE;


CREATE TABLE "mi_permission" ("permission_id" UUID NOT NULL, "permission_key" TEXT NOT NULL, "description" TEXT, CONSTRAINT "mi_permission_pkey" PRIMARY KEY ("permission_id"));

ALTER TABLE "mi_permission" ADD CONSTRAINT "mi_permission_permission_key_key" UNIQUE ("permission_key");


CREATE TABLE "mi_role_report_permission" ("role_id" UUID NOT NULL, "report_id" UUID NOT NULL, "permission_id" UUID NOT NULL, "granted" BOOLEAN DEFAULT TRUE NOT NULL, CONSTRAINT "mi_role_report_permission_pkey" PRIMARY KEY ("role_id", "report_id", "permission_id"));

CREATE INDEX "mi_rrp_role_report_idx" ON "mi_role_report_permission" USING btree("role_id", "report_id");

CREATE INDEX "mi_rrp_permission_idx" ON "mi_role_report_permission" USING btree("permission_id");

CREATE INDEX "mi_rrp_report_idx" ON "mi_role_report_permission" USING btree("report_id");

ALTER TABLE "mi_role_report_permission" ADD CONSTRAINT "mi_role_report_permission_permission_id_fkey" FOREIGN KEY ("permission_id") REFERENCES "mi_permission" ("permission_id") ON UPDATE NO ACTION ON DELETE RESTRICT;

ALTER TABLE "mi_role_report_permission" ADD CONSTRAINT "mi_role_report_permission_report_id_fkey" FOREIGN KEY ("report_id") REFERENCES "mi_report" ("report_id") ON UPDATE NO ACTION ON DELETE CASCADE;

ALTER TABLE "mi_role_report_permission" ADD CONSTRAINT "mi_role_report_permission_role_id_fkey" FOREIGN KEY ("role_id") REFERENCES "mi_role" ("role_id") ON UPDATE NO ACTION ON DELETE CASCADE;


CREATE TABLE "mi_user_report_permission" ("user_id" UUID NOT NULL, "report_id" UUID NOT NULL, "permission_id" UUID NOT NULL, "granted" BOOLEAN NOT NULL, "reason" TEXT, "granted_at" TIMESTAMP WITH TIME ZONE DEFAULT NOW() NOT NULL, CONSTRAINT "mi_user_report_permission_pkey" PRIMARY KEY ("user_id", "report_id", "permission_id"));

CREATE INDEX "mi_rrp_report_permission_idx" ON "mi_role_report_permission" USING btree("report_id", "permission_id");

CREATE INDEX "mi_urp_user_report_idx" ON "mi_user_report_permission" USING btree("user_id", "report_id");

CREATE INDEX "mi_urp_report_idx" ON "mi_user_report_permission" USING btree("report_id");

CREATE INDEX "mi_urp_permission_idx" ON "mi_user_report_permission" USING btree("permission_id");

ALTER TABLE "mi_user_report_permission" ADD CONSTRAINT "mi_user_report_permission_permission_id_fkey" FOREIGN KEY ("permission_id") REFERENCES "mi_permission" ("permission_id") ON UPDATE NO ACTION ON DELETE RESTRICT;

ALTER TABLE "mi_user_report_permission" ADD CONSTRAINT "mi_user_report_permission_report_id_fkey" FOREIGN KEY ("report_id") REFERENCES "mi_report" ("report_id") ON UPDATE NO ACTION ON DELETE CASCADE;

ALTER TABLE "mi_user_report_permission" ADD CONSTRAINT "mi_user_report_permission_user_id_fkey" FOREIGN KEY ("user_id") REFERENCES "mi_user" ("user_id") ON UPDATE NO ACTION ON DELETE CASCADE;

DROP VIEW IF EXISTS "mi_effective_report_permission";

CREATE OR REPLACE VIEW "mi_effective_report_permission" AS
SELECT
    u.display_name,
    u.email,
    rep.report_key,
    rep.title,
    rep.description,
    COALESCE(upr.granted, bool_or(rpr.granted), false) AS granted
FROM mi_user u
JOIN mi_report rep ON true
JOIN mi_permission p ON p.permission_key = 'REPORT_VIEW'
LEFT JOIN mi_user_report_permission upr
    ON upr.user_id = u.user_id
   AND upr.report_id = rep.report_id
   AND upr.permission_id = p.permission_id
LEFT JOIN mi_user_role ur
    ON ur.user_id = u.user_id
LEFT JOIN mi_role_report_permission rpr
    ON rpr.role_id = ur.role_id
   AND rpr.report_id = rep.report_id
   AND rpr.permission_id = p.permission_id
GROUP BY
    u.display_name,
    u.email,
    rep.report_id,
    p.permission_id,
    upr.granted
HAVING COALESCE(upr.granted, bool_or(rpr.granted), false) = true;

DROP VIEW IF EXISTS mi_effective_report_all_permission;

CREATE OR REPLACE VIEW "mi_effective_report_all_permission" AS 
SELECT rep.report_key,
      COALESCE(u.external_subject_normalized, u.external_subject) AS external_subject,
      p.permission_key
FROM (((((mi_user u
   JOIN mi_report rep ON (true))
   JOIN mi_permission p ON (true))
   LEFT JOIN mi_user_report_permission upr ON (((upr.user_id = u.user_id) AND (upr.report_id = rep.report_id) AND (upr.permission_id = p.permission_id))))
   LEFT JOIN mi_user_role ur ON ((ur.user_id = u.user_id)))
   LEFT JOIN mi_role_report_permission rpr ON (((rpr.role_id = ur.role_id) AND (rpr.report_id = rep.report_id) AND (rpr.permission_id = p.permission_id))))
GROUP BY u.display_name, u.external_subject, u.external_subject_normalized, rep.report_key, rep.report_id, p.permission_id, upr.granted
HAVING ((COALESCE(upr.granted, bool_or(rpr.granted), false) = true) AND (COALESCE(rep.is_active, false) = true));

