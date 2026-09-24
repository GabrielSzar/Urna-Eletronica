using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using Urna.Core.Enums;
using Urna.Core.Models;

namespace Urna.Data.Database;

public class CsvHandler
{
    private static readonly Dictionary<string, string> MapaCargos = new()
    {
        ["PRESIDENTE"] = "Presidente",
        ["GOVERNADOR"] = "Governador",
        ["SENADOR"] = "Senador",
        ["DEPUTADO FEDERAL"] = "DeputadoFederal",
        ["DEPUTADO ESTADUAL"] = "DeputadoEstadual",
    };
    private static readonly Dictionary<string, Partidos> MapaPartidos = new()
    {
        ["MISSÃO"] = Partidos.MISSAO,
        ["UNIÃO"] = Partidos.UNIAO,
    };

    private static Partidos ResolverPartido(string sigla)
    {
        if (MapaPartidos.TryGetValue(sigla, out var partidoComAcento))
        {
            return partidoComAcento;
        }

        if (Enum.TryParse<Partidos>(sigla, out var partido))
        {
            return partido;
        }

        return Partidos.SemPartido;
    }
    public static IEnumerable<CandidatoModel> LerCandidatos(string nomeArquivo, string prefixoUf, Func<string, bool> filtroCargo)
    {
        var caminho = Path.Combine(AppContext.BaseDirectory, "Data", nomeArquivo);
        using var reader = new StreamReader(caminho, Encoding.Latin1);
        using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ";" });

        return csv.GetRecords<CandidatoCsv>()
            .Where(r => filtroCargo(r.DsCargo))
            .Select(r => new CandidatoModel
            {
                Nome = r.NmUrnaCandidato,
                Cargo = MapaCargos[r.DsCargo],
                Numero = r.NrCandidato,
                Partido = ResolverPartido(r.SgPartido),
                FotoCandidato = $"Assets/Img_Candidatos/{MapaCargos[r.DsCargo]}/{prefixoUf}{r.SqCandidato}_div.jpg"
            })
            .ToList();
    }
}
public class CandidatoCsv
{
    [Name("DS_CARGO")] public string DsCargo { get; set; } = "";
    [Name("NR_CANDIDATO")] public int NrCandidato { get; set; }
    [Name("NM_URNA_CANDIDATO")] public string NmUrnaCandidato { get; set; } = "";
    [Name("SG_PARTIDO")] public string SgPartido { get; set; } = "";
    [Name("SQ_CANDIDATO")] public string SqCandidato { get; set; } = "";
}