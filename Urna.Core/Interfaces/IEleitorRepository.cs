using Urna.Core.Models;

namespace Urna.Core.Interfaces;

public interface IEleitorRepository
{
    bool JaVotou(string cpf);
    void MarcarComoVotado(string cpf);
    EleitorModel BuscarPorCpf(string cpf);
}