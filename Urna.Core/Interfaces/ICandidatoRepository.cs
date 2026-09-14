using Urna.Core.Enums;
using Urna.Core.Models;

namespace Urna.Core.Interfaces;

public interface ICandidatoRepository
{
    List<CandidatoModel> ListarTodos();
    CandidatoModel BuscarPorNumero(int numero);
    void RegistrarVoto(int candidatoId, TipoVoto tipo);
}