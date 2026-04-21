-- liquibase formatted sql

-- changeset codex:0180-131

create table if not exists _ct_label_summaries
(
    las_id                     numeric(12) not null
        primary key,
    las_loc_id_identifying     numeric(12)
        constraint fk_ct_label_summaries_las_loc_id_identifying
            references _ct_locations,
    las_loc_id_labels          numeric(12)
        constraint fk_ct_label_summaries_las_loc_id_labels
            references _ct_locations,
    las_label_version_number   numeric(2),
    las_last_submitted_date    date,
    las_default_label_type     varchar(2),
    las_default_sheet_quantity numeric(4),
    las_current_user           varchar(10),
    las_current_status         varchar(2),
    las_current_modified_date  date,
    las_current_pid            numeric(3),
    las_version                numeric(6),
    row_number                 numeric
);
