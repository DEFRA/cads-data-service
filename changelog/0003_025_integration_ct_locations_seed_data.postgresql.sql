-- liquibase formatted sql

-- changeset dev_db_build:0003-025-inttest-ct-locations splitStatements:false context:integration
INSERT INTO cts.ct_locations (
    loc_receive_ppaf_flag,
    loc_id,
    loc_slt_id,
    loc_lty_id,
    loc_cty_id,
    loc_receive_labels_flag,
    loc_effective_from,
    loc_effective_to,
    loc_cessation_reason,
    loc_premises_type,
    loc_comments,
    loc_map_reference,
    loc_source_identifier,
    loc_source_reference,
    loc_tel_number,
    loc_mobile_number,
    loc_fax_number,
    loc_email_address,
    loc_current_status,
    loc_current_user,
    loc_current_modified_date,
    loc_current_pid,
    loc_reason_code,
    loc_version,
    row_number,
    record_type,
    record_count,
    imported_date
) VALUES
    ('Y', 1, NULL, 2, 278, 'N', DATE '1996-07-01', DATE '1998-01-17', 'BC', 'AH', 'PA-COMMENT-1', NULL, 'VT', '99037438', 'PA-TEL-1', 'PA-MOB-1', 'PA-FAX-1', 'pa_email_1@defra.gov.uk', '1', 'm184910', DATE '2005-06-14', 29, 'AC', 1, 1, 'D', 1, TIMESTAMP '2026-05-01 17:05:45.95947'),
    ('Y', 2, NULL, 2, 278, 'N', DATE '1996-07-01', DATE '1998-01-17', 'BC', 'AH', 'PA-COMMENT-2', NULL, 'VT', '99012657', 'PA-TEL-2', 'PA-MOB-2', 'PA-FAX-2', 'pa_email_2@defra.gov.uk', '1', 'm184910', DATE '2005-06-14', 29, 'AC', 1, 2, 'D', 2, TIMESTAMP '2026-05-01 17:05:45.95947'),
    ('Y', 3, NULL, 2, 278, 'N', DATE '1996-07-01', DATE '1998-01-19', 'BC', 'AH', 'PA-COMMENT-3', NULL, 'VT', '99014178', 'PA-TEL-3', 'PA-MOB-3', 'PA-FAX-3', 'pa_email_3@defra.gov.uk', '1', 'm184910', DATE '2005-06-14', 29, 'AC', 1, 3, 'D', 3, TIMESTAMP '2026-05-01 17:05:45.95947'),
    ('Y', 4, NULL, 2, 278, 'N', DATE '1996-07-01', DATE '1998-01-17', 'BC', 'AH', 'PA-COMMENT-4', NULL, 'VT', '99037447', 'PA-TEL-4', 'PA-MOB-4', 'PA-FAX-4', 'pa_email_4@defra.gov.uk', '1', 'm184910', DATE '2005-06-14', 29, 'AC', 1, 4, 'D', 4, TIMESTAMP '2026-05-01 17:05:45.95947'),
    ('Y', 5, NULL, 2, 278, 'N', DATE '1996-07-01', DATE '1998-01-17', 'BC', 'AH', 'PA-COMMENT-5', NULL, 'VT', '99017453', 'PA-TEL-5', 'PA-MOB-5', 'PA-FAX-5', 'pa_email_5@defra.gov.uk', '1', 'm184910', DATE '2005-06-14', 29, 'AC', 1, 5, 'D', 5, TIMESTAMP '2026-05-01 17:05:45.95947')
ON CONFLICT DO NOTHING;
	
INSERT INTO cts.ct_location_identifiers (
    lid_id,
    lid_loc_id,
    lid_effective_from_date,
    lid_identifier,
    lid_full_identifier,
    lid_sub_identifier,
    lid_effective_to_date,
    lid_current_status,
    lid_current_modified_date,
    lid_current_user,
    lid_current_pid,
    lid_current_amend_reason,
    lid_version,
    row_number,
    record_type,
    record_count,
    imported_date
) VALUES
    (1, 1, DATE '1996-07-01', '00/477/0081', 'AH-00/477/0081', NULL, NULL, '1', DATE '1998-01-17', 'x901841', 39, NULL, 1, 236, 'D', 236, CURRENT_TIMESTAMP),
	(2, 2, DATE '1996-07-01', '00/585/0044', 'AH-00/585/0044', NULL, NULL, '1', DATE '1998-01-17', 'x901841', 39, NULL, 1, 244, 'D', 244, CURRENT_TIMESTAMP),
	(3, 3, DATE '1996-07-01', '00/653/0039', 'AH-00/653/0039', NULL, NULL, '1', DATE '1998-01-17', 'x901841', 39, NULL, 1, 253, 'D', 253, CURRENT_TIMESTAMP),
    (4, 4, DATE '1996-07-01', '00/723/0102', 'AH-00/723/0102', NULL, NULL, '1', DATE '1998-01-17', 'x901841', 39, NULL, 1, 950, 'D', 950, CURRENT_TIMESTAMP),
    (5, 5, DATE '1996-07-01', '00/852/6001', 'AH-00/852/6001', NULL, NULL, '1', DATE '1998-01-17', 'x901841', 39, NULL, 1, 959, 'D', 959, CURRENT_TIMESTAMP)
ON CONFLICT DO NOTHING;
