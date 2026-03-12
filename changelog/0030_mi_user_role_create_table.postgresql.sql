-- liquibase formatted sql

-- changeset MarkGent1:1772802149629-1 splitStatements:false
CREATE TABLE "mi_user_role" ("user_id" UUID NOT NULL, "role_id" UUID NOT NULL, "granted_at" TIMESTAMP WITH TIME ZONE DEFAULT NOW() NOT NULL, CONSTRAINT "mi_user_role_pkey" PRIMARY KEY ("user_id", "role_id"));

-- changeset MarkGent1:1772802149629-2 splitStatements:false
CREATE INDEX "mi_user_role_user_idx" ON "mi_user_role" USING btree("user_id");

-- changeset MarkGent1:1772802149629-3 splitStatements:false
CREATE INDEX "mi_user_role_role_idx" ON "mi_user_role" USING btree("role_id");

-- changeset MarkGent1:1772802149629-4 splitStatements:false
ALTER TABLE "mi_user_role" ADD CONSTRAINT "mi_user_role_role_id_fkey" FOREIGN KEY ("role_id") REFERENCES "mi_role" ("role_id") ON UPDATE NO ACTION ON DELETE CASCADE;

-- changeset MarkGent1:1772802149629-5 splitStatements:false
ALTER TABLE "mi_user_role" ADD CONSTRAINT "mi_user_role_user_id_fkey" FOREIGN KEY ("user_id") REFERENCES "mi_user" ("user_id") ON UPDATE NO ACTION ON DELETE CASCADE;

