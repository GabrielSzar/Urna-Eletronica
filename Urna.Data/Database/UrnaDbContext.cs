using Microsoft.EntityFrameworkCore;
using Urna.Core.Database.Models;

namespace Urna.Data.Database;

public class UrnaDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(DatabaseConfig.GetConnString());
    }

    public DbSet<EleitorModel> Eleitors { get; set; }
    public DbSet<CandidatoModel> Candidatos { get; set; }
}