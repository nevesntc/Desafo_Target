using System.Text;
using DesafioTarget.App.Menu;
using DesafioTarget.App.Opcoes;
using DesafioTarget.Domain.Comissoes;
using DesafioTarget.Domain.Estoque;
using DesafioTarget.Domain.Juros;
using DesafioTarget.Infrastructure.Json;
using DesafioTarget.Infrastructure.Memoria;

Console.OutputEncoding = Encoding.UTF8;

var pastaDados = Path.Combine(AppContext.BaseDirectory, "Dados");
var relogio = TimeProvider.System;
var terminal = new Terminal();

var estoqueService = new EstoqueService(
    new ProdutoJsonRepository(Path.Combine(pastaDados, "estoque.json")),
    new MovimentacaoMemoryRepository(),
    relogio);

IOpcaoMenu[] opcoes =
[
    new CalcularComissaoOpcao(
        new VendaJsonRepository(Path.Combine(pastaDados, "vendas.json")),
        new CalculadoraComissao(PoliticaComissaoPorFaixa.Padrao()),
        terminal),
    new MovimentarEstoqueOpcao(estoqueService, terminal),
    new CalcularJurosOpcao(new CalculadoraJuros(relogio), terminal)
];

new MenuPrincipal(opcoes, terminal).Executar();
