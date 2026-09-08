namespace Urna.Core.Database.Models;

public class CandidatoModel
{
    public int Id { get; set; }
    public int Numero { get; set; }
    public string ?Nome { get; set; }
    public string ?Cargo { get; set; }
    public int NumVotos { get; set; } = 0;
}