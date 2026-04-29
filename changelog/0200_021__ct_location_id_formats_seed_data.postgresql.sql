-- liquibase formatted sql

-- changeset ref_table_seed:0200-019 splitStatements:false
INSERT INTO public._ct_location_id_formats VALUES (1, 1, '0', 'CPH format', '1', '99/999/9999', '1', '1', '1997-01-01', 33, 1);
INSERT INTO public._ct_location_id_formats VALUES (1, 2, '0', 'Meat Hygiene No./Priv. res fmt', '1', '9999', '1', '1', '1997-01-01', 33, 2);
INSERT INTO public._ct_location_id_formats VALUES (1, 3, '2', 'letter plus CPH', '2', 'X999999999', 'C900000', '1', '1997-12-10', 33, 3);
INSERT INTO public._ct_location_id_formats VALUES (1, 4, '2', 'Collection Centre format', '0', 'XXXXX', 'x901190', '1', '1998-07-24', 33, 4);
INSERT INTO public._ct_location_id_formats VALUES (1, 5, '2', 'Agency', '1', '9999999', 'x905756', '1', '2005-10-19', NULL, 5);
