-- sonar-ignore-start
insert into mi_report (report_id, report_key, title, description)
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
   'Compulsory Scrapie Flock Scheme audit view');
-- sonar-ignore-end