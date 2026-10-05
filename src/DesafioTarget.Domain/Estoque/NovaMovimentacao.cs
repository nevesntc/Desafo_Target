namespace DesafioTarget.Domain.Estoque;

public sealed record NovaMovimentacao(
    int CodigoProduto,
    TipoMovimentacao Tipo,
    int Quantidade,
    string Descricao);
