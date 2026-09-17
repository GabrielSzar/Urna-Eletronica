using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using Urna.Business;
using Urna.Core.Interfaces;
using Urna.Data.Repositories;
using Urna.UI.Services;
using Urna.UI.ViewModels;
using Urna.UI.Views;

namespace Urna.UI;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {   
        var services = new ServiceCollection();
        services.AddSingleton<IEleitorRepository, EleitorRepository>();
        services.AddSingleton<ICandidatoRepository, CandidatoRepository>();
        services.AddSingleton<IApuracaoService, ApuracaoService>();
        services.AddSingleton<IAudioService, AudioService>();
        services.AddSingleton<INavigationService>(sp => sp.GetRequiredService<MainWindowViewModel>());
        services.AddSingleton<MainWindowViewModel>();
        services.AddTransient<IdentificacaoViewModel>();
        services.AddTransient<VotacaoViewModel>();
        services.AddTransient<ResultadoViewModel>();
        var provider = services.BuildServiceProvider();
        
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var mainWindowViewModel = provider.GetRequiredService<MainWindowViewModel>();
            mainWindowViewModel.CurrentViewModel = provider.GetRequiredService<IdentificacaoViewModel>();
            desktop.MainWindow = new MainWindow { DataContext = mainWindowViewModel };
        }
        base.OnFrameworkInitializationCompleted();
    }
}