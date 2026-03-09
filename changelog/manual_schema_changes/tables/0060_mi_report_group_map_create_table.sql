create table mi_report_group_map (
    group_id  uuid not null,
    report_id uuid not null,

	constraint mi_report_group_map_pkey
        primary key (group_id, report_id),

	constraint mi_report_group_map_group_id_fkey
        foreign key (group_id)
        references mi_report_group(group_id)
        on delete cascade,

    constraint mi_report_group_map_report_id_fkey
        foreign key (report_id)
        references mi_report(report_id)
        on delete cascade
);