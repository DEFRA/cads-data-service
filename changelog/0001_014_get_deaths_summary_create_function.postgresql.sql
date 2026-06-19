-- liquibase formatted sql

-- changeset david:0001_014 endDelimiter://
DROP FUNCTION IF EXISTS public.get_deaths_summary(
    p_death_date_from DATE,
    p_death_date_to   DATE
);

CREATE OR REPLACE FUNCTION public.get_deaths_summary(
    p_death_date_from date,
    p_death_date_to date
)
    RETURNS TABLE (
                      "death_year" int,
                      "death_month" text,
                      "country" text,
                      "county" text,
                      "breed_type" text,
                      "breed" text,
                      "breed_code" text,
                      "sex" text,
                      "age_at_death_months" int,
                      "number_of_deaths" bigint
                  )
    LANGUAGE sql
AS $$
select
    extract(year from mov.mov_movement_date)::int as "death_year",
    trim(to_char(mov.mov_movement_date, 'Month')) as "death_month",
    case
        when cty.cty_uk_area = 'S' then 'Scotland'
        when cty.cty_uk_area = 'W' then 'Wales'
        when cty.cty_name is not null then 'England'
        else 'Unknown'
        end as "country",
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
    brd.brd_code as "breed_code",
    ran.ran_sex as "sex",
    (
        extract(year from age(mov.mov_movement_date, ran.ran_birth_date))::int * 12
        + extract(month from age(mov.mov_movement_date, ran.ran_birth_date))::int
        ) as "age_at_death_months",
    count(*) as "number_of_deaths"
from cts.ct_registered_animals ran
         join cts.ct_registered_movements mov
              on mov.mov_id = ran.ran_mov_id_death
         left join cts.ct_breeds brd
                   on brd.brd_id = ran.ran_brd_id
         left join cts.ct_locations loc
                   on loc.loc_id = mov.mov_loc_id
         left join cts.ct_counties cty
                   on cty.cty_id = loc.loc_cty_id
where mov.mov_movement_date is not null
  and ran.ran_birth_date is not null
  and mov.mov_movement_date >= p_death_date_from
  and mov.mov_movement_date < p_death_date_to
group by
    extract(year from mov.mov_movement_date),
    trim(to_char(mov.mov_movement_date, 'Month')),
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
    brd.brd_code,
    ran.ran_sex,
    (
        extract(year from age(mov.mov_movement_date, ran.ran_birth_date))::int * 12
        + extract(month from age(mov.mov_movement_date, ran.ran_birth_date))::int
        )
order by
    "death_year",
    "death_month",
    "country",
    "county",
    "breed_type",
    "breed",
    "breed_code",
    "sex",
    "age_at_death_months";
$$;
