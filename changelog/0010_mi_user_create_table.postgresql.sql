-- liquibase formatted sql

-- changeset MarkGent1:1772801813571-1 splitStatements:false
CREATE TABLE "mi_user" ("user_id" UUID NOT NULL, "external_subject" TEXT NOT NULL, "display_name" TEXT NOT NULL, "email" TEXT, "is_active" BOOLEAN DEFAULT TRUE NOT NULL, "created_at" TIMESTAMP WITH TIME ZONE DEFAULT NOW() NOT NULL, CONSTRAINT "mi_user_pkey" PRIMARY KEY ("user_id"));

-- changeset MarkGent1:1772801813571-2 splitStatements:false
ALTER TABLE "mi_user" ADD CONSTRAINT "mi_user_external_subject_key" UNIQUE ("external_subject");

