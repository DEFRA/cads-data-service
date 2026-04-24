-- liquibase formatted sql

-- changeset ref_table_seed:0200-023 splitStatements:false
INSERT INTO public._ct_location_party_rel_types VALUES (4, 'KN', 'KEEPER NAME', 'N', 'Y', 'Y', 'Y', 'Y', 'HAS THE KEEPER AT', 'IS THE KEEPER FOR', 'f800702', '1', '1999-04-15', 37, 1, 1);
INSERT INTO public._ct_location_party_rel_types VALUES (5, 'CA', 'CORRES. ADDRESS', 'Y', 'N', 'Y', 'Y', 'Y', 'HAS CORRESPONDENCE SENT TO', 'IS THE CORRES. ADDRESS FOR', 'f800702', '1', '1999-04-15', NULL, 1, 2);
INSERT INTO public._ct_location_party_rel_types VALUES (8, 'CO', 'CONTACT (OTHER)', 'Y', 'N', 'N', 'N', 'Y', 'HAS ALTERNATIVE CONTACT ADDR.', 'IS ANOTHER CONTACT ADDRESS FOR', 'f800702', '1', '1999-04-15', 37, 1, 3);
