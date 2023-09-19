namespace SampleAnalysis.Infrastructure.Persistence.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SampleAnalysis.Domain.Models;

public class EventFrameValueConfiguration : IEntityTypeConfiguration<EventFrameValue>
{
    public void Configure(EntityTypeBuilder<EventFrameValue> builder)
    {
        builder
            .HasKey(e => e.Id)
            .HasName("PK_EventFrameValues_Id");

        builder
            .Property(e => e.Id)
            .HasDefaultValueSql("(newid())");

        builder
            .HasOne(d => d.EventFrame)
            .WithMany(p => p.EventFrameValues)
            .HasConstraintName("FK__EventFram__Event__36B12243");

        builder
            .HasOne(d => d.Userfield)
            .WithMany(p => p.EventFrameValues)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK__EventFram__Userf__37A5467C");
    }
}