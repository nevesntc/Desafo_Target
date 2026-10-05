using DesafioTarget.App.Menu;
using DesafioTarget.Domain.Juros;

namespace DesafioTarget.App.Opcoes;

public sealed class CalcularJurosOpcao : IOpcaoMenu
{
    private readonly CalculadoraJuros _calculadora;
    private readonly Terminal _terminal;

    public CalcularJurosOpcao(CalculadoraJuros calculadora, Terminal terminal)
    {
        _calculadora = calculadora;
        _terminal = terminal;
    }

    public string Titulo => "Calcular juros por atraso";

    public void Executar()
    {
        var valor = LerValorPositivo();
        var vencimento = _terminal.LerData("Data de vencimento (dd/mm/aaaa): ");

        var resultado = _calculadora.Calcular(valor, vencimento);

        _terminal.PularLinha();
        _terminal.Escrever($"Data do cálculo:  {Terminal.FormatarData(resultado.DataCalculo)}");
        _terminal.Escrever($"Vencimento:       {Terminal.FormatarData(resultado.DataVencimento)}");
        _terminal.Escrever($"Dias em atraso:   {resultado.DiasEmAtraso}");
        _terminal.Escrever($"Taxa diária:      {Terminal.FormatarPercentual(_calculadora.TaxaDiaria)} ao dia");
        _terminal.Escrever($"Valor original:   {Terminal.FormatarMoeda(resultado.ValorOriginal)}");
        _terminal.Escrever($"Juros:            {Terminal.FormatarMoeda(resultado.ValorJuros)}");
        _terminal.Escrever($"Valor atualizado: {Terminal.FormatarMoeda(resultado.ValorAtualizado)}");

        if (!resultado.EmAtraso)
            _terminal.Escrever("Título dentro do prazo, não há juros a cobrar.");
    }

    private decimal LerValorPositivo()
    {
        while (true)
        {
            var valor = _terminal.LerDecimal("Valor (R$): ");
            if (valor > 0)
                return valor;

            _terminal.EscreverErro("O valor deve ser maior que zero.");
        }
    }
}
