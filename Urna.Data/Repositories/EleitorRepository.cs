using Urna.Core.Interfaces;
using Urna.Core.Models;
using Urna.Data.Database;

namespace Urna.Data.Repositories;

public class EleitorRepository : IEleitorRepository
{
    public void AdicionarEleitor(string cpf)
    {
        EleitorModel novoEleitor = new EleitorModel()
        {
            Cpf = cpf,
            JaVotou = false
        };

        using (var context = new UrnaDbContext())
        {
            var eleitor = context.Eleitors.Add(novoEleitor);

            context.SaveChanges();
        }
    }

    public bool JaVotou(string cpf)
    {
        using (var context = new UrnaDbContext())
        {
            var eleitor = context.Eleitors.FirstOrDefault(eleitor => eleitor.Cpf == cpf);
            if (eleitor == null)
            {
                throw new InvalidDataException("O metodo JaVotou() não achou nenhum eleitor");
            }
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
            if (eleitor == null)
            {
                throw new InvalidDataException("O metodo MarcarComoVotado() não achou nenhum eleitor");
            }
            eleitor.JaVotou = true;

            context.SaveChanges();
        }
    }

    public EleitorModel? BuscarPorCpf(string cpf)
    {
        using (var context = new UrnaDbContext())
        {
            return context.Eleitors.FirstOrDefault(eleitor => eleitor.Cpf == cpf);
        }
    }
}