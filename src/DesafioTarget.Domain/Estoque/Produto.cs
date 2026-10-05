using DesafioTarget.Domain.Comum;

namespace DesafioTarget.Domain.Estoque;

public sealed class Produto
{
    public Produto(int codigo, string descricao, int quantidadeEmEstoque)
    {
        if (quantidadeEmEstoque < 0)
            throw new DomainException("O estoque inicial não pode ser negativo.");

        Codigo = codigo;
        Descricao = descricao;
        QuantidadeEmEstoque = quantidadeEmEstoque;
    }

    public int Codigo { get; }
    public string Descricao { get; }
    public int QuantidadeEmEstoque { get; private set; }

    public void DarEntrada(int quantidade)
    {
        ValidarQuantidade(quantidade);
        QuantidadeEmEstoque += quantidade;
    }

    public void DarSaida(int quantidade)
    {
        ValidarQuantidade(quantidade);

        if (quantidade > QuantidadeEmEstoque)
            throw new EstoqueInsuficienteException(this, quantidade);

        QuantidadeEmEstoque -= quantidade;
    }

    private static void ValidarQuantidade(int quantidade)
    {
        if (quantidade <= 0)
            throw new DomainException("A quantidade movimentada deve ser maior que zero.");
    }
}
