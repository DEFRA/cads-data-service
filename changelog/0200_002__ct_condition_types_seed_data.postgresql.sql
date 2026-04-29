-- liquibase formatted sql

-- changeset ref_table_seed:0200-017 splitStatements:false
INSERT INTO public._ct_condition_types VALUES (1, 'CIENF', 'CI Enforcement', '2001-06-20', 'Cattle Identification Enforcement', NULL, NULL, 'CPS', 'f800702', '1', '2001-06-29', 66, 1, 1);
INSERT INTO public._ct_condition_types VALUES (2, 'AHVG', 'AHVG Condition', '2001-04-26', 'AHVG Condition', NULL, NULL, 'BDA', 'x902926', '1', '2001-04-26', 40, 1, 2);
INSERT INTO public._ct_condition_types VALUES (3, 'CII', 'CII', '1998-01-01', 'Cattle Identification Inspections', NULL, NULL, 'ADM', 'f801344', '1', '1999-01-28', 66, 1, 3);
INSERT INTO public._ct_condition_types VALUES (4, 'ENF', 'Enf Referral', '1999-01-01', 'Enforcement Referral', NULL, NULL, 'ADM', 'f800702', '1', '2000-01-18', 66, 1, 4);
INSERT INTO public._ct_condition_types VALUES (5, 'M_ANO', 'Movem''t Anomaly', '2001-01-01', 'Movement Anomaly', NULL, NULL, 'CPS', 'f800702', '1', '2001-09-19', 66, 1, 5);
INSERT INTO public._ct_condition_types VALUES (6, 'SDET', 'SDET', '1998-01-01', 'Scottish Dam Upload', NULL, NULL, 'CPS', 'x903052', '1', '1999-03-09', 66, 1, 6);
INSERT INTO public._ct_condition_types VALUES (7, 'ADMIN', 'Administration', '1999-09-01', 'Administration', NULL, NULL, 'CPS', 'f800702', '1', '1999-11-16', 66, 1, 7);
INSERT INTO public._ct_condition_types VALUES (8, 'DIS', 'Disease', '1991-01-01', 'Disease Notification', NULL, NULL, 'ADM', 'f801344', '1', '1999-01-28', 66, 1, 8);
INSERT INTO public._ct_condition_types VALUES (9, 'CHR', 'CHR', '2000-05-01', 'Cattle Herd Registration', NULL, NULL, 'CPS', 'x903124', '1', '2000-05-15', 66, 1, 9);
INSERT INTO public._ct_condition_types VALUES (10, 'AH', 'Animal Health', '2001-11-01', 'Animal Health', NULL, NULL, NULL, 'x905756', '1', '2001-11-22', 66, 1, 10);
INSERT INTO public._ct_condition_types VALUES (11, 'EXNTF', 'Ext Notificatn', '2003-08-13', 'External Notification', NULL, NULL, NULL, 'f800702', '1', '2003-08-22', 66, 1, 11);
