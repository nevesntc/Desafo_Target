namespace DesafioTarget.Domain.Estoque;

public sealed record Movimentacao(
    int Numero,
    int CodigoProduto,
    TipoMovimentacao Tipo,
    int Quantidade,
    string Descricao,
    DateTime DataHora,
    int EstoqueFinal);
