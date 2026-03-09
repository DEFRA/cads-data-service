insert into mi_role_report_permission (role_id, report_id, permission_id, granted)
select
  r.role_id,
  rep.report_id,
  p.permission_id,
  true
from mi_role r
cross join mi_report rep
join mi_permission p
    on p.permission_key = 'REPORT_VIEW'
where r.role_key = 'MI_ADMIN';