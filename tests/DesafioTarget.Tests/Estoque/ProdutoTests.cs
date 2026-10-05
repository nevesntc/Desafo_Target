using DesafioTarget.Domain.Comum;
using DesafioTarget.Domain.Estoque;

namespace DesafioTarget.Tests.Estoque;

public class ProdutoTests
{
    [Fact]
    public void Entrada_soma_ao_estoque()
    {
        var produto = new Produto(101, "Caneta Azul", 150);

        produto.DarEntrada(50);

        Assert.Equal(200, produto.QuantidadeEmEstoque);
    }

    [Fact]
    public void Saida_subtrai_do_estoque()
    {
        var produto = new Produto(101, "Caneta Azul", 150);

        produto.DarSaida(150);

        Assert.Equal(0, produto.QuantidadeEmEstoque);
    }

    [Fact]
    public void Saida_maior_que_o_estoque_nao_eh_permitida()
    {
        var produto = new Produto(102, "Caderno Universitário", 75);

        Assert.Throws<EstoqueInsuficienteException>(() => produto.DarSaida(76));
        Assert.Equal(75, produto.QuantidadeEmEstoque);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-5)]
    public void Quantidade_deve_ser_maior_que_zero(int quantidade)
    {
        var produto = new Produto(103, "Borracha Branca", 200);

        Assert.Throws<DomainException>(() => produto.DarEntrada(quantidade));
        Assert.Throws<DomainException>(() => produto.DarSaida(quantidade));
    }
}
