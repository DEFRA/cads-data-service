create table mi_user_role (
    user_id uuid not null,
    role_id uuid not null,
    granted_at timestamptz not null default now(),

	constraint mi_user_role_pkey
		primary key(user_id, role_id),

	constraint mi_user_role_user_id_fkey
        foreign key (user_id)
        references mi_user(user_id)
        on delete cascade,

    constraint mi_user_role_role_id_fkey
        foreign key (role_id)
        references mi_role(role_id)
        on delete cascade
);

-- IDX: support frequently resolving permissions via user > role
create index mi_user_role_user_idx
    on mi_user_role (user_id);

-- IDX: support frequently resolving permissions via user > role
create index mi_user_role_role_idx
    on mi_user_role (role_id);