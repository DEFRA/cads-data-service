drop view if exists mi_effective_report_permission;

create view mi_effective_report_permission as
select
    u.display_name,
    u.email,
    rep.report_key,
    rep.title,
    rep.description,
    coalesce(
        upr.granted,
        bool_or(rpr.granted),
        false
    ) as granted
from mi_user u
join mi_report rep
    on true
join mi_permission p
    on p.permission_key = 'REPORT_VIEW'
left join mi_user_report_permission upr
	on upr.user_id = u.user_id
		and upr.report_id = rep.report_id
		and upr.permission_id = p.permission_id
left join mi_user_role ur
	on ur.user_id = u.user_id
left join mi_role_report_permission rpr
	on rpr.role_id = ur.role_id
		and rpr.report_id = rep.report_id
		and rpr.permission_id = p.permission_id
group by
    u.display_name,
    u.email,
    rep.report_id,
    p.permission_id,
    upr.granted
having coalesce(
       upr.granted,
       bool_or(rpr.granted),
       false
       )= true;