-- liquibase formatted sql

-- changeset MarkGent1:1772802619383-3 splitStatements:false
CREATE VIEW "mi_effective_report_permission" AS SELECT u.user_id,
    r.report_id,
    p.permission_id,
    COALESCE(upr.granted, (max((rpr.granted)::integer))::boolean, false) AS granted
   FROM (((((mi_user u
     JOIN mi_report r on true)
     JOIN mi_permission p on true)
     LEFT JOIN mi_user_report_permission upr ON (((upr.user_id = u.user_id) AND (upr.report_id = r.report_id) AND (upr.permission_id = p.permission_id))))
     LEFT JOIN mi_user_role ur ON ((ur.user_id = u.user_id)))
     LEFT JOIN mi_role_report_permission rpr ON (((rpr.role_id = ur.role_id) AND (rpr.report_id = r.report_id) AND (rpr.permission_id = p.permission_id))))
  GROUP BY u.user_id, r.report_id, p.permission_id, upr.granted;
