# PostgreSQL in CDP

CDP supports Aurora Postgres + serverless v2 + read replicas.

## Baseline configuration for Aurora PostgreSQL Serverless on CDP

By default CDP will create an Aurora cluster with the following configuration.

| Write Instance Count	| Reader Instance Count	| Minium ACU | Maximum ACU |
| --------------------  | --------------------- | ---------- | ----------- |
| 1	                    | 1	                    | 0.5        | 4           |

Each CDP environment runs in a shared cluster.

PERF-TEST and PROD provision read replicas.

# PostgreSQL in Azure

| Option | Model | Scale pattern | Read replicas | Good fit for 0.5B+ row tables? |
| -------| ----- | ------------- | ------------- | ------------------------------ |
| PostgreSQL – Flexible Server | Single primary | Vertical + read replicas | Yes | Yes, if tuned & partitioned |
| PostgreSQL – Elastic Clusters | Citus (distributed PG) | Horizontal sharding + replicas | Yes | Yes, for true scale‑out |

## 1. Azure Database for PostgreSQL – Flexible Server

This is the "default" managed Postgres in Azure.

**Key points:**

 * **Single primary** instance (write node).

 * **Vertical scale:** change vCores, memory, storage online (short restart required).

 * **Read replicas:** up to 10 read replicas per primary; async replication, each is its own Flexible Server.

 * **HA:** zone‑redundant HA option within a region.

 * **Storage:** up to multiple TB per server (variations per tier), enough for 0.5B+ row tables if we take care to design indexes/partitioning.

 * **No true "serverless v2" equivalent:** you can scale up/down, but not Aurora‑style continuous autoscaling. There is Burstable tier and autoscale IOPS, but not Aurora serverless semantics.

For our use case for a **CADS PoC:**

 * We can run one write instance + multiple read‑only replicas for reporting/analytics to reduce lock contention on the primary.

 * We will still have a single write bottleneck, so we need to take care and ensure:

     * good indexing
     * table partitioning
     * careful transaction design

For a **PoC** that mirrors "Aurora Postgres + read replicas", Flexible Server + read replicas is the closest like‑for‑like in MS Azure.

## 2. Azure Database for PostgreSQL – Elastic Clusters (Citus)

Elastic Clusters = managed Citus on top of Flexible Server.

**Key points:**

 * **Distributed Postgres:** multiple nodes (Flexible Servers) wired together via Citus.

 * **Horizontal sharding:**

    * Row‑based sharding (distribute big tables by key).
    * Schema‑based sharding (per‑tenant schemas).

 * **Scale‑out:** add nodes to increase capacity and parallelism.

 * **Coordinator + workers:** you connect to a coordinator; Citus routes queries.

 * **Read scaling:** you can still use read replicas, but the big win is **parallel query execution across shards**.

 * Much better fit if you have very large analytical workloads

For 0.5B+ row tables, Elastic Clusters gives you room to grow horizontally instead of just utilising vertical scaling.

## 3. Read/write topology in Azure Postgres

**Flexible Server:**

 * 1 primary (read/write).

 * Up to 10 read replicas (read‑only).

 * Async replication → replicas are slightly behind; good for reporting, APIs that tolerate slight lag.

**Elastic Clusters:**

 * Coordinator + worker nodes.

 * You can still use read replicas, but often you scale reads by:

     * adding more worker nodes
     * sharding large tables
     * using Citus' parallel query

**Note.** Read replicas don't remove locks on the primary (writes still lock there), but they offload read pressure, which often reduces contention enough in practice.

# What should we do for a PoC moving into Azure?

Given CADS is coming from **Aurora Postgres serverless v2 + read replicas**, and we know that we will have some **very large tables**, we should structure the PoC in two phases:

## Phase 1 – Like‑for‑like baseline (simplest path)

 * Use **Azure Database for PostgreSQL – Flexible Server**:

    * Tier: **General Purpose** or **Memory Optimized**.
    * Start with a realistic vCore/memory size (not tiny).

 * Enable:

    * **Zone‑redundant HA**.
    * 1–3 read replicas for reporting / heavy read workloads.

 * In the schema:

    * Partition the largest tables (range or hash).
    * Add the right covering indexes for your hot paths.

 * **Goal:** prove you can match or approach current Aurora behaviour with minimal architectural change.

