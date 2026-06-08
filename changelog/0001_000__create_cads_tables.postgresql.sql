-- liquibase formatted sql

-- changeset schema:0001-000-cads splitStatements:false


create table if not exists party_location
(
    identifier varchar(50) not null
        primary key
);


create table if not exists party_type
(
    type varchar(30) not null
        primary key
);


create table if not exists party_state
(
    state varchar(20) not null
        primary key
);


create table if not exists party_species
(
    species varchar(50) not null
        primary key
);


create table if not exists party
(
    number              integer     not null
        primary key,
    party_type          varchar(30) not null
        constraint fk_party_type
            references party_type,
    first_name          text,
    last_name           text,
    name                text        not null,
    mobile              text,
    landline            text,
    email               text,
    location_identifier varchar(50)
        constraint fk_party_location
            references party_location,
    party_state         varchar(20) not null
        constraint fk_party_state
            references party_state
);
create index if not exists idx_party_location_identifier
    on party (location_identifier);


create table if not exists party_haulier
(
    identifier           text    not null
        primary key,
    party_number         integer not null
        constraint fk_party_haulier_party
            references party,
    authorisation_number text    not null,
    start_date           date    not null,
    end_date             date,
    constraint chk_party_haulier_dates
        check ((end_date IS NULL) OR (end_date >= start_date))
);
create index if not exists idx_party_haulier_party_number
    on party_haulier (party_number);


create table if not exists party_haulier_species
(
    species            varchar(50) not null
        constraint fk_party_haulier_species_species
            references party_species,
    haulier_identifier text        not null
        constraint fk_party_haulier_species_haulier
            references party_haulier,
    primary key (species, haulier_identifier)
);


create table if not exists location_country
(
    code                             varchar(10)           not null
        primary key,
    name                             varchar(100)          not null,
    european_union_trade_member_flag boolean default false not null,
    devolved_authority_flag          boolean default false not null,
    home_country_flag                boolean default false not null
);


create table if not exists location_postcode
(
    postcode varchar(10) not null
        primary key
);


create table if not exists location_site_type
(
    type        varchar(10)  not null
        primary key,
    description varchar(100) not null
);


create table if not exists location_activity
(
    type        varchar(10)  not null
        primary key,
    description varchar(100) not null
);


create table if not exists location_site_source
(
    source varchar(20) not null
        primary key
);


create table if not exists location_site_state
(
    state varchar(20) not null
        primary key
);


create table if not exists location_site_role
(
    role varchar(20) not null
        primary key
);


create table if not exists location_associated_site_type
(
    type varchar(50) not null
        primary key
);


create table if not exists location_site_identifier_type
(
    type varchar(50) not null
        primary key
);


create table if not exists location_party_ref
(
    party_identifier bigint not null
        primary key
);


create table if not exists location
(
    identifier          varchar(50) not null
        primary key,
    uprn                bigint,
    single_line_address varchar(500),
    postcode            varchar(10)
        constraint fk_location_postcode
            references location_postcode,
    os_map_reference    varchar(30),
    easting             integer,
    northing            integer,
    country_code        varchar(10)
        constraint fk_location_country
            references location_country
);
create index if not exists idx_location_country_code
    on location (country_code);
create index if not exists idx_location_postcode
    on location (postcode);


create table if not exists location_site
(
    identifier                      varchar(50)           not null
        primary key,
    site_type                       varchar(10)           not null
        constraint fk_location_site_type
            references location_site_type,
    name                            varchar(255)          not null,
    location_identifier             varchar(50)           not null
        constraint fk_location_site_location
            references location,
    site_source                     varchar(20)           not null
        constraint fk_location_site_source
            references location_site_source,
    destroy_identity_documents_flag boolean default false not null,
    state                           varchar(20)           not null
        constraint fk_location_site_state
            references location_site_state,
    start_date                      date                  not null,
    end_date                        date,
    constraint chk_location_site_dates
        check ((end_date IS NULL) OR (end_date >= start_date))
);
create index if not exists idx_location_site_type
    on location_site (site_type);
