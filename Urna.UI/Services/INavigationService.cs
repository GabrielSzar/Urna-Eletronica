using Urna.UI.ViewModels;
namespace Urna.UI.Services;

public interface INavigationService
{
    ViewModelBase CurrentViewModel { get; set; }
    void NavigateTo<T>() where T : ViewModelBase;

}