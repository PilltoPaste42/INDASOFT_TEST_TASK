namespace SampleAnalysis.WebAPI;

using SampleAnalysis.Application;
using SampleAnalysis.Infrastructure;
using SampleAnalysis.Infrastructure.Persistence;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddInfrastructureServices(builder.Configuration);
        builder.Services.AddApplicationServices();

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            using (var scope = app.Services.CreateScope())
            {
                var initialiser = scope.ServiceProvider.GetRequiredService<AppDbContextInitializer>();
                _ = initialiser.InitialiseAsync();
            }
        }

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}