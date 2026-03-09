create table mi_report (
    report_id    uuid not null,
    report_key   text not null,
    title        text not null,
    description  text,
    is_active    boolean not null default true,
    created_at   timestamptz not null default now(),

	constraint mi_report_pkey
        primary key (report_id),

    constraint mi_report_report_key_key
        unique (report_key)
);