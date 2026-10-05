using DesafioTarget.Domain.Comum;
using DesafioTarget.Domain.Estoque;
using DesafioTarget.Infrastructure.Memoria;
using DesafioTarget.Tests.Fakes;

namespace DesafioTarget.Tests.Estoque;

public class EstoqueServiceTests
{
    private static readonly DateTime Agora = new(2026, 10, 5, 14, 30, 0);

    private readonly EstoqueService _service = new(
        new ProdutoMemoryRepository(
            new Produto(101, "Caneta Azul", 150),
            new Produto(102, "Caderno Universitário", 75)),
        new MovimentacaoMemoryRepository(),
        new RelogioFixo(Agora));

    [Fact]
    public void Entrada_retorna_o_estoque_final_do_produto()
    {
        var movimentacao = _service.Movimentar(
            new NovaMovimentacao(101, TipoMovimentacao.Entrada, 50, "Compra de fornecedor"));

        Assert.Equal(200, movimentacao.EstoqueFinal);
        Assert.Equal("Compra de fornecedor", movimentacao.Descricao);
        Assert.Equal(Agora, movimentacao.DataHora);
    }

    [Fact]
    public void Saida_retorna_o_estoque_final_do_produto()
    {
        var movimentacao = _service.Movimentar(
            new NovaMovimentacao(102, TipoMovimentacao.Saida, 25, "Venda"));

        Assert.Equal(50, movimentacao.EstoqueFinal);
    }

    [Fact]
    public void Cada_movimentacao_recebe_um_numero_unico()
    {
        var primeira = _service.Movimentar(new NovaMovimentacao(101, TipoMovimentacao.Entrada, 1, "Ajuste"));
        var segunda = _service.Movimentar(new NovaMovimentacao(102, TipoMovimentacao.Entrada, 1, "Ajuste"));
        var terceira = _service.Movimentar(new NovaMovimentacao(101, TipoMovimentacao.Saida, 1, "Venda"));

        Assert.Equal([1, 2, 3], new[] { primeira.Numero, segunda.Numero, terceira.Numero });
        Assert.Equal(2, _service.ListarMovimentacoes(101).Count);
    }

    [Fact]
    public void Movimentacoes_seguidas_acumulam_no_estoque()
    {
        _service.Movimentar(new NovaMovimentacao(101, TipoMovimentacao.Saida, 100, "Venda"));
        var ultima = _service.Movimentar(new NovaMovimentacao(101, TipoMovimentacao.Entrada, 30, "Devolução"));

        Assert.Equal(80, ultima.EstoqueFinal);
    }

    [Fact]
    public void Produto_inexistente_gera_erro()
    {
        Assert.Throws<ProdutoNaoEncontradoException>(() =>
            _service.Movimentar(new NovaMovimentacao(999, TipoMovimentacao.Entrada, 1, "Teste")));
    }

    [Fact]
    public void Descricao_eh_obrigatoria()
    {
        Assert.Throws<DomainException>(() =>
            _service.Movimentar(new NovaMovimentacao(101, TipoMovimentacao.Entrada, 1, "  ")));
    }

    [Fact]
    public void Saida_sem_estoque_nao_registra_movimentacao()
    {
        Assert.Throws<EstoqueInsuficienteException>(() =>
            _service.Movimentar(new NovaMovimentacao(102, TipoMovimentacao.Saida, 100, "Venda")));

        Assert.Empty(_service.ListarMovimentacoes(102));
    }
}
