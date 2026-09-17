using System.Threading.Tasks;

namespace Urna.UI.Services;

public interface IAudioService
{
    public Task TocarAudio(string tipo);
}