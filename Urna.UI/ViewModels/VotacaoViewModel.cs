using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Urna.Core.Interfaces;
using Urna.Core.Models;
using Urna.UI.Models;
using Urna.UI.Services;

namespace Urna.UI.ViewModels;

public partial class VotacaoViewModel : ViewModelBase
{   
    private readonly ICandidatoRepository _candidatoRepository;
    private readonly IAudioService _audioService;
    private readonly INavigationService _navigationService;
    
    public VotacaoViewModel(ICandidatoRepository candidatoRepository, IAudioService audioService, INavigationService navigationService)
    {
        _candidatoRepository = candidatoRepository;
        _audioService = audioService;
        _navigationService = navigationService;
    }
    [ObservableProperty] private ObservableCollection<Cargo> _cargosLista = 
        [new ("DeputadoFederal", true, 4), 
         new ("DeputadoEstadual", false, 5),
         new ("Senador", false, 3),
         new ("Senador", false, 3),
         new ("Governador", false, 2),
         new ("Presidente", false, 2)];
    [ObservableProperty] 
    [NotifyPropertyChangedFor(nameof(QuadradosVisuais))]
    private int _indiceCargoAtual = 0;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(QuadradosVisuais))]
    private string _numeroDigitado = string.Empty;
    [ObservableProperty] private string? _aviso = string.Empty;
    [ObservableProperty] private string? _avisosMenores = string.Empty;
    [ObservableProperty] private string? _escolhidoNome = string.Empty;
    [ObservableProperty] private string? _escolhidoPartido = string.Empty;
    [ObservableProperty] private Bitmap? _escolhidoFoto = null;
    public IEnumerable<char> QuadradosVisuais => 
        NumeroDigitado.PadRight(MaxDigitos, ' ');
    private int MaxDigitos => CargosLista[IndiceCargoAtual].Digitos; 
    private string? CargoAtual => CargosLista[IndiceCargoAtual].Nome;
    private string _votoPrimeiroSenador = string.Empty;
    private CandidatoModel? _candidatoEscolhido = new CandidatoModel();
    private bool _branco = false;
    private bool _nulo = false;
    
    [RelayCommand]
    private void AdicionarNumero(string digito)
    {   
        Limpar();
        if (NumeroDigitado.Length >= MaxDigitos)
        {   
            Aviso = "VOTO NULO";
            AvisosMenores = "CONFIRMA para CONFIRMAR este voto\n" +
                            "CORRIGE para REINICIAR este voto";
            return;
        }
        NumeroDigitado += digito;
        if (NumeroDigitado.Length == MaxDigitos)
        {
            var candidatosLista = _candidatoRepository.ListarTodos();
            var candidato = candidatosLista.FirstOrDefault(c => c.Numero == Convert.ToInt32(NumeroDigitado) && c.Cargo == CargoAtual);
            if ( candidato == null)
            {
                _candidatoEscolhido = candidatosLista.FirstOrDefault(c => c.Nome == "Nulo" && c.Cargo == CargoAtual);
                if (_candidatoEscolhido == null)
                {
                    Console.WriteLine($"Candidato NULO não encontrado, Cargo: {CargoAtual}");
                }
                _nulo = true;
                Aviso = "VOTO NULO";
                AvisosMenores = "CONFIRMA para CONFIRMAR este voto\n" +
                                "CORRIGE para REINICIAR este voto";
                return;
            }

            _candidatoEscolhido = candidato;
            EscolhidoNome = candidato.Nome;
            EscolhidoPartido = candidato.Partido.ToString();
            EscolhidoFoto = new Bitmap(AssetLoader.Open(new Uri($"avares://Urna.UI/{candidato.FotoCandidato}")));

        }
    }

    [RelayCommand]
    private async Task Branco()
    {
        if (NumeroDigitado == string.Empty && _branco)
        {
            await _audioService.TocarAudio("Alerta");
        }
        NumeroDigitado = string.Empty;
        _branco = true;
        Aviso = "VOTO EM BRANCO";
        AvisosMenores = "CONFIRMA para CONFIRMAR este voto\n" +
                        "CORRIGE para REINICIAR este voto";
    }
    [RelayCommand]
    private void Corrige()
    {
        NumeroDigitado = string.Empty;
        Limpar();
    }
    [RelayCommand]
    private async Task ConfirmarVoto()
    {   
        var candidatosLista = _candidatoRepository.ListarTodos();
        if (_branco)
        {   
            _candidatoEscolhido = candidatosLista.First(c => c.Nome == "Branco" && c.Cargo == CargoAtual);
            Console.WriteLine($"Adicionando Canditado {_candidatoEscolhido.Id}, {_candidatoEscolhido.Nome}, {_candidatoEscolhido.Cargo} {_candidatoEscolhido.NumVotos}, {_candidatoEscolhido.Partido} {_candidatoEscolhido.Partido}");
            _candidatoRepository.RegistrarVoto(_candidatoEscolhido.Id);
            await _audioService.TocarAudio("Confirmar");
            await AvancarVoto();
            _branco = false;
            return;
        }
        if (NumeroDigitado.Length != MaxDigitos) 
            return;
        
        if (NumeroDigitado != _votoPrimeiroSenador)
        {   
            Console.WriteLine($"Adicionando Canditado ID: {_candidatoEscolhido.Id}" +
                              $"\nNome: {_candidatoEscolhido.Nome}" +
                              $"\nCargo:  {_candidatoEscolhido.Cargo}" +
                              $"\nNumero de Votos: {_candidatoEscolhido.NumVotos}" +
                              $"\nPartido {_candidatoEscolhido.Partido}" +
                              $"\nNumero: {_candidatoEscolhido.Numero}\n");
            _candidatoRepository.RegistrarVoto(_candidatoEscolhido.Id); // Adiciona no Banco
            await _audioService.TocarAudio("Confirmar");
        }
        await AvancarVoto();
    }
    
    private async Task AvancarVoto()
    {   // caso o voto seja o primeiro senador 
        // [0] DeputadoFederal [1] DeputadoEstadual [2] Senador
        _votoPrimeiroSenador = IndiceCargoAtual == 2 ? NumeroDigitado : string.Empty; 
        if (IndiceCargoAtual >= CargosLista.Count - 1)
        {
            await FimVotacao();
        }
        else
        {
            IndiceCargoAtual += 1;
        }  
        NumeroDigitado = string.Empty;
        CargoASerVotado();
        Limpar();
    }

    private void Limpar()
    {
        _branco = false;
        _nulo = false;
        EscolhidoFoto = null;
        EscolhidoNome = string.Empty;
        EscolhidoPartido = string.Empty;
        Aviso = string.Empty;
        AvisosMenores = string.Empty;
        
    }
    private async Task FimVotacao()
    {   
        
        await _audioService.TocarAudio("Fim");
        _navigationService.NavigateTo<ResultadoViewModel>();
        Limpar();
        IndiceCargoAtual = 0;
    }

    private void CargoASerVotado()
    {
        var selecionado = CargosLista.FirstOrDefault(i => i.Selecionado == true);
        if (selecionado == null)
        {
            CargosLista[0].Selecionado = true;
            return;
        }
        var index = CargosLista.IndexOf(selecionado);
        CargosLista[index].Selecionado = false;
        index = index >= CargosLista.Count - 1 ? 0 : index + 1;
        CargosLista[index].Selecionado = true;
    }
    
}
