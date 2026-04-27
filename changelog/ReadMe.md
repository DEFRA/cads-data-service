# Liquibase Workflow Guide

## Managing Schema Changes with Reference & CADS Databases

This guide explains how to install Liquibase, configure your environment, maintain a reference (“golden”) database, and generate migration scripts for the CADS database using Liquibase’s diff workflow.

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
liquibase diff 
	--url=jdbc:postgresql://localhost:5432/<POSTGRES_DB> 
	--username=<POSTGRES_USER> 
	--password=<POSTGRES_PASSWORD> 
	--reference-url=jdbc:postgresql://localhost:54432/<POSTGRES_REF_DB> 
	--reference-username=<POSTGRES_USER> 
```

Note. If you run this command from the `changelog` folder with your liquibase.properties set up you only need to use `liquibase diff`

This shows what changed between:
- reference_schema
- cads_data_service

### Step 4 — Generate a migration changelog

```
liquibase diff-changelog 
	--changelog-file=changelog/<XXXX_NEW_CHANGESET_NAME>.postgresql.sql 
	--url=jdbc:postgresql://localhost:5432/<POSTGRES_DB> 
	--username=<POSTGRES_USER> 
	--password=<POSTGRES_PASSWORD> 
	--reference-url=jdbc:postgresql://localhost:54432/<POSTGRES_REF_DB> 
	--reference-username=<POSTGRES_USER> 
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

**Naming conventions:**

```
<sequence>_<scope>_<action>_<object>.postgresql.sql
<sequence>_<domain>_seed_<object>_<purpose>.sql
```

Where:

- sequence = a zero‑padded number (0010, 0020, 0030)
- scope = the domain (user, report, permission, role, schema, etc.)
- action = create, alter, add, drop, seed, view, etc.
- object = the table/view/index/etc

**Note.** You should break it into multiple changesets grouped by domain.

### Examples

New

```
0010_user_create_table.sql
0020_role_create_table.sql
0030_permission_create_table.sql
0040_user_role_create_table.sql
0050_permission_create_view_effective_report_permission.sql
```

Alterations

```
0011_user_add_column_last_login.sql
0012_report_add_index_report_key.sql
0013_permission_seed_initial_permissions.sql
0014_user_role_add_fk_constraints.sql
```

Seeds

```
0110_permission_seed_initial_permissions.sql
0120_report_seed_initial_reports.sql
```

Sequencing provides: spacing for future changes.

Spacing changesets is a strategic choice, not a requirement.
It gives you:
- room to insert future changes
- a clean, readable history
- a timeline that tells a story
- no renumbering headaches
- a schema that scales with your platform

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
