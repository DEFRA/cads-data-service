# Liquibase Workflow Guide

## Managing Schema Changes with Reference & CADS Databases

This guide explains how to install Liquibase, configure your environment, maintain a reference (“golden”) database, and generate migration scripts for the CADS database using Liquibase’s diff workflow.

## Changelog Organisation

The complete Liquibase changelog is organised as follows:

- `0000` — table DDL, split by `cads`, `cts`, `cts_transactions`, and `cts_audit`
- `0001` — indexes, split by schema
- `0002` — functions and stored procedures, held centrally in the `cads` schema
- `0003` — integrations
- `0004` — ordinary DEV database seed data
- `0005` — fake data

This ensures that tables and indexes exist before any data is loaded.

All functions and stored procedures are held in the `cads` schema. A routine that operates on another schema includes the target schema at the start of its name. For example, a procedure that truncates the `cts_audit` schema is named `cads.cts_audit_truncate_all_tables()`.

### Removing DEV and Fake Data

Before importing real data, remove the existing DEV and fake data while retaining all tables, indexes, constraints, functions, permissions, and other database structures:

```sql
BEGIN;
CALL cads.cts_audit_truncate_all_tables();
CALL cads.cts_transactions_truncate_all_tables();
CALL cads.cts_truncate_all_tables();
CALL cads.truncate_all_tables();
COMMIT;
```

These procedures use `TRUNCATE TABLE ... RESTART IDENTITY CASCADE`. This deletes all data in the four application schemas and resets identity sequences, so only run them when the existing data is no longer required.

### Pseudonymising Existing Data

To pseudonymise all supported data in one transaction, run:

```sql
CALL cads.pseudonymise_all_data();
```

Individual schemas can be pseudonymised with:

```sql
CALL cads.pseudonymise_data();
CALL cads.cts_pseudonymise_data();
CALL cads.cts_transactions_pseudonymise_data();
CALL cads.cts_audit_pseudonymise_data();
```

## 1. Installation & Setup

**Install Java (required for Liquibase)**

```
choco install temurin17 --force
java --version
```

**Set JAVA_HOME**

```
setx JAVA_HOME "C:\Program Files\Eclipse Adoptium\jdk-17.0.17.10-hotspot"
```

### Install Liquibase

**Liquibase is installed via Chocolatey**

```
liquibase --version
```

### Install Liquibase LPM (PostgreSQL extension + JDBC driver)

Run this from:

```
C:\ProgramData\chocolatey\bin
```

Powershell:

```
liquibase lpm add postgresql
```

This installs:
- PostgreSQL JDBC driver
- Liquibase PostgreSQL extensio

### Install PostgreSQL CLI (optional)

Only needed if you want a local Postgres instance outside Docker.

```
choco install postgresql
psql --version
```

### Stop Windows PostgreSQL Services (to avoid port conflicts)

```
Get-Service *postgres*
Stop-Service postgresql-x64-18
```

## 2. Docker Environment

### Pull pgAdmin

```
docker pull dpage/pgadmin4:snapshot
```

### Set PostgreSQL password for CLI tools

```
$env:PGPASSWORD="postgres"
```

**Useful Docker commands**

List running containers:

```
docker ps
```

## 3. Running the Databases

**Start the full environment:**

```
docker-compose -f docker-compose.yml -f docker-compose.override.yml up --build -d
```

**Stop and remove volumes:**

```
docker-compose -f docker-compose.yml -f docker-compose.override.yml down -v
```

## 4. Validating Database Connectivity

**Check if Postgres is accepting connections:**

```
pg_isready
```

**Expected output:**

```
:5432 - accepting connections
```

## 5. Connecting to Databases

### CADS Database

```
psql -h localhost -p 5432 -U postgres -d cads_data_service
```

### Reference Database

```
psql -h localhost -p 54432 -U postgres -d reference_schema
```

## 6. Liquibase Dual‑Database Workflow

Liquibase compares:
- Reference DB → the ideal schema
- CADS DB → the actual schema

Liquibase then generates migration scripts to bring CADS in line with the reference.

### Step 1 — Bring the Reference DB up to the current schema

