-- liquibase formatted sql

-- changeset dev_db_build:0005-anonymisation splitStatements:true
UPDATE _ct_parties
SET par_initials = chr(65 + (get_byte(decode(md5(par_id::text), 'hex'), 0) % 26)) || chr(65 + (get_byte(decode(md5(par_id::text), 'hex'), 1) % 26)),
    par_surname = 'Surname_' || par_id::text,
    par_title = (ARRAY['MR','MRS','MS','MISS','DR'])[1 + (get_byte(decode(md5(par_id::text), 'hex'), 2) % 5)],
    par_email_address = 'pa_email_' || par_id::text || '@defra.gov.uk',
    par_fax_number = 'PA-FAX-' || par_id::text,
    par_tel_number = 'PA-TEL-' || par_id::text,
    par_mobile_number = 'PA-MOB-' || par_id::text,
    par_comments = 'PA-COMMENT-' || par_id::text
WHERE par_id IS NOT NULL;

UPDATE _ct_locations
SET loc_comments = 'PA-COMMENT-' || loc_id::text,
    loc_tel_number = 'PA-TEL-' || loc_id::text,
    loc_mobile_number = 'PA-MOB-' || loc_id::text,
    loc_fax_number = 'PA-FAX-' || loc_id::text,
    loc_email_address = 'pa_email_' || loc_id::text || '@defra.gov.uk'
WHERE loc_id IS NOT NULL;

UPDATE _ct_addresses
SET adr_name = 'ADR-NAME-' || adr_id::text,
    adr_address_2 = 'ADR-ADDR2-' || adr_id::text,
    adr_address_3 = 'ADR-ADDR3-' || adr_id::text,
    adr_address_4 = 'ADR-ADDR4-' || adr_id::text,
    adr_address_5 = 'ADR-ADDR5-' || adr_id::text,
    adr_post_code = 'PC' || lpad((adr_id % 100000)::text, 5, '0')
WHERE adr_id IS NOT NULL;

UPDATE _ct_label_requests
SET lar_keeper_title = (ARRAY['MR','MRS','MS','MISS','DR'])[1 + (get_byte(decode(md5(lar_id::text), 'hex'), 0) % 5)],
    lar_keeper_initials = 'LK' || lar_id::text,
    lar_keeper_surname = 'Surname_' || lar_id::text,
    lar_label_loc_name = 'LAR-LOC-' || lar_id::text,
    lar_label_address_2 = 'LAR-ADDR2-' || lar_id::text,
    lar_label_address_3 = 'LAR-ADDR3-' || lar_id::text,
    lar_label_address_4 = 'LAR-ADDR4-' || lar_id::text,
    lar_label_address_5 = 'LAR-ADDR5-' || lar_id::text,
    lar_label_post_code = 'PC' || lpad((lar_id % 100000)::text, 5, '0'),
    lar_corr_title = (ARRAY['MR','MRS','MS','MISS','DR'])[1 + (get_byte(decode(md5(lar_id::text), 'hex'), 1) % 5)],
    lar_corr_initials = 'LC' || lar_id::text,
    lar_corr_surname = 'Surname_' || lar_id::text,
    lar_corr_loc_name = 'LAR-CORR-' || lar_id::text,
    lar_corr_address_2 = 'LAR-CADDR2-' || lar_id::text,
    lar_corr_address_3 = 'LAR-CADDR3-' || lar_id::text,
    lar_corr_address_4 = 'LAR-CADDR4-' || lar_id::text,
    lar_corr_address_5 = 'LAR-CADDR5-' || lar_id::text,
    lar_corr_post_code = 'PC' || lpad((lar_id % 100000)::text, 5, '0')
WHERE lar_id IS NOT NULL;

UPDATE _ct_web_users
SET wur_mobile_number = 'WUR-MOB-' || wur_id::text,
    wur_telephone_number = 'WUR-TEL-' || wur_id::text,
    wur_user_name = 'WUR-USER-' || wur_id::text,
    wur_user_location = 'WUR-LOC-' || wur_id::text,
    wur_address_2 = 'WUR-ADDR2-' || wur_id::text,
    wur_address_3 = 'WUR-ADDR3-' || wur_id::text,
    wur_address_4 = 'WUR-ADDR4-' || wur_id::text,
    wur_address_5 = 'WUR-ADDR5-' || wur_id::text,
    wur_post_code = 'PC' || lpad((wur_id % 100000)::text, 5, '0'),
    wur_email_address = 'wur_email_' || wur_id::text || '@defra.gov.uk'
WHERE wur_id IS NOT NULL;

UPDATE _ct_comms_addresses
SET coa_email_address = 'coa_email_' || coa_id::text || '@defra.gov.uk'
WHERE coa_id IS NOT NULL;

UPDATE _ct_cts_users
SET cus_room_name = 'CUS-ROOM-' || cus_id::text,
    cus_email_address = 'cus_email_' || cus_id::text || '@defra.gov.uk'
WHERE cus_id IS NOT NULL;

UPDATE _ct_email_log
SET eml_email_addr_recd = 'eml_recv_' || eml_id::text || '@defra.gov.uk',
    eml_email_addr_sent = 'eml_sent_' || eml_id::text || '@defra.gov.uk'
WHERE eml_id IS NOT NULL;

UPDATE _ct_location_party_rels
SET lpr_comments = 'LPR-COMMENT-' || lpr_id::text
WHERE lpr_id IS NOT NULL;

UPDATE _ct_location_relationships
SET llr_comments = 'LLR-COMMENT-' || llr_id::text
WHERE llr_id IS NOT NULL;

UPDATE _ct_condition_markers
SET com_comments = 'COM-COMMENT-' || com_id::text
WHERE com_id IS NOT NULL;

UPDATE _ct_susp_condition_markers
SET scm_comments = 'SCM-COMMENT-' || scm_id::text
WHERE scm_id IS NOT NULL;
