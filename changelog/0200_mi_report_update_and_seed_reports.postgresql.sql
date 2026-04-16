-- liquibase formatted sql
-- sonar-ignore-start

--changeset andy:update-and-seed-gb-user-reports-and-permissions
DO $$
    BEGIN
    -- Insert new reports for GB cattle registrations and deaths
    INSERT INTO mi_report (report_id, report_key, title, description)
    VALUES
        (gen_random_uuid(),
        'gb_cattle_registrations',
        'GB cattle registrations',
        'Monthly GB cattle registrations'),
        (gen_random_uuid(),
        'gb_cattle_deaths',
        'GB cattle deaths',
        'Monthly GB cattle deaths');

    -- Update role report permissions
    INSERT INTO mi_role_report_permission (role_id, report_id, permission_id, granted)
    SELECT
        r.role_id,
        rep.report_id,
        p.permission_id,
        true
    FROM mi_role r
     CROSS JOIN mi_report rep
     JOIN mi_permission p
      ON p.permission_key = 'REPORT_VIEW'
    WHERE r.role_key = 'MI_ADMIN'
    AND rep.report_key IN ('gb_cattle_registrations', 'gb_cattle_deaths');
END $$

-- sonar-ignore-end