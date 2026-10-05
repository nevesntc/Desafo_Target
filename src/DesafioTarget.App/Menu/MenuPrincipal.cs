using DesafioTarget.Domain.Comum;

namespace DesafioTarget.App.Menu;

public sealed class MenuPrincipal
{
    private readonly IReadOnlyList<IOpcaoMenu> _opcoes;
    private readonly Terminal _terminal;

    public MenuPrincipal(IReadOnlyList<IOpcaoMenu> opcoes, Terminal terminal)
    {
        _opcoes = opcoes;
        _terminal = terminal;
    }

    public void Executar()
    {
        while (true)
        {
            ExibirOpcoes();

            var escolha = _terminal.LerInteiro("Escolha uma opção: ");
            if (escolha == 0)
                return;

            if (escolha < 1 || escolha > _opcoes.Count)
            {
                _terminal.EscreverErro("Opção inválida.");
                continue;
            }

            ExecutarOpcao(_opcoes[escolha - 1]);
        }
    }

    private void ExibirOpcoes()
    {
        _terminal.EscreverTitulo("Desafio Target Sistemas");

        for (var i = 0; i < _opcoes.Count; i++)
            _terminal.Escrever($"{i + 1} - {_opcoes[i].Titulo}");

        _terminal.Escrever("0 - Sair");
        _terminal.PularLinha();
    }

    private void ExecutarOpcao(IOpcaoMenu opcao)
    {
        _terminal.EscreverTitulo(opcao.Titulo);

        try
        {
            opcao.Executar();
        }
        catch (DomainException ex)
        {
            _terminal.EscreverErro(ex.Message);
        }

        _terminal.AguardarTecla();
    }
}
