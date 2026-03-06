create table mi_user_report_permission (
    user_id        uuid not null,
    report_id      uuid not null,
    permission_id  uuid not null,
    granted        boolean not null,
    reason         text,
    granted_at     timestamptz not null default now(),

	constraint mi_user_report_permission_pkey
        primary key (user_id, report_id, permission_id),

    constraint mi_user_report_permission_user_id_fkey
        foreign key (user_id)
        references mi_user(user_id)
        on delete cascade,

    constraint mi_user_report_permission_report_id_fkey
        foreign key (report_id)
        references mi_report(report_id)
        on delete cascade,

    constraint mi_user_report_permission_permission_id_fkey
        foreign key (permission_id)
        references mi_permission(permission_id)
);

-- IDX: support fast lookup of user → report → permissions
create index mi_urp_user_report_idx
    on mi_user_report_permission (user_id, report_id);

-- IDX: support fast lookup of all overrides for a report
create index mi_urp_report_idx
    on mi_user_report_permission (report_id);

-- IDX: support fast lookup of all users with a specific permission
create index mi_urp_permission_idx
    on mi_user_report_permission (permission_id);

-- IDX: support fast lookup of all permissions user X has for report Y
create index mi_urp_user_report_permission_idx
    on mi_user_report_permission (user_id, report_id, permission_id);

-- IDX: support fast lookup of all users with report X and with permission Y
create index mi_rrp_report_permission_idx
    on mi_role_report_permission (report_id, permission_id);