create table mi_role (
    role_id      uuid not null,
    role_key     text not null,
    description  text,
    created_at   timestamptz not null default now(),

	constraint mi_role_pkey
		primary key(role_id),

	constraint mi_role_role_key_key
        unique (role_key)
);