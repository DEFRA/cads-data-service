-- liquibase formatted sql
-- changeset andy:0220 endDelimiter://
DROP FUNCTION IF EXISTS public.get_births_summary(
    p_birth_date_from DATE,
    p_birth_date_to   DATE
);

CREATE OR REPLACE FUNCTION public.get_births_summary(
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
    extract(year from _ct_registered_animals.ran_birth_date)::int as "birth_year",
    trim(to_char(_ct_registered_animals.ran_birth_date, 'Month')) as "birth_month",
    case
        when _ct_counties.cty_uk_area = 'S' then 'Scotland'
        when _ct_counties.cty_uk_area = 'W' then 'Wales'
        when _ct_counties.cty_name is not null then 'England'
        else 'Unknown'
        end as "country",
    null::text as "gov_region",
    _ct_counties.cty_name as "county",
    case
        when _ct_breeds.brd_type = 'D' then 'Dairy'
        else 'Non Dairy'
        end as "breed_type",
    upper(
        coalesce(
            _ct_breeds.brd_long_description,
            _ct_breeds.brd_short_description,
            _ct_breeds.brd_code
        )
    ) as "breed",
    _ct_registered_animals.ran_sex as "sex",
    case
        when _ct_valid_applications.vap_application_type = 'B' then 'Birth Application'
        else _ct_valid_applications.vap_application_type
        end as "application_type",
    count(*) as "number_of_births"
from _ct_registered_animals
         left join _ct_breeds
                   on _ct_breeds.brd_id = _ct_registered_animals.ran_brd_id
         left join _ct_valid_applications
                   on _ct_valid_applications.vap_id = _ct_registered_animals.ran_vap_id
         left join _ct_locations
                   on _ct_locations.loc_id = _ct_registered_animals.ran_loc_id_passport
         left join _ct_counties
                   on _ct_counties.cty_id = _ct_locations.loc_cty_id
where _ct_registered_animals.ran_birth_date is not null
  and _ct_registered_animals.ran_birth_date >= p_birth_date_from
  and _ct_registered_animals.ran_birth_date < p_birth_date_to
group by
    extract(year from _ct_registered_animals.ran_birth_date),
    trim(to_char(_ct_registered_animals.ran_birth_date, 'Month')),
    case
        when _ct_counties.cty_uk_area = 'S' then 'Scotland'
        when _ct_counties.cty_uk_area = 'W' then 'Wales'
        when _ct_counties.cty_name is not null then 'England'
        else 'Unknown'
        end,
    _ct_counties.cty_name,
    case
        when _ct_breeds.brd_type = 'D' then 'Dairy'
        else 'Non Dairy'
        end,
    upper(
        coalesce(
            _ct_breeds.brd_long_description,
            _ct_breeds.brd_short_description,
            _ct_breeds.brd_code
        )
    ),
    _ct_registered_animals.ran_sex,
    case
        when _ct_valid_applications.vap_application_type = 'B' then 'Birth Application'
        else _ct_valid_applications.vap_application_type
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