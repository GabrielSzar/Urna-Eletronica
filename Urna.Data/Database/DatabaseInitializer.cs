using Microsoft.EntityFrameworkCore;

namespace Urna.Data.Database;

public class DatabaseInitializer
{
    public static void Initializer()
    {
        using (var dbContext = new UrnaDbContext())
        {
            dbContext.Database.Migrate();
        }
    }
}