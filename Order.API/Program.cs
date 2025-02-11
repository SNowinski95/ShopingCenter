using Order.Application;
using Order.Infrastructure.Configuration;
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
//Mock for IProductApi 
builder.Services.AddScoped<IProductApi, ProductApi>();
builder.Services.AddCache(builder.Configuration);
builder.Services.AddConsumer<RaportQueueConfig>(builder.Configuration);
builder.Services.AddMediatR(cfg => { cfg.RegisterServicesFromAssemblies([typeof(Program).Assembly, ApplicationAssemblyReference.Assembly]); });

builder.Services.AddSingleton<IRaportGenerator, RaportGenerator>();
builder.Services.AddReposiotry();
//TODO: add exception middleware
var app = builder.Build();
// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();
var raportGenerator =  app.Services.GetRequiredService<IRaportGenerator>();
await raportGenerator.GenerateRaprot();
app.MapControllers();

app.Run();
