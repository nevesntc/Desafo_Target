using DesafioTarget.Domain.Estoque;

namespace DesafioTarget.Tests.Fakes;

internal sealed class ProdutoMemoryRepository : IProdutoRepository
{
    private readonly Dictionary<int, Produto> _produtos;

    public ProdutoMemoryRepository(params Produto[] produtos)
    {
        _produtos = produtos.ToDictionary(p => p.Codigo);
    }

    public IReadOnlyList<Produto> ListarTodos() => _produtos.Values.ToList();

    public Produto? ObterPorCodigo(int codigo) => _produtos.GetValueOrDefault(codigo);

    public void Atualizar(Produto produto) => _produtos[produto.Codigo] = produto;
}
