-- liquibase formatted sql

-- changeset MarkGent1:1776696134738-1 splitStatements:false
DROP VIEW IF EXISTS mi_effective_report_all_permission;

CREATE OR REPLACE VIEW "mi_effective_report_all_permission" AS SELECT rep.report_key,
    u.external_subject,
    p.permission_key
   FROM (((((mi_user u
     JOIN mi_report rep ON (true))
     JOIN mi_permission p ON (true))
     LEFT JOIN mi_user_report_permission upr ON (((upr.user_id = u.user_id) AND (upr.report_id = rep.report_id) AND (upr.permission_id = p.permission_id))))
     LEFT JOIN mi_user_role ur ON ((ur.user_id = u.user_id)))
     LEFT JOIN mi_role_report_permission rpr ON (((rpr.role_id = ur.role_id) AND (rpr.report_id = rep.report_id) AND (rpr.permission_id = p.permission_id))))
  GROUP BY u.display_name, u.external_subject, rep.report_key, rep.report_id, p.permission_id, upr.granted
 HAVING ((COALESCE(upr.granted, bool_or(rpr.granted), false) = true) AND (COALESCE(rep.is_active, false) = true));