```
liquibase --url=jdbc:postgresql://localhost:54432/reference_schema --username=postgres --password=postgres --contexts=local update
```
If the changelog file can't be found you can use:
```
liquibase --url=jdbc:postgresql://localhost:54432/reference_schema --username=postgres --password=postgres --contexts=local --changeLogFile=db.changelog.xml --search-path=changelog  update
```

### Step 2 — Make schema changes in the Reference DB

You manually modify the reference database only:
- add tables
- rename columns
- change constraints
- drop indexes
- etc.

Never manually modify the CADS database.

### Step 3 — Detect differences

```
liquibase diff \
	--url=jdbc:postgresql://localhost:5432/cads_data_service \
	--username=<POSTGRES_USER> \
	--password=<POSTGRES_PASSWORD> \
	--reference-url=jdbc:postgresql://localhost:54432/reference_schema \
	--reference-username=<POSTGRES_USER> \
	--reference-password=<POSTGRES_PASSWORD>
```

Note. If you run this command from the `changelog` folder with your liquibase.properties set up you only need to use `liquibase diff`

This shows what changed between:
- reference_schema
- cads_data_service

### Step 4 — Generate a migration changelog

```
liquibase diff-changelog 
	--changelog-file=changelog/<XXXX_NEW_CHANGESET_NAME>.postgresql.sql \
	--url=jdbc:postgresql://localhost:5432/cads_data_service  \
	--username=<POSTGRES_USER> \
	--password=<POSTGRES_PASSWORD> \
	--reference-url=jdbc:postgresql://localhost:54432/reference_schema  \
	--reference-username=<POSTGRES_USER> \
	--reference-password=<POSTGRES_PASSWORD>
```

Note. If you run this command from the `changelog` folder with your liquibase.properties set up you only need to use `liquibase diff-changelog --changelog-file=<XXXX_NEW_CHANGESET_NAME>.postgresql.sql`

Liquibase outputs a migration script containing:
- addColumn
- createTable
- dropColumn
- addForeignKeyConstraint
- etc.

Review and commit this file.

**Naming convention:**

```text
<series>_<sequence>_<description>.postgresql.sql
```

Where:

- `series` identifies the type of database object or data: `0000` tables, `0001` indexes, `0002` routines, `0003` integrations, `0004` ordinary DEV data, or `0005` fake data
- `sequence` is a zero-padded number within that series
- `description` identifies the schema, object, or purpose

Examples:

```text
0000_020_cts_tables.postgresql.sql
0001_002_cts_transactions_indexes.postgresql.sql
0002_019_cts_truncate_tables_routine.postgresql.sql
0003_001_cts_file_imports_integration.postgresql.sql
0004_019_ct_sublocation_types_seed_data.postgresql.sql
0005_001_ct_workgroups_seed_data_faker_data.postgresql.sql
```

### Step 5

Update the master changelog file, `changelog/db.changelog.xml`, with the new changeset.

```
<include file="changelog/<XXXX_NEW_CHANGESET_NAME>.postgresql.sql" />
```

### Step 6 — Apply the migration to the CADS database

```
liquibase --contexts=local update --changelog-file=<migration-name>.postgresql.sql
```

This updates the CADS database to match the reference schema.

You can also update the CADS database using the below:

```
liquibase --url=jdbc:postgresql://localhost:5432/cads_data_service --username=postgres --password=postgres --contexts=local update
```

Note. If you run this command from the `changelog` folder with your liquibase.properties set up you only need to use `liquibase --contexts=local update`

## 7. Reference vs CADS Responsibilities

| Action                                   | Reference DB | CADS DB |
|------------------------------------------|--------------|---------|
| Manually edit schema                     | **Yes**      | **No**  |
| Liquibase generates changes from         | **Reference**| **Target** |
| Liquibase applies changes to             | No           | **Yes** |
| Should drift?                            | No           | No      |
| Should be rebuilt often?                 | Yes          | No      |

## 8. Summary

- The **reference database** is your "golden schema".
- You manually update the reference database.
- Liquibase compares reference → CADS.
- Liquibase generates migration scripts.
- You apply those scripts to CADS.
- CADS is never manually edited.

This workflow ensures:

- clean schema evolution
- reproducible migrations
- no drift
- safe production deployments
