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
                      "Birth Year" int,
                      "Birth Month" text,
                      "Country" text,
                      "Gov Region" text,
                      "County" text,
                      "Breed Type" text,
                      "Breed" text,
                      "Sex" text,
                      "Application Type" text,
                      "Number Of Births" bigint
                  )
    LANGUAGE sql
AS $$
select
    extract(year from _ct_registered_animals.ran_birth_date)::int as "Birth Year",
    trim(to_char(_ct_registered_animals.ran_birth_date, 'Month')) as "Birth Month",
    coalesce(_ct_countries.cry_name, 'Unknown') as "Country",
    null::text as "Gov Region",
    _ct_counties.cty_name as "County",
    case
        when _ct_breeds.brd_type = 'D' then 'Dairy'
        else 'Non Dairy'
        end as "Breed Type",
    upper(
        coalesce(
            _ct_breeds.brd_long_description,
            _ct_breeds.brd_short_description,
            _ct_breeds.brd_code
        )
    ) as "Breed",
    _ct_registered_animals.ran_sex as "Sex",
    case
        when _ct_valid_applications.vap_application_type = 'B' then 'Birth Application'
        else _ct_valid_applications.vap_application_type
        end as "Application Type",
    count(*) as "Number Of Births"
from _ct_registered_animals
         left join _ct_breeds
                   on _ct_breeds.brd_id = _ct_registered_animals.ran_brd_id
         left join _ct_valid_applications
                   on _ct_valid_applications.vap_id = _ct_registered_animals.ran_vap_id
         left join _ct_locations
                   on _ct_locations.loc_id = _ct_registered_animals.ran_loc_id_passport
         left join _ct_counties
                   on _ct_counties.cty_id = _ct_locations.loc_cty_id
         left join _ct_countries
                   on _ct_countries.cry_id = _ct_registered_animals.ran_cry_id_chr_origin
where _ct_registered_animals.ran_birth_date is not null
  and _ct_registered_animals.ran_birth_date >= p_birth_date_from
  and _ct_registered_animals.ran_birth_date < p_birth_date_to
group by
    extract(year from _ct_registered_animals.ran_birth_date),
    trim(to_char(_ct_registered_animals.ran_birth_date, 'Month')),
    coalesce(_ct_countries.cry_name, 'Unknown'),
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
    "Birth Year",
    "Birth Month",
    "Country",
    "County",
    "Breed Type",
    "Breed",
    "Sex",
    "Application Type";
$$;