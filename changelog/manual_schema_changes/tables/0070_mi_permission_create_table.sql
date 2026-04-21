create table mi_permission (
    permission_id uuid not null,
    permission_key text not null,
    description text,

	constraint mi_permission_pkey
        primary key (permission_id),

	constraint mi_permission_permission_key_key
        unique (permission_key)
);