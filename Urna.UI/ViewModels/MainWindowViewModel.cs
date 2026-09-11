using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using Avalonia.Data.Converters;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Urna.UI.Models;

namespace Urna.UI.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{   
    [ObservableProperty] private ObservableCollection<Cargo> _cargosLista = 
        [new ("DeputadoFederal", true, 4), 
         new ("DeputadoEstadual", false, 5),
         new ("Senador", false, 3),
         new ("Senador", false, 3),
         new ("Governador", false, 2),
         new ("Presidente", false, 2)];
    public IEnumerable<char> QuadradosVisuais => NumeroDigitado.PadRight(MaxDigitos, ' ');
    [ObservableProperty] 
    [NotifyPropertyChangedFor(nameof(QuadradosVisuais))]
    //Qunado IndiceCArgoAtual Mudar Atualize QuadrosVisuais
    private int _indiceCargoAtual = 0;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(QuadradosVisuais))]
    private string _numeroDigitado = string.Empty;
    private int MaxDigitos => CargosLista[IndiceCargoAtual].Digitos; 
    private string? CargoAtual => CargosLista[IndiceCargoAtual].Nome;
    private string _votoPrimeiroSenador = string.Empty;
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
        // Função de Aparecer para confirmar
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
    {   // caso o voto seja o primeiro senador
        _votoPrimeiroSenador = IndiceCargoAtual == 3 ? NumeroDigitado : string.Empty; 
        if (IndiceCargoAtual > CargosLista.Count - 2)
        {
            FimVotacao();
        }
        else
        {
            IndiceCargoAtual += 1;
        }  
        NumeroDigitado = string.Empty;
        CargoASerVotado();
    }
    

    private void FimVotacao()
    {
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

public class SelecionadoParaBrushConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        bool selecionado = (bool)value!;
        return selecionado ? Brushes.Black : Brushes.White;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
