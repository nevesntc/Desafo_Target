using DesafioTarget.Domain.Juros;
using DesafioTarget.Tests.Fakes;

namespace DesafioTarget.Tests.Juros;

public class CalculadoraJurosTests
{
    private static readonly DateTime Hoje = new(2026, 10, 5);

    private readonly CalculadoraJuros _calculadora = new(new RelogioFixo(Hoje));

    [Fact]
    public void Aplica_2_e_meio_porcento_por_dia_de_atraso()
    {
        var resultado = _calculadora.Calcular(1000m, new DateOnly(2026, 9, 25));

        Assert.Equal(10, resultado.DiasEmAtraso);
        Assert.Equal(250m, resultado.ValorJuros);
        Assert.Equal(1250m, resultado.ValorAtualizado);
    }

    [Fact]
    public void Um_dia_de_atraso()
    {
        var resultado = _calculadora.Calcular(150.75m, new DateOnly(2026, 10, 4));

        Assert.Equal(1, resultado.DiasEmAtraso);
        Assert.Equal(3.77m, resultado.ValorJuros);
    }

    [Fact]
    public void Vencimento_hoje_nao_gera_juros()
    {
        var resultado = _calculadora.Calcular(500m, new DateOnly(2026, 10, 5));

        Assert.False(resultado.EmAtraso);
        Assert.Equal(0m, resultado.ValorJuros);
    }

    [Fact]
    public void Vencimento_futuro_nao_gera_juros()
    {
        var resultado = _calculadora.Calcular(500m, new DateOnly(2026, 12, 1));

        Assert.Equal(0, resultado.DiasEmAtraso);
        Assert.Equal(500m, resultado.ValorAtualizado);
    }

    [Fact]
    public void Considera_a_virada_de_mes_e_ano()
    {
        var calculadora = new CalculadoraJuros(new RelogioFixo(new DateTime(2026, 1, 2)));

        var resultado = calculadora.Calcular(100m, new DateOnly(2025, 12, 30));

        Assert.Equal(3, resultado.DiasEmAtraso);
        Assert.Equal(7.50m, resultado.ValorJuros);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-10)]
    public void Valor_deve_ser_positivo(decimal valor)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _calculadora.Calcular(valor, new DateOnly(2026, 9, 1)));
    }
}
