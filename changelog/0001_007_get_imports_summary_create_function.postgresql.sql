-- liquibase formatted sql

-- changeset tom:0001_007 endDelimiter://
-- this is a placeholder for the function - the data is NOT CORRECT
-- and is simply FAKED from the deaths function
DROP FUNCTION IF EXISTS public.get_imports_summary(
    p_import_date_from DATE,
    p_import_date_to   DATE
);

CREATE OR REPLACE FUNCTION public.get_imports_summary(
    p_import_date_from date,
    p_import_date_to date
)
    RETURNS TABLE (
                      "country" text,
                      "month_year" date,
                      "age_at_import_months" int,
                      "age_band" text,
                      "breed_type" text,
                      "sex" text,
                      "number_of_imports" bigint
                  )
    LANGUAGE sql
AS $$
select
    case
        when cty.cty_uk_area = 'S' then 'Scotland'
        when cty.cty_uk_area = 'W' then 'Wales'
        when cty.cty_name is not null then 'England'
        else 'Unknown'
        end as "country",
    
    date_trunc('month', mov.mov_movement_date) as "month_year",
    
    (        extract(year from age(mov.mov_movement_date, ran.ran_birth_date))::int * 12
        + extract(month from age(mov.mov_movement_date, ran.ran_birth_date))::int
        ) as "age_at_import_months",
    
    concat(extract(year from age(mov.mov_movement_date, ran.ran_birth_date))::int * 12
        , ' to ',
           extract(year from age(mov.mov_movement_date, ran.ran_birth_date))::int * 12 + 12
    ) as "age_band",
    
    case
        when brd.brd_type = 'D' then 'Dairy'
        else 'Non Dairy'
        end as "breed_type",
    ran.ran_sex as "sex",
    count(*) as "number_of_imports"
from _ct_registered_animals ran
         join _ct_registered_movements mov
              on mov.mov_id = ran.ran_mov_id_death
         left join _ct_breeds brd
                   on brd.brd_id = ran.ran_brd_id
         left join _ct_locations loc
                   on loc.loc_id = mov.mov_loc_id
         left join _ct_counties cty
                   on cty.cty_id = loc.loc_cty_id
where mov.mov_movement_date is not null
  and ran.ran_birth_date is not null
  and mov.mov_movement_date >= p_import_date_from
  and mov.mov_movement_date < p_import_date_to
group by
    date_trunc('month', mov.mov_movement_date),
    case
        when cty.cty_uk_area = 'S' then 'Scotland'
        when cty.cty_uk_area = 'W' then 'Wales'
        when cty.cty_name is not null then 'England'
        else 'Unknown'
        end,
    case
        when brd.brd_type = 'D' then 'Dairy'
        else 'Non Dairy'
        end,
    ran.ran_sex,
    (
        extract(year from age(mov.mov_movement_date, ran.ran_birth_date))::int * 12
        + extract(month from age(mov.mov_movement_date, ran.ran_birth_date))::int
        ),
           concat(extract(year from age(mov.mov_movement_date, ran.ran_birth_date))::int * 12
        , ' to ',
           extract(year from age(mov.mov_movement_date, ran.ran_birth_date))::int * 12 + 12
    )
order by
    "country",
    "month_year",
    "age_at_import_months",
    "breed_type",
    "sex";
$$;