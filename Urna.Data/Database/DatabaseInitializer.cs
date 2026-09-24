using Microsoft.EntityFrameworkCore;
using Urna.Core.Enums;
using Urna.Core.Models;

namespace Urna.Data.Database;

public class DatabaseInitializer
{
    public void Initializer()
    {
        using (var context = new UrnaDbContext())
        {
            context.Database.Migrate();

            if (!context.Candidatos.Any())
            {
                AddCandidatos(context);
            }
        }
    }

    private static void AddCandidatos(UrnaDbContext context)
    {
        var candidatos = new List<CandidatoModel>();
        candidatos.AddRange(CsvHandler.LerCandidatos("consulta_cand_2026_BR.csv", "FBR", cargo => cargo == "PRESIDENTE"));
        candidatos.AddRange(CsvHandler.LerCandidatos("consulta_cand_2026_MG.csv", "FMG",
            cargo => cargo is "GOVERNADOR" or "SENADOR" or "DEPUTADO FEDERAL" or "DEPUTADO ESTADUAL"));

        foreach (var cargo in candidatos.Select(c => c.Cargo).Distinct().ToList())
        {
            candidatos.Add(new CandidatoModel { Nome = "Branco", Cargo = cargo, Numero = -1, Partido = Partidos.SemPartido, FotoCandidato = $"Assets/Img_Candidatos/branco.jpg" });
            candidatos.Add(new CandidatoModel { Nome = "Nulo", Cargo = cargo, Numero = -1, Partido = Partidos.SemPartido, FotoCandidato = $"Assets/Img_Candidatos/nulo.jpg" });
        }

        context.Candidatos.AddRange(candidatos);
        context.SaveChanges();
    }
}