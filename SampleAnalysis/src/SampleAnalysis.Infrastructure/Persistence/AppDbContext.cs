namespace SampleAnalysis.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;

using SampleAnalysis.Application.Common.Interfaces;
using SampleAnalysis.Domain.Models;

public class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<EventFrame> EventFrames { get; set; }

    public DbSet<EventFrameType> EventFrameTypes { get; set; }

    public DbSet<EventFrameTypeValue> EventFrameTypeValues { get; set; }

    public DbSet<EventFrameValue> EventFrameValues { get; set; }

    public DbSet<Link> Links { get; set; }

    public async Task<int> SaveChangesAsync()
    {
        return await base.SaveChangesAsync();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        base.OnModelCreating(builder);
    }
}