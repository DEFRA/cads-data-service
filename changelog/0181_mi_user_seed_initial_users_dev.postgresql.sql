-- liquibase formatted sql

--changeset mark:cleanup-gary-fletcher-roles context:dev
DELETE FROM mi_user_role
WHERE user_id IN (
    SELECT user_id FROM mi_user WHERE email = 'gary.fletcher@defra.gov.uk'
);

--changeset mark:cleanup-gary-fletcher-user context:dev
DELETE FROM mi_user
WHERE email = 'gary.fletcher@defra.gov.uk';


--changeset mark:seed-mark-gent context:dev
INSERT INTO mi_user (user_id, external_subject, display_name, email, is_active)
VALUES (gen_random_uuid(), 'mark.gent@defra.gov.uk', 'Mark Gent', 'mark.gent@defra.gov.uk', true);

--changeset mark:seed-mark-gent-role context:dev
INSERT INTO mi_user_role (user_id, role_id)
SELECT u.user_id, r.role_id
FROM mi_user u
JOIN mi_role r ON r.role_key = 'MI_ADMIN'
WHERE u.email = 'mark.gent@defra.gov.uk';

--changeset mark:seed-gary-fletcher context:dev
INSERT INTO mi_user (user_id, external_subject, display_name, email, is_active)
VALUES (gen_random_uuid(), 'gary.fletcher@defra.gov.uk', 'Gary Fletcher', 'gary.fletcher@defra.gov.uk', true);

--changeset mark:seed-gary-fletcher-role context:dev
INSERT INTO mi_user_role (user_id, role_id)
SELECT u.user_id, r.role_id
FROM mi_user u
JOIN mi_role r ON r.role_key = 'MI_ADMIN'
WHERE u.email = 'gary.fletcher@defra.gov.uk';


--changeset mark:seed-craig-john context:dev
INSERT INTO mi_user (user_id, external_subject, display_name, email, is_active)
VALUES (gen_random_uuid(), 'craig.john@defra.gov.uk', 'Craig John', 'craig.john@defra.gov.uk', true);

--changeset mark:seed-craig-john-role context:dev
INSERT INTO mi_user_role (user_id, role_id)
SELECT u.user_id, r.role_id
FROM mi_user u
JOIN mi_role r ON r.role_key = 'MI_ADMIN'
WHERE u.email = 'craig.john@defra.gov.uk';


--changeset mark:seed-david-baillie context:dev
INSERT INTO mi_user (user_id, external_subject, display_name, email, is_active)
VALUES (gen_random_uuid(), 'David.Baillie@defra.gov.uk', 'David Baillie', 'David.Baillie@defra.gov.uk', true);

--changeset mark:seed-david-baillie-role context:dev
INSERT INTO mi_user_role (user_id, role_id)
SELECT u.user_id, r.role_id
FROM mi_user u
JOIN mi_role r ON r.role_key = 'MI_ADMIN'
WHERE u.email = 'David.Baillie@defra.gov.uk';


--changeset mark:seed-andy-rose context:dev
INSERT INTO mi_user (user_id, external_subject, display_name, email, is_active)
VALUES (gen_random_uuid(), 'Andy.Rose@defra.gov.uk', 'Andy Rose', 'Andy.Rose@defra.gov.uk', true);

--changeset mark:seed-andy-rose-role context:dev
INSERT INTO mi_user_role (user_id, role_id)
SELECT u.user_id, r.role_id
FROM mi_user u
JOIN mi_role r ON r.role_key = 'MI_ADMIN'
WHERE u.email = 'Andy.Rose@defra.gov.uk';


--changeset mark:seed-tom-harrison context:dev
INSERT INTO mi_user (user_id, external_subject, display_name, email, is_active)
VALUES (gen_random_uuid(), 'Tom.Harrison@defra.gov.uk', 'Tom Harrison', 'Tom.Harrison@defra.gov.uk', true);

--changeset mark:seed-tom-harrison-role context:dev
INSERT INTO mi_user_role (user_id, role_id)
SELECT u.user_id, r.role_id
FROM mi_user u
JOIN mi_role r ON r.role_key = 'MI_ADMIN'
WHERE u.email = 'Tom.Harrison@defra.gov.uk';