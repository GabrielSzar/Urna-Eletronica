using Urna.Core.Enums;
using Urna.Core.Interfaces;
using Urna.Core.Models;

namespace Urna.Business;

public class ApuracaoService : IApuracaoService
{
    public Dictionary<CandidatoModel, int> ContarVotos(List<CandidatoModel> candidatos)
    {
        throw new NotImplementedException();
    }

    public CandidatoModel ApurarVencedor(Dictionary<CandidatoModel, int> resultado)
    {
        throw new NotImplementedException();
    }

    public bool ValidarCpf(string cpf)
    {
        throw new NotImplementedException();
    }

    public TipoVoto ClassificarVoto(int numeroDigitado, List<CandidatoModel> candidatos)
    {
        throw new NotImplementedException();
    }
}