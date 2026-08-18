-- liquibase formatted sql

-- changeset seed:0002-002-mi-report-groups-seed splitStatements:false
alter table cads.mi_report_group
    add column if not exists display_order integer not null default 0;

alter table cads.mi_report_group
    alter column display_order drop default;

insert into cads.mi_report_group (group_id, group_key, title, display_order)
values
    (gen_random_uuid(), 'bcms', 'BCMS', 1),
    (gen_random_uuid(), 'example_reports_and_templates', 'Example Reports and Templates', 2);

insert into cads.mi_report_group_map (group_id, report_id)
select grp.group_id, rep.report_id
from cads.mi_report_group grp
join cads.mi_report rep
    on rep.report_key in (
        'gb_cattle_deaths',
        'gb_cattle_registrations',
        'gb_cattle_imports'
    )
where grp.group_key = 'bcms';

insert into cads.mi_report_group_map (group_id, report_id)
select grp.group_id, rep.report_id
from cads.mi_report_group grp
join cads.mi_report rep
    on rep.report_key not in (
        'gb_cattle_deaths',
        'gb_cattle_registrations',
        'gb_cattle_imports'
    )
where grp.group_key = 'example_reports_and_templates';
