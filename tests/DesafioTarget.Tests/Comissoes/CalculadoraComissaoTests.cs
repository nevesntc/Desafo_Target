using DesafioTarget.Domain.Comissoes;

namespace DesafioTarget.Tests.Comissoes;

public class CalculadoraComissaoTests
{
    private readonly CalculadoraComissao _calculadora = new(PoliticaComissaoPorFaixa.Padrao());

    [Fact]
    public void Agrupa_as_vendas_e_soma_a_comissao_de_cada_vendedor()
    {
        var vendas = new[]
        {
            new Venda("Ana", 1000m),   // 50,00
            new Venda("Ana", 200m),    //  2,00
            new Venda("Ana", 50m),     //  0,00
            new Venda("Bruno", 300m)   //  3,00
        };

        var resultado = _calculadora.CalcularPorVendedor(vendas);

        var ana = Assert.Single(resultado, c => c.Vendedor == "Ana");
        Assert.Equal(3, ana.QuantidadeVendas);
        Assert.Equal(1250m, ana.TotalVendido);
        Assert.Equal(52m, ana.TotalComissao);

        var bruno = Assert.Single(resultado, c => c.Vendedor == "Bruno");
        Assert.Equal(3m, bruno.TotalComissao);
    }

    [Fact]
    public void Ordena_do_maior_para_o_menor_comissionado()
    {
        var vendas = new[]
        {
            new Venda("Carlos", 150m),
            new Venda("Ana", 900m)
        };

        var resultado = _calculadora.CalcularPorVendedor(vendas);

        Assert.Equal(["Ana", "Carlos"], resultado.Select(c => c.Vendedor));
    }

    [Fact]
    public void Sem_vendas_retorna_lista_vazia()
    {
        Assert.Empty(_calculadora.CalcularPorVendedor([]));
    }
}
