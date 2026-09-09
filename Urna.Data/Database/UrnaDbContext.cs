using Microsoft.EntityFrameworkCore;
using Urna.Core.Database.Models;

namespace Urna.Data.Database;

public class UrnaDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(DatabaseConfig.GetConnString());
    }

    DbSet<EleitorModel> Eleitors { get; set; }
    DbSet<CandidatoModel> Candidatos { get; set; }
}