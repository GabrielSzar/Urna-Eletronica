using CommunityToolkit.Mvvm.Input;
using Urna.UI.Services;

namespace Urna.UI.ViewModels;
        
public partial class ResultadoViewModel : ViewModelBase
{
        private readonly INavigationService _navigationService;
        public ResultadoViewModel(INavigationService navigationService)
        {
                _navigationService = navigationService;
        }

        [RelayCommand]
        private void ResultadoBotao(string onde)
        {
                switch (onde)
                {
                        case "Finalizar":
                                return;
                        case "Novamente":
                                _navigationService.NavigateTo<IdentificacaoViewModel>();
                                break;
                }
        }
}