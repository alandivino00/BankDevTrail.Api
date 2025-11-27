using BankDevTrail.Api.Data;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

   
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddDbContext<BankContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BankDatabase")));

    // Add services to the container.

    builder.Services.AddControllers();

    builder.Services.AddScoped<BankDevTrail.Api.Repositories.IClienteRepository, BankDevTrail.Api.Repositories.ClienteRepository>();
    builder.Services.AddScoped<BankDevTrail.Api.Service.IClienteService, BankDevTrail.Api.Service.ClienteService>();

    builder.Services.AddScoped<BankDevTrail.Api.Repositories.IContaRepository, BankDevTrail.Api.Repositories.ContaRepository>();
    builder.Services.AddScoped<BankDevTrail.Api.Service.IContaService, BankDevTrail.Api.Service.ContaService>();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.MapGet("/health", async (BankContext context) =>
    {
        bool canConnect;
        try
        {
            canConnect = await context.Database.CanConnectAsync();
        }
        catch
        {
            canConnect = false;
        }

        if (canConnect)
            return Results.Ok("Meu banco esta funcionando.");

        return Results.Problem("Meu banco falhou");
    });

    app.Run();
    }
}
