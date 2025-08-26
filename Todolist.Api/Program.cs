//===================================================
// Copyright (c) 2025 Elshod Ibadullayev
// Free To Use For Learning and Development
// Project: Todolist.Api
//===================================================

using Todolist.Api.Brokers.Loggings;
using Todolist.Api.Brokers.Storages;
using Todolist.Api.Services.Foundations.TaskItems;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();
        builder.Services.AddDbContext<StorageBroker>();

        AddBrokers(builder);
        AddFoundationServices(builder);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
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