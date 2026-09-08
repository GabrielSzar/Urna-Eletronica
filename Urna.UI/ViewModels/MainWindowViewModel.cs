using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Urna.UI.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private static readonly string[] OrdemCargos = 
        ["DeputadoFederal", "DeputadoEstadual", "Senador", "Senador", "Governador", "Presidente"];

    private static readonly Dictionary<string, int> DigitosPorCargo = new()
    {
        { "DeputadoFederal", 4 },
        { "DeputadoEstadual", 5 },
        { "Senador", 3 },
        { "Governador", 2 },
        { "Presidente", 2 }
    };

    private string _votoPrimeiroSenador =string.Empty;
    [ObservableProperty] 
    private int _indiceCargoAtual = 0;
    [ObservableProperty]
    private string _numeroDigitado = string.Empty;
    [ObservableProperty]
    private int _maxDigitos = DigitosPorCargo[OrdemCargos[0]]; 
    private string _cargoAtual => OrdemCargos[IndiceCargoAtual];
    partial void OnIndiceCargoAtualChanged(int value)
    {
        OnPropertyChanged(nameof(_cargoAtual));
        MaxDigitos = DigitosPorCargo[_cargoAtual];
    }
    [RelayCommand]
    private void AdicionarNumero(string digito)
    {   
        if (NumeroDigitado.Length >= MaxDigitos)
            return;
        NumeroDigitado += digito;
    }

    [RelayCommand]
    private void Branco()
    {
        AvancarVoto();
    }
    [RelayCommand]
    private void Corrige()
    {
        NumeroDigitado = string.Empty;      
    }
    [RelayCommand]
    private void ConfirmarVoto()
    {
        if (NumeroDigitado.Length != MaxDigitos)
            return;
        var numero = Convert.ToInt32(NumeroDigitado);
        // var candidatos = _candidatoRepository.ListarPorCargo(CargoAtual);
        // var tipo = _apuracaoService.ClassificarVoto(numero, candidatos,IdsEscolhidosNoCargoAtual);
        // _candidatoRepository.RegistrarVoto(numero, tipo);
        AvancarVoto();
    }

    private void AvancarVoto()
    {   
        _votoPrimeiroSenador = IndiceCargoAtual == 3 ? NumeroDigitado : string.Empty; // caso o voto seja o primeiro senador
        if (IndiceCargoAtual > OrdemCargos.Length - 1)
        {
            FimVotacao();
        }
        else
        {
            IndiceCargoAtual += 1;
        }  
        NumeroDigitado = string.Empty;
        MaxDigitos = DigitosPorCargo[_cargoAtual];
    }

    private void FimVotacao()
    {
        IndiceCargoAtual = 0;
    }
    public IEnumerable<char> QuadradosVisuais =>
        NumeroDigitado.PadRight(MaxDigitos, ' ');
    partial void OnNumeroDigitadoChanged(string value) => OnPropertyChanged(nameof(QuadradosVisuais));
    partial void OnMaxDigitosChanged(int value) => OnPropertyChanged(nameof(QuadradosVisuais));
}
