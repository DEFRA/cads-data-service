-- liquibase formatted sql

-- changeset MarkGent1:1772802016473-1 splitStatements:false
CREATE TABLE "mi_role" ("role_id" UUID NOT NULL, "role_key" TEXT NOT NULL, "description" TEXT, "created_at" TIMESTAMP WITH TIME ZONE DEFAULT NOW() NOT NULL, CONSTRAINT "mi_role_pkey" PRIMARY KEY ("role_id"));

-- changeset MarkGent1:1772802016473-2 splitStatements:false
ALTER TABLE "mi_role" ADD CONSTRAINT "mi_role_role_key_key" UNIQUE ("role_key");

