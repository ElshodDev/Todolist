//===================================================
// Copyright (c) 2025 Elshod Ibadullayev
// Free To Use For Learning and Development
// Project: Todolist.Api
//===================================================

using Microsoft.EntityFrameworkCore;
using Todolist.Api.Brokers.Loggings;
using Todolist.Api.Brokers.Storages;
using Todolist.Api.Services.Foundations.TaskItems;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.WebHost.UseUrls("http://+:80");

        builder.Services.AddControllers();
        builder.Services.AddDbContext<StorageBroker>(options =>
            options.UseSqlServer(connectionString));

        AddBrokers(builder);
        AddFoundationServices(builder);
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<StorageBroker>();
            db.Database.Migrate();
        }

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todolist API v1");
                c.RoutePrefix = "";
            });
        }

        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }

    private static void AddBrokers(WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<IStorageBroker, StorageBroker>();
        builder.Services.AddTransient<ILoggingBroker, LoggingBroker>();
    }

    private static void AddFoundationServices(WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<ITaskItemService, TaskItemService>();
    }
}
