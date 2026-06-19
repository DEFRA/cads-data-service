-- liquibase formatted sql

--changeset mark:seed-mip-viewer-user context:local
INSERT INTO cads.mi_user (user_id, external_subject, display_name, email, is_active)
VALUES (gen_random_uuid(), 'mip-viewer-user@internal.test', 'Test MIP Viewer', 'mip-viewer-user@internal.test', true);

--changeset mark:seed-mip-viewer-role context:local
INSERT INTO cads.mi_user_role (user_id, role_id)
SELECT u.user_id, r.role_id
FROM cads.mi_user u
JOIN cads.mi_role r ON r.role_key = 'MI_ADMIN'
WHERE u.email = 'mip-viewer-user@internal.test';
