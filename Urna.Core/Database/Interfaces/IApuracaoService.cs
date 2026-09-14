using Urna.Core.Database.Models;
using Urna.Core.Database.Enums;

namespace Urna.Core.Database.Interfaces;

public interface IApuracaoService
{
    Dictionary<CandidatoModel, int> ContarVotos(List<CandidatoModel> candidatos);
    CandidatoModel ApurarVencedor(Dictionary<CandidatoModel, int> resultado);
    bool ValidarCpf(string cpf);
    TipoVoto ClassificarVoto(int numeroDigitado, List<CandidatoModel> candidatos);
}