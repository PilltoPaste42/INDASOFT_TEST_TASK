namespace SampleAnalysis.Infrastructure.Persistence.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SampleAnalysis.Domain.Models;

public class EventFrameTypeValueConfiguration : IEntityTypeConfiguration<EventFrameTypeValue>
{
    public void Configure(EntityTypeBuilder<EventFrameTypeValue> builder)
    {
        builder
            .HasKey(e => e.Id)
            .HasName("PK_EventFrameTypeValues_Id");

        builder
            .Property(e => e.Id)
            .HasDefaultValueSql("(newid())");

        builder
            .HasOne(d => d.EventFrameType)
            .WithMany(p => p.EventFrameTypeValues)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK__EventFram__Event__38996AB5");
    }
}