namespace DesafioTarget.Domain.Comissoes;

public sealed class CalculadoraComissao
{
    private readonly IPoliticaComissao _politica;

    public CalculadoraComissao(IPoliticaComissao politica)
    {
        _politica = politica;
    }

    public IReadOnlyList<ComissaoVendedor> CalcularPorVendedor(IEnumerable<Venda> vendas)
    {
        return vendas
            .GroupBy(v => v.Vendedor)
            .Select(grupo => new ComissaoVendedor(
                Vendedor: grupo.Key,
                QuantidadeVendas: grupo.Count(),
                TotalVendido: grupo.Sum(v => v.Valor),
                TotalComissao: grupo.Sum(v => _politica.CalcularComissao(v.Valor))))
            .OrderByDescending(c => c.TotalComissao)
            .ToList();
    }
}
