-- NOSONAR
insert into mi_permission (permission_id, permission_key, description)
values
  (gen_random_uuid(), 'REPORT_VIEW', 'View report'),
  (gen_random_uuid(), 'REPORT_EXPORT', 'Export report data');