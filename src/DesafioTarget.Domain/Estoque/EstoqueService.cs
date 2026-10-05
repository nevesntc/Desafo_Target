using DesafioTarget.Domain.Comum;

namespace DesafioTarget.Domain.Estoque;

public sealed class EstoqueService
{
    private readonly IProdutoRepository _produtos;
    private readonly IMovimentacaoRepository _movimentacoes;
    private readonly TimeProvider _relogio;

    public EstoqueService(
        IProdutoRepository produtos,
        IMovimentacaoRepository movimentacoes,
        TimeProvider relogio)
    {
        _produtos = produtos;
        _movimentacoes = movimentacoes;
        _relogio = relogio;
    }

    public IReadOnlyList<Produto> ListarProdutos() => _produtos.ListarTodos();

    public IReadOnlyList<Movimentacao> ListarMovimentacoes(int codigoProduto) =>
        _movimentacoes.ListarPorProduto(codigoProduto);

    public Movimentacao Movimentar(NovaMovimentacao dados)
    {
        if (string.IsNullOrWhiteSpace(dados.Descricao))
            throw new DomainException("Informe uma descrição para a movimentação.");

        var produto = _produtos.ObterPorCodigo(dados.CodigoProduto)
                      ?? throw new ProdutoNaoEncontradoException(dados.CodigoProduto);

        switch (dados.Tipo)
        {
            case TipoMovimentacao.Entrada:
                produto.DarEntrada(dados.Quantidade);
                break;
            case TipoMovimentacao.Saida:
                produto.DarSaida(dados.Quantidade);
                break;
            default:
                throw new DomainException("Tipo de movimentação inválido.");
        }

        _produtos.Atualizar(produto);

        var movimentacao = new Movimentacao(
            Numero: _movimentacoes.GerarProximoNumero(),
            CodigoProduto: produto.Codigo,
            Tipo: dados.Tipo,
            Quantidade: dados.Quantidade,
            Descricao: dados.Descricao.Trim(),
            DataHora: _relogio.GetLocalNow().DateTime,
            EstoqueFinal: produto.QuantidadeEmEstoque);

        _movimentacoes.Adicionar(movimentacao);

        return movimentacao;
    }
}
