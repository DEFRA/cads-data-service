-- liquibase formatted sql

-- changeset david:0001_007 endDelimiter://
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
                    "age_at_import_months" integer, 
                    "age_band" text, 
                    "breed_type" text, 
                    "sex" text, 
                    "number_of_imports" bigint
        ) 
    LANGUAGE sql
AS $$
SELECT
    q."country",
    q."month_year",
    q."age_at_import_months",
    CASE
        WHEN q."age_at_import_months" < 1 THEN 'Under 1'
        WHEN q."age_at_import_months" < 2 THEN '1 to 2'
        WHEN q."age_at_import_months" < 3 THEN '2 to 3'
        WHEN q."age_at_import_months" < 4 THEN '3 to 4'
        WHEN q."age_at_import_months" < 5 THEN '4 to 5'
        WHEN q."age_at_import_months" < 6 THEN '5 to 6'
        WHEN q."age_at_import_months" < 12 THEN '6 to 12'
        WHEN q."age_at_import_months" < 24 THEN '12 to 24'
        WHEN q."age_at_import_months" < 30 THEN '24 to 30'
        WHEN q."age_at_import_months" < 36 THEN '30 to 36'
        WHEN q."age_at_import_months" < 48 THEN '36 to 48'
        WHEN q."age_at_import_months" < 60 THEN '48 to 60'
        WHEN q."age_at_import_months" < 72 THEN '60 to 72'
        ELSE '72 and Over'
    END AS "age_band",
    q."breed_type",
    q."sex",
    count(*) AS "number_of_imports"
FROM (
    SELECT
        upper(c.cry_name) AS "country",
        date_trunc('month', a.ran_birth_date)::date AS "month_year",
        a.ran_birth_date,
        a.ran_current_modified_date,
        extract(year FROM age(a.ran_current_modified_date, a.ran_birth_date))::int
            AS "age_at_import_months",
        CASE WHEN b.brd_type = 'D' THEN 'Dairy' ELSE 'Non Dairy' END AS "breed_type",
        a.ran_sex AS "sex"
    FROM _ct_registered_animals a
    JOIN _ct_countries c
        ON c.cry_id = a.ran_cry_id_chr_origin
    LEFT JOIN _ct_breeds b
        ON b.brd_id = a.ran_brd_id
    WHERE a.ran_current_modified_date >= p_import_date_from
      AND a.ran_current_modified_date < p_import_date_to
      AND a.ran_cry_id_chr_origin IS NOT NULL
      AND a.ran_birth_date IS NOT NULL
      AND a.ran_birth_date <= a.ran_current_modified_date
) q
GROUP BY
    q."country",
    q."month_year",
    q.ran_birth_date,
    q.ran_current_modified_date,
    q."age_at_import_months",
    q."breed_type",
    q."sex"
ORDER BY
    q."country",
    q."month_year",
    q.ran_birth_date,
    q.ran_current_modified_date,
    q."age_at_import_months",
    q."breed_type",
    q."sex";
$$;