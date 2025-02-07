using Microsoft.Extensions.Caching.StackExchangeRedis;
using Order.Domain;
using Order.Infrastructure.Domain;
using Order.Infrastructure.Proxy;
using RestEase.HttpClientFactory;
using ZiggyCreatures.Caching.Fusion;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//change to redis
builder.Services.AddFusionCache();
builder.Services.AddSingleton<TimeProvider>();
//move to extension
//TODO: Add API endpoint from configuration
builder.Services.AddRestEaseClient<IUserApi>("");
builder.Services.AddRestEaseClient<IProductApi>("");
builder.Services.AddScoped<IOrderRepository, OrderReposiotry>();
//TODO: register MongoDB table
//TODO: add exception middleware
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
