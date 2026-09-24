using Microsoft.Extensions.Configuration;

namespace Urna.Data.Database;

public class DatabaseConfig
{
    public static string GetConnString()
    {   // Removi o appsettigns.json porque tava dando muito erro de pegar o caminho do banco
        // e tava criando varios arquivos diferentes
        var caminhoAbsoluto = Path.Combine(AppContext.BaseDirectory, "UrnaBanco.db");
        return $"Data Source={caminhoAbsoluto}";
    }
}