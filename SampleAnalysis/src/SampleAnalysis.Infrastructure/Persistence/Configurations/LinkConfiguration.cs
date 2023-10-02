namespace SampleAnalysis.Infrastructure.Persistence.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SampleAnalysis.Domain.Models;

public class LinkConfiguration : IEntityTypeConfiguration<Link>
{
    public void Configure(EntityTypeBuilder<Link> builder)
    {
        builder
            .HasKey(e => e.Id)
            .HasName("PK_Links_Id");

        builder
            .Property(e => e.Id)
            .HasDefaultValueSql("(newid())");

        builder
            .HasOne(d => d.ChildEventFrame)
            .WithMany(p => p.LinkChildEventFrames)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK__Links__ChildEven__4BAC3F29");

        builder
            .HasOne(d => d.ChildEventFrameType)
            .WithMany(p => p.LinkChildEventFrameTypes)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK__Links__ChildEven__4D94879B");

        builder
            .HasOne(d => d.ParentEventFrame)
            .WithMany(p => p.LinkParentEventFrames)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK__Links__ParentEve__4AB81AF0");

        builder
            .HasOne(d => d.ParentEventFrameType)
            .WithMany(p => p.LinkParentEventFrameTypes)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK__Links__ParentEve__4CA06362");
    }
}