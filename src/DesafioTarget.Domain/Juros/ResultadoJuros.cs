namespace DesafioTarget.Domain.Juros;

public sealed record ResultadoJuros(
    decimal ValorOriginal,
    DateOnly DataVencimento,
    DateOnly DataCalculo,
    int DiasEmAtraso,
    decimal ValorJuros)
{
    public decimal ValorAtualizado => ValorOriginal + ValorJuros;
    public bool EmAtraso => DiasEmAtraso > 0;
}
