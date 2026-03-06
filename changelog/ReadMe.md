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
psql -h localhost -p 55432 -U postgres -d reference_schema
```

## 6. Liquibase Dual‑Database Workflow

Liquibase compares:
- Reference DB → the ideal schema
- CADS DB → the actual schema

Liquibase then generates migration scripts to bring CADS in line with the reference.

### Step 1 — Bring the Reference DB up to the current schema

```
liquibase --url=jdbc:postgresql://localhost:55432/reference_schema --username=postgres --password=postgres update
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
```

This shows what changed between:
- reference_schema
- cads_data_service

### Step 4 — Generate a migration changelog

```
liquibase diffChangelog --changelog-file=<migration-name>.postgresql.sql
```

Liquibase outputs a migration script containing:
- addColumn
- createTable
- dropColumn
- addForeignKeyConstraint
- etc.

Review and commit this file.

### Step 5 — Apply the migration to the CADS database

```
liquibase update --changelog-file=<migration-name>.postgresql.sql
```

This updates the CADS database to match the reference schema.

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
