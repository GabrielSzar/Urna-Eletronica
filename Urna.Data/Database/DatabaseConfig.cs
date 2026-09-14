using Microsoft.Extensions.Configuration;

namespace Urna.Data.Database;

public class DatabaseConfig
{
    public static string GetConnString()
    {
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetParent(Directory.GetCurrentDirectory()).FullName)
            .AddJsonFile("appsettings.json")
            .Build();

        return config.GetConnectionString("Default") ?? string.Empty;
    }
}