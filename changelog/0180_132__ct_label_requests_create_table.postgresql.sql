-- liquibase formatted sql

-- changeset codex:0180-132

create table if not exists _ct_label_requests
(
    lar_id                      numeric(12) not null
        primary key,
    lar_las_id                  numeric(12)
        constraint fk_ct_label_requests_lar_las_id
            references _ct_label_summaries,
    lar_sheet_quantity          numeric(10),
    lar_label_type              varchar(2),
    lar_label_version           numeric(6),
    lar_submitted_date          date,
    lar_requested_date          date,
    lar_reason_code             varchar(2),
    lar_print_method            varchar(2),
    lar_labels_interface_file   varchar(25),
    lar_keeper_title            varchar(10),
    lar_keeper_initials         varchar(12),
    lar_keeper_surname          varchar(30),
    lar_label_loc_type          varchar(2),
    lar_label_loc_identifier    varchar(20),
    lar_label_subloc_identifier varchar(2),
    lar_label_loc_name          varchar(35),
    lar_label_address_2         varchar(35),
    lar_label_address_3         varchar(35),
    lar_label_address_4         varchar(35),
    lar_label_address_5         varchar(35),
    lar_label_post_code         varchar(8),
    lar_corr_loc_type           varchar(2),
    lar_corr_loc_identifier     varchar(20),
    lar_corr_subloc_identifier  varchar(2),
    lar_corr_title              varchar(10),
    lar_corr_initials           varchar(12),
    lar_corr_surname            varchar(30),
    lar_corr_loc_name           varchar(35),
    lar_corr_address_2          varchar(35),
    lar_corr_address_3          varchar(35),
    lar_corr_address_4          varchar(35),
    lar_corr_address_5          varchar(35),
    lar_corr_post_code          varchar(8),
    lar_current_user            varchar(10),
    lar_current_status          varchar(2),
    lar_current_modified_date   date,
    lar_current_pid             numeric(3),
    lar_version                 numeric(6),
    row_number                  numeric
);
