using Microsoft.EntityFrameworkCore;
using Urna.Core.Enums;
using Urna.Core.Models;

namespace Urna.Data.Database;

public class DatabaseInitializer
{
    public void Initializer()
    {
        using (var context = new UrnaDbContext())
        {
            context.Database.Migrate();

            if (!context.Candidatos.Any())
            {
                AddCandidatos(context);
            }
        }
    }

    private void AddCandidatos(UrnaDbContext context)
    {
        CandidatoModel[] candidatosPresidente =
        [
            new CandidatoModel { Nome = "Luiz Inácio Lula da Silva", Cargo = "Presidente", Numero = 13, Partido = Partidos.PT, FotoCandidato = "Assets/Img_Candidatos/lula.png" },
            new CandidatoModel { Nome = "Flávio Bolsonaro e Alfredo Gaspar", Cargo = "Presidente", Numero = 22, Partido = Partidos.PL, FotoCandidato = "Assets/Img_Candidatos/flavio.png" },
            new CandidatoModel { Nome = "Renan Santos", Cargo = "Presidente", Numero = 14, Partido = Partidos.Missão, FotoCandidato = "Assets/Img_Candidatos/renan.png" },
            new CandidatoModel { Nome = "Hertz Dias", Cargo = "Presidente", Numero = 16, Partido = Partidos.PSTU, FotoCandidato = "Assets/Img_Candidatos/hertz.png" },
            new CandidatoModel { Nome = "Edmilson Costa", Cargo = "Presidente", Numero = 21, Partido = Partidos.PCB, FotoCandidato = "Assets/Img_Candidatos/edmilson.png" },
            new CandidatoModel { Nome = "Clariana Barão", Cargo = "Presidente", Numero = 27, Partido = Partidos.DC, FotoCandidato = "Assets/Img_Candidatos/clariana.png" },
            new CandidatoModel { Nome = "Rui Costa Pimenta", Cargo = "Presidente", Numero = 29, Partido = Partidos.PCO, FotoCandidato = "Assets/Img_Candidatos/rui.png" },
            new CandidatoModel { Nome = "Romeu Zema", Cargo = "Presidente", Numero = 30, Partido = Partidos.Novo, FotoCandidato = "Assets/Img_Candidatos/zema.png" },
            new CandidatoModel { Nome = "Veterinário Wilson Grassi", Cargo = "Presidente", Numero = 35, Partido = Partidos.Democrata, FotoCandidato = "Assets/Img_Candidatos/wilson.png" },
            new CandidatoModel { Nome = "Ronaldo Caiado", Cargo = "Presidente", Numero = 55, Partido = Partidos.PSD, FotoCandidato = "Assets/Img_Candidatos/ronaldo.png" },
            new CandidatoModel { Nome = "Samara Martins", Cargo = "Presidente", Numero = 80, Partido = Partidos.UP, FotoCandidato = "Assets/Img_Candidatos/samara.png" },
            new CandidatoModel { Nome = "Augusto Cury", Cargo = "Presidente", Numero = 70, Partido = Partidos.Avante, FotoCandidato = "Assets/Img_Candidatos/cury.png" },
            new CandidatoModel { Nome = "Nulo", Cargo = "Presidente", Numero = -1, Partido = Partidos.SemPartido, FotoCandidato = "Assets/Img_Candidatos/nulo.png"},
            new CandidatoModel { Nome = "Branco", Cargo = "Presidente", Numero = -1, Partido = Partidos.SemPartido, FotoCandidato = "Assets/Img_Candidatos/branco.png"},
            new CandidatoModel { Nome = "Tung Tung Tung Sahur God", Cargo = "Presidente", Numero = 76, Partido = Partidos.SemPartido, FotoCandidato = "Assets/Img_Candidatos/branco.png"},
            new CandidatoModel { Nome = "67 Kid", Cargo = "Presidente", Numero = 67, Partido = Partidos.SemPartido, FotoCandidato = "Assets/Img_Candidatos/nulo.png"}
        ];
        CandidatoModel[] candidatosGovernador =
        [   
            new CandidatoModel { Nome = "Nulo", Cargo = "Governador", Numero = -1, Partido = Partidos.SemPartido, FotoCandidato = "Assets/Img_Candidatos/nulo.png"},
            new CandidatoModel { Nome = "Branco", Cargo = "Governador", Numero = -1, Partido = Partidos.SemPartido, FotoCandidato = "Assets/Img_Candidatos/branco.png"},
        ];
        CandidatoModel[] candidatosSenador =
        [   
            new CandidatoModel { Nome = "Nulo", Cargo = "Senador", Numero = -1, Partido = Partidos.SemPartido, FotoCandidato = "Assets/Img_Candidatos/nulo.png"},
            new CandidatoModel { Nome = "Branco", Cargo = "Senador", Numero = -1, Partido = Partidos.SemPartido, FotoCandidato = "Assets/Img_Candidatos/branco.png"},
        ];
        CandidatoModel[] candidatosDpEstadual =
        [   
            new CandidatoModel { Nome = "Nulo", Cargo = "DeputadoEstadual", Numero = -1, Partido = Partidos.SemPartido, FotoCandidato = "Assets/Img_Candidatos/nulo.png"},
            new CandidatoModel { Nome = "Branco", Cargo = "DeputadoEstadual", Numero = -1, Partido = Partidos.SemPartido, FotoCandidato = "Assets/Img_Candidatos/branco.png"},
        ];
        CandidatoModel[] candidatosDpFederal =
        [   
            new CandidatoModel { Nome = "Nulo", Cargo = "DeputadoFederal", Numero = -1, Partido = Partidos.SemPartido, FotoCandidato = "Assets/Img_Candidatos/nulo.png"},
            new CandidatoModel { Nome = "Branco", Cargo = "DeputadoFederal", Numero = -1, Partido = Partidos.SemPartido, FotoCandidato = "Assets/Img_Candidatos/branco.png"},
        ];
        context.Candidatos.AddRange(candidatosPresidente);
        context.Candidatos.AddRange(candidatosGovernador);
        context.Candidatos.AddRange(candidatosSenador);
        context.Candidatos.AddRange(candidatosDpFederal);
        context.Candidatos.AddRange(candidatosDpEstadual);
        context.SaveChanges();
    }
}