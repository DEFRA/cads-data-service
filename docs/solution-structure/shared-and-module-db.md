# 1 Overview

# 1.1 BuildingBlocks.Core holds the canonical shared domain

 - Animal
 - Location
 - Party
 - etc.

# 1.2 BuildingBlocks.Infrastructure holds the shared EF configurations

 - AnimalConfiguration
 - LocationConfiguration
 - etc.

# 1.3 Each module has its own DbContext

 - MiBffReadDbContext
 - MiBffWriteDbContext

 - StorageBridgeReadDbContext
 - StorageBridgeWriteDbContext

 - etc.

# 1.4 Each DbContext imports the shared model

**Note.** Not via inheritance but via:

```
modelBuilder.ApplyConfigurationsFromAssembly(typeof(AnimalConfiguration).Assembly);
```

# 1.5 Modules remain independent

They only depend on BuildingBlocks, never on each other.

# 2 Modular model

The correct mental model is having three kinds of domain models:

## 2.1 Module‑specific domain entities

Owned by a single module.
Live inside that module's **Core** project.

Example:

 - MiBff-specific reporting aggregates
 - StorageBridge-specific ingestion metadata

These stay inside the module.

## 2.2 Cross‑module canonical entities

Used by multiple modules.
Represent real business concepts.
Have a single source of truth.

Examples:

 - Animal
 - Location
 - Party
 - Movement

These belong in:

```
BuildingBlocks.Core
  /Domain
    /Livestock
      Animal.cs
      Location.cs
```

## 2.3 Shared EF configurations

These belong in:

```
BuildingBlocks.Infrastructure
  /Persistence
    /Configurations
      AnimalConfiguration.cs
      LocationConfiguration.cs
```

## 2.4 How modules consume shared entities

Each module has its own DbContext:

 - StorageBridgeReadDbContext
 - StorageBridgeWriteDbContext

Each DbContext imports the shared model:

```
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    // Import shared canonical entities
    modelBuilder.ApplyConfigurationsFromAssembly(
        typeof(AnimalConfiguration).Assembly
    );

    // Import module-specific entities
    modelBuilder.ApplyConfigurationsFromAssembly(
        typeof(StorageBridgeReadDbContext).Assembly
    );
}
```

# 3. First principles (modular monolith rules)

A modular monolith enforces:

## Modules own their own data

StorageBridge owns its tables.
SystemAdmin owns its tables.

## Modules cannot call each other's application services

That creates tight coupling.

## Modules can depend on shared canonical domain models

Which we have already placed in BuildingBlocks.Core.

## Modules can update shared canonical tables directly

As long as:

 * The entity is canonical
 * The EF configuration is canonical
 * The DbContext includes it
 * The module has permission to update it

# 4. Summary

✔ No inheritance
✔ No shared base DbContext
✔ No cross-module coupling
✔ Each module chooses what it needs
✔ Shared entities stay consistent across modules

Why this works beautifully

**Modules stay independent**
Each module has its own DbContext, migrations, and persistence boundary.

**Shared domain stays canonical**
Animals, Locations, etc. are defined once.

**EF Core stays modular**
Each DbContext builds its own model from:
 - Shared configurations
 - Module-specific configurations

**You avoid the “God DbContext” anti-pattern**
No single DbContext knows everything.

**A subtle but important rule**
Only put entities in BuildingBlocks.Core if they are truly cross‑module domain concepts.
If an entity is only used by one module, it stays in that module.
This keeps BuildingBlocks clean and prevents domain bloat.