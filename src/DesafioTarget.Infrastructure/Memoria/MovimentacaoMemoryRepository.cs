using DesafioTarget.Domain.Estoque;

namespace DesafioTarget.Infrastructure.Memoria;

public sealed class MovimentacaoMemoryRepository : IMovimentacaoRepository
{
    private readonly List<Movimentacao> _movimentacoes = [];
    private int _ultimoNumero;

    public int GerarProximoNumero() => Interlocked.Increment(ref _ultimoNumero);

    public void Adicionar(Movimentacao movimentacao) => _movimentacoes.Add(movimentacao);

    public IReadOnlyList<Movimentacao> ListarPorProduto(int codigoProduto) =>
        _movimentacoes
            .Where(m => m.CodigoProduto == codigoProduto)
            .OrderBy(m => m.Numero)
            .ToList();
}