create index if not exists idx_location_site_location_identifier
    on location_site (location_identifier);
create index if not exists idx_location_site_source
    on location_site (site_source);
create index if not exists idx_location_site_state
    on location_site (state);


create table if not exists location_site_type_activity
(
    site_type varchar(10) not null
        constraint fk_location_site_type_activity_site_type
            references location_site_type,
    activity  varchar(10) not null
        constraint fk_location_site_type_activity_activity
            references location_activity,
    primary key (site_type, activity)
);


create table if not exists location_site_party
(
    site_identifier  varchar(50) not null
        constraint fk_location_site_party_site
            references location_site,
    party_identifier bigint      not null
        constraint fk_location_site_party_party
            references location_party_ref,
    site_role        varchar(20) not null
        constraint fk_location_site_party_role
            references location_site_role,
    start_date       date        not null,
    end_date         date,
    primary key (site_identifier, party_identifier, site_role, start_date),
    constraint chk_location_site_party_dates
        check ((end_date IS NULL) OR (end_date >= start_date))
);
create index if not exists idx_location_site_party_identifier
    on location_site_party (party_identifier);


create table if not exists location_site_activity
(
    site_identifier varchar(50) not null
        constraint fk_location_site_activity_site
            references location_site,
    site_type       varchar(10) not null,
    activity        varchar(10) not null,
    start_date      date        not null,
    end_date        date,
    primary key (site_identifier, site_type, activity, start_date),
    constraint fk_location_site_activity_site_type_activity
        foreign key (site_type, activity) references location_site_type_activity,
    constraint chk_location_site_activity_dates
        check ((end_date IS NULL) OR (end_date >= start_date))
);
create index if not exists idx_location_site_activity_site_identifier
    on location_site_activity (site_identifier);


create table if not exists location_associated_site
(
    site_identifier            varchar(50) not null
        constraint fk_location_associated_site_site
            references location_site,
    associated_site_identifier varchar(50) not null
        constraint fk_location_associated_site_related_site
            references location_site,
    associated_site_type       varchar(50) not null
        constraint fk_location_associated_site_type
            references location_associated_site_type,
    start_date                 date        not null,
    end_date                   date,
    primary key (site_identifier, associated_site_identifier, associated_site_type, start_date),
    constraint chk_location_associated_site_dates
        check ((end_date IS NULL) OR (end_date >= start_date))
);
create index if not exists idx_location_associated_site_related_site
    on location_associated_site (associated_site_identifier);


create table if not exists location_site_identifier
(
    site_identifier  varchar(50)  not null
        constraint fk_location_site_identifier_site
            references location_site,
    identifier_type  varchar(50)  not null
        constraint fk_location_site_identifier_type
            references location_site_identifier_type,
    identifier_value varchar(100) not null,
    primary key (site_identifier, identifier_type, identifier_value)
);


create table if not exists animal_site_ref
(
    identifier text not null
        primary key
);


create table if not exists animal_party_ref
(
    identifier integer not null
        primary key
);


create table if not exists animal_species
(
    species text not null
        primary key
);


create table if not exists animal_sex
(
    sex text not null
        primary key
);


create table if not exists animal_production_type
(
    type text not null
        primary key
);


create table if not exists animal_role
(
    role text not null
        primary key
);


create table if not exists animal_state
(
    state text not null
        primary key
);


create table if not exists animal_breed_state
(
    state text not null
        primary key
);


create table if not exists animal_collective_state
(
    state text not null
        primary key
);


create table if not exists animal_collective_role
(
    role text not null
        primary key
);


create table if not exists animal_lost_or_stolen_state
(
    state text not null
        primary key
);


create table if not exists animal_original_identifier_type
(
    type text not null
        primary key
);


create table if not exists animal_registration_category
(
    category text not null
        primary key
);


create table if not exists animal_resolution_type
(
    resolution text not null
        primary key
);


create table if not exists animal_identifier
(
    identifier        text not null
        primary key,
    animal_identifier text not null
        unique
);


