﻿namespace Order.Infrastructure.Configuration;

public class DatabaseConfig
{
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
    public string CollectionName { get; set; }
}