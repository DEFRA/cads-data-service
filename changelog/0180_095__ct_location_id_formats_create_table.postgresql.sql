-- liquibase formatted sql

-- changeset codex:0180-095

create table if not exists _ct_location_id_formats
(
    lif_version               numeric(6),
    lif_id                    numeric(12) not null
        primary key,
    lif_subloc_type_reqd      varchar(1),
    lif_description           varchar(30),
    lif_loc_type_reqd         varchar(1),
    lif_format_pattern        varchar(15),
    lif_current_user          varchar(10),
    lif_current_status        varchar(2),
    lif_current_modified_date date,
    lif_current_pid           numeric(3),
    row_number                numeric
);