create table if not exists animal_breed
(
    species          text    not null
        constraint fk_animal_breed_species
            references animal_species,
    breed_code       text    not null,
    breed            text,
    cross_breed_flag boolean not null,
    state            text    not null
        constraint fk_animal_breed_state
            references animal_breed_state,
    primary key (species, breed_code)
);
create index if not exists idx_animal_breed_species
    on animal_breed (species);


create table if not exists animal_collective_ref
(
    species         text not null
        constraint fk_animal_collective_ref_species
            references animal_species,
    site_identifier text not null
        constraint fk_animal_collective_ref_site
            references animal_site_ref,
    state           text not null
        constraint fk_animal_collective_ref_state
            references animal_collective_state,
    primary key (species, site_identifier)
);
create index if not exists idx_animal_collective_ref_site_identifier
    on animal_collective_ref (site_identifier);


create table if not exists animal_genotype
(
    species  text not null
        constraint fk_animal_genotype_species
            references animal_species,
    genotype text not null,
    primary key (species, genotype)
);
create index if not exists idx_animal_genotype_species
    on animal_genotype (species);


create table if not exists animal_death_reason
(
    species text not null
        constraint fk_animal_death_reason_species
            references animal_species,
    reason  text not null,
    primary key (species, reason)
);
create index if not exists idx_animal_death_reason_species
    on animal_death_reason (species);


create table if not exists animal_species_production_type
(
    species         text not null
        constraint fk_animal_species_production_type_species
            references animal_species,
    production_type text not null
        constraint fk_animal_species_production_type_production_type
            references animal_production_type,
    primary key (species, production_type)
);
create index if not exists idx_animal_species_production_type_species
    on animal_species_production_type (species);


create table if not exists animal
(
    identifier                   text not null
        primary key,
    animal_identifier_identifier text
        constraint fk_animal_identifier
            references animal_identifier,
    original_identifier          text,
    species                      text not null
        constraint fk_animal_species
            references animal_species,
    breed_species                text,
    breed_code                   text,
    genotype_species             text,
    genotype                     text,
    name                         text,
    sex                          text not null
        constraint fk_animal_sex
            references animal_sex,
    production_type              text,
    identification_date          date,
    received_date                date,
    genetic_dam_identifier       text
        constraint fk_animal_genetic_dam
            references animal,
    sire_identifier              text
        constraint fk_animal_sire
            references animal,
    birth_dam_identifier         text
        constraint fk_animal_birth_dam
            references animal,
    registration_site_identifier text not null
        constraint fk_animal_registration_site
            references animal_site_ref,
    registration_date            date not null,
    registration_category        text not null
        constraint fk_animal_registration_category
            references animal_registration_category,
    constraint fk_animal_breed
        foreign key (breed_species, breed_code) references animal_breed,
    constraint fk_animal_genotype
        foreign key (genotype_species, genotype) references animal_genotype,
    constraint fk_animal_species_production_type
        foreign key (species, production_type) references animal_species_production_type
);
create index if not exists idx_animal_species
    on animal (species);
create index if not exists idx_animal_registration_site_identifier
    on animal (registration_site_identifier);


create table if not exists animal_mark
(
    species                    text not null,
    collective_site_identifier text not null,
    mark                       text not null,
    start_date                 date not null,
    end_date                   date,
    primary key (species, collective_site_identifier, mark),
    constraint fk_animal_mark_collective_ref
        foreign key (species, collective_site_identifier) references animal_collective_ref,
    constraint chk_animal_mark_dates
        check ((end_date IS NULL) OR (end_date >= start_date))
);
create index if not exists idx_animal_mark_collective
    on animal_mark (species, collective_site_identifier);


create table if not exists animal_birth
(
    animal_identifier                     text    not null
        primary key
        constraint fk_animal_birth_animal
            references animal,
    birth_site_identifier                 text    not null
        constraint fk_animal_birth_site
            references animal_site_ref,
    birth_date                            date,
    birth_year                            text,
    birth_mark_species                    text,
    birth_mark_collective_site_identifier text,
    birth_mark                            text,
    assisted_birth_flag                   boolean not null,
    multiple_births_flag                  boolean not null,
    embryo_transfer_flag                  boolean not null,
    constraint fk_animal_birth_mark
        foreign key (birth_mark_species, birth_mark_collective_site_identifier, birth_mark) references animal_mark
);


