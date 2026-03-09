create table mi_report_group (
    group_id   uuid not null,
    group_key  text not null,
    title      text not null,

	constraint mi_report_group_pkey
        primary key (group_id),

    constraint mi_report_group_group_key_key
        unique (group_key)
);