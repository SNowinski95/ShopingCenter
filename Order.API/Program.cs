using BuildingBlocks.Infrastructure.Queue;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.DependencyInjection;
using Order.Application;
using Order.Domain;
using Order.Infrastructure.Configuration;
using Order.Infrastructure.Domain;
using Order.Infrastructure.JobScheduling;
using Order.Infrastructure.Proxy;
using Order.Infrastructure.ServiceExtension;
using Order.Mock.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//change to redis
builder.Services.AddMemoryCache();
builder.Services.AddSingleton(TimeProvider.System);
//move to extension
//TODO: Add API endpoint from configuration
builder.Services.AddMongoDBCollections(builder.Configuration);
//builder.Services.AddProxy(builder.Configuration);
//Mock for IUserApi 
builder.Services.AddScoped<IUserApi, UserApi>();
builder.Services.AddMediatR(cfg => { cfg.RegisterServicesFromAssemblies([typeof(Program).Assembly, ApplicationAssemblyReference.Assembly]); });
//Mock for IProductApi 
builder.Services.AddScoped<IProductApi, ProductApi>();
builder.Services.AddTransient<IConsumer>(_=> new Consumer(builder.Configuration.GetValue<string>("RabbitMqHostName")));
builder.Services.AddTransient<IRaportGenerator, RaportGenerator>();
builder.Services.AddScoped<IOrderRepository, OrderReposiotry>();
//TODO: register MongoDB table
//TODO: add exception middleware
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
