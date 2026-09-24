using Urna.Core.Enums;

namespace Urna.Core.Models;

public class CandidatoModel()
{
    public int Id { get; set; }
    public int Numero { get; set; }
    public string? Nome { get; set; }
    public string? Cargo { get; set; }
    public int NumVotos { get; set; } = 0;
    public Partidos Partido { get; set; }
    public string? FotoCandidato { get; set; }
}
