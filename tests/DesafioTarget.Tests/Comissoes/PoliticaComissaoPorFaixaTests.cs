using DesafioTarget.Domain.Comissoes;

namespace DesafioTarget.Tests.Comissoes;

public class PoliticaComissaoPorFaixaTests
{
    private readonly PoliticaComissaoPorFaixa _politica = PoliticaComissaoPorFaixa.Padrao();

    [Theory]
    [InlineData(0, 0)]
    [InlineData(75.30, 0)]
    [InlineData(99.99, 0)]
    public void Venda_abaixo_de_100_nao_gera_comissao(decimal valor, decimal esperado)
    {
        Assert.Equal(esperado, _politica.CalcularComissao(valor));
    }

    [Theory]
    [InlineData(100, 1)]
    [InlineData(250.30, 2.50)]
    [InlineData(499.99, 5)]
    public void Venda_entre_100_e_500_gera_1_porcento(decimal valor, decimal esperado)
    {
        Assert.Equal(esperado, _politica.CalcularComissao(valor));
    }

    [Theory]
    [InlineData(500, 25)]
    [InlineData(1200.50, 60.03)]
    [InlineData(2100.40, 105.02)]
    public void Venda_a_partir_de_500_gera_5_porcento(decimal valor, decimal esperado)
    {
        Assert.Equal(esperado, _politica.CalcularComissao(valor));
    }

    [Fact]
    public void Venda_negativa_nao_eh_aceita()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _politica.CalcularComissao(-1));
    }
}
