using DesafioTarget.Domain.Comissoes;

namespace DesafioTarget.Infrastructure.Json;

public sealed class VendaJsonRepository : IVendaRepository
{
    private readonly string _caminhoArquivo;

    public VendaJsonRepository(string caminhoArquivo)
    {
        _caminhoArquivo = caminhoArquivo;
    }

    public IReadOnlyList<Venda> ListarTodas()
    {
        var arquivo = JsonFileReader.Ler<ArquivoVendas>(_caminhoArquivo);

        return arquivo.Vendas
            .Select(v => new Venda(v.Vendedor, v.Valor))
            .ToList();
    }

    private sealed record ArquivoVendas(List<VendaDto> Vendas);

    private sealed record VendaDto(string Vendedor, decimal Valor);
}
