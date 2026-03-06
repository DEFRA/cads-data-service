create table mi_role_report_permission (
    role_id        uuid not null,
    report_id      uuid not null,
    permission_id  uuid not null,
    granted        boolean not null default true,

	constraint mi_role_report_permission_pkey
        primary key (role_id, report_id, permission_id),

	constraint mi_role_report_permission_role_id_fkey
        foreign key (role_id)
        references mi_role(role_id)
        on delete cascade,

	constraint mi_role_report_permission_report_id_fkey
        foreign key (report_id)
        references mi_report(report_id)
        on delete cascade,

    constraint mi_role_report_permission_permission_id_fkey
        foreign key (permission_id)
        references mi_permission(permission_id)
);

-- IDX: support fast lookup of all permissions for a role on a report
create index mi_rrp_role_report_idx
    on mi_role_report_permission (role_id, report_id);

-- IDX: support fast lookup of all roles that grant a specific permission
create index mi_rrp_permission_idx
    on mi_role_report_permission (permission_id);

-- IDX: support fast lookup of all permissions for a given report
create index mi_rrp_report_idx
    on mi_role_report_permission (report_id);