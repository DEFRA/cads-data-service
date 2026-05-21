using Cads.Cds.StorageBridge.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cads.Cds.StorageBridge.Infrastructure.Persistance.EntityConfigurations;

public class CtLocationConfiguration : IEntityTypeConfiguration<CtLocation>
{
    public void Configure(EntityTypeBuilder<CtLocation> builder)
    {
        builder.ToTable("_ct_locations");

        builder.HasKey(x => x.LocId)
            .HasName("_ct_locations_pkey");

        builder.Property(x => x.LocId)
            .HasColumnName("loc_id")
            .HasColumnType("int")
            .IsRequired();

        builder.Property(x => x.ReceivePpafFlag)
            .HasColumnName("loc_receive_ppaf_flag")
            .HasColumnType("varchar(1)");

        builder.Property(x => x.SltId)
            .HasColumnName("loc_slt_id")
            .HasColumnType("bigint");

        builder.Property(x => x.LtyId)
            .HasColumnName("loc_lty_id")
            .HasColumnType("bigint");

        builder.Property(x => x.CtyId)
            .HasColumnName("loc_cty_id")
            .HasColumnType("bigint");

        builder.Property(x => x.ReceiveLabelsFlag)
            .HasColumnName("loc_receive_labels_flag")
            .HasColumnType("varchar(1)");

        builder.Property(x => x.EffectiveFrom)
            .HasColumnName("loc_effective_from")
            .HasColumnType("date");

        builder.Property(x => x.EffectiveTo)
            .HasColumnName("loc_effective_to")
            .HasColumnType("date");

        builder.Property(x => x.CessationReason)
            .HasColumnName("loc_cessation_reason")
            .HasColumnType("varchar(2)");

        builder.Property(x => x.PremisesType)
            .HasColumnName("loc_premises_type")
            .HasColumnType("varchar(4)");

        builder.Property(x => x.Comments)
            .HasColumnName("loc_comments")
            .HasColumnType("varchar(400)");

        builder.Property(x => x.MapReference)
            .HasColumnName("loc_map_reference")
            .HasColumnType("varchar(12)");

        builder.Property(x => x.SourceIdentifier)
            .HasColumnName("loc_source_identifier")
            .HasColumnType("varchar(2)");

        builder.Property(x => x.SourceReference)
            .HasColumnName("loc_source_reference")
            .HasColumnType("varchar(20)");

        builder.Property(x => x.TelNumber)
            .HasColumnName("loc_tel_number")
            .HasColumnType("varchar(25)");

        builder.Property(x => x.MobileNumber)
            .HasColumnName("loc_mobile_number")
            .HasColumnType("varchar(25)");

        builder.Property(x => x.FaxNumber)
            .HasColumnName("loc_fax_number")
            .HasColumnType("varchar(25)");

        builder.Property(x => x.EmailAddress)
            .HasColumnName("loc_email_address")
            .HasColumnType("varchar(50)");

        builder.Property(x => x.CurrentStatus)
            .HasColumnName("loc_current_status")
            .HasColumnType("varchar(2)");

        builder.Property(x => x.CurrentUser)
            .HasColumnName("loc_current_user")
            .HasColumnType("varchar(10)");

        builder.Property(x => x.CurrentModifiedDate)
            .HasColumnName("loc_current_modified_date")
            .HasColumnType("date");

        builder.Property(x => x.CurrentPid)
            .HasColumnName("loc_current_pid")
            .HasColumnType("integer");

        builder.Property(x => x.ReasonCode)
            .HasColumnName("loc_reason_code")
            .HasColumnType("varchar(2)");

        builder.Property(x => x.Version)
            .HasColumnName("loc_version")
            .HasColumnType("integer");

        builder.Property(x => x.FakeData)
            .HasColumnName("fake_data")
            .HasColumnType("integer")
            .HasDefaultValue(0)
            .IsRequired();

        builder.Property(x => x.RowNumber)
            .HasColumnName("row_number")
            .HasColumnType("bigint");

        builder.Property(x => x.RecordType)
            .HasColumnName("record_type")
            .HasColumnType("varchar(1)");

        builder.Property(x => x.RecordCount)
            .HasColumnName("record_count")
            .HasColumnType("bigint");

        builder.Property(x => x.ImportedDate)
            .HasColumnName("imported_date")
            .HasColumnType("timestamp without time zone")
            .HasDefaultValueSql("current_timestamp");
    }
}