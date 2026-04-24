-- liquibase formatted sql

-- changeset ref_table_seed:0200-016 splitStatements:false
INSERT INTO public._ct_eartag_reasons VALUES (1, 'BC', 'A', 'BC Available', 'Ear Tag available for Back Capture', '1', NULL, NULL, NULL, 1, 1);
INSERT INTO public._ct_eartag_reasons VALUES (2, 'MD', 'N', 'Damaged - CTS', 'Ear Tag Cancelled via CTS - Damaged', '1', NULL, NULL, NULL, 1, 2);
INSERT INTO public._ct_eartag_reasons VALUES (3, 'M', 'N', 'Lost', 'Ear tag cancelled (Lost) - May reissue', '1', NULL, NULL, NULL, 1, 3);
INSERT INTO public._ct_eartag_reasons VALUES (4, 'US', 'N', 'Ear Tag Used', 'Ear Tag has been used', '1', NULL, NULL, NULL, 1, 4);
INSERT INTO public._ct_eartag_reasons VALUES (5, 'KE', 'N', 'Canc Keeper Err', 'Ear Tag cancelled - keeper error', '1', NULL, NULL, NULL, 1, 5);
INSERT INTO public._ct_eartag_reasons VALUES (6, 'AU', 'A', 'Ordered', 'Ear tag ordered', '1', 'x902791', '2000-03-20', NULL, 1, 6);
INSERT INTO public._ct_eartag_reasons VALUES (7, 'CA', 'N', 'Canc Office Err', 'Ear Tag cancelled - office error', '1', NULL, NULL, NULL, 1, 7);
INSERT INTO public._ct_eartag_reasons VALUES (8, 'S', 'N', 'Stolen', 'Ear tag cancelled(Stolen) Cannot reissue', '1', NULL, NULL, NULL, 1, 8);
INSERT INTO public._ct_eartag_reasons VALUES (9, 'T', 'N', 'Stolen', 'Ear tag cancelled (Stolen) - May reissue', '1', NULL, NULL, NULL, 1, 9);
INSERT INTO public._ct_eartag_reasons VALUES (10, 'C', 'N', 'Canc User Error', 'Ear tag cancelled - May reissue', '1', NULL, NULL, NULL, 1, 10);
INSERT INTO public._ct_eartag_reasons VALUES (11, 'D', 'N', 'Damaged', 'Ear tag cancelled (Damaged) -May reissue', '1', NULL, NULL, NULL, 1, 11);
INSERT INTO public._ct_eartag_reasons VALUES (12, 'ML', 'N', 'Lost - CTS', 'Ear Tag Cancelled via CTS - Lost', '1', NULL, NULL, NULL, 1, 12);
INSERT INTO public._ct_eartag_reasons VALUES (13, 'E', 'N', 'HQ Issued Tag', 'Ear tag cancelled - Cannot reissue', '1', NULL, NULL, NULL, 1, 13);
INSERT INTO public._ct_eartag_reasons VALUES (14, 'F', 'N', 'HQ Issued Tag', 'Ear tag cancelled - May re-issue', '1', NULL, NULL, NULL, 1, 14);
INSERT INTO public._ct_eartag_reasons VALUES (15, 'Y', 'N', 'Other', 'Ear tag cancelled - Cannot reissue', '1', NULL, NULL, NULL, 1, 15);
INSERT INTO public._ct_eartag_reasons VALUES (16, 'Z', 'N', 'Other', 'Ear tag cancelled - May re-issue', '1', NULL, NULL, NULL, 1, 16);
INSERT INTO public._ct_eartag_reasons VALUES (17, 'MS', 'N', 'Stolen - CTS', 'Ear Tag Cancelled via CTS - Stolen', '1', NULL, NULL, NULL, 1, 17);
INSERT INTO public._ct_eartag_reasons VALUES (18, 'MC', 'N', 'Cancel - CTS', 'Ear Tag Cancelled via CTS', '1', NULL, NULL, NULL, 1, 18);
INSERT INTO public._ct_eartag_reasons VALUES (19, 'L', 'N', 'Lost', 'Ear tag cancelled (Lost) -Cannot reissue', '1', NULL, NULL, NULL, 1, 19);
