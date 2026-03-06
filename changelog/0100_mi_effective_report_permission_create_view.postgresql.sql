-- liquibase formatted sql

-- changeset MarkGent1:1772802619383-3 splitStatements:false
CREATE VIEW "mi_effective_report_permission" AS SELECT u.user_id,
    r.report_id,
    p.permission_id,
    COALESCE(upr.granted, (max((rpr.granted)::integer))::boolean, false) AS granted
   FROM (((((mi_user u
     CROSS JOIN mi_report r)
     CROSS JOIN mi_permission p)
     LEFT JOIN mi_user_report_permission upr ON (((upr.user_id = u.user_id) AND (upr.report_id = r.report_id) AND (upr.permission_id = p.permission_id))))
     LEFT JOIN mi_user_role ur ON ((ur.user_id = u.user_id)))
     LEFT JOIN mi_role_report_permission rpr ON (((rpr.role_id = ur.role_id) AND (rpr.report_id = r.report_id) AND (rpr.permission_id = p.permission_id))))
  GROUP BY u.user_id, r.report_id, p.permission_id, upr.granted;

-- changeset MarkGent1:1772802619383-1 splitStatements:false
DO $$ DECLARE constraint_name varchar;
BEGIN
  SELECT tc.CONSTRAINT_NAME into strict constraint_name
    FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS tc
    WHERE CONSTRAINT_TYPE = 'PRIMARY KEY'
      AND TABLE_NAME = 'mi_user_report_permission' AND TABLE_SCHEMA = 'public';
    EXECUTE 'alter table "public"."mi_user_report_permission" drop constraint "' || constraint_name || '"';
END $$;

-- changeset MarkGent1:1772802619383-2 splitStatements:false
ALTER TABLE "mi_user_report_permission" ADD CONSTRAINT "mi_user_report_permission_pkey" PRIMARY KEY ("user_id", "report_id", "permission_id");

