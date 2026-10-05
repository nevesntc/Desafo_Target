namespace DesafioTarget.Domain.Comissoes;

/// <summary>
/// Define como a comissão de uma venda é calculada.
/// Permite trocar a regra (ex.: campanhas, metas) sem alterar a calculadora.
/// </summary>
public interface IPoliticaComissao
{
    decimal CalcularComissao(decimal valorVenda);
}