## Phase 2 – Scale‑out exploration (if Phase 1 looks tight)

If a single primary is going to be the long‑term bottleneck (especially with 0.5B+ row tables and heavy concurrent workloads), we could spin up a **PoC with Elastic Clusters (Citus)**:

 * Identify 1–2 **very large, high‑traffic tables** and **shard** them by a sensible key.

 * Compare:

    * query latency
    * concurrency
    * maintenance operations (vacuum, index rebuilds)
    * operational complexity

 * **Goal:** This provides us with a clear view: "Can we stay on a big single Flexible Server + replicas, or do we need distributed Postgres"

# Useful reading

## Partitioning

Inside a single PostgreSQL server.

"One big table, split into smaller physical chunks, but still queried as one table."

**Why you use it**

 * Faster queries on large tables
 * Faster index scans
 * Avoids table‑wide locks
 * Makes 0.5B+ row tables manageable

### Common partition types

#### A) Range partitioning

Split by a continuous value.

Examples:

 * By date: 2023, 2024, 2025
 * By ID ranges: 1–10M, 10M–20M, etc.

Good when:

 * You query by date
 * You archive old data
 * You insert in time order

#### B) Hash partitioning

Postgres hashes a key and distributes rows evenly.

Example:

 * Partition by party_id
 * Rows get spread across N partitions automatically

Good when:

 * You need even distribution
 * You don't have a natural date key
 * You want to reduce contention on hot tables

**Key point:**

Partitioning **does not create multiple servers**.
It’s still **one Postgres instance**, just organised better.

## Sharding

Sharding is **horizontal distribution across multiple machines**.

In Azure, this is what **Elastic Clusters (Citus)** gives you.

"One logical database, but the data is spread across multiple servers."

This is how you scale beyond what a single Postgres box can handle.

### Sharding models in Citus

#### A) Row‑based sharding (distributed tables)

You pick a distribution key (e.g. party_id, tenant_id, account_id).

Citus then:

 * Splits the table into shards
 * Places shards across multiple worker nodes
 * Runs queries in parallel across nodes

Good for:

 * Very large tables (hundreds of millions to billions of rows)
 * Multi‑tenant systems
 * High write throughput
 * Parallel analytical queries

This is the closest Azure equivalent to **Aurora Serverless v2 + read replicas**, but with true horizontal scale.

#### B) Schema‑based sharding (per‑tenant schemas)

Instead of splitting rows, you split **schemas**.

Example:

 * Tenant A → schema tenant_a
 * Tenant B → schema tenant_b
 * Tenant C → schema tenant_c

Citus places each schema on a different worker node.

Good for:

 * SaaS platforms
 * Strong tenant isolation
 * Different tenants having different data sizes

Not ideal if you have very large shared tables.

## Partitioning-Sharding comparison

| Concept | Where it happens | Purpose | Good for |
| ------- | ---------------- | ------- | -------- |
| Partitioning | Inside one Postgres server | Organise a huge table into smaller pieces | 100M–1B row tables on a single machine |
| Sharding | Across multiple servers | Scale out horizontally | Billions of rows, high concurrency, multi‑tenant |

# Useful links

 - [Flexible Server](https://learn.microsoft.com/en-us/azure/postgresql/overview)
 - [Scaling resources in Azure Database for PostgreSQL](https://learn.microsoft.com/en-us/azure/postgresql/scale/concepts-scaling-resources?utm_source=copilot.com)
 - [What is Citus?](https://learn.microsoft.com/en-us/postgresql/citus/what-is-citus?view=citus-14)
 [Sharding models on elastic clusters in Azure Database for PostgreSQL](https://learn.microsoft.com/en-us/azure/postgresql/elastic-clusters/concepts-elastic-clusters-sharding-models)