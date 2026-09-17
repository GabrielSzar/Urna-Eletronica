using CommunityToolkit.Mvvm.ComponentModel;
using Urna.UI.Services;

namespace Urna.UI.ViewModels;

public partial class MainWindowViewModel : ViewModelBase, INavigationService
{
    [ObservableProperty] private ViewModelBase _currentViewModel = null!;
}