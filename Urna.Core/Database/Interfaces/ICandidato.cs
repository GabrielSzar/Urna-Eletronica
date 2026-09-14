using Urna.Core.Database.Models;
using Urna.Core.Database.Enums;

namespace Urna.Core.Database.Interfaces;

public interface ICandidato
{
    List<CandidatoModel> ListarTodos();
    CandidatoModel BuscarPorNumero(int numero);
    void RegistrarVoto(int candidatoId, TipoVoto tipo);
}