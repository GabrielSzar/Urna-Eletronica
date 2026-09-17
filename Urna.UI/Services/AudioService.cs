using System;
using System.IO;
using System.Threading.Tasks;
using NetCoreAudio;

namespace Urna.UI.Services;

public class AudioService : IAudioService
{
    public async Task TocarAudio(string tipo)
    {   
        Player audioPlayer = new Player();
        var caminhoAudio = Path.Combine(AppContext.BaseDirectory, "Assets", "Audio", $"{tipo}.mp3");
        try
        {
            await audioPlayer.Play(caminhoAudio);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao tocar áudio: {ex.Message}");
        }
    }
}