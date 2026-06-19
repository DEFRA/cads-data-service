-- liquibase formatted sql

-- changeset mark-gent:0001_018 endDelimiter://
DROP FUNCTION IF EXISTS public.get_locations(
    cph varchar(17),
    lastModifiedDate date
);

CREATE OR REPLACE FUNCTION public.get_locations(
    cph varchar(17),
    lastModifiedDate date
)
	RETURNS TABLE (
		lid_identifier varchar(14),
		lid_full_identifier varchar(17),
		lid_sub_identifier varchar(2),
		lid_effective_from_date date,
		lid_effective_to_date date,
		lid_current_modified_date date,
		lty_short_description varchar(20),
		lty_long_description varchar(60),
		loc_map_reference varchar(12),
		loc_effective_from date,
		loc_effective_to date,
		loc_cessation_reason varchar(2),
		loc_comments varchar(400),
		loc_source_identifier varchar(2),
		loc_tel_number varchar(25),
		loc_mobile_number varchar(25),
		loc_fax_number varchar(25),
		loc_email_address varchar(50),
		loc_current_modified_date date,
		loc_reason_code varchar(2),
		loc_version numeric(6),
		cty_name varchar(25),
		cty_vet_area_desc varchar(100),
		cty_passport_area_desc varchar(100),
		cty_admin_office varchar(2),
		cty_bcms_team varchar(10),
		cty_inspection_area varchar(2),
		cty_data_mgt_area_desc varchar(100),
		cty_current_status varchar(2),
		lif_description varchar(30),
		imported_date timestamp without time zone
	)
	LANGUAGE sql
AS $$
select
    lid_identifier,
    lid_full_identifier,
    lid_sub_identifier,
    lid_effective_from_date,
    lid_effective_to_date,
    lid_current_modified_date,
    lty_short_description,
    lty_long_description,
    loc_map_reference,
    loc_effective_from,
    loc_effective_to,
    loc_cessation_reason,
    loc_comments,
    loc_source_identifier,
    loc_tel_number,
    loc_mobile_number,
    loc_fax_number,
    loc_email_address,
    loc_current_modified_date,
    loc_reason_code,
    loc_version,
    cty_name,
    pvl_vet.pvl_param_long_desc as cty_vet_area_desc,
    pvl_passport.pvl_param_long_desc as cty_passport_area_desc,
    cty.cty_admin_office,
    cty.cty_bcms_team,
    cty.cty_inspection_area,
    pvl_dmreg.pvl_param_long_desc as cty_data_mgt_area_desc,
    cty_current_status,
    lif.lif_description,
    l.imported_date
from cts.ct_locations l
left join cts.ct_location_types lty
    on lty.lty_id = l.loc_lty_id
left join cts.ct_sublocation_types slt
    on slt.slt_id = l.loc_slt_id
left join cts.ct_counties cty
    on cty.cty_id = l.loc_cty_id
left join cts.ct_location_id_formats lif
    on lif.lif_id = lty.lty_lif_id
left join cts.ct_addresses adr
    on adr.adr_loc_id = l.loc_id
left join cts.ct_location_identifiers lid
    on lid.lid_loc_id = l.loc_id
left join cts.ct_location_relationships llr_parent
    on llr_parent.llr_loc_id_parent = l.loc_id
left join cts.ct_location_relationships llr_child
    on llr_child.llr_loc_id_child = l.loc_id
left join cts.ct_location_rel_types lrt_parent
    on lrt_parent.lrt_id = llr_parent.llr_lrt_id
left join cts.ct_param_value pvl_vet
    on pvl_vet.pvl_param = 'CP.VTAREA'
   and pvl_vet.pvl_param_value = cty.cty_vet_area
left join cts.ct_param_value_group pvg_passport
    on pvg_passport.pvg_param = 'CP.DMREG'
   and pvg_passport.pvg_group_value = '2'
   and pvg_passport.pvg_param_value = cty.cty_passport_area
left join cts.ct_param_value pvl_passport
    on pvl_passport.pvl_id = pvg_passport.pvg_pvl_id
left join cts.ct_param_value_group pvg_dmreg
    on pvg_dmreg.pvg_param = 'CP.DMREG'
   and pvg_dmreg.pvg_group_value = '1'
   and pvg_dmreg.pvg_param_value = cty.cty_data_mgt_area
left join cts.ct_param_value pvl_dmreg
    on pvl_dmreg.pvl_id = pvg_dmreg.pvg_pvl_id
where (get_locations.cph IS NULL OR lid_full_identifier = get_locations.cph)
  and (get_locations.lastModifiedDate IS NULL OR loc_current_modified_date = get_locations.lastModifiedDate)
order by lid.lid_current_modified_date;
$$;
