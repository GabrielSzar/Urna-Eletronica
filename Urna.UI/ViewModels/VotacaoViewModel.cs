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
using Urna.UI.Models;
using Urna.UI.Services;

namespace Urna.UI.ViewModels;

public partial class VotacaoViewModel : ViewModelBase
{   
    private readonly ICandidatoRepository _candidatoRepository;
    private readonly IEleitorRepository _eleitorRepository;
    private readonly IApuracaoService _apuracaoService;
    private readonly IAudioService _audioService;
    private readonly INavigationService _navigationService;
    private readonly ResultadoViewModel _resultadoViewModel;
    
    public VotacaoViewModel(ICandidatoRepository candidatoRepository, IEleitorRepository eleitorRepository, IApuracaoService apuracaoService, IAudioService audioService, INavigationService navigationService, ResultadoViewModel resultadoViewModel)
    {
        _candidatoRepository = candidatoRepository;
        _eleitorRepository = eleitorRepository;
        _apuracaoService = apuracaoService;
        _audioService = audioService;
        _navigationService = navigationService;
        _resultadoViewModel = resultadoViewModel;
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
    public IEnumerable<char> QuadradosVisuais => 
        NumeroDigitado.PadRight(MaxDigitos, ' ');
    private int MaxDigitos => CargosLista[IndiceCargoAtual].Digitos; 
    private string? CargoAtual => CargosLista[IndiceCargoAtual].Nome;
    private string _votoPrimeiroSenador = string.Empty;
    private bool _branco = false;
    //Faltando pegar a lista de cantidatos do banco de dados, placeholdes abaixo
    [ObservableProperty] private string? _escolhidoNome = "Lula";
    [ObservableProperty] private string? _escolhidoPartido = "PT";
    [ObservableProperty] private Bitmap _escolhidoFoto = new(AssetLoader.Open(new Uri("avares://Urna.UI/Assets/Img_Candidatos/lulaUrna.jpeg")));
    [RelayCommand]
    private void AdicionarNumero(string digito)
    {   
        // Aparecer nome do cantidado, e partido na tela, com foto
        // Quando o numero chegar no limite
        Limpar();
        if (NumeroDigitado.Length >= MaxDigitos)
            return;
        NumeroDigitado += digito;
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
    {   // Adicionar som de confirmar voto
        if (_branco)
        {
            await AvancarVoto();
            _branco = false;
            return;
        }
        if (NumeroDigitado.Length != MaxDigitos) 
            return;
        
        if (NumeroDigitado != _votoPrimeiroSenador)
        {   
            await _audioService.TocarAudio("Confirmar");
            // Convert.ToInt32(NumeroDigitado);
            // CargoAtual
            // numero e cargo vão para o banco de dados
            // apenas se o seu número não for igual ao
            // do senador anterior, aqui ‘string’.empty
            // não cai porque voto branco retorna lá em cima
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
        Aviso = string.Empty;
        AvisosMenores = string.Empty;
    }
    private async Task FimVotacao()
    {   
        
        await _audioService.TocarAudio("Fim");
        // Adicionar som de fim de votação voto
        // Chamar a contagem de votos
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
