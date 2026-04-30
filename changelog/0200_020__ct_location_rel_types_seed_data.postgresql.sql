-- liquibase formatted sql

-- changeset ref_table_seed:0200-018 splitStatements:false
INSERT INTO public._ct_location_rel_types VALUES (47, 'AF', 'Agent For', 'N', 'N', 'Y', 'N', 'N', 'IS AGENT FOR', 'HAS AGENT', '2005-10-19', '1', 'x905756', NULL, 1, 1);
INSERT INTO public._ct_location_rel_types VALUES (1, 'SU', 'SUB-LOCATION', 'N', 'N', 'Y', 'N', 'Y', 'IS THE PARENT LOCATION FOR', 'IS A SUB-LOCATION OF', '1999-03-30', '1', 'x903053', 37, 1, 2);
INSERT INTO public._ct_location_rel_types VALUES (2, 'SF', 'SHARED/FAC', 'N', 'N', 'Y', 'N', 'N', 'SHARES THE FACILITIES OF', 'SHARES ITS FACILITIES WITH', '1999-03-30', '1', 'x903053', 37, 1, 3);
INSERT INTO public._ct_location_rel_types VALUES (3, 'AL', 'ADDITIONAL LAND', 'N', 'N', 'Y', 'N', 'N', 'USES LAND AT', 'HAS ITS LAND USED BY', '1999-03-30', '1', 'x903053', NULL, 1, 4);
INSERT INTO public._ct_location_rel_types VALUES (6, 'OW', 'OWNER', 'N', 'N', 'Y', 'Y', 'N', 'IS OWNED BY', 'IS THE OWNER OF', '1999-03-30', '1', 'x903053', 37, 1, 5);
INSERT INTO public._ct_location_rel_types VALUES (7, 'SP', 'SPLIT', 'Y', 'N', 'Y', 'N', 'N', 'PREVIOUSLY CONTAINED', 'WAS PART OF', '1999-03-30', '1', 'x903053', NULL, 1, 6);
INSERT INTO public._ct_location_rel_types VALUES (9, 'UL', 'Uses Land', 'N', 'N', 'Y', 'N', 'N', 'KEEPS CATTLE AT', 'CONTAINS CATTLE FROM', '1999-03-30', '1', 'x903053', 37, 1, 7);
INSERT INTO public._ct_location_rel_types VALUES (10, 'ME', 'MERGED', 'N', 'N', 'Y', 'Y', 'N', 'IS NOW PART OF', 'HAS SUBSUMED', '1999-03-30', '1', 'x903053', NULL, 1, 8);
INSERT INTO public._ct_location_rel_types VALUES (48, 'TH', 'Temp Holding', 'Y', 'N', 'Y', 'N', 'N', 'HAS A TEMP HOLDING AT', 'IS A TEMP HOLDING OF', '2015-12-03', '1', 'x912716', NULL, 1, 9);
