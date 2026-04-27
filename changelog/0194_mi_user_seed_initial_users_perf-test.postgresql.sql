-- liquibase formatted sql

--changeset mark:cleanup-gary-fletcher-roles context:perf-test
DELETE FROM mi_user_role
WHERE user_id IN (
    SELECT user_id FROM mi_user WHERE email = '${gfEmail}'
);

--changeset mark:cleanup-gary-fletcher-user context:perf-test
DELETE FROM mi_user
WHERE email = '${gfEmail}';


--changeset mark:seed-mark-gent context:perf-test
INSERT INTO mi_user (user_id, external_subject, display_name, email, is_active)
VALUES (gen_random_uuid(), '${mgEmail}', '${mgName}', '${mgEmail}', true);

--changeset mark:seed-mark-gent-role context:perf-test
INSERT INTO mi_user_role (user_id, role_id)
SELECT u.user_id, r.role_id
FROM mi_user u
JOIN mi_role r ON r.role_key = 'MI_ADMIN'
WHERE u.email = '${mgEmail}';

--changeset mark:seed-gary-fletcher context:perf-test
INSERT INTO mi_user (user_id, external_subject, display_name, email, is_active)
VALUES (gen_random_uuid(), '${gfEmail}', '${gfName}', '${gfEmail}', true);

--changeset mark:seed-gary-fletcher-role context:perf-test
INSERT INTO mi_user_role (user_id, role_id)
SELECT u.user_id, r.role_id
FROM mi_user u
JOIN mi_role r ON r.role_key = 'MI_ADMIN'
WHERE u.email = '${gfEmail}';


--changeset mark:seed-craig-john context:perf-test
INSERT INTO mi_user (user_id, external_subject, display_name, email, is_active)
VALUES (gen_random_uuid(), '${cjEmail}', '${cjName}', '${cjEmail}', true);

--changeset mark:seed-craig-john-role context:perf-test
INSERT INTO mi_user_role (user_id, role_id)
SELECT u.user_id, r.role_id
FROM mi_user u
JOIN mi_role r ON r.role_key = 'MI_ADMIN'
WHERE u.email = '${cjEmail}';


--changeset mark:seed-david-baillie context:perf-test
INSERT INTO mi_user (user_id, external_subject, display_name, email, is_active)
VALUES (gen_random_uuid(), '${dbEmail}', '${dbName}', '${dbEmail}', true);

--changeset mark:seed-david-baillie-role context:perf-test
INSERT INTO mi_user_role (user_id, role_id)
SELECT u.user_id, r.role_id
FROM mi_user u
JOIN mi_role r ON r.role_key = 'MI_ADMIN'
WHERE u.email = '${dbEmail}';


--changeset mark:seed-andy-rose context:perf-test
INSERT INTO mi_user (user_id, external_subject, display_name, email, is_active)
VALUES (gen_random_uuid(), '${arEmail}', '${arName}', '${arEmail}', true);

--changeset mark:seed-andy-rose-role context:perf-test
INSERT INTO mi_user_role (user_id, role_id)
SELECT u.user_id, r.role_id
FROM mi_user u
JOIN mi_role r ON r.role_key = 'MI_ADMIN'
WHERE u.email = '${arEmail}';


--changeset mark:seed-tom-harrison context:perf-test
INSERT INTO mi_user (user_id, external_subject, display_name, email, is_active)
VALUES (gen_random_uuid(), '${thEmail}', '${thName}', '${thEmail}', true);

--changeset mark:seed-tom-harrison-role context:perf-test
INSERT INTO mi_user_role (user_id, role_id)
SELECT u.user_id, r.role_id
FROM mi_user u
JOIN mi_role r ON r.role_key = 'MI_ADMIN'
WHERE u.email = '${thEmail}';