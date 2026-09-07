using System;
using System.Collections.Generic;
using Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Schemas.Cads.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Schemas.Cads.Contexts;

public partial class CadsGraphQLDbContext : DbContext
{
    public CadsGraphQLDbContext(DbContextOptions<CadsGraphQLDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Animal> Animals { get; set; }

    public virtual DbSet<AnimalBirth> AnimalBirths { get; set; }

    public virtual DbSet<AnimalBreed> AnimalBreeds { get; set; }

    public virtual DbSet<AnimalBreedState> AnimalBreedStates { get; set; }

    public virtual DbSet<AnimalCollective> AnimalCollectives { get; set; }

    public virtual DbSet<AnimalCollectiveDeath> AnimalCollectiveDeaths { get; set; }

    public virtual DbSet<AnimalCollectiveParty> AnimalCollectiveParties { get; set; }

    public virtual DbSet<AnimalCollectiveRef> AnimalCollectiveRefs { get; set; }

    public virtual DbSet<AnimalCollectiveRegistration> AnimalCollectiveRegistrations { get; set; }

    public virtual DbSet<AnimalCollectiveRole> AnimalCollectiveRoles { get; set; }

    public virtual DbSet<AnimalCollectiveState> AnimalCollectiveStates { get; set; }

    public virtual DbSet<AnimalDeath> AnimalDeaths { get; set; }

    public virtual DbSet<AnimalDeathReason> AnimalDeathReasons { get; set; }

    public virtual DbSet<AnimalGenotype> AnimalGenotypes { get; set; }

    public virtual DbSet<AnimalIdentifier> AnimalIdentifiers { get; set; }

    public virtual DbSet<AnimalLostOrStolenState> AnimalLostOrStolenStates { get; set; }

    public virtual DbSet<AnimalLostOrStolenStatus> AnimalLostOrStolenStatuses { get; set; }

    public virtual DbSet<AnimalMark> AnimalMarks { get; set; }

    public virtual DbSet<AnimalNoticeToIdentify> AnimalNoticeToIdentifies { get; set; }

    public virtual DbSet<AnimalOriginalIdentifierType> AnimalOriginalIdentifierTypes { get; set; }

    public virtual DbSet<AnimalParty> AnimalParties { get; set; }

    public virtual DbSet<AnimalPartyRef> AnimalPartyRefs { get; set; }

    public virtual DbSet<AnimalProductionType> AnimalProductionTypes { get; set; }

    public virtual DbSet<AnimalRegistrationCategory> AnimalRegistrationCategories { get; set; }

    public virtual DbSet<AnimalResolutionType> AnimalResolutionTypes { get; set; }

    public virtual DbSet<AnimalRole> AnimalRoles { get; set; }

    public virtual DbSet<AnimalSex> AnimalSexes { get; set; }

    public virtual DbSet<AnimalSiteRef> AnimalSiteRefs { get; set; }

    public virtual DbSet<AnimalSpeciesProductionType> AnimalSpeciesProductionTypes { get; set; }

    public virtual DbSet<AnimalSpecy> AnimalSpecies { get; set; }

    public virtual DbSet<AnimalState> AnimalStates { get; set; }

    public virtual DbSet<AnimalStatus> AnimalStatuses { get; set; }

    public virtual DbSet<AnimalUnregisteredParent> AnimalUnregisteredParents { get; set; }

    public virtual DbSet<CtsFileImport> CtsFileImports { get; set; }

    public virtual DbSet<CtsFileImportStatus> CtsFileImportStatuses { get; set; }

    public virtual DbSet<CtsFileImportsLog> CtsFileImportsLogs { get; set; }

    public virtual DbSet<CtsFileProcessingStatus> CtsFileProcessingStatuses { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<LocationActivity> LocationActivities { get; set; }

    public virtual DbSet<LocationAssociatedSite> LocationAssociatedSites { get; set; }

    public virtual DbSet<LocationAssociatedSiteType> LocationAssociatedSiteTypes { get; set; }

    public virtual DbSet<LocationCountry> LocationCountries { get; set; }

    public virtual DbSet<LocationPartyRef> LocationPartyRefs { get; set; }

    public virtual DbSet<LocationPostcode> LocationPostcodes { get; set; }

    public virtual DbSet<LocationSite> LocationSites { get; set; }

    public virtual DbSet<LocationSiteActivity> LocationSiteActivities { get; set; }

    public virtual DbSet<LocationSiteIdentifier> LocationSiteIdentifiers { get; set; }

    public virtual DbSet<LocationSiteIdentifierType> LocationSiteIdentifierTypes { get; set; }

    public virtual DbSet<LocationSiteParty> LocationSiteParties { get; set; }

    public virtual DbSet<LocationSiteRole> LocationSiteRoles { get; set; }

    public virtual DbSet<LocationSiteSource> LocationSiteSources { get; set; }

    public virtual DbSet<LocationSiteState> LocationSiteStates { get; set; }

    public virtual DbSet<LocationSiteType> LocationSiteTypes { get; set; }

    public virtual DbSet<LocationSiteTypeActivity> LocationSiteTypeActivities { get; set; }

    public virtual DbSet<MiEffectiveReportAllPermission> MiEffectiveReportAllPermissions { get; set; }

    public virtual DbSet<MiEffectiveReportPermission> MiEffectiveReportPermissions { get; set; }

    public virtual DbSet<MiPermission> MiPermissions { get; set; }

    public virtual DbSet<MiReport> MiReports { get; set; }

    public virtual DbSet<MiReportGroup> MiReportGroups { get; set; }

    public virtual DbSet<MiRole> MiRoles { get; set; }

    public virtual DbSet<MiRoleReportPermission> MiRoleReportPermissions { get; set; }

    public virtual DbSet<MiUser> MiUsers { get; set; }

    public virtual DbSet<MiUserReportPermission> MiUserReportPermissions { get; set; }

    public virtual DbSet<MiUserRole> MiUserRoles { get; set; }

    public virtual DbSet<Party> Parties { get; set; }

    public virtual DbSet<PartyHaulier> PartyHauliers { get; set; }

    public virtual DbSet<PartyLocation> PartyLocations { get; set; }

    public virtual DbSet<PartySpecy> PartySpecies { get; set; }

    public virtual DbSet<PartyState> PartyStates { get; set; }

    public virtual DbSet<PartyType> PartyTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Animal>(entity =>
        {
            entity.HasKey(e => e.Identifier).HasName("animal_pkey");

            entity.ToTable("animal", "cads");

            entity.HasIndex(e => e.RegistrationSiteIdentifier, "idx_animal_registration_site_identifier");

            entity.HasIndex(e => e.Species, "idx_animal_species");

            entity.Property(e => e.Identifier).HasColumnName("identifier");
            entity.Property(e => e.AnimalIdentifierIdentifier).HasColumnName("animal_identifier_identifier");
            entity.Property(e => e.BirthDamIdentifier).HasColumnName("birth_dam_identifier");
            entity.Property(e => e.BreedCode).HasColumnName("breed_code");
            entity.Property(e => e.BreedSpecies).HasColumnName("breed_species");
            entity.Property(e => e.GeneticDamIdentifier).HasColumnName("genetic_dam_identifier");
            entity.Property(e => e.Genotype).HasColumnName("genotype");
            entity.Property(e => e.GenotypeSpecies).HasColumnName("genotype_species");
            entity.Property(e => e.IdentificationDate).HasColumnName("identification_date");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.OriginalIdentifier).HasColumnName("original_identifier");
            entity.Property(e => e.ProductionType).HasColumnName("production_type");
            entity.Property(e => e.ReceivedDate).HasColumnName("received_date");
            entity.Property(e => e.RegistrationCategory).HasColumnName("registration_category");
            entity.Property(e => e.RegistrationDate).HasColumnName("registration_date");
            entity.Property(e => e.RegistrationSiteIdentifier).HasColumnName("registration_site_identifier");
            entity.Property(e => e.Sex).HasColumnName("sex");
            entity.Property(e => e.SireIdentifier).HasColumnName("sire_identifier");
            entity.Property(e => e.Species).HasColumnName("species");

            entity.HasOne(d => d.AnimalIdentifierIdentifierNavigation).WithMany(p => p.Animals)
                .HasForeignKey(d => d.AnimalIdentifierIdentifier)
                .HasConstraintName("fk_animal_identifier");

            entity.HasOne(d => d.BirthDamIdentifierNavigation).WithMany(p => p.InverseBirthDamIdentifierNavigation)
                .HasForeignKey(d => d.BirthDamIdentifier)
                .HasConstraintName("fk_animal_birth_dam");

            entity.HasOne(d => d.GeneticDamIdentifierNavigation).WithMany(p => p.InverseGeneticDamIdentifierNavigation)
                .HasForeignKey(d => d.GeneticDamIdentifier)
                .HasConstraintName("fk_animal_genetic_dam");

            entity.HasOne(d => d.RegistrationCategoryNavigation).WithMany(p => p.Animals)
                .HasForeignKey(d => d.RegistrationCategory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_animal_registration_category");

            entity.HasOne(d => d.RegistrationSiteIdentifierNavigation).WithMany(p => p.Animals)
                .HasForeignKey(d => d.RegistrationSiteIdentifier)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_animal_registration_site");

            entity.HasOne(d => d.SexNavigation).WithMany(p => p.Animals)
                .HasForeignKey(d => d.Sex)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_animal_sex");

            entity.HasOne(d => d.SireIdentifierNavigation).WithMany(p => p.InverseSireIdentifierNavigation)
                .HasForeignKey(d => d.SireIdentifier)
                .HasConstraintName("fk_animal_sire");

            entity.HasOne(d => d.SpeciesNavigation).WithMany(p => p.Animals)
                .HasForeignKey(d => d.Species)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_animal_species");

            entity.HasOne(d => d.AnimalBreed).WithMany(p => p.Animals)
                .HasForeignKey(d => new { d.BreedSpecies, d.BreedCode })
                .HasConstraintName("fk_animal_breed");

            entity.HasOne(d => d.AnimalGenotype).WithMany(p => p.Animals)
                .HasForeignKey(d => new { d.GenotypeSpecies, d.Genotype })
                .HasConstraintName("fk_animal_genotype");

            entity.HasOne(d => d.AnimalSpeciesProductionType).WithMany(p => p.Animals)
                .HasForeignKey(d => new { d.Species, d.ProductionType })
                .HasConstraintName("fk_animal_species_production_type");
        });

        modelBuilder.Entity<AnimalBirth>(entity =>
        {
            entity.HasKey(e => e.AnimalIdentifier).HasName("animal_birth_pkey");

            entity.ToTable("animal_birth", "cads");

            entity.Property(e => e.AnimalIdentifier).HasColumnName("animal_identifier");
            entity.Property(e => e.AssistedBirthFlag).HasColumnName("assisted_birth_flag");
            entity.Property(e => e.BirthDate).HasColumnName("birth_date");
            entity.Property(e => e.BirthMark).HasColumnName("birth_mark");
            entity.Property(e => e.BirthMarkCollectiveSiteIdentifier).HasColumnName("birth_mark_collective_site_identifier");
            entity.Property(e => e.BirthMarkSpecies).HasColumnName("birth_mark_species");
            entity.Property(e => e.BirthSiteIdentifier).HasColumnName("birth_site_identifier");
            entity.Property(e => e.BirthYear).HasColumnName("birth_year");
            entity.Property(e => e.EmbryoTransferFlag).HasColumnName("embryo_transfer_flag");
            entity.Property(e => e.MultipleBirthsFlag).HasColumnName("multiple_births_flag");

            entity.HasOne(d => d.AnimalIdentifierNavigation).WithOne(p => p.AnimalBirth)
                .HasForeignKey<AnimalBirth>(d => d.AnimalIdentifier)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_animal_birth_animal");

            entity.HasOne(d => d.BirthSiteIdentifierNavigation).WithMany(p => p.AnimalBirths)
                .HasForeignKey(d => d.BirthSiteIdentifier)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_animal_birth_site");

            entity.HasOne(d => d.AnimalMark).WithMany(p => p.AnimalBirths)
                .HasForeignKey(d => new { d.BirthMarkSpecies, d.BirthMarkCollectiveSiteIdentifier, d.BirthMark })
                .HasConstraintName("fk_animal_birth_mark");
        });

        modelBuilder.Entity<AnimalBreed>(entity =>
        {
            entity.HasKey(e => new { e.Species, e.BreedCode }).HasName("animal_breed_pkey");

            entity.ToTable("animal_breed", "cads");

            entity.HasIndex(e => e.Species, "idx_animal_breed_species");

            entity.Property(e => e.Species).HasColumnName("species");
            entity.Property(e => e.BreedCode).HasColumnName("breed_code");
            entity.Property(e => e.Breed).HasColumnName("breed");
            entity.Property(e => e.CrossBreedFlag).HasColumnName("cross_breed_flag");
            entity.Property(e => e.State).HasColumnName("state");

            entity.HasOne(d => d.SpeciesNavigation).WithMany(p => p.AnimalBreeds)
                .HasForeignKey(d => d.Species)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_animal_breed_species");

            entity.HasOne(d => d.StateNavigation).WithMany(p => p.AnimalBreeds)
                .HasForeignKey(d => d.State)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_animal_breed_state");
        });

        modelBuilder.Entity<AnimalBreedState>(entity =>
        {
            entity.HasKey(e => e.State).HasName("animal_breed_state_pkey");

            entity.ToTable("animal_breed_state", "cads");

            entity.Property(e => e.State).HasColumnName("state");
        });

        modelBuilder.Entity<AnimalCollective>(entity =>
        {
            entity.HasKey(e => new { e.AnimalIdentifier, e.Species, e.HomeCollectiveSiteIdentifier, e.StartDate }).HasName("animal_collective_pkey");

            entity.ToTable("animal_collective", "cads");

            entity.HasIndex(e => new { e.Species, e.CurrentCollectiveSiteIdentifier }, "idx_animal_collective_current");

            entity.Property(e => e.AnimalIdentifier).HasColumnName("animal_identifier");
            entity.Property(e => e.Species).HasColumnName("species");
            entity.Property(e => e.HomeCollectiveSiteIdentifier).HasColumnName("home_collective_site_identifier");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.CurrentCollectiveSiteIdentifier).HasColumnName("current_collective_site_identifier");
            entity.Property(e => e.EndDate).HasColumnName("end_date");

            entity.HasOne(d => d.AnimalIdentifierNavigation).WithMany(p => p.AnimalCollectives)
                .HasForeignKey(d => d.AnimalIdentifier)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_animal_collective_animal");

            entity.HasOne(d => d.AnimalCollectiveRef).WithMany(p => p.AnimalCollectiveAnimalCollectiveRefs)
                .HasForeignKey(d => new { d.Species, d.CurrentCollectiveSiteIdentifier })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_animal_collective_current");

            entity.HasOne(d => d.AnimalCollectiveRefNavigation).WithMany(p => p.AnimalCollectiveAnimalCollectiveRefNavigations)
                .HasForeignKey(d => new { d.Species, d.HomeCollectiveSiteIdentifier })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_animal_collective_home");
        });

        modelBuilder.Entity<AnimalCollectiveDeath>(entity =>
        {
            entity.HasKey(e => e.Identifier).HasName("animal_collective_death_pkey");

            entity.ToTable("animal_collective_death", "cads");

            entity.HasIndex(e => new { e.Species, e.SiteIdentifier }, "idx_animal_collective_death_collective_ref");

            entity.Property(e => e.Identifier)
                .ValueGeneratedNever()
                .HasColumnName("identifier");
            entity.Property(e => e.CarcassCollectionSiteIdentifier).HasColumnName("carcass_collection_site_identifier");
            entity.Property(e => e.DeathDate).HasColumnName("death_date");
            entity.Property(e => e.DeathReason).HasColumnName("death_reason");
            entity.Property(e => e.DeathReasonSpecies).HasColumnName("death_reason_species");
            entity.Property(e => e.Mark).HasColumnName("mark");
            entity.Property(e => e.MarkCollectiveSiteIdentifier).HasColumnName("mark_collective_site_identifier");
            entity.Property(e => e.MarkSpecies).HasColumnName("mark_species");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.SiteIdentifier).HasColumnName("site_identifier");
            entity.Property(e => e.Species).HasColumnName("species");

            entity.HasOne(d => d.CarcassCollectionSiteIdentifierNavigation).WithMany(p => p.AnimalCollectiveDeaths)
                .HasForeignKey(d => d.CarcassCollectionSiteIdentifier)
                .HasConstraintName("fk_animal_collective_death_carcass_site");

            entity.HasOne(d => d.AnimalDeathReason).WithMany(p => p.AnimalCollectiveDeaths)
                .HasForeignKey(d => new { d.DeathReasonSpecies, d.DeathReason })
                .HasConstraintName("fk_animal_collective_death_reason");

            entity.HasOne(d => d.AnimalCollectiveRef).WithMany(p => p.AnimalCollectiveDeaths)
                .HasForeignKey(d => new { d.Species, d.SiteIdentifier })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_animal_collective_death_collective_ref");

            entity.HasOne(d => d.AnimalMark).WithMany(p => p.AnimalCollectiveDeaths)
                .HasForeignKey(d => new { d.MarkSpecies, d.MarkCollectiveSiteIdentifier, d.Mark })
                .HasConstraintName("fk_animal_collective_death_mark");
        });

