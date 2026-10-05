using DesafioTarget.App.Menu;
using DesafioTarget.Domain.Comissoes;

namespace DesafioTarget.App.Opcoes;

public sealed class CalcularComissaoOpcao : IOpcaoMenu
{
    private readonly IVendaRepository _vendas;
    private readonly CalculadoraComissao _calculadora;
    private readonly Terminal _terminal;

    public CalcularComissaoOpcao(IVendaRepository vendas, CalculadoraComissao calculadora, Terminal terminal)
    {
        _vendas = vendas;
        _calculadora = calculadora;
        _terminal = terminal;
    }

    public string Titulo => "Calcular comissão dos vendedores";

    public void Executar()
    {
        var comissoes = _calculadora.CalcularPorVendedor(_vendas.ListarTodas());

        _terminal.Escrever($"{"Vendedor",-20} {"Vendas",6} {"Total vendido",16} {"Comissão",14}");
        _terminal.Escrever(new string('-', 59));

        foreach (var comissao in comissoes)
        {
            _terminal.Escrever(
                $"{comissao.Vendedor,-20} {comissao.QuantidadeVendas,6} " +
                $"{Terminal.FormatarMoeda(comissao.TotalVendido),16} " +
                $"{Terminal.FormatarMoeda(comissao.TotalComissao),14}");
        }

        _terminal.Escrever(new string('-', 59));
        _terminal.Escrever($"{"Total",-20} {comissoes.Sum(c => c.QuantidadeVendas),6} " +
                           $"{Terminal.FormatarMoeda(comissoes.Sum(c => c.TotalVendido)),16} " +
                           $"{Terminal.FormatarMoeda(comissoes.Sum(c => c.TotalComissao)),14}");
    }
}
