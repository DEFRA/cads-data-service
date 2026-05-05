-- liquibase formatted sql

-- changeset ref_table_seed:0200-022 splitStatements:false
INSERT INTO public._ct_eartag_types VALUES (1, 'NT', 'Y', 'Numeric', 1, 'Numeric', 'x903916', '1', '1999-07-19', 1, 1, 1);
INSERT INTO public._ct_eartag_types VALUES (2, 'FT', 'Y', 'Free Text', 4, 'Free Text', 'x901187', '1', '1999-05-13', 1, 1, 2);
INSERT INTO public._ct_eartag_types VALUES (3, 'BA', 'Y', 'Barimo', 2, 'Barimo', 'x903916', '1', '1999-07-19', 1, 1, 3);
INSERT INTO public._ct_eartag_types VALUES (4, 'PB', 'Y', 'Pre-Barimo', 3, 'Pre-Barimo', 'x901189', '1', '1999-10-13', 1, 1, 4);
