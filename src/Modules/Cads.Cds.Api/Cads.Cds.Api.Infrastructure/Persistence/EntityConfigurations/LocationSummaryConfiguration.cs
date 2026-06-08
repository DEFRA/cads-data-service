using Cads.Cds.Api.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cads.Cds.Api.Infrastructure.Persistence.EntityConfigurations;

public class LocationSummaryConfiguration : IEntityTypeConfiguration<LocationSummary>
{
    public void Configure(EntityTypeBuilder<LocationSummary> builder)
    {
        builder.HasNoKey();

        builder.Property(x => x.LidIdentifier).HasColumnName("lid_identifier");
        builder.Property(x => x.LidFullIdentifier).HasColumnName("lid_full_identifier");
        builder.Property(x => x.LidSubIdentifier).HasColumnName("lid_sub_identifier");
        builder.Property(x => x.LidEffectiveFromDate).HasColumnName("lid_effective_from_date");
        builder.Property(x => x.LidEffectiveToDate).HasColumnName("lid_effective_to_date");
        builder.Property(x => x.LidCurrentModifiedDate).HasColumnName("lid_current_modified_date");

        builder.Property(x => x.LtyShortDescription).HasColumnName("lty_short_description");
        builder.Property(x => x.LtyLongDescription).HasColumnName("lty_long_description");

        builder.Property(x => x.LocMapReference).HasColumnName("loc_map_reference");
        builder.Property(x => x.LocEffectiveFrom).HasColumnName("loc_effective_from");
        builder.Property(x => x.LocEffectiveTo).HasColumnName("loc_effective_to");
        builder.Property(x => x.LocCessationReason).HasColumnName("loc_cessation_reason");
        builder.Property(x => x.LocComments).HasColumnName("loc_comments");
        builder.Property(x => x.LocSourceIdentifier).HasColumnName("loc_source_identifier");
        builder.Property(x => x.LocTelNumber).HasColumnName("loc_tel_number");
        builder.Property(x => x.LocMobileNumber).HasColumnName("loc_mobile_number");
        builder.Property(x => x.LocFaxNumber).HasColumnName("loc_fax_number");
        builder.Property(x => x.LocEmailAddress).HasColumnName("loc_email_address");
        builder.Property(x => x.LocCurrentModifiedDate).HasColumnName("loc_current_modified_date");
        builder.Property(x => x.LocReasonCode).HasColumnName("loc_reason_code");
        builder.Property(x => x.LocVersion).HasColumnName("loc_version");

        builder.Property(x => x.CtyName).HasColumnName("cty_name");
        builder.Property(x => x.CtyVetAreaDesc).HasColumnName("cty_vet_area_desc");
        builder.Property(x => x.CtyPassportAreaDesc).HasColumnName("cty_passport_area_desc");
        builder.Property(x => x.CtyAdminOffice).HasColumnName("cty_admin_office");
        builder.Property(x => x.CtyBcmsTeam).HasColumnName("cty_bcms_team");
        builder.Property(x => x.CtyInspectionArea).HasColumnName("cty_inspection_area");
        builder.Property(x => x.CtyDataMgtAreaDesc).HasColumnName("cty_data_mgt_area_desc");
        builder.Property(x => x.CtyCurrentStatus).HasColumnName("cty_current_status");

        builder.Property(x => x.LifDescription).HasColumnName("lif_description");
        builder.Property(x => x.ImportedDate).HasColumnName("imported_date");
    }
}