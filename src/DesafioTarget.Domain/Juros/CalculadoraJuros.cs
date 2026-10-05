namespace DesafioTarget.Domain.Juros;

/// <summary>
/// Calcula juros simples por dia de atraso, considerando a data de hoje.
/// </summary>
public sealed class CalculadoraJuros
{
    public const decimal TaxaDiariaPadrao = 0.025m;

    private readonly TimeProvider _relogio;
    private readonly decimal _taxaDiaria;

    public CalculadoraJuros(TimeProvider relogio, decimal taxaDiaria = TaxaDiariaPadrao)
    {
        if (taxaDiaria < 0)
            throw new ArgumentOutOfRangeException(nameof(taxaDiaria), "A taxa diária não pode ser negativa.");

        _relogio = relogio;
        _taxaDiaria = taxaDiaria;
    }

    public decimal TaxaDiaria => _taxaDiaria;

    public ResultadoJuros Calcular(decimal valor, DateOnly dataVencimento)
    {
        if (valor <= 0)
            throw new ArgumentOutOfRangeException(nameof(valor), "O valor deve ser maior que zero.");

        var hoje = DateOnly.FromDateTime(_relogio.GetLocalNow().DateTime);
        var diasEmAtraso = Math.Max(0, hoje.DayNumber - dataVencimento.DayNumber);
        var juros = Math.Round(valor * _taxaDiaria * diasEmAtraso, 2, MidpointRounding.AwayFromZero);

        return new ResultadoJuros(valor, dataVencimento, hoje, diasEmAtraso, juros);
    }
}
