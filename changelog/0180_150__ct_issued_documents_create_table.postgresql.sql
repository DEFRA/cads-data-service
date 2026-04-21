-- liquibase formatted sql

-- changeset codex:0180-150

create table if not exists _ct_issued_documents
(
    ido_id                       numeric(12) not null
        primary key,
    ido_loc_id                   numeric(12)
        constraint fk_ct_issued_documents_ido_loc_id
            references _ct_locations,
    ido_creation_date            date,
    ido_reason_code              varchar(2),
    ido_interface_file_name      varchar(25),
    ido_passpt_layout_ver_number varchar(10),
    ido_interface_txn_number     numeric(4),
    ido_passport_version_number  numeric(3),
    ido_current_status           varchar(2),
    ido_current_modified_date    date,
    ido_current_user             varchar(10),
    ido_current_pid              numeric(3),
    ido_ran_id                   numeric(12)
        constraint fk_ct_issued_documents_ido_ran_id
            references _ct_registered_animals,
    ido_version                  numeric(6),
    row_number                   numeric
);
