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

    public CandidatoModel BuscarPorNumero(int numero)
    {
        CandidatoModel candidatoModel = new CandidatoModel()
        {
            Id = 0
        };

        using (var context = new UrnaDbContext())
        {
            return context.Candidatos.FirstOrDefault(candidato => candidato.Numero == numero)
                ?? candidatoModel;
        }
    }

    public void RegistrarVoto(int candidatoId, TipoVoto tipo)
    {
        if (tipo == TipoVoto.Branco)
        {
            using (var context = new UrnaDbContext())
            {
                var maisVotado = context.Candidatos.OrderByDescending(candidato => candidato.NumVotos).First();
                
                maisVotado.NumVotos++;
                
                context.SaveChanges();
            }

            return;
        }

        if (tipo == TipoVoto.Nulo)
        {
            return;
        }

        using (var context = new UrnaDbContext())
        {
            CandidatoModel candidato = context.Candidatos.Find(candidatoId);

            candidato.NumVotos++;

            context.SaveChanges();
        }
    }
}