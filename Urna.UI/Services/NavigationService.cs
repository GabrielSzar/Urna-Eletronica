using Urna.UI.ViewModels;

namespace Urna.UI.Services;

public class NavigationService : INavigationService
{
    public ViewModelBase CurrentViewModel { get; set; }
}