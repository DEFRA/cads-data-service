-- liquibase formatted sql

--changeset andy:seed-matthew-hodgson context:ext-test
INSERT INTO cads.mi_user (user_id, external_subject, display_name, email, is_active)
VALUES (gen_random_uuid(), '${mhEmail}', '${mhName}', '${mhEmail}', true);

--changeset andy:seed-matthew-hodgson-role context:ext-test
INSERT INTO cads.mi_user_role (user_id, role_id)
SELECT u.user_id, r.role_id
FROM cads.mi_user u
         JOIN cads.mi_role r ON r.role_key = 'MI_ADMIN'
WHERE u.email = '${mhEmail}';

--changeset andy:seed-mark-stocker context:ext-test
INSERT INTO cads.mi_user (user_id, external_subject, display_name, email, is_active)
VALUES (gen_random_uuid(), '${msEmail}', '${msName}', '${msEmail}', true);

--changeset andy:seed-mark-stocker-role context:ext-test
INSERT INTO cads.mi_user_role (user_id, role_id)
SELECT u.user_id, r.role_id
FROM cads.mi_user u
         JOIN cads.mi_role r ON r.role_key = 'MI_ADMIN'
WHERE u.email = '${msEmail}';


--changeset andy:seed-verrol-skerrit context:ext-test
INSERT INTO cads.mi_user (user_id, external_subject, display_name, email, is_active)
VALUES (gen_random_uuid(), '${vsEmail}', '${vsName}', '${vsEmail}', true);

--changeset andy:seed-verrol-skerrit-role context:ext-test
INSERT INTO cads.mi_user_role (user_id, role_id)
SELECT u.user_id, r.role_id
FROM cads.mi_user u
         JOIN cads.mi_role r ON r.role_key = 'MI_ADMIN'
WHERE u.email = '${vsEmail}';


--changeset mark:seed-andrew-norman context:ext-test
INSERT INTO cads.mi_user (user_id, external_subject, display_name, email, is_active)
VALUES (gen_random_uuid(), '${anEmail}', '${anName}', '${anEmail}', true);

--changeset mark:seed-andrew-norman-role context:ext-test
INSERT INTO cads.mi_user_role (user_id, role_id)
SELECT u.user_id, r.role_id
FROM cads.mi_user u
         JOIN cads.mi_role r ON r.role_key = 'MI_ADMIN'
WHERE u.email = '${anEmail}';