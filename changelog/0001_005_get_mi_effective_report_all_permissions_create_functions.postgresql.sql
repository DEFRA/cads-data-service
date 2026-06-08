DROP VIEW IF EXISTS mi_effective_report_all_permission;

DROP FUNCTION IF EXISTS public.get_mi_effective_report_all_permission(
    p_external_subject TEXT,
    p_report_key   TEXT
);

CREATE OR REPLACE FUNCTION public.get_mi_effective_report_all_permission(
    p_external_subject TEXT,
    p_report_key   TEXT
    )
    RETURNS TABLE (
                    "report_key" TEXT,
                    "external_subject" TEXT,
                    "permission_key" TEXT
                  )
    LANGUAGE sql
AS $$
SELECT 
    rep.report_key as "report_key",
    COALESCE(u.external_subject_normalized, u.external_subject) as "external_subject",
    p.permission_key as "permission_key"
FROM (((((mi_user u
    JOIN mi_report rep ON (true))
    JOIN mi_permission p ON (true))
    LEFT JOIN mi_user_report_permission upr ON (((upr.user_id = u.user_id) AND (upr.report_id = rep.report_id) AND (upr.permission_id = p.permission_id))))
    LEFT JOIN mi_user_role ur ON ((ur.user_id = u.user_id)))
    LEFT JOIN mi_role_report_permission rpr ON (((rpr.role_id = ur.role_id) AND (rpr.report_id = rep.report_id) AND (rpr.permission_id = p.permission_id))))
WHERE 
    ((u.external_subject_normalized = p_external_subject) AND 
    (p_report_key is null or rep.report_key = p_report_key))
GROUP BY u.display_name, u.external_subject, u.external_subject_normalized, rep.report_key, rep.report_id, p.permission_id, upr.granted
HAVING ((COALESCE(upr.granted, bool_or(rpr.granted), false)) AND (COALESCE(rep.is_active, false)))
$$;