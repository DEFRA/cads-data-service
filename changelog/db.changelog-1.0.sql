-- liquibase formatted sql

-- changeset liquibase:1771515540299-1 splitStatements:false
CREATE TABLE "organisation" ("id" UUID DEFAULT gen_random_uuid() NOT NULL, "name" TEXT NOT NULL, "created_at" TIMESTAMP WITH TIME ZONE DEFAULT NOW() NOT NULL, CONSTRAINT "organisation_pkey" PRIMARY KEY ("id"));

