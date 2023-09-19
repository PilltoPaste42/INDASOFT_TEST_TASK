namespace SampleAnalysis.Infrastructure.Persistence.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SampleAnalysis.Domain.Models;

public class EventFrameTypeConfiguration : IEntityTypeConfiguration<EventFrameType>
{
    public void Configure(EntityTypeBuilder<EventFrameType> builder)
    {
        builder
            .HasKey(e => e.Id)
            .HasName("PK_EventFrameTypes_Id");

        builder
            .Property(e => e.Id)
            .HasDefaultValueSql("(newid())");
    }
}