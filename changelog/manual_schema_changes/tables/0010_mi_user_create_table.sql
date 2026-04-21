create table mi_user (
    user_id           uuid not null,
    external_subject  text not null,
    display_name      text not null,
    email             text,
    is_active         boolean not null default true,
    created_at        timestamptz not null default now(),

	constraint mi_user_pkey
		primary key(user_id),

	constraint mi_user_external_subject_key
        unique (external_subject)
);