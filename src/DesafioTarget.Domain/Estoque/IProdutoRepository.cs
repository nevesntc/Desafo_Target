namespace DesafioTarget.Domain.Estoque;

public interface IProdutoRepository
{
    IReadOnlyList<Produto> ListarTodos();
    Produto? ObterPorCodigo(int codigo);
    void Atualizar(Produto produto);
}
