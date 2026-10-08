using Urna.Core.Models;

namespace Urna.Core.Interfaces;

public interface IEleitorRepository
{
    void AdicionarEleitor(string cpf);
    bool JaVotou(string cpf);
    void MarcarComoVotado(string cpf);
    EleitorModel? BuscarPorCpf(string cpf);
}