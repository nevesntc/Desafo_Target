using DesafioTarget.Domain.Estoque;

namespace DesafioTarget.Infrastructure.Json;

/// <summary>
/// Carrega o estoque inicial do arquivo JSON e mantém as alterações em memória
/// durante a execução do programa.
/// </summary>
public sealed class ProdutoJsonRepository : IProdutoRepository
{
    private readonly Dictionary<int, Produto> _produtos;

    public ProdutoJsonRepository(string caminhoArquivo)
    {
        var arquivo = JsonFileReader.Ler<ArquivoEstoque>(caminhoArquivo);

        _produtos = arquivo.Estoque
            .Select(p => new Produto(p.CodigoProduto, p.DescricaoProduto, p.Estoque))
            .ToDictionary(p => p.Codigo);
    }

    public IReadOnlyList<Produto> ListarTodos() =>
        _produtos.Values.OrderBy(p => p.Codigo).ToList();

    public Produto? ObterPorCodigo(int codigo) =>
        _produtos.GetValueOrDefault(codigo);

    public void Atualizar(Produto produto) =>
        _produtos[produto.Codigo] = produto;

    private sealed record ArquivoEstoque(List<ProdutoDto> Estoque);

    private sealed record ProdutoDto(int CodigoProduto, string DescricaoProduto, int Estoque);
}
