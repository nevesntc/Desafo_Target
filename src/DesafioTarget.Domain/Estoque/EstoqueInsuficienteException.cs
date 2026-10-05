using DesafioTarget.Domain.Comum;

namespace DesafioTarget.Domain.Estoque;

public sealed class EstoqueInsuficienteException : DomainException
{
    public EstoqueInsuficienteException(Produto produto, int quantidadeSolicitada)
        : base($"Estoque insuficiente para o produto {produto.Codigo} - {produto.Descricao}. " +
               $"Disponível: {produto.QuantidadeEmEstoque}, solicitado: {quantidadeSolicitada}.")
    {
    }
}
