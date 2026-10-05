using System.Text.Json;

namespace DesafioTarget.Infrastructure.Json;

internal static class JsonFileReader
{
    private static readonly JsonSerializerOptions Opcoes = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public static T Ler<T>(string caminhoArquivo)
    {
        if (!File.Exists(caminhoArquivo))
            throw new FileNotFoundException($"Arquivo de dados não encontrado: {caminhoArquivo}", caminhoArquivo);

        using var stream = File.OpenRead(caminhoArquivo);

        return JsonSerializer.Deserialize<T>(stream, Opcoes)
               ?? throw new InvalidDataException($"Não foi possível ler o arquivo {caminhoArquivo}.");
    }
}
