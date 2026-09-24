using Microsoft.EntityFrameworkCore;
using Urna.Core.Enums;
using Urna.Core.Interfaces;
using Urna.Core.Models;
using Urna.Data.Database;

namespace Urna.Data.Repositories;

public class CandidatoRepository : ICandidatoRepository
{
    public List<CandidatoModel> ListarTodos()
    {
        using (var context = new UrnaDbContext())
        {
            return context.Candidatos.AsNoTracking().ToList();
        }
    }

    public CandidatoModel? BuscarPorNumero(int numero)
    {
        using (var context = new UrnaDbContext())
        {
            return context.Candidatos.FirstOrDefault(candidato => candidato.Numero == numero)
                ?? null;
        }
    }

    public void RegistrarVoto(int candidatoId)
    {   // Removi o TipoVoto, porque o VotacaoViewModel já precisa saber o tipo do voto
        // para mostrar o alerta "Voto Branco" "Voto Nulo" para o usuario
        using (var context = new UrnaDbContext())
        {
            var candidato = context.Candidatos.Find(candidatoId);
            if (candidato == null)
            {   
                Console.WriteLine($"Canditado ID {candidatoId} não encontrado");
                return;
            }
            candidato.NumVotos++;
            context.SaveChanges();
        }
    }
}