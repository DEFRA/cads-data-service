-- liquibase formatted sql

--changeset mark:seed-sa-cads-mip-dev context:dev
INSERT INTO mi_user (user_id, external_subject, display_name, email, is_active)
VALUES (gen_random_uuid(), '${jtDevEmail}', '${jtDevName}', '${jtDevEmail}', true);

--changeset mark:seed-sa-cads-mip-dev-role context:dev
INSERT INTO mi_user_role (user_id, role_id)
SELECT u.user_id, r.role_id
FROM mi_user u
JOIN mi_role r ON r.role_key = 'MI_ADMIN'
WHERE u.email = '${jtDevEmail}';
