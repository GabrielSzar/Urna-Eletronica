using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Animation.Easings;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Urna.Core.Interfaces;
using Urna.Data.Repositories;
using Urna.UI.Services;
namespace Urna.UI.ViewModels;

public partial class IdentificacaoViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;
    private readonly ICpfService _cpfService;
    private readonly EleitorRepository _eleitorRepository;

    public IdentificacaoViewModel(INavigationService navigationService, ICpfService cpfService, EleitorRepository eleitorRepository)
    {
        _navigationService = navigationService;
        _cpfService = cpfService;
        _eleitorRepository = eleitorRepository;
    }
    [ObservableProperty] private string _mensagemCpf = string.Empty;
    [ObservableProperty] private IBrush _corMensagemCpf = Brushes.Transparent;

    [RelayCommand]
    public async Task VerificarCpf(string cpf)
    {
        if (cpf.Length != 11)
        {
            MensagemCpf = "O Cpf deve ter 11 Digitos!";
            CorMensagemCpf = Brushes.Red;
            return;
        }
        if (!cpf.All(char.IsDigit))
        {
            MensagemCpf = "A matrícula deve somente números!";
            CorMensagemCpf = Brushes.Red;
            return;
        }

        if (_cpfService.ValidarCpf(cpf) == false)
        {
            MensagemCpf = "Cpf Invalido!";
            CorMensagemCpf = Brushes.Red;
            return;
        }

        var eleitor = _eleitorRepository.BuscarPorCpf(cpf);

        if (eleitor is null)
        {
            _eleitorRepository.AdicionarEleitor(cpf);
            eleitor = _eleitorRepository.BuscarPorCpf(cpf);
        }

        if (eleitor.JaVotou)
        {
            MensagemCpf = "Não é possível votar mais de 1 vez!";
            CorMensagemCpf = Brushes.Red;
            return;
        }

        CorMensagemCpf = SolidColorBrush.Parse("#415158");
        MensagemCpf = "Carregando...";

        _eleitorRepository.MarcarComoVotado(cpf);

        await Task.Delay(2000);
        _navigationService.NavigateTo<VotacaoViewModel>();
    }
}