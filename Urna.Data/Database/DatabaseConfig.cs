using Microsoft.Extensions.Configuration;

namespace Urna.Data.Database;

public class DatabaseConfig
{
    public static string GetConnString()
    {
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        return config.GetConnectionString("Default") ?? string.Empty;
    }
}