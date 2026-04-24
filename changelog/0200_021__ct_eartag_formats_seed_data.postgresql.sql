-- liquibase formatted sql

-- changeset ref_table_seed:0200-021 splitStatements:false
INSERT INTO public._ct_eartag_formats VALUES (3, 'Pre-Barimo', 'XXXXXX XXXXX', 14, NULL, 'x901189', '1', '1999-10-13', 1, 1, 1);
INSERT INTO public._ct_eartag_formats VALUES (1, 'Numeric', 'AA-XXNNNNNNNNNN', 14, NULL, 'x903916', '1', '1999-07-21', 1, 1, 2);
INSERT INTO public._ct_eartag_formats VALUES (4, 'Free Text', 'XXXXXXXXXXXXXX', 14, NULL, 'x901187', '1', '1999-05-13', 1, 1, 3);
INSERT INTO public._ct_eartag_formats VALUES (2, 'Barimo', 'AAXXNNNN NNNNN', 14, NULL, 'x901187', '1', '1999-05-13', 1, 1, 4);