        modelBuilder.Entity<AnimalCollectiveParty>(entity =>
        {
            entity.HasKey(e => new { e.Species, e.SiteIdentifier, e.PartyIdentifier, e.CollectiveRole, e.StartDate }).HasName("animal_collective_party_pkey");

            entity.ToTable("animal_collective_party", "cads");

            entity.HasIndex(e => e.PartyIdentifier, "idx_animal_collective_party_party_identifier");

            entity.Property(e => e.Species).HasColumnName("species");
            entity.Property(e => e.SiteIdentifier).HasColumnName("site_identifier");
            entity.Property(e => e.PartyIdentifier).HasColumnName("party_identifier");
            entity.Property(e => e.CollectiveRole).HasColumnName("collective_role");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.EndDate).HasColumnName("end_date");

            entity.HasOne(d => d.CollectiveRoleNavigation).WithMany(p => p.AnimalCollectiveParties)
                .HasForeignKey(d => d.CollectiveRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_animal_collective_party_role");

            entity.HasOne(d => d.PartyIdentifierNavigation).WithMany(p => p.AnimalCollectiveParties)
                .HasForeignKey(d => d.PartyIdentifier)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_animal_collective_party_party");

            entity.HasOne(d => d.AnimalCollectiveRef).WithMany(p => p.AnimalCollectiveParties)
                .HasForeignKey(d => new { d.Species, d.SiteIdentifier })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_animal_collective_party_collective_ref");
        });

