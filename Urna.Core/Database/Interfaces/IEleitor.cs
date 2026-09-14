using Urna.Core.Database.Models;

namespace TesteAvalonia.Core.Database.Interfaces;

public interface IEleitor
{
    bool JaVotou(string cpf);
    void MarcarComoVotado(string cpf);
    EleitorModel BuscarPorCpf(string cpf);
}