using Urna.Core.Enums;
using Urna.Core.Models;

namespace Urna.Core.Interfaces;

public interface IApuracaoService
{
    Dictionary<CandidatoModel, int> ContarVotos(List<CandidatoModel> candidatos);
    CandidatoModel ApurarVencedor(Dictionary<CandidatoModel, int> resultado);
    bool ValidarCpf(string cpf);
    TipoVoto ClassificarVoto(int numeroDigitado, List<CandidatoModel> candidatos);
}