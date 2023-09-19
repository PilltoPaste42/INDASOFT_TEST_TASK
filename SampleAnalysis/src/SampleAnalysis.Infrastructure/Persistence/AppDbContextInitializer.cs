namespace SampleAnalysis.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;

public class AppDbContextInitializer
{
    private readonly AppDbContext _context;

    public AppDbContextInitializer(AppDbContext context)
    {
        _context = context;
    }

    public async Task InitialiseAsync()
    {
        await _context.Database.MigrateAsync();
    }
}