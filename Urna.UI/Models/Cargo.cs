using CommunityToolkit.Mvvm.ComponentModel;

namespace Urna.UI.Models;

public partial class Cargo : ObservableObject
{
    public string? Nome { get; set; }
    
    [ObservableProperty] private bool _selecionado;
    public int Digitos { get;}

    public Cargo(string nome, bool selecionado, int digitos)
    { 
     Nome = nome;
     Selecionado  = selecionado; 
     Digitos = digitos;
    }
}