DO
$$
BEGIN
   IF NOT EXISTS (
      SELECT FROM pg_database WHERE datname = 'cads_data_service'
   ) THEN
      CREATE DATABASE "cads_data_service";
   END IF;
END
$$;