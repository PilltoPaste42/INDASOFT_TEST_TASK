namespace SampleAnalysis.Application.Common.Interfaces;

using Microsoft.EntityFrameworkCore;

using SampleAnalysis.Domain.Models;

public interface IAppDbContext
{
    public DbSet<EventFrame> EventFrames { get; set; }

    public DbSet<EventFrameType> EventFrameTypes { get; set; }

    public DbSet<EventFrameTypeValue> EventFrameTypeValues { get; set; }

    public DbSet<EventFrameValue> EventFrameValues { get; set; }

    public DbSet<Link> Links { get; set; }

    public Task<int> SaveChangesAsync();
}