using InsurantSales.Application.MindeeService;
using InsurantSales.Application.OpenAIService;
using InsurantSales.Application.OpenAIService.Interfaces;
using InsurantSales.Application.TelegramBotHandler;
using InsurantSales.Application.TelegramBotHandler.Interfaces;
using InsurantSales.Application.TelegramBotService;
using InsurantSales.Application.TelegramBotService.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mindee;
using System;
using System.Reflection;
using Telegram.Bot;
using Microsoft.EntityFrameworkCore;
using InsurantSales.DataAccess;
using InsurantSales.DataAccess.Interfaces;
using InsurantSales.Application.Interfaces;
using InsurantSales.Application.Services;

var builder = Host.CreateApplicationBuilder(args);
var config = builder.Configuration;

builder.Services.AddHttpClient();
builder.Services.AddScoped<IBotService, BotService>();
builder.Services.AddScoped<IBotUpdateHandler, BotUpdateHandler>();
builder.Services.AddScoped<IOpenAIService, OpenAIService>();
builder.Services.AddScoped<IMindeeService, MindeeService>();
builder.Services.AddScoped<IUnitOfWork, InsurantSales_UnitOfWork>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IDocumentService, DocumentService>();

builder.Services.AddSingleton<ITelegramBotClient>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var token = config["Telegram:BotToken"]!;
    return new TelegramBotClient(token);
});

builder.Services.AddSingleton<MindeeClient>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var apiKey = config["Mindee:ApiKey"] ?? throw new ArgumentNullException("Mindee:ApiKey");
    return new MindeeClient(apiKey);
});

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(Assembly.Load("InsurantSales.Application"));
});

builder.Services.AddDbContext<InsurantSales_DataContext>(options => options.UseSqlServer(config.GetConnectionString("DatabaseConnectionString"), b => b.MigrationsAssembly("InsurantSalesTelegramBot")));

var host = builder.Build();

using (var scope = host.Services.CreateScope())
{
    var botService = scope.ServiceProvider.GetRequiredService<IBotService>();
    await botService.StartAsync();
}

await host.RunAsync();