create table if not exists animal_collective
(
    animal_identifier                  text not null
        constraint fk_animal_collective_animal
            references animal,
    species                            text not null,
    home_collective_site_identifier    text not null,
    current_collective_site_identifier text not null,
    start_date                         date not null,
    end_date                           date,
    primary key (animal_identifier, species, home_collective_site_identifier, start_date),
    constraint fk_animal_collective_home
        foreign key (species, home_collective_site_identifier) references animal_collective_ref,
    constraint fk_animal_collective_current
        foreign key (species, current_collective_site_identifier) references animal_collective_ref,
    constraint chk_animal_collective_dates
        check ((end_date IS NULL) OR (end_date >= start_date))
);
create index if not exists idx_animal_collective_current
    on animal_collective (species, current_collective_site_identifier);


create table if not exists animal_death
(
    animal_identifier                  text    not null
        primary key
        constraint fk_animal_death_animal
            references animal,
    death_date                         date    not null,
    death_reported_date                date,
    death_site_identifier              text    not null
        constraint fk_animal_death_site
            references animal_site_ref,
    death_reason_species               text,
    death_reason                       text,
    carcass_collection_site_identifier text
        constraint fk_animal_death_carcass_site
            references animal_site_ref,
    tse_test_required_flag             boolean not null,
    death_received_date                date,
    constraint fk_animal_death_reason
        foreign key (death_reason_species, death_reason) references animal_death_reason
);
create index if not exists idx_animal_death_site
    on animal_death (death_site_identifier);


create table if not exists animal_party
(
    animal_identifier text    not null
        constraint fk_animal_party_animal
            references animal,
    party_identifier  integer not null
        constraint fk_animal_party_party
            references animal_party_ref,
    animal_role       text    not null
        constraint fk_animal_party_role
            references animal_role,
    start_date        date    not null,
    end_date          date,
    primary key (animal_identifier, party_identifier, animal_role, start_date),
    constraint chk_animal_party_dates
        check ((end_date IS NULL) OR (end_date >= start_date))
);
create index if not exists idx_animal_party_party_identifier
    on animal_party (party_identifier);


create table if not exists animal_status
(
    animal_identifier text not null
        constraint fk_animal_status_animal
            references animal,
    animal_state      text not null
        constraint fk_animal_status_state
            references animal_state,
    start_date        date not null,
    end_date          date,
    primary key (animal_identifier, animal_state, start_date),
    constraint chk_animal_status_dates
        check ((end_date IS NULL) OR (end_date >= start_date))
);
create index if not exists idx_animal_status_animal_identifier
    on animal_status (animal_identifier);


create table if not exists animal_lost_or_stolen_status
(
    animal_identifier      text    not null
        constraint fk_animal_lost_or_stolen_status_animal
            references animal,
    event_date             date    not null,
    state                  text    not null
        constraint fk_animal_lost_or_stolen_status_state
            references animal_lost_or_stolen_state,
    crime_reference_number text,
    home_site_identifier   text    not null
        constraint fk_animal_lost_or_stolen_status_home_site
            references animal_site_ref,
    found_dead_flag        boolean not null,
    received_date          date,
    primary key (animal_identifier, event_date)
);
create index if not exists idx_animal_lost_or_stolen_status_home_site_identifier
    on animal_lost_or_stolen_status (home_site_identifier);


