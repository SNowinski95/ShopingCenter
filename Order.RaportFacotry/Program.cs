// See https://aka.ms/new-console-template for more information
using Hangfire;
using Microsoft.Extensions.Configuration;
try
{
    Console.WriteLine("Generate report applicaton");
    var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();
    RecurringJob.AddOrUpdate("RaportRequest",() => Console.WriteLine("table traversal, sending Id to queue"), Cron.Monthly());
}catch(Exception e)
{
    Console.WriteLine(e.ToString());
}
