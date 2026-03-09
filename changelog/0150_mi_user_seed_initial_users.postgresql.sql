insert into mi_user (
    user_id,
    external_subject,
    display_name,
    email,
    is_active
)
values (
    gen_random_uuid(),
    'gary.fletcher@defra.gov.uk',
    'Gary Fletcher',
    'gary.fletcher@defra.gov.uk',
    true
);