        modelBuilder.Entity<AnimalCollectiveRef>(entity =>
        {
            entity.HasKey(e => new { e.Species, e.SiteIdentifier }).HasName("animal_collective_ref_pkey");

            entity.ToTable("animal_collective_ref", "cads");

            entity.HasIndex(e => e.SiteIdentifier, "idx_animal_collective_ref_site_identifier");

            entity.Property(e => e.Species).HasColumnName("species");
            entity.Property(e => e.SiteIdentifier).HasColumnName("site_identifier");
            entity.Property(e => e.State).HasColumnName("state");

            entity.HasOne(d => d.SiteIdentifierNavigation).WithMany(p => p.AnimalCollectiveRefs)
                .HasForeignKey(d => d.SiteIdentifier)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_animal_collective_ref_site");

            entity.HasOne(d => d.SpeciesNavigation).WithMany(p => p.AnimalCollectiveRefs)
                .HasForeignKey(d => d.Species)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_animal_collective_ref_species");

            entity.HasOne(d => d.StateNavigation).WithMany(p => p.AnimalCollectiveRefs)
                .HasForeignKey(d => d.State)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_animal_collective_ref_state");
        });

        modelBuilder.Entity<AnimalCollectiveRegistration>(entity =>
        {
            entity.HasKey(e => e.Identifier).HasName("animal_collective_registration_pkey");

            entity.ToTable("animal_collective_registration", "cads");

            entity.HasIndex(e => new { e.Species, e.SiteIdentifier }, "idx_animal_collective_registration_collective_ref");

            entity.Property(e => e.Identifier)
                .ValueGeneratedNever()
                .HasColumnName("identifier");
            entity.Property(e => e.BirthYear).HasColumnName("birth_year");
            entity.Property(e => e.BreedCode).HasColumnName("breed_code");
            entity.Property(e => e.BreedSpecies).HasColumnName("breed_species");
            entity.Property(e => e.Genotype).HasColumnName("genotype");
            entity.Property(e => e.GenotypeSpecies).HasColumnName("genotype_species");
            entity.Property(e => e.IdentificationDate).HasColumnName("identification_date");
            entity.Property(e => e.Mark).HasColumnName("mark");
            entity.Property(e => e.MarkCollectiveSiteIdentifier).HasColumnName("mark_collective_site_identifier");
            entity.Property(e => e.MarkSpecies).HasColumnName("mark_species");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.RegistrationDate).HasColumnName("registration_date");
            entity.Property(e => e.SiteIdentifier).HasColumnName("site_identifier");
            entity.Property(e => e.Species).HasColumnName("species");

            entity.HasOne(d => d.AnimalBreed).WithMany(p => p.AnimalCollectiveRegistrations)
                .HasForeignKey(d => new { d.BreedSpecies, d.BreedCode })
                .HasConstraintName("fk_animal_collective_registration_breed");

            entity.HasOne(d => d.AnimalGenotype).WithMany(p => p.AnimalCollectiveRegistrations)
                .HasForeignKey(d => new { d.GenotypeSpecies, d.Genotype })
                .HasConstraintName("fk_animal_collective_registration_genotype");

            entity.HasOne(d => d.AnimalCollectiveRef).WithMany(p => p.AnimalCollectiveRegistrations)
                .HasForeignKey(d => new { d.Species, d.SiteIdentifier })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_animal_collective_registration_collective_ref");

            entity.HasOne(d => d.AnimalMark).WithMany(p => p.AnimalCollectiveRegistrations)
                .HasForeignKey(d => new { d.MarkSpecies, d.MarkCollectiveSiteIdentifier, d.Mark })
                .HasConstraintName("fk_animal_collective_registration_mark");
        });

        modelBuilder.Entity<AnimalCollectiveRole>(entity =>
        {
            entity.HasKey(e => e.Role).HasName("animal_collective_role_pkey");

            entity.ToTable("animal_collective_role", "cads");

            entity.Property(e => e.Role).HasColumnName("role");
        });

        modelBuilder.Entity<AnimalCollectiveState>(entity =>
        {
            entity.HasKey(e => e.State).HasName("animal_collective_state_pkey");

            entity.ToTable("animal_collective_state", "cads");

            entity.Property(e => e.State).HasColumnName("state");
        });

        modelBuilder.Entity<AnimalDeath>(entity =>
        {
            entity.HasKey(e => e.AnimalIdentifier).HasName("animal_death_pkey");

            entity.ToTable("animal_death", "cads");

            entity.HasIndex(e => e.DeathSiteIdentifier, "idx_animal_death_site");

            entity.Property(e => e.AnimalIdentifier).HasColumnName("animal_identifier");
            entity.Property(e => e.CarcassCollectionSiteIdentifier).HasColumnName("carcass_collection_site_identifier");
            entity.Property(e => e.DeathDate).HasColumnName("death_date");
            entity.Property(e => e.DeathReason).HasColumnName("death_reason");
            entity.Property(e => e.DeathReasonSpecies).HasColumnName("death_reason_species");
            entity.Property(e => e.DeathReceivedDate).HasColumnName("death_received_date");
            entity.Property(e => e.DeathReportedDate).HasColumnName("death_reported_date");
            entity.Property(e => e.DeathSiteIdentifier).HasColumnName("death_site_identifier");
            entity.Property(e => e.TseTestRequiredFlag).HasColumnName("tse_test_required_flag");

            entity.HasOne(d => d.AnimalIdentifierNavigation).WithOne(p => p.AnimalDeath)
                .HasForeignKey<AnimalDeath>(d => d.AnimalIdentifier)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_animal_death_animal");

            entity.HasOne(d => d.CarcassCollectionSiteIdentifierNavigation).WithMany(p => p.AnimalDeathCarcassCollectionSiteIdentifierNavigations)
                .HasForeignKey(d => d.CarcassCollectionSiteIdentifier)
                .HasConstraintName("fk_animal_death_carcass_site");

            entity.HasOne(d => d.DeathSiteIdentifierNavigation).WithMany(p => p.AnimalDeathDeathSiteIdentifierNavigations)
                .HasForeignKey(d => d.DeathSiteIdentifier)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_animal_death_site");

            entity.HasOne(d => d.AnimalDeathReason).WithMany(p => p.AnimalDeaths)
                .HasForeignKey(d => new { d.DeathReasonSpecies, d.DeathReason })
                .HasConstraintName("fk_animal_death_reason");
        });

        modelBuilder.Entity<AnimalDeathReason>(entity =>
        {
            entity.HasKey(e => new { e.Species, e.Reason }).HasName("animal_death_reason_pkey");

            entity.ToTable("animal_death_reason", "cads");

            entity.HasIndex(e => e.Species, "idx_animal_death_reason_species");

            entity.Property(e => e.Species).HasColumnName("species");
            entity.Property(e => e.Reason).HasColumnName("reason");

            entity.HasOne(d => d.SpeciesNavigation).WithMany(p => p.AnimalDeathReasons)
                .HasForeignKey(d => d.Species)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_animal_death_reason_species");
        });

        modelBuilder.Entity<AnimalGenotype>(entity =>
        {
            entity.HasKey(e => new { e.Species, e.Genotype }).HasName("animal_genotype_pkey");

            entity.ToTable("animal_genotype", "cads");

            entity.HasIndex(e => e.Species, "idx_animal_genotype_species");

            entity.Property(e => e.Species).HasColumnName("species");
            entity.Property(e => e.Genotype).HasColumnName("genotype");

            entity.HasOne(d => d.SpeciesNavigation).WithMany(p => p.AnimalGenotypes)
                .HasForeignKey(d => d.Species)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_animal_genotype_species");
        });

        modelBuilder.Entity<AnimalIdentifier>(entity =>
        {
            entity.HasKey(e => e.Identifier).HasName("animal_identifier_pkey");

            entity.ToTable("animal_identifier", "cads");

            entity.HasIndex(e => e.AnimalIdentifier1, "animal_identifier_animal_identifier_key").IsUnique();

            entity.Property(e => e.Identifier).HasColumnName("identifier");
            entity.Property(e => e.AnimalIdentifier1).HasColumnName("animal_identifier");
        });

        modelBuilder.Entity<AnimalLostOrStolenState>(entity =>
        {
            entity.HasKey(e => e.State).HasName("animal_lost_or_stolen_state_pkey");

            entity.ToTable("animal_lost_or_stolen_state", "cads");

            entity.Property(e => e.State).HasColumnName("state");
        });

        modelBuilder.Entity<AnimalLostOrStolenStatus>(entity =>
        {
            entity.HasKey(e => new { e.AnimalIdentifier, e.EventDate }).HasName("animal_lost_or_stolen_status_pkey");

            entity.ToTable("animal_lost_or_stolen_status", "cads");

            entity.HasIndex(e => e.HomeSiteIdentifier, "idx_animal_lost_or_stolen_status_home_site_identifier");

            entity.Property(e => e.AnimalIdentifier).HasColumnName("animal_identifier");
            entity.Property(e => e.EventDate).HasColumnName("event_date");
            entity.Property(e => e.CrimeReferenceNumber).HasColumnName("crime_reference_number");
            entity.Property(e => e.FoundDeadFlag).HasColumnName("found_dead_flag");
            entity.Property(e => e.HomeSiteIdentifier).HasColumnName("home_site_identifier");
            entity.Property(e => e.ReceivedDate).HasColumnName("received_date");
            entity.Property(e => e.State).HasColumnName("state");

            entity.HasOne(d => d.AnimalIdentifierNavigation).WithMany(p => p.AnimalLostOrStolenStatuses)
                .HasForeignKey(d => d.AnimalIdentifier)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_animal_lost_or_stolen_status_animal");

            entity.HasOne(d => d.HomeSiteIdentifierNavigation).WithMany(p => p.AnimalLostOrStolenStatuses)
                .HasForeignKey(d => d.HomeSiteIdentifier)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_animal_lost_or_stolen_status_home_site");

            entity.HasOne(d => d.StateNavigation).WithMany(p => p.AnimalLostOrStolenStatuses)
                .HasForeignKey(d => d.State)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_animal_lost_or_stolen_status_state");
        });

        modelBuilder.Entity<AnimalMark>(entity =>
        {
            entity.HasKey(e => new { e.Species, e.CollectiveSiteIdentifier, e.Mark }).HasName("animal_mark_pkey");

            entity.ToTable("animal_mark", "cads");

            entity.HasIndex(e => new { e.Species, e.CollectiveSiteIdentifier }, "idx_animal_mark_collective");

            entity.Property(e => e.Species).HasColumnName("species");
            entity.Property(e => e.CollectiveSiteIdentifier).HasColumnName("collective_site_identifier");
            entity.Property(e => e.Mark).HasColumnName("mark");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.StartDate).HasColumnName("start_date");

            entity.HasOne(d => d.AnimalCollectiveRef).WithMany(p => p.AnimalMarks)
                .HasForeignKey(d => new { d.Species, d.CollectiveSiteIdentifier })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_animal_mark_collective_ref");
        });

        modelBuilder.Entity<AnimalNoticeToIdentify>(entity =>
        {
            entity.HasKey(e => e.NoticeReference).HasName("animal_notice_to_identify_pkey");

            entity.ToTable("animal_notice_to_identify", "cads");

            entity.HasIndex(e => e.SiteIdentifier, "idx_animal_notice_to_identify_site_identifier");

            entity.Property(e => e.NoticeReference).HasColumnName("notice_reference");
            entity.Property(e => e.AdditionalDetails).HasColumnName("additional_details");
            entity.Property(e => e.AnimalIdentifier).HasColumnName("animal_identifier");
            entity.Property(e => e.BreedCode).HasColumnName("breed_code");
            entity.Property(e => e.BreedSpecies).HasColumnName("breed_species");
            entity.Property(e => e.DnaProvenFlag).HasColumnName("dna_proven_flag");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.InspectionReference).HasColumnName("inspection_reference");
            entity.Property(e => e.InspectionYear).HasColumnName("inspection_year");
            entity.Property(e => e.IssueDate).HasColumnName("issue_date");
            entity.Property(e => e.OriginalAnimalIdentifier).HasColumnName("original_animal_identifier");
            entity.Property(e => e.OriginalAnimalIdentifierType).HasColumnName("original_animal_identifier_type");
            entity.Property(e => e.Resolution).HasColumnName("resolution");
            entity.Property(e => e.Sex).HasColumnName("sex");
            entity.Property(e => e.SiteIdentifier).HasColumnName("site_identifier");
            entity.Property(e => e.Species).HasColumnName("species");

            entity.HasOne(d => d.AnimalIdentifierNavigation).WithMany(p => p.AnimalNoticeToIdentifyAnimalIdentifierNavigations)
                .HasForeignKey(d => d.AnimalIdentifier)
                .HasConstraintName("fk_animal_notice_to_identify_animal");

            entity.HasOne(d => d.OriginalAnimalIdentifierNavigation).WithMany(p => p.AnimalNoticeToIdentifyOriginalAnimalIdentifierNavigations)
                .HasForeignKey(d => d.OriginalAnimalIdentifier)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_animal_notice_to_identify_original_animal");

            entity.HasOne(d => d.OriginalAnimalIdentifierTypeNavigation).WithMany(p => p.AnimalNoticeToIdentifies)
                .HasForeignKey(d => d.OriginalAnimalIdentifierType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_animal_notice_to_identify_original_type");

            entity.HasOne(d => d.ResolutionNavigation).WithMany(p => p.AnimalNoticeToIdentifies)
                .HasForeignKey(d => d.Resolution)
                .HasConstraintName("fk_animal_notice_to_identify_resolution");

            entity.HasOne(d => d.SexNavigation).WithMany(p => p.AnimalNoticeToIdentifies)
                .HasForeignKey(d => d.Sex)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_animal_notice_to_identify_sex");

            entity.HasOne(d => d.SiteIdentifierNavigation).WithMany(p => p.AnimalNoticeToIdentifies)
                .HasForeignKey(d => d.SiteIdentifier)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_animal_notice_to_identify_site");

            entity.HasOne(d => d.SpeciesNavigation).WithMany(p => p.AnimalNoticeToIdentifies)
                .HasForeignKey(d => d.Species)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_animal_notice_to_identify_species");

            entity.HasOne(d => d.AnimalBreed).WithMany(p => p.AnimalNoticeToIdentifies)
                .HasForeignKey(d => new { d.BreedSpecies, d.BreedCode })
                .HasConstraintName("fk_animal_notice_to_identify_breed");
        });

        modelBuilder.Entity<AnimalOriginalIdentifierType>(entity =>
        {
            entity.HasKey(e => e.Type).HasName("animal_original_identifier_type_pkey");

            entity.ToTable("animal_original_identifier_type", "cads");

            entity.Property(e => e.Type).HasColumnName("type");
        });

        modelBuilder.Entity<AnimalParty>(entity =>
        {
            entity.HasKey(e => new { e.AnimalIdentifier, e.PartyIdentifier, e.AnimalRole, e.StartDate }).HasName("animal_party_pkey");

            entity.ToTable("animal_party", "cads");

            entity.HasIndex(e => e.PartyIdentifier, "idx_animal_party_party_identifier");

            entity.Property(e => e.AnimalIdentifier).HasColumnName("animal_identifier");
            entity.Property(e => e.PartyIdentifier).HasColumnName("party_identifier");
            entity.Property(e => e.AnimalRole).HasColumnName("animal_role");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.EndDate).HasColumnName("end_date");

            entity.HasOne(d => d.AnimalIdentifierNavigation).WithMany(p => p.AnimalParties)
                .HasForeignKey(d => d.AnimalIdentifier)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_animal_party_animal");

            entity.HasOne(d => d.AnimalRoleNavigation).WithMany(p => p.AnimalParties)
                .HasForeignKey(d => d.AnimalRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_animal_party_role");

            entity.HasOne(d => d.PartyIdentifierNavigation).WithMany(p => p.AnimalParties)
                .HasForeignKey(d => d.PartyIdentifier)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_animal_party_party");
        });

        modelBuilder.Entity<AnimalPartyRef>(entity =>
        {
            entity.HasKey(e => e.Identifier).HasName("animal_party_ref_pkey");

            entity.ToTable("animal_party_ref", "cads");

            entity.Property(e => e.Identifier)
                .ValueGeneratedNever()
                .HasColumnName("identifier");
        });

        modelBuilder.Entity<AnimalProductionType>(entity =>
        {
            entity.HasKey(e => e.Type).HasName("animal_production_type_pkey");

            entity.ToTable("animal_production_type", "cads");

            entity.Property(e => e.Type).HasColumnName("type");
        });

        modelBuilder.Entity<AnimalRegistrationCategory>(entity =>
        {
            entity.HasKey(e => e.Category).HasName("animal_registration_category_pkey");

            entity.ToTable("animal_registration_category", "cads");

            entity.Property(e => e.Category).HasColumnName("category");
        });

        modelBuilder.Entity<AnimalResolutionType>(entity =>
        {
            entity.HasKey(e => e.Resolution).HasName("animal_resolution_type_pkey");

            entity.ToTable("animal_resolution_type", "cads");

            entity.Property(e => e.Resolution).HasColumnName("resolution");
        });

        modelBuilder.Entity<AnimalRole>(entity =>
        {
            entity.HasKey(e => e.Role).HasName("animal_role_pkey");

            entity.ToTable("animal_role", "cads");

            entity.Property(e => e.Role).HasColumnName("role");
        });

        modelBuilder.Entity<AnimalSex>(entity =>
        {
            entity.HasKey(e => e.Sex).HasName("animal_sex_pkey");

            entity.ToTable("animal_sex", "cads");

            entity.Property(e => e.Sex).HasColumnName("sex");
        });

        modelBuilder.Entity<AnimalSiteRef>(entity =>
        {
            entity.HasKey(e => e.Identifier).HasName("animal_site_ref_pkey");

            entity.ToTable("animal_site_ref", "cads");

            entity.Property(e => e.Identifier).HasColumnName("identifier");
        });

        modelBuilder.Entity<AnimalSpeciesProductionType>(entity =>
        {
            entity.HasKey(e => new { e.Species, e.ProductionType }).HasName("animal_species_production_type_pkey");

            entity.ToTable("animal_species_production_type", "cads");

            entity.HasIndex(e => e.Species, "idx_animal_species_production_type_species");

            entity.Property(e => e.Species).HasColumnName("species");
            entity.Property(e => e.ProductionType).HasColumnName("production_type");

            entity.HasOne(d => d.ProductionTypeNavigation).WithMany(p => p.AnimalSpeciesProductionTypes)
                .HasForeignKey(d => d.ProductionType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_animal_species_production_type_production_type");

            entity.HasOne(d => d.SpeciesNavigation).WithMany(p => p.AnimalSpeciesProductionTypes)
                .HasForeignKey(d => d.Species)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_animal_species_production_type_species");
        });

        modelBuilder.Entity<AnimalSpecy>(entity =>
        {
            entity.HasKey(e => e.Species).HasName("animal_species_pkey");

            entity.ToTable("animal_species", "cads");

            entity.Property(e => e.Species).HasColumnName("species");
        });

        modelBuilder.Entity<AnimalState>(entity =>
        {
            entity.HasKey(e => e.State).HasName("animal_state_pkey");

            entity.ToTable("animal_state", "cads");

            entity.Property(e => e.State).HasColumnName("state");
        });

        modelBuilder.Entity<AnimalStatus>(entity =>
        {
            entity.HasKey(e => new { e.AnimalIdentifier, e.AnimalState, e.StartDate }).HasName("animal_status_pkey");

            entity.ToTable("animal_status", "cads");

            entity.HasIndex(e => e.AnimalIdentifier, "idx_animal_status_animal_identifier");

            entity.Property(e => e.AnimalIdentifier).HasColumnName("animal_identifier");
            entity.Property(e => e.AnimalState).HasColumnName("animal_state");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.EndDate).HasColumnName("end_date");

            entity.HasOne(d => d.AnimalIdentifierNavigation).WithMany(p => p.AnimalStatuses)
                .HasForeignKey(d => d.AnimalIdentifier)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_animal_status_animal");

            entity.HasOne(d => d.AnimalStateNavigation).WithMany(p => p.AnimalStatuses)
                .HasForeignKey(d => d.AnimalState)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_animal_status_state");
        });

        modelBuilder.Entity<AnimalUnregisteredParent>(entity =>
        {
            entity.HasKey(e => e.AnimalIdentifier).HasName("animal_unregistered_parents_pkey");

            entity.ToTable("animal_unregistered_parents", "cads");

            entity.Property(e => e.AnimalIdentifier).HasColumnName("animal_identifier");
            entity.Property(e => e.BirthDamAnimalIdentifier).HasColumnName("birth_dam_animal_identifier");
            entity.Property(e => e.GeneticDamAnimalIdentifier).HasColumnName("genetic_dam_animal_identifier");
            entity.Property(e => e.SireAnimalIdentifier).HasColumnName("sire_animal_identifier");

            entity.HasOne(d => d.AnimalIdentifierNavigation).WithOne(p => p.AnimalUnregisteredParent)
                .HasForeignKey<AnimalUnregisteredParent>(d => d.AnimalIdentifier)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_animal_unregistered_parents_animal");
        });

        modelBuilder.Entity<CtsFileImport>(entity =>
        {
            entity.HasKey(e => e.CtsFileImportId).HasName("cts_file_imports_pkey");

            entity.ToTable("cts_file_imports", "cads");

            entity.HasIndex(e => e.DestinationTableName, "cts_file_imports_destination_table_name_idx");

            entity.HasIndex(e => e.FileName, "cts_file_imports_file_name_idx").IsUnique();

            entity.HasIndex(e => e.GroupKey, "cts_file_imports_group_key_idx");

            entity.HasIndex(e => e.ImportStatusId, "cts_file_imports_import_status_id_idx");

            entity.HasIndex(e => e.ProcessingStatusId, "cts_file_imports_processing_status_id_idx");

            entity.Property(e => e.CtsFileImportId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("cts_file_import_id");
            entity.Property(e => e.AddedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("added_at");
            entity.Property(e => e.BatchDate).HasColumnName("batch_date");
            entity.Property(e => e.DestinationTableName).HasColumnName("destination_table_name");
            entity.Property(e => e.FailedAttempts)
                .HasDefaultValue((short)0)
                .HasColumnName("failed_attempts");
            entity.Property(e => e.FileName).HasColumnName("file_name");
            entity.Property(e => e.GroupKey).HasColumnName("group_key");
            entity.Property(e => e.ImportEndAt).HasColumnName("import_end_at");
            entity.Property(e => e.ImportStartAt).HasColumnName("import_start_at");
            entity.Property(e => e.ImportStatusId)
                .HasDefaultValue((short)1)
                .HasColumnName("import_status_id");
            entity.Property(e => e.ImportType).HasColumnName("import_type");
            entity.Property(e => e.LastErrorReason).HasColumnName("last_error_reason");
            entity.Property(e => e.LastFilePartImported).HasColumnName("last_file_part_imported");
            entity.Property(e => e.ProcessingEndAt).HasColumnName("processing_end_at");
            entity.Property(e => e.ProcessingStartAt).HasColumnName("processing_start_at");
            entity.Property(e => e.ProcessingStatusId)
                .HasDefaultValue((short)1)
                .HasColumnName("processing_status_id");
            entity.Property(e => e.RowsFound).HasColumnName("rows_found");
            entity.Property(e => e.RowsImported).HasColumnName("rows_imported");
            entity.Property(e => e.TotalRowsToProcess).HasColumnName("total_rows_to_process");

            entity.HasOne(d => d.ImportStatus).WithMany(p => p.CtsFileImports)
                .HasForeignKey(d => d.ImportStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("cts_file_imports_import_status_id_fkey");

            entity.HasOne(d => d.ProcessingStatus).WithMany(p => p.CtsFileImports)
                .HasForeignKey(d => d.ProcessingStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("cts_file_imports_processing_status_id_fkey");
        });

        modelBuilder.Entity<CtsFileImportStatus>(entity =>
        {
            entity.HasKey(e => e.ImportStatusId).HasName("cts_file_import_statuses_pkey");

            entity.ToTable("cts_file_import_statuses", "cads");

            entity.HasIndex(e => e.StatusDescription, "cts_file_import_statuses_status_description_key").IsUnique();

            entity.Property(e => e.ImportStatusId)
                .ValueGeneratedNever()
                .HasColumnName("import_status_id");
            entity.Property(e => e.StatusDescription).HasColumnName("status_description");
        });

        modelBuilder.Entity<CtsFileImportsLog>(entity =>
        {
            entity.HasKey(e => e.CtsFileImportLogId).HasName("cts_file_imports_log_pkey");

            entity.ToTable("cts_file_imports_log", "cads");

            entity.HasIndex(e => e.CtsFileImportId, "cts_file_imports_log_file_import_id_idx");

            entity.HasIndex(e => e.LoggedAt, "cts_file_imports_log_logged_at_idx");

            entity.Property(e => e.CtsFileImportLogId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("cts_file_import_log_id");
            entity.Property(e => e.CtsFileImportId).HasColumnName("cts_file_import_id");
            entity.Property(e => e.DeleteEndedAt).HasColumnName("delete_ended_at");
            entity.Property(e => e.DeleteStartedAt).HasColumnName("delete_started_at");
            entity.Property(e => e.DeletedRecords).HasColumnName("deleted_records");
            entity.Property(e => e.ErrorMessage).HasColumnName("error_message");
            entity.Property(e => e.ExpectedRecords).HasColumnName("expected_records");
            entity.Property(e => e.InsertEndedAt).HasColumnName("insert_ended_at");
            entity.Property(e => e.InsertStartedAt).HasColumnName("insert_started_at");
            entity.Property(e => e.InsertedRecords).HasColumnName("inserted_records");
            entity.Property(e => e.LogLevel)
                .HasDefaultValueSql("'info'::text")
                .HasColumnName("log_level");
            entity.Property(e => e.LogMessage).HasColumnName("log_message");
            entity.Property(e => e.LoggedAt)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnName("logged_at");
            entity.Property(e => e.ProcessedRecords).HasColumnName("processed_records");
            entity.Property(e => e.ProcessingEndedAt).HasColumnName("processing_ended_at");
            entity.Property(e => e.ProcessingStartedAt).HasColumnName("processing_started_at");
            entity.Property(e => e.UpdateEndedAt).HasColumnName("update_ended_at");
            entity.Property(e => e.UpdateStartedAt).HasColumnName("update_started_at");
            entity.Property(e => e.UpdatedRecords).HasColumnName("updated_records");

            entity.HasOne(d => d.CtsFileImport).WithMany(p => p.CtsFileImportsLogs)
                .HasForeignKey(d => d.CtsFileImportId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("cts_file_imports_log_file_import_fkey");
        });

        modelBuilder.Entity<CtsFileProcessingStatus>(entity =>
        {
            entity.HasKey(e => e.ProcessingStatusId).HasName("cts_file_processing_statuses_pkey");

            entity.ToTable("cts_file_processing_statuses", "cads");

            entity.HasIndex(e => e.StatusDescription, "cts_file_processing_statuses_status_description_key").IsUnique();

            entity.Property(e => e.ProcessingStatusId)
                .ValueGeneratedNever()
                .HasColumnName("processing_status_id");
            entity.Property(e => e.StatusDescription).HasColumnName("status_description");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.Identifier).HasName("location_pkey");

            entity.ToTable("location", "cads");

            entity.HasIndex(e => e.CountryCode, "idx_location_country_code");

            entity.HasIndex(e => e.Postcode, "idx_location_postcode");

            entity.Property(e => e.Identifier)
                .HasMaxLength(50)
                .HasColumnName("identifier");
            entity.Property(e => e.CountryCode)
                .HasMaxLength(10)
                .HasColumnName("country_code");
            entity.Property(e => e.Easting).HasColumnName("easting");
            entity.Property(e => e.Northing).HasColumnName("northing");
            entity.Property(e => e.OsMapReference)
                .HasMaxLength(30)
                .HasColumnName("os_map_reference");
            entity.Property(e => e.Postcode)
                .HasMaxLength(10)
                .HasColumnName("postcode");
            entity.Property(e => e.SingleLineAddress)
                .HasMaxLength(500)
                .HasColumnName("single_line_address");
            entity.Property(e => e.Uprn).HasColumnName("uprn");

            entity.HasOne(d => d.CountryCodeNavigation).WithMany(p => p.Locations)
                .HasForeignKey(d => d.CountryCode)
                .HasConstraintName("fk_location_country");

            entity.HasOne(d => d.PostcodeNavigation).WithMany(p => p.Locations)
                .HasForeignKey(d => d.Postcode)
                .HasConstraintName("fk_location_postcode");
        });

        modelBuilder.Entity<LocationActivity>(entity =>
        {
            entity.HasKey(e => e.Type).HasName("location_activity_pkey");

            entity.ToTable("location_activity", "cads");

            entity.Property(e => e.Type)
                .HasMaxLength(10)
                .HasColumnName("type");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .HasColumnName("description");
        });

        modelBuilder.Entity<LocationAssociatedSite>(entity =>
        {
            entity.HasKey(e => new { e.SiteIdentifier, e.AssociatedSiteIdentifier, e.AssociatedSiteType, e.StartDate }).HasName("location_associated_site_pkey");

            entity.ToTable("location_associated_site", "cads");

            entity.HasIndex(e => e.AssociatedSiteIdentifier, "idx_location_associated_site_related_site");

            entity.Property(e => e.SiteIdentifier)
                .HasMaxLength(50)
                .HasColumnName("site_identifier");
            entity.Property(e => e.AssociatedSiteIdentifier)
                .HasMaxLength(50)
                .HasColumnName("associated_site_identifier");
            entity.Property(e => e.AssociatedSiteType)
                .HasMaxLength(50)
                .HasColumnName("associated_site_type");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.EndDate).HasColumnName("end_date");

            entity.HasOne(d => d.AssociatedSiteIdentifierNavigation).WithMany(p => p.LocationAssociatedSiteAssociatedSiteIdentifierNavigations)
                .HasForeignKey(d => d.AssociatedSiteIdentifier)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_location_associated_site_related_site");

            entity.HasOne(d => d.AssociatedSiteTypeNavigation).WithMany(p => p.LocationAssociatedSites)
                .HasForeignKey(d => d.AssociatedSiteType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_location_associated_site_type");

            entity.HasOne(d => d.SiteIdentifierNavigation).WithMany(p => p.LocationAssociatedSiteSiteIdentifierNavigations)
                .HasForeignKey(d => d.SiteIdentifier)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_location_associated_site_site");
        });

        modelBuilder.Entity<LocationAssociatedSiteType>(entity =>
        {
            entity.HasKey(e => e.Type).HasName("location_associated_site_type_pkey");

            entity.ToTable("location_associated_site_type", "cads");

            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
        });

        modelBuilder.Entity<LocationCountry>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("location_country_pkey");

            entity.ToTable("location_country", "cads");

            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .HasColumnName("code");
            entity.Property(e => e.DevolvedAuthorityFlag).HasColumnName("devolved_authority_flag");
            entity.Property(e => e.EuropeanUnionTradeMemberFlag).HasColumnName("european_union_trade_member_flag");
            entity.Property(e => e.HomeCountryFlag).HasColumnName("home_country_flag");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<LocationPartyRef>(entity =>
        {
            entity.HasKey(e => e.PartyIdentifier).HasName("location_party_ref_pkey");

            entity.ToTable("location_party_ref", "cads");

            entity.Property(e => e.PartyIdentifier)
                .ValueGeneratedNever()
                .HasColumnName("party_identifier");
        });

        modelBuilder.Entity<LocationPostcode>(entity =>
        {
            entity.HasKey(e => e.Postcode).HasName("location_postcode_pkey");

            entity.ToTable("location_postcode", "cads");

            entity.Property(e => e.Postcode)
                .HasMaxLength(10)
                .HasColumnName("postcode");
        });

        modelBuilder.Entity<LocationSite>(entity =>
        {
            entity.HasKey(e => e.Identifier).HasName("location_site_pkey");

            entity.ToTable("location_site", "cads");

            entity.HasIndex(e => e.LocationIdentifier, "idx_location_site_location_identifier");

            entity.HasIndex(e => e.SiteSource, "idx_location_site_source");

            entity.HasIndex(e => e.State, "idx_location_site_state");

            entity.HasIndex(e => e.SiteType, "idx_location_site_type");

            entity.Property(e => e.Identifier)
                .HasMaxLength(50)
                .HasColumnName("identifier");
            entity.Property(e => e.DestroyIdentityDocumentsFlag).HasColumnName("destroy_identity_documents_flag");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.LocationIdentifier)
                .HasMaxLength(50)
                .HasColumnName("location_identifier");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.SiteSource)
                .HasMaxLength(20)
                .HasColumnName("site_source");
            entity.Property(e => e.SiteType)
                .HasMaxLength(10)
                .HasColumnName("site_type");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.State)
                .HasMaxLength(20)
                .HasColumnName("state");

            entity.HasOne(d => d.LocationIdentifierNavigation).WithMany(p => p.LocationSites)
                .HasForeignKey(d => d.LocationIdentifier)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_location_site_location");

            entity.HasOne(d => d.SiteSourceNavigation).WithMany(p => p.LocationSites)
                .HasForeignKey(d => d.SiteSource)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_location_site_source");

            entity.HasOne(d => d.SiteTypeNavigation).WithMany(p => p.LocationSites)
                .HasForeignKey(d => d.SiteType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_location_site_type");

            entity.HasOne(d => d.StateNavigation).WithMany(p => p.LocationSites)
                .HasForeignKey(d => d.State)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_location_site_state");
        });

        modelBuilder.Entity<LocationSiteActivity>(entity =>
        {
            entity.HasKey(e => new { e.SiteIdentifier, e.SiteType, e.Activity, e.StartDate }).HasName("location_site_activity_pkey");

            entity.ToTable("location_site_activity", "cads");

            entity.HasIndex(e => e.SiteIdentifier, "idx_location_site_activity_site_identifier");

            entity.Property(e => e.SiteIdentifier)
                .HasMaxLength(50)
                .HasColumnName("site_identifier");
            entity.Property(e => e.SiteType)
                .HasMaxLength(10)
                .HasColumnName("site_type");
            entity.Property(e => e.Activity)
                .HasMaxLength(10)
                .HasColumnName("activity");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.EndDate).HasColumnName("end_date");

            entity.HasOne(d => d.SiteIdentifierNavigation).WithMany(p => p.LocationSiteActivities)
                .HasForeignKey(d => d.SiteIdentifier)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_location_site_activity_site");

            entity.HasOne(d => d.LocationSiteTypeActivity).WithMany(p => p.LocationSiteActivities)
                .HasForeignKey(d => new { d.SiteType, d.Activity })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_location_site_activity_site_type_activity");
        });

        modelBuilder.Entity<LocationSiteIdentifier>(entity =>
        {
            entity.HasKey(e => new { e.SiteIdentifier, e.IdentifierType, e.IdentifierValue }).HasName("location_site_identifier_pkey");

            entity.ToTable("location_site_identifier", "cads");

            entity.Property(e => e.SiteIdentifier)
                .HasMaxLength(50)
                .HasColumnName("site_identifier");
            entity.Property(e => e.IdentifierType)
                .HasMaxLength(50)
                .HasColumnName("identifier_type");
            entity.Property(e => e.IdentifierValue)
                .HasMaxLength(100)
                .HasColumnName("identifier_value");

            entity.HasOne(d => d.IdentifierTypeNavigation).WithMany(p => p.LocationSiteIdentifiers)
                .HasForeignKey(d => d.IdentifierType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_location_site_identifier_type");

            entity.HasOne(d => d.SiteIdentifierNavigation).WithMany(p => p.LocationSiteIdentifiers)
                .HasForeignKey(d => d.SiteIdentifier)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_location_site_identifier_site");
        });

        modelBuilder.Entity<LocationSiteIdentifierType>(entity =>
        {
            entity.HasKey(e => e.Type).HasName("location_site_identifier_type_pkey");

            entity.ToTable("location_site_identifier_type", "cads");

            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
        });

        modelBuilder.Entity<LocationSiteParty>(entity =>
        {
            entity.HasKey(e => new { e.SiteIdentifier, e.PartyIdentifier, e.SiteRole, e.StartDate }).HasName("location_site_party_pkey");

            entity.ToTable("location_site_party", "cads");

            entity.HasIndex(e => e.PartyIdentifier, "idx_location_site_party_identifier");

            entity.Property(e => e.SiteIdentifier)
                .HasMaxLength(50)
                .HasColumnName("site_identifier");
            entity.Property(e => e.PartyIdentifier).HasColumnName("party_identifier");
            entity.Property(e => e.SiteRole)
                .HasMaxLength(20)
                .HasColumnName("site_role");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.EndDate).HasColumnName("end_date");

            entity.HasOne(d => d.PartyIdentifierNavigation).WithMany(p => p.LocationSiteParties)
                .HasForeignKey(d => d.PartyIdentifier)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_location_site_party_party");

            entity.HasOne(d => d.SiteIdentifierNavigation).WithMany(p => p.LocationSiteParties)
                .HasForeignKey(d => d.SiteIdentifier)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_location_site_party_site");

            entity.HasOne(d => d.SiteRoleNavigation).WithMany(p => p.LocationSiteParties)
                .HasForeignKey(d => d.SiteRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_location_site_party_role");
        });

        modelBuilder.Entity<LocationSiteRole>(entity =>
        {
            entity.HasKey(e => e.Role).HasName("location_site_role_pkey");

            entity.ToTable("location_site_role", "cads");

            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .HasColumnName("role");
        });

        modelBuilder.Entity<LocationSiteSource>(entity =>
        {
            entity.HasKey(e => e.Source).HasName("location_site_source_pkey");

            entity.ToTable("location_site_source", "cads");

            entity.Property(e => e.Source)
                .HasMaxLength(20)
                .HasColumnName("source");
        });

        modelBuilder.Entity<LocationSiteState>(entity =>
        {
            entity.HasKey(e => e.State).HasName("location_site_state_pkey");

            entity.ToTable("location_site_state", "cads");

            entity.Property(e => e.State)
                .HasMaxLength(20)
                .HasColumnName("state");
        });

        modelBuilder.Entity<LocationSiteType>(entity =>
        {
            entity.HasKey(e => e.Type).HasName("location_site_type_pkey");

            entity.ToTable("location_site_type", "cads");

            entity.Property(e => e.Type)
                .HasMaxLength(10)
                .HasColumnName("type");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .HasColumnName("description");
        });

        modelBuilder.Entity<LocationSiteTypeActivity>(entity =>
        {
            entity.HasKey(e => new { e.SiteType, e.Activity }).HasName("location_site_type_activity_pkey");

            entity.ToTable("location_site_type_activity", "cads");

            entity.Property(e => e.SiteType)
                .HasMaxLength(10)
                .HasColumnName("site_type");
            entity.Property(e => e.Activity)
                .HasMaxLength(10)
                .HasColumnName("activity");

            entity.HasOne(d => d.ActivityNavigation).WithMany(p => p.LocationSiteTypeActivities)
                .HasForeignKey(d => d.Activity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_location_site_type_activity_activity");

            entity.HasOne(d => d.SiteTypeNavigation).WithMany(p => p.LocationSiteTypeActivities)
                .HasForeignKey(d => d.SiteType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_location_site_type_activity_site_type");
        });

        modelBuilder.Entity<MiEffectiveReportAllPermission>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("mi_effective_report_all_permission", "cads");

            entity.Property(e => e.ExternalSubject).HasColumnName("external_subject");
            entity.Property(e => e.PermissionKey).HasColumnName("permission_key");
            entity.Property(e => e.ReportKey).HasColumnName("report_key");
        });

        modelBuilder.Entity<MiEffectiveReportPermission>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("mi_effective_report_permission", "cads");

            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.DisplayName).HasColumnName("display_name");
            entity.Property(e => e.ExternalSubject).HasColumnName("external_subject");
            entity.Property(e => e.Granted).HasColumnName("granted");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.ReportId).HasColumnName("report_id");
            entity.Property(e => e.ReportKey).HasColumnName("report_key");
            entity.Property(e => e.Title).HasColumnName("title");
        });

        modelBuilder.Entity<MiPermission>(entity =>
        {
            entity.HasKey(e => e.PermissionId).HasName("mi_permission_pkey");

            entity.ToTable("mi_permission", "cads");

            entity.HasIndex(e => e.PermissionKey, "mi_permission_permission_key_key").IsUnique();

            entity.Property(e => e.PermissionId)
                .ValueGeneratedNever()
                .HasColumnName("permission_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.PermissionKey).HasColumnName("permission_key");
        });

        modelBuilder.Entity<MiReport>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("mi_report_pkey");

            entity.ToTable("mi_report", "cads");

            entity.HasIndex(e => e.ReportKey, "mi_report_report_key_key").IsUnique();

            entity.Property(e => e.ReportId)
                .ValueGeneratedNever()
                .HasColumnName("report_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.ReportKey).HasColumnName("report_key");
            entity.Property(e => e.Title).HasColumnName("title");
        });

        modelBuilder.Entity<MiReportGroup>(entity =>
        {
            entity.HasKey(e => e.GroupId).HasName("mi_report_group_pkey");

            entity.ToTable("mi_report_group", "cads");

            entity.HasIndex(e => e.GroupKey, "mi_report_group_group_key_key").IsUnique();

            entity.Property(e => e.GroupId)
                .ValueGeneratedNever()
                .HasColumnName("group_id");
            entity.Property(e => e.DisplayOrder).HasColumnName("display_order");
            entity.Property(e => e.GroupKey).HasColumnName("group_key");
            entity.Property(e => e.Title).HasColumnName("title");

            entity.HasMany(d => d.Reports).WithMany(p => p.Groups)
                .UsingEntity<Dictionary<string, object>>(
                    "MiReportGroupMap",
                    r => r.HasOne<MiReport>().WithMany()
                        .HasForeignKey("ReportId")
                        .HasConstraintName("mi_report_group_map_report_id_fkey"),
                    l => l.HasOne<MiReportGroup>().WithMany()
                        .HasForeignKey("GroupId")
                        .HasConstraintName("mi_report_group_map_group_id_fkey"),
                    j =>
                    {
                        j.HasKey("GroupId", "ReportId").HasName("mi_report_group_map_pkey");
                        j.ToTable("mi_report_group_map", "cads");
                        j.IndexerProperty<Guid>("GroupId").HasColumnName("group_id");
                        j.IndexerProperty<Guid>("ReportId").HasColumnName("report_id");
                    });
        });

        modelBuilder.Entity<MiRole>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("mi_role_pkey");

            entity.ToTable("mi_role", "cads");

            entity.HasIndex(e => e.RoleKey, "mi_role_role_key_key").IsUnique();

            entity.Property(e => e.RoleId)
                .ValueGeneratedNever()
                .HasColumnName("role_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.RoleKey).HasColumnName("role_key");
        });

        modelBuilder.Entity<MiRoleReportPermission>(entity =>
        {
            entity.HasKey(e => new { e.RoleId, e.ReportId, e.PermissionId }).HasName("mi_role_report_permission_pkey");

            entity.ToTable("mi_role_report_permission", "cads");

            entity.HasIndex(e => e.PermissionId, "mi_rrp_permission_idx");

            entity.HasIndex(e => e.ReportId, "mi_rrp_report_idx");

            entity.HasIndex(e => new { e.ReportId, e.PermissionId }, "mi_rrp_report_permission_idx");

            entity.HasIndex(e => new { e.RoleId, e.ReportId }, "mi_rrp_role_report_idx");

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.ReportId).HasColumnName("report_id");
            entity.Property(e => e.PermissionId).HasColumnName("permission_id");
            entity.Property(e => e.Granted)
                .HasDefaultValue(true)
                .HasColumnName("granted");

            entity.HasOne(d => d.Permission).WithMany(p => p.MiRoleReportPermissions)
                .HasForeignKey(d => d.PermissionId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("mi_role_report_permission_permission_id_fkey");

            entity.HasOne(d => d.Report).WithMany(p => p.MiRoleReportPermissions)
                .HasForeignKey(d => d.ReportId)
                .HasConstraintName("mi_role_report_permission_report_id_fkey");

            entity.HasOne(d => d.Role).WithMany(p => p.MiRoleReportPermissions)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("mi_role_report_permission_role_id_fkey");
        });

        modelBuilder.Entity<MiUser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("mi_user_pkey");

            entity.ToTable("mi_user", "cads");

            entity.HasIndex(e => e.ExternalSubjectNormalized, "mi_user_external_subject_normalized_key").IsUnique();

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("user_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.DisplayName).HasColumnName("display_name");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.ExternalSubject).HasColumnName("external_subject");
            entity.Property(e => e.ExternalSubjectNormalized)
                .HasComputedColumnSql("lower(external_subject)", true)
                .HasColumnName("external_subject_normalized");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
        });

        modelBuilder.Entity<MiUserReportPermission>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.ReportId, e.PermissionId }).HasName("mi_user_report_permission_pkey");

            entity.ToTable("mi_user_report_permission", "cads");

            entity.HasIndex(e => e.PermissionId, "mi_urp_permission_idx");

            entity.HasIndex(e => e.ReportId, "mi_urp_report_idx");

            entity.HasIndex(e => new { e.UserId, e.ReportId }, "mi_urp_user_report_idx");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.ReportId).HasColumnName("report_id");
            entity.Property(e => e.PermissionId).HasColumnName("permission_id");
            entity.Property(e => e.Granted).HasColumnName("granted");
            entity.Property(e => e.GrantedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("granted_at");
            entity.Property(e => e.Reason).HasColumnName("reason");

            entity.HasOne(d => d.Permission).WithMany(p => p.MiUserReportPermissions)
                .HasForeignKey(d => d.PermissionId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("mi_user_report_permission_permission_id_fkey");

            entity.HasOne(d => d.Report).WithMany(p => p.MiUserReportPermissions)
                .HasForeignKey(d => d.ReportId)
                .HasConstraintName("mi_user_report_permission_report_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.MiUserReportPermissions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("mi_user_report_permission_user_id_fkey");
        });

        modelBuilder.Entity<MiUserRole>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.RoleId }).HasName("mi_user_role_pkey");

            entity.ToTable("mi_user_role", "cads");

            entity.HasIndex(e => e.RoleId, "mi_user_role_role_idx");

            entity.HasIndex(e => e.UserId, "mi_user_role_user_idx");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.GrantedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("granted_at");

            entity.HasOne(d => d.Role).WithMany(p => p.MiUserRoles)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("mi_user_role_role_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.MiUserRoles)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("mi_user_role_user_id_fkey");
        });

        modelBuilder.Entity<Party>(entity =>
        {
            entity.HasKey(e => e.Number).HasName("party_pkey");

            entity.ToTable("party", "cads");

            entity.HasIndex(e => e.LocationIdentifier, "idx_party_location_identifier");

            entity.Property(e => e.Number)
                .ValueGeneratedNever()
                .HasColumnName("number");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.FirstName).HasColumnName("first_name");
            entity.Property(e => e.Landline).HasColumnName("landline");
            entity.Property(e => e.LastName).HasColumnName("last_name");
            entity.Property(e => e.LocationIdentifier)
                .HasMaxLength(50)
                .HasColumnName("location_identifier");
            entity.Property(e => e.Mobile).HasColumnName("mobile");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.PartyState)
                .HasMaxLength(20)
                .HasColumnName("party_state");
            entity.Property(e => e.PartyType)
                .HasMaxLength(30)
                .HasColumnName("party_type");

            entity.HasOne(d => d.LocationIdentifierNavigation).WithMany(p => p.Parties)
                .HasForeignKey(d => d.LocationIdentifier)
                .HasConstraintName("fk_party_location");

            entity.HasOne(d => d.PartyStateNavigation).WithMany(p => p.Parties)
                .HasForeignKey(d => d.PartyState)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_party_state");

            entity.HasOne(d => d.PartyTypeNavigation).WithMany(p => p.Parties)
                .HasForeignKey(d => d.PartyType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_party_type");
        });

        modelBuilder.Entity<PartyHaulier>(entity =>
        {
            entity.HasKey(e => e.Identifier).HasName("party_haulier_pkey");

            entity.ToTable("party_haulier", "cads");

            entity.HasIndex(e => e.PartyNumber, "idx_party_haulier_party_number");

            entity.Property(e => e.Identifier).HasColumnName("identifier");
            entity.Property(e => e.AuthorisationNumber).HasColumnName("authorisation_number");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.PartyNumber).HasColumnName("party_number");
            entity.Property(e => e.StartDate).HasColumnName("start_date");

            entity.HasOne(d => d.PartyNumberNavigation).WithMany(p => p.PartyHauliers)
                .HasForeignKey(d => d.PartyNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_party_haulier_party");
        });

        modelBuilder.Entity<PartyLocation>(entity =>
        {
            entity.HasKey(e => e.Identifier).HasName("party_location_pkey");

            entity.ToTable("party_location", "cads");

            entity.Property(e => e.Identifier)
                .HasMaxLength(50)
                .HasColumnName("identifier");
        });

        modelBuilder.Entity<PartySpecy>(entity =>
        {
            entity.HasKey(e => e.Species).HasName("party_species_pkey");

            entity.ToTable("party_species", "cads");

            entity.Property(e => e.Species)
                .HasMaxLength(50)
                .HasColumnName("species");

            entity.HasMany(d => d.HaulierIdentifiers).WithMany(p => p.Species)
                .UsingEntity<Dictionary<string, object>>(
                    "PartyHaulierSpecy",
                    r => r.HasOne<PartyHaulier>().WithMany()
                        .HasForeignKey("HaulierIdentifier")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_party_haulier_species_haulier"),
                    l => l.HasOne<PartySpecy>().WithMany()
                        .HasForeignKey("Species")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_party_haulier_species_species"),
                    j =>
                    {
                        j.HasKey("Species", "HaulierIdentifier").HasName("party_haulier_species_pkey");
                        j.ToTable("party_haulier_species", "cads");
                        j.IndexerProperty<string>("Species")
                            .HasMaxLength(50)
                            .HasColumnName("species");
                        j.IndexerProperty<string>("HaulierIdentifier").HasColumnName("haulier_identifier");
                    });
        });

        modelBuilder.Entity<PartyState>(entity =>
        {
            entity.HasKey(e => e.State).HasName("party_state_pkey");

            entity.ToTable("party_state", "cads");

            entity.Property(e => e.State)
                .HasMaxLength(20)
                .HasColumnName("state");
        });

        modelBuilder.Entity<PartyType>(entity =>
        {
            entity.HasKey(e => e.Type).HasName("party_type_pkey");

            entity.ToTable("party_type", "cads");

            entity.Property(e => e.Type)
                .HasMaxLength(30)
                .HasColumnName("type");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
