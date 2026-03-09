insert into mi_user_role (user_id, role_id)
select
  u.user_id,
  r.role_id
from mi_user u
join mi_role r
    on r.role_key = 'MI_ADMIN'
where u.email = 'gary.fletcher@defra.gov.uk';