using System;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using Urna.UI.Services;

namespace Urna.UI.ViewModels;

public partial class MainWindowViewModel : ViewModelBase, INavigationService
{
    [ObservableProperty] private ViewModelBase _currentViewModel = null!;
    private readonly IServiceProvider _serviceProvider;
    
    public MainWindowViewModel(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    public void NavigateTo<T>() where T : ViewModelBase
    {
        CurrentViewModel = _serviceProvider.GetRequiredService<T>();
    }
}