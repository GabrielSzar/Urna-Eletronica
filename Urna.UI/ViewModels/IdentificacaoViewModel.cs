using System.Linq;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Urna.UI.Services;
namespace Urna.UI.ViewModels;

public partial class IdentificacaoViewModel : ViewModelBase
{
    [ObservableProperty] public string mensagemMatricula = string.Empty;
    [ObservableProperty] public IBrush corMensagemMatricula = Brushes.Transparent;
    private readonly INavigationService _navigationService;

    public IdentificacaoViewModel(INavigationService navigationService, VotacaoViewModel votacaoViewModel)
    {
        _navigationService = navigationService;
    }

    [RelayCommand]
    public void ValidarMatricula(string matricula)
    {
        if (string.IsNullOrEmpty(matricula))
        {
            MensagemMatricula = "A matrícula deve ter 8 caracteres";
            CorMensagemMatricula = Brushes.Red;
            return;
        }

        if (matricula.Length > 8)
        {
            MensagemMatricula = "A matrícula deve ter somente 8 caracteres";
            CorMensagemMatricula = Brushes.Red;
            return;
        }

        if (matricula.Length < 8)
        {
            MensagemMatricula = "A matrícula deve ter 8 caracteres";
            CorMensagemMatricula = Brushes.Red;
            return;
        }

        if (!matricula.All(char.IsDigit))
        {
            MensagemMatricula = "A matrícula deve somente números";
            CorMensagemMatricula = Brushes.Red;
            return;
        }

        if (!matricula.StartsWith("1250"))
        {
            MensagemMatricula = "Matrícula inválida!";
            CorMensagemMatricula = Brushes.Red;
            return;
        }

        MensagemMatricula = "Matrícula válida!";
        CorMensagemMatricula = SolidColorBrush.Parse("#415158");
        _navigationService.NavigateTo<VotacaoViewModel>();
    }
}