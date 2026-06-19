-- liquibase formatted sql

-- changeset seed:0002-mi-seed splitStatements:false


-- sonar-ignore-start
insert into cads.mi_permission (permission_id, permission_key, description)
values
  (gen_random_uuid(), 'REPORT_VIEW', 'View report'),
  (gen_random_uuid(), 'REPORT_EXPORT', 'Export report data');
-- sonar-ignore-end

-- sonar-ignore-start
insert into cads.mi_role (role_id, role_key, description)
values
    (gen_random_uuid(), 'MI_ADMIN', 'MI administrator with full report access');
-- sonar-ignore-end

-- sonar-ignore-start
insert into cads.mi_report (report_id, report_key, title, description)
values
  (gen_random_uuid(), 'holding_summary',
   'Holding summary',
   'Summary of a single holding, including movements, species and mapped holdings'),

  (gen_random_uuid(), 'animal_summary',
   'Animal summary',
   'Movement history and mapped journey for an individual animal'),

  (gen_random_uuid(), 'movements_all_holdings',
   'Movements (all holdings)',
   'High-level movement metrics including cross-border flows'),

  (gen_random_uuid(), 'movement_summary_holding',
   'Movement summary (holding)',
   'On/off movement summaries for a holding'),

  (gen_random_uuid(), 'journey_by_haulier',
   'Journey by haulier',
   'Journey-level view by vehicle and haulier'),

  (gen_random_uuid(), 'zonal_movements_summary',
   'Zonal movements summary',
   'Into-zone, out-of-zone and within-zone movement summaries'),

  (gen_random_uuid(), 'cohort_tracing',
   'Cohort tracing',
   'Cohort holdings and animal status summary'),

  (gen_random_uuid(), 'sheep_goat_inspections',
   'Sheep and goat inspections',
   'Inspection-oriented holding and movement view'),

  (gen_random_uuid(), 'unregistered_herds_flocks',
   'Unregistered herds and flocks',
   'Data quality view of unregistered and archived herds/flocks'),

  (gen_random_uuid(), 'scrapie_flock_scheme_audit',
   'Scrapie flock scheme audit',
   'Compulsory Scrapie Flock Scheme audit view'),

    (gen_random_uuid(), 'gb_cattle_registrations',
    'GB cattle registrations',
    'Monthly GB cattle registrations'),

    (gen_random_uuid(), 'gb_cattle_deaths',
    'GB cattle deaths',
    'Monthly GB cattle deaths');

-- sonar-ignore-end


insert into cads.mi_role_report_permission (role_id, report_id, permission_id, granted)
select
  r.role_id,
  rep.report_id,
  p.permission_id,
  true
from cads.mi_role r
cross join cads.mi_report rep
join cads.mi_permission p
    on p.permission_key = 'REPORT_VIEW'
where r.role_key = 'MI_ADMIN';