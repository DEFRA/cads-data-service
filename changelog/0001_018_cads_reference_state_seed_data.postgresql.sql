-- liquibase formatted sql

-- changeset dev_db_build:0001-019 splitStatements:false
INSERT INTO cads.party_type (type) VALUES
    ('person'),
    ('organisation')
ON CONFLICT (type) DO NOTHING;

INSERT INTO cads.animal_state (state) VALUES
    ('dead'),
    ('lost'),
    ('registered'),
    ('stolen')
ON CONFLICT (state) DO NOTHING;

INSERT INTO cads.party_state (state) VALUES
    ('active'),
    ('inactive'),
    ('unknown')
ON CONFLICT (state) DO NOTHING;

INSERT INTO cads.party_species (species) VALUES
    ('Bison'),
    ('Buffalo'),
    ('Cattle'),
    ('Deer'),
    ('Domestic Pigs'),
    ('Goats'),
    ('Sheep')
ON CONFLICT (species) DO NOTHING;

-- Design values for location_site_type and location_activity exceed the current
-- cads schema varchar(10) key columns, so they are not seeded here.
INSERT INTO cads.location_site_source (source) VALUES
    ('sam'),
    ('lip')
ON CONFLICT (source) DO NOTHING;

INSERT INTO cads.location_site_state (state) VALUES
    ('active'),
    ('inactive')
ON CONFLICT (state) DO NOTHING;

INSERT INTO cads.location_site_role (role) VALUES
    ('contact')
ON CONFLICT (role) DO NOTHING;

INSERT INTO cads.location_associated_site_type (type) VALUES
    ('temporaryHolding'),
    ('mainSite')
ON CONFLICT (type) DO NOTHING;

INSERT INTO cads.location_site_identifier_type (type) VALUES
    ('fsaNumber'),
    ('holdingNumber'),
    ('portNumber')
ON CONFLICT (type) DO NOTHING;

INSERT INTO cads.animal_species (species) VALUES
    ('Bison'),
    ('Buffalo'),
    ('Cattle'),
    ('Deer'),
    ('Domestic Pigs'),
    ('Goats'),
    ('Sheep')
ON CONFLICT (species) DO NOTHING;

INSERT INTO cads.animal_sex (sex) VALUES
    ('male'),
    ('female')
ON CONFLICT (sex) DO NOTHING;

INSERT INTO cads.animal_production_type (type) VALUES
    ('dairy'),
    ('beef'),
    ('meat'),
    ('laying'),
    ('other')
ON CONFLICT (type) DO NOTHING;

INSERT INTO cads.animal_role (role) VALUES
    ('Owner')
ON CONFLICT (role) DO NOTHING;

INSERT INTO cads.animal_breed_state (state) VALUES
    ('active'),
    ('inactive')
ON CONFLICT (state) DO NOTHING;

INSERT INTO cads.animal_collective_state (state) VALUES
    ('active'),
    ('inactive'),
    ('unregistered')
ON CONFLICT (state) DO NOTHING;

INSERT INTO cads.animal_collective_role (role) VALUES
    ('Keeper')
ON CONFLICT (role) DO NOTHING;

INSERT INTO cads.animal_lost_or_stolen_state (state) VALUES
    ('lost'),
    ('stolen'),
    ('found')
ON CONFLICT (state) DO NOTHING;

INSERT INTO cads.animal_original_identifier_type (type) VALUES
    ('ukTag'),
    ('temporaryTag'),
    ('ntid')
ON CONFLICT (type) DO NOTHING;

INSERT INTO cads.animal_registration_category (category) VALUES
    ('birthRegistration'),
    ('registration'),
    ('noticeToIdentify')
ON CONFLICT (category) DO NOTHING;

INSERT INTO cads.animal_resolution_type (resolution) VALUES
    ('reTag'),
    ('assignTag'),
    ('slaughterVoluntary'),
    ('slaughterCompulsory')
ON CONFLICT (resolution) DO NOTHING;
