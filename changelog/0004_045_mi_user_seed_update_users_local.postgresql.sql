-- liquibase formatted sql

--changeset dev_db_build:0004-045-01
DELETE FROM cads.mi_user_role ur
USING cads.mi_user u
WHERE u.user_id = ur.user_id
  AND u.email = '${anEmail}';

DELETE FROM cads.mi_user
WHERE email = '${anEmail}';
