namespace Urna.Core.Database.Models;

public class CandidatoModel
{
    public int Id { get; set; }
    public int Numero { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Cargo { get; set; } = string.Empty;
    public int NumVotos { get; set; } = 0;
}