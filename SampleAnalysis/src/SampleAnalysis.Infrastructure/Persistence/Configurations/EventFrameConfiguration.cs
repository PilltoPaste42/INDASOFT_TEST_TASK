namespace SampleAnalysis.Infrastructure.Persistence.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SampleAnalysis.Domain.Models;

public class EventFrameConfiguration : IEntityTypeConfiguration<EventFrame>
{
    public void Configure(EntityTypeBuilder<EventFrame> builder)
    {
        builder
            .HasKey(e => e.Id)
            .HasName("PK_EventFrames_Id");

        builder
            .Property(e => e.Id)
            .HasDefaultValueSql("(newid())");

        builder
            .HasOne(d => d.EventFrameType)
            .WithMany(p => p.EventFrames)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK__EventFram__Event__276EDEB3");
    }
}