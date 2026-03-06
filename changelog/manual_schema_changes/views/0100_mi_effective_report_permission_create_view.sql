create view mi_effective_report_permission as
select
    u.user_id,
    r.report_id,
    p.permission_id,
    coalesce(
        upr.granted,
        max((rpr.granted)::int)::boolean,
        false
    ) as granted
from mi_user u
cross join mi_report r
cross join mi_permission p
left join mi_user_report_permission upr
	on upr.user_id = u.user_id
		and upr.report_id = r.report_id
		and upr.permission_id = p.permission_id
left join mi_user_role ur
	on ur.user_id = u.user_id
left join mi_role_report_permission rpr
	on rpr.role_id = ur.role_id
		and rpr.report_id = r.report_id
		and rpr.permission_id = p.permission_id
group by
    u.user_id,
    r.report_id,
    p.permission_id,
    upr.granted;