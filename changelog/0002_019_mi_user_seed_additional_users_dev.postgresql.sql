-- liquibase formatted sql

--changeset andy:seed-matthew-hodgson context:dev
INSERT INTO mi_user (user_id, external_subject, display_name, email, is_active)
VALUES (gen_random_uuid(), '${mhEmail}', '${mhName}', '${mhEmail}', true);

--changeset andy:seed-matthew-hodgson-role context:dev
INSERT INTO mi_user_role (user_id, role_id)
SELECT u.user_id, r.role_id
FROM mi_user u
JOIN mi_role r ON r.role_key = 'MI_ADMIN'
WHERE u.email = '${mhEmail}';

--changeset andy:seed-mark-stocker context:dev
INSERT INTO mi_user (user_id, external_subject, display_name, email, is_active)
VALUES (gen_random_uuid(), '${msEmail}', '${msName}', '${msEmail}', true);

--changeset andy:seed-mark-stocker-role context:dev
INSERT INTO mi_user_role (user_id, role_id)
SELECT u.user_id, r.role_id
FROM mi_user u
JOIN mi_role r ON r.role_key = 'MI_ADMIN'
WHERE u.email = '${msEmail}';


--changeset andy:seed-verrol-skerrit context:dev
INSERT INTO mi_user (user_id, external_subject, display_name, email, is_active)
VALUES (gen_random_uuid(), '${vsEmail}', '${vsName}', '${vsEmail}', true);

--changeset andy:seed-verrol-skerrit-role context:dev
INSERT INTO mi_user_role (user_id, role_id)
SELECT u.user_id, r.role_id
FROM mi_user u
JOIN mi_role r ON r.role_key = 'MI_ADMIN'
WHERE u.email = '${vsEmail}';