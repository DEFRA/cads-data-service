# CADS – Azure PoC (AKS + PostgreSQL Flexible Server)

## Current Hosting (CDP)

CADS is currently hosted on the **Core Delivery Platform (CDP)**, a multi‑tenant AWS environment running microservices on shared ECS Fargate clusters. CDP provides a standardised hosting model but imposes strict constraints on deployment flexibility, database access, and scaling.

### Key Characteristics

- **Two zones:** Public (UIs) and Protected (APIs)
- **Shared ECS clusters:** No per‑service isolation, no multi‑container support
- **Shared Aurora PostgreSQL:** 0.5–4 ACU limit, no tenant‑level scaling, restricted privileges
- **Strict database isolation:** One service -> one database; no cross‑service access
- **Deployment model:** GitHub‑driven pipeline -> SQS -> Lambda -> ECS

### Limitations

- No multi‑container workloads (unlike Kubernetes)
- No control over compute, networking, or storage
- Limited PostgreSQL capabilities & configurability
- No performance isolation or independent scaling
- Restricted RBAC and no infrastructure‑as‑code control

These constraints limit CADS in areas such as ETL, analytics, ingestion performance, and microservice evolution.

---

## Proposed Azure PoC

We propose a **small, internal PoC** to explore Azure Kubernetes Service (AKS) and Azure Database for PostgreSQL Flexible Server.  
This PoC follows the **CRDB‑Lite** process and is intended only for internal IT audiences.

### Purpose

- Validate AKS deployment patterns using CCoE standards  
- Explore PostgreSQL Flexible Server features (partitioning, scaling, replicas)  
- Test basic outbound connectivity to:
  - **AWS S3** (simple authenticated request)
  - **CDP‑hosted APIs** (simple API call)
- Validate Azure‑native components:
  - Key Vault, Storage Accounts, Firewall/NAT, Front Door Private Link

### Scope (Minimal)

- Small AKS cluster  
- Single PostgreSQL Flexible Server  
- Basic application deployment  
- Simple outbound connectivity tests  
- No ingestion pipelines, no production data, no DR/HA requirements  

### Address Space

A **small address space** is requested, sufficient only for one AKS cluster and supporting services.

---

## Target Architecture (PoC‑Scale)

- **Ingress:** Azure Front Door (Private Link), Internal Load Balancer + AKS Ingress  
- **Compute:** AKS (private API server, simple API/UI pods)  
- **Data:** PostgreSQL Flexible Server (private), Storage Account (private endpoint)  
- **Security:** Key Vault (private), Firewall/NAT for controlled outbound  
- **Outbound:** AWS S3 + CDP APIs (validation only)

### C4 Context diagram

![C4 Context diagram](./images/c4-1-context.png)

### C4 Container diagram

![C4 Container diagram](./images/c4-2-container.png)

---

## PostgreSQL PoC Structure

### Phase 1 — Like‑for‑like baseline
- General Purpose or Memory Optimised tier  
- Realistic vCore/memory sizing  
- Zone‑redundant HA  
- 1–3 read replicas  
- Partitioning of largest tables  
- Appropriate indexing  

**Goal:** Validate that Azure PostgreSQL can match current Aurora behaviour.

### Phase 2 — Scale‑out (optional)
Explore **Elastic Clusters (Citus)** if Phase 1 shows bottlenecks.

- Shard 1–2 large tables  
- Compare latency, concurrency, maintenance  

**Goal:** Determine whether CADS needs distributed PostgreSQL.

---

## Why This PoC Is Low‑Risk

- No production data  
- No ingestion or downstream integrations  
- Minimal cost and footprint  
- Fully reversible  
- Internal IT audience only  
