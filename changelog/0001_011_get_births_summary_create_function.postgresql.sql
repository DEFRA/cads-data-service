-- liquibase formatted sql
-- changeset andy:0004_031 endDelimiter://
DROP FUNCTION IF EXISTS cads.get_births_summary(
    p_birth_date_from DATE,
    p_birth_date_to   DATE
);

CREATE OR REPLACE FUNCTION cads.get_births_summary(
    p_birth_date_from date,
    p_birth_date_to date
)
    RETURNS TABLE (
                      "birth_year" int,
                      "birth_month" text,
                      "country" text,
                      "gov_region" text,
                      "county" text,
                      "breed_type" text,
                      "breed" text,
                      "sex" text,
                      "application_type" text,
                      "number_of_births" bigint
                  )
    LANGUAGE sql
AS $$
select
    extract(year from ran.ran_birth_date)::int as "birth_year",
    trim(to_char(ran.ran_birth_date, 'Month')) as "birth_month",
    case
        when cty.cty_uk_area = 'S' then 'Scotland'
        when cty.cty_uk_area = 'W' then 'Wales'
        when cty.cty_name is not null then 'England'
        else 'Unknown'
        end as "country",
    null::text as "gov_region",
    cty.cty_name as "county",
    case
        when brd.brd_type = 'D' then 'Dairy'
        else 'Non Dairy'
        end as "breed_type",
    upper(
        coalesce(
            brd.brd_long_description,
            brd.brd_short_description,
            brd.brd_code
        )
    ) as "breed",
    ran.ran_sex as "sex",
    case
        when vap.vap_application_type = 'B' then 'Birth Application'
        else vap.vap_application_type
        end as "application_type",
    count(*) as "number_of_births"
from cts.ct_registered_animals ran
         left join cts.ct_breeds brd
                   on brd.brd_id = ran.ran_brd_id
         left join cts.ct_valid_applications vap
                   on vap.vap_id = ran.ran_vap_id
         left join cts.ct_locations loc
                   on loc.loc_id = ran.ran_loc_id_passport
         left join cts.ct_counties cty
                   on cty.cty_id = loc.loc_cty_id
where ran.ran_birth_date is not null
  and ran.ran_birth_date >= p_birth_date_from
  and ran.ran_birth_date < p_birth_date_to
group by
    extract(year from ran.ran_birth_date),
    trim(to_char(ran.ran_birth_date, 'Month')),
    case
        when cty.cty_uk_area = 'S' then 'Scotland'
        when cty.cty_uk_area = 'W' then 'Wales'
        when cty.cty_name is not null then 'England'
        else 'Unknown'
        end,
    cty.cty_name,
    case
        when brd.brd_type = 'D' then 'Dairy'
        else 'Non Dairy'
        end,
    upper(
        coalesce(
            brd.brd_long_description,
            brd.brd_short_description,
            brd.brd_code
        )
    ),
    ran.ran_sex,
    case
        when vap.vap_application_type = 'B' then 'Birth Application'
        else vap.vap_application_type
        end
order by
    "birth_year",
    "birth_month",
    "country",
    "county",
    "breed_type",
    "breed",
    "sex",
    "application_type";
$$;