using CommunityToolkit.Mvvm.Input;
using Urna.UI.Services;
namespace Urna.UI.ViewModels;

public partial class IdentificacaoViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;

    public IdentificacaoViewModel(INavigationService navigationService, VotacaoViewModel votacaoViewModel)
    {
        _navigationService = navigationService;
    }

    [RelayCommand]
    private void VerificarMatricula()
    {
        _navigationService.NavigateTo<VotacaoViewModel>();
    }
}