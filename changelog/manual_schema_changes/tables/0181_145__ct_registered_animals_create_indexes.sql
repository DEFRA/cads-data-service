-- Primary filter column — essential
CREATE INDEX idx_ran_birth_date ON _ct_registered_animals (ran_birth_date)
    WHERE ran_birth_date IS NOT NULL;

-- Covering index to avoid heap fetches for the join/group columns
-- (add columns used in JOINs and GROUP BY to avoid table lookups)
CREATE INDEX idx_ran_covering ON _ct_registered_animals (
                                                         ran_birth_date, ran_brd_id, ran_vap_id, ran_loc_id_passport,
                                                         ran_cry_id_chr_origin, ran_sex
    ) WHERE ran_birth_date IS NOT NULL;
