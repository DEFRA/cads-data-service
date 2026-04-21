-- liquibase formatted sql

-- changeset codex:0180-089

create table if not exists _ct_file_layouts
(
    flt_id                numeric,
    flt_process_name      varchar(20),
    flt_record_type       varchar(1),
    flt_element_name      varchar(30),
    flt_element_desc      varchar(50),
    flt_element_index     numeric,
    flt_element_tests     varchar(10),
    flt_data_type         varchar(8),
    flt_data_length       numeric,
    flt_data_precision    numeric,
    flt_file_format       varchar(30),
    flt_conversion_format varchar(30),
    flt_unidata_name      varchar(10),
    row_number            numeric
);
