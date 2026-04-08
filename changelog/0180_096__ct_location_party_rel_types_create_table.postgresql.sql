-- liquibase formatted sql

-- changeset codex:0180-096

create table if not exists _ct_location_party_rel_types
(
    lpt_id                    numeric(12) not null
        primary key,
    lpt_code                  varchar(2),
    lpt_description           varchar(60),
    lpt_gaps_allowed          char,
    lpt_mandatory             char,
    lpt_primary_single_link   char,
    lpt_second_single_link    char,
    lpt_hierarchical_link     char,
    lpt_relship_text_down     varchar(30),
    lpt_relship_text_up       varchar(30),
    lpt_current_user          varchar(10),
    lpt_current_status        varchar(2),
    lpt_current_modified_date date,
    lpt_current_pid           numeric(3),
    lpt_version               numeric(6),
    row_number                numeric
);
