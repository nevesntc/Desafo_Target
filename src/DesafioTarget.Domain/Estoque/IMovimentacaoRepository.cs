namespace DesafioTarget.Domain.Estoque;

public interface IMovimentacaoRepository
{
    int GerarProximoNumero();
    void Adicionar(Movimentacao movimentacao);
    IReadOnlyList<Movimentacao> ListarPorProduto(int codigoProduto);
}
