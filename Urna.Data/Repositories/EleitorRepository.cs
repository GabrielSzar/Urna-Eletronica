using TesteAvalonia.Core.Database.Interfaces;
using Urna.Core.Database.Models;
using Urna.Data.Database;

namespace Urna.Data.Repositories;

public class EleitorRepository : IEleitor
{
    public bool JaVotou(string cpf)
    {
        using (var context = new UrnaDbContext())
        {
            var eleitor = context.Eleitors.FirstOrDefault(eleitor => eleitor.Cpf == cpf);

            if (eleitor.JaVotou)
            {
                return true;
            }

            return false;
        }
    }

    public void MarcarComoVotado(string cpf)
    {
        using (var context = new UrnaDbContext())
        {
            var eleitor = context.Eleitors.FirstOrDefault(eleitor => eleitor.Cpf == cpf);

            eleitor.JaVotou = true;

            context.SaveChanges();
        }
    }

    public EleitorModel BuscarPorCpf(string cpf)
    {
        using (var context = new UrnaDbContext())
        {
            return context.Eleitors.FirstOrDefault(eleitor => eleitor.Cpf == cpf);
        }
    }
}