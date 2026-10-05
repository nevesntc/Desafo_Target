using DesafioTarget.Domain.Comum;

namespace DesafioTarget.Domain.Estoque;

public sealed class ProdutoNaoEncontradoException : DomainException
{
    public ProdutoNaoEncontradoException(int codigoProduto)
        : base($"Produto {codigoProduto} não encontrado.")
    {
    }
}
