-- liquibase formatted sql

-- changeset ref_table_seed:0200-020 splitStatements:false
INSERT INTO public._ct_location_types VALUES ('N', '1', '1', '2005-10-19', 'x905756', 34, 1, 13, 'AG', 5, 1, 'Agency', 0, 'Agency', '4', 'Y', 'N', 'Y', 'N', 1);
INSERT INTO public._ct_location_types VALUES ('N', '1', '1', '1999-10-22', 'x903052', 34, 1, 1, 'SH', 2, 1, 'S/HOUSE', 0, 'SLAUGHTER HOUSE', '2', 'Y', 'Y', 'Y', 'Y', 2);
INSERT INTO public._ct_location_types VALUES ('Y', '1', '1', '2001-11-07', 'x902692', 34, 1, 2, 'AH', 1, 1, 'AG. HOLD', 0, 'AGRICULTURAL HOLDING', '1', 'Y', 'Y', 'Y', 'Y', 3);
INSERT INTO public._ct_location_types VALUES (NULL, '1', '1', '1997-12-16', 'x902652', 34, 1, 3, 'CL', 1, 1, 'C/LAND', 0, 'COMMON LAND', '1', NULL, 'Y', NULL, 'Y', 4);
INSERT INTO public._ct_location_types VALUES ('N', '1', '1', '2005-10-06', 'x905756', 34, 1, 4, 'CC', 4, 2, 'calf coll.', 2, 'Calf collection', '3', 'Y', 'Y', 'Y', 'Y', 5);
