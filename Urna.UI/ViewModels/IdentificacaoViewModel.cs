using CommunityToolkit.Mvvm.Input;
using Urna.UI.Services;
namespace Urna.UI.ViewModels;

public partial class IdentificacaoViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;
    private readonly VotacaoViewModel _votacaoViewModel;

    public IdentificacaoViewModel(INavigationService navigationService, VotacaoViewModel votacaoViewModel)
    {
        _navigationService = navigationService;
        _votacaoViewModel = votacaoViewModel;
    }

    [RelayCommand]
    private void VerificarMatricula()
    {
        _navigationService.CurrentViewModel = _votacaoViewModel;
    }
}