using DesafioTarget.App.Menu;
using DesafioTarget.Domain.Estoque;

namespace DesafioTarget.App.Opcoes;

public sealed class MovimentarEstoqueOpcao : IOpcaoMenu
{
    private readonly EstoqueService _estoque;
    private readonly Terminal _terminal;

    public MovimentarEstoqueOpcao(EstoqueService estoque, Terminal terminal)
    {
        _estoque = estoque;
        _terminal = terminal;
    }

    public string Titulo => "Movimentar estoque";

    public void Executar()
    {
        ExibirProdutos();

        var codigoProduto = _terminal.LerInteiro("Código do produto: ");
        var tipo = LerTipo();
        var quantidade = _terminal.LerInteiro("Quantidade: ");
        var descricao = _terminal.LerTexto("Descrição (ex.: compra de fornecedor, venda, devolução): ");

        var movimentacao = _estoque.Movimentar(new NovaMovimentacao(codigoProduto, tipo, quantidade, descricao));

        _terminal.PularLinha();
        _terminal.Escrever($"Movimentação nº {movimentacao.Numero} registrada em {Terminal.FormatarData(movimentacao.DataHora)}.");
        _terminal.Escrever($"{DescreverTipo(movimentacao.Tipo)} de {movimentacao.Quantidade} un. - {movimentacao.Descricao}");
        _terminal.Escrever($"Estoque final do produto {movimentacao.CodigoProduto}: {movimentacao.EstoqueFinal} un.");
    }

    private void ExibirProdutos()
    {
        _terminal.Escrever($"{"Código",-8} {"Produto",-28} {"Estoque",8}");
        _terminal.Escrever(new string('-', 46));

        foreach (var produto in _estoque.ListarProdutos())
            _terminal.Escrever($"{produto.Codigo,-8} {produto.Descricao,-28} {produto.QuantidadeEmEstoque,8}");

        _terminal.PularLinha();
    }

    private static string DescreverTipo(TipoMovimentacao tipo) =>
        tipo == TipoMovimentacao.Entrada ? "Entrada" : "Saída";

    private TipoMovimentacao LerTipo()
    {
        while (true)
        {
            var opcao = _terminal.LerInteiro("Tipo (1 - Entrada | 2 - Saída): ");

            if (Enum.IsDefined(typeof(TipoMovimentacao), opcao))
                return (TipoMovimentacao)opcao;

            _terminal.EscreverErro("Tipo inválido.");
        }
    }
}