create table if not exists animal_collective_registration
(
    identifier                      integer not null
        primary key,
    species                         text    not null,
    site_identifier                 text    not null,
    quantity                        integer not null,
    birth_year                      text    not null,
    registration_date               date    not null,
    identification_date             date    not null,
    genotype_species                text,
    genotype                        text,
    mark_species                    text,
    mark_collective_site_identifier text,
    mark                            text,
    breed_species                   text,
    breed_code                      text,
    constraint fk_animal_collective_registration_collective_ref
        foreign key (species, site_identifier) references animal_collective_ref,
    constraint fk_animal_collective_registration_genotype
        foreign key (genotype_species, genotype) references animal_genotype,
    constraint fk_animal_collective_registration_mark
        foreign key (mark_species, mark_collective_site_identifier, mark) references animal_mark,
    constraint fk_animal_collective_registration_breed
        foreign key (breed_species, breed_code) references animal_breed
);
create index if not exists idx_animal_collective_registration_collective_ref
    on animal_collective_registration (species, site_identifier);


create table if not exists animal_collective_death
(
    identifier                         integer not null
        primary key,
    species                            text    not null,
    site_identifier                    text    not null,
    quantity                           integer not null,
    death_date                         date    not null,
    death_reason_species               text,
    death_reason                       text,
    carcass_collection_site_identifier text
        constraint fk_animal_collective_death_carcass_site
            references animal_site_ref,
    mark_species                       text,
    mark_collective_site_identifier    text,
    mark                               text,
    constraint fk_animal_collective_death_collective_ref
        foreign key (species, site_identifier) references animal_collective_ref,
    constraint fk_animal_collective_death_reason
        foreign key (death_reason_species, death_reason) references animal_death_reason,
    constraint fk_animal_collective_death_mark
        foreign key (mark_species, mark_collective_site_identifier, mark) references animal_mark
);
create index if not exists idx_animal_collective_death_collective_ref
    on animal_collective_death (species, site_identifier);


create table if not exists animal_collective_party
(
    species          text    not null,
    site_identifier  text    not null,
    party_identifier integer not null
        constraint fk_animal_collective_party_party
            references animal_party_ref,
    collective_role  text    not null
        constraint fk_animal_collective_party_role
            references animal_collective_role,
    start_date       date    not null,
    end_date         date,
    primary key (species, site_identifier, party_identifier, collective_role, start_date),
    constraint fk_animal_collective_party_collective_ref
        foreign key (species, site_identifier) references animal_collective_ref,
    constraint chk_animal_collective_party_dates
        check ((end_date IS NULL) OR (end_date >= start_date))
);
create index if not exists idx_animal_collective_party_party_identifier
    on animal_collective_party (party_identifier);


create table if not exists animal_notice_to_identify
(
    notice_reference                text    not null
        primary key,
    species                         text    not null
        constraint fk_animal_notice_to_identify_species
            references animal_species,
    animal_identifier               text
        constraint fk_animal_notice_to_identify_animal
            references animal,
    dna_proven_flag                 boolean not null,
    original_animal_identifier      text    not null
        constraint fk_animal_notice_to_identify_original_animal
            references animal,
    original_animal_identifier_type text    not null
        constraint fk_animal_notice_to_identify_original_type
            references animal_original_identifier_type,
    breed_species                   text,
    breed_code                      text,
    sex                             text    not null
        constraint fk_animal_notice_to_identify_sex
            references animal_sex,
    issue_date                      date    not null,
    end_date                        date,
    inspection_year                 text,
    additional_details              text,
    inspection_reference            text,
    site_identifier                 text    not null
        constraint fk_animal_notice_to_identify_site
            references animal_site_ref,
    resolution                      text
        constraint fk_animal_notice_to_identify_resolution
            references animal_resolution_type,
    constraint fk_animal_notice_to_identify_breed
        foreign key (breed_species, breed_code) references animal_breed,
    constraint chk_animal_notice_to_identify_dates
        check ((end_date IS NULL) OR (end_date >= issue_date))
);
create index if not exists idx_animal_notice_to_identify_site_identifier
    on animal_notice_to_identify (site_identifier);


create table if not exists animal_unregistered_parents
(
    animal_identifier             text not null
        primary key
        constraint fk_animal_unregistered_parents_animal
            references animal,
    sire_animal_identifier        text,
    genetic_dam_animal_identifier text,
    birth_dam_animal_identifier   text
);
