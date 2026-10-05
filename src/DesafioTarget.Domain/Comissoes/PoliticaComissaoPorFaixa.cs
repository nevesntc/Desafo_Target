namespace DesafioTarget.Domain.Comissoes;

public sealed class PoliticaComissaoPorFaixa : IPoliticaComissao
{
    private readonly IReadOnlyList<FaixaComissao> _faixas;

    public PoliticaComissaoPorFaixa(IEnumerable<FaixaComissao> faixas)
    {
        _faixas = faixas.OrderBy(f => f.ValorMinimo).ToList();

        if (_faixas.Count == 0)
            throw new ArgumentException("Informe ao menos uma faixa de comissão.", nameof(faixas));
    }

    /// <summary>
    /// Regra do desafio: abaixo de R$ 100 não gera comissão,
    /// abaixo de R$ 500 gera 1% e a partir de R$ 500 gera 5%.
    /// </summary>
    public static PoliticaComissaoPorFaixa Padrao() => new(
    [
        new FaixaComissao(ValorMinimo: 0m, Percentual: 0m),
        new FaixaComissao(ValorMinimo: 100m, Percentual: 0.01m),
        new FaixaComissao(ValorMinimo: 500m, Percentual: 0.05m)
    ]);

    public decimal CalcularComissao(decimal valorVenda)
    {
        if (valorVenda < 0)
            throw new ArgumentOutOfRangeException(nameof(valorVenda), "O valor da venda não pode ser negativo.");

        var faixa = _faixas.LastOrDefault(f => valorVenda >= f.ValorMinimo);
        if (faixa is null)
            return 0m;

        return Math.Round(valorVenda * faixa.Percentual, 2, MidpointRounding.AwayFromZero);
    }
}
