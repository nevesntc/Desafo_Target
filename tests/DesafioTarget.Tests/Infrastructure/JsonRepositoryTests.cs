using DesafioTarget.Infrastructure.Json;

namespace DesafioTarget.Tests.Infrastructure;

public class JsonRepositoryTests
{
    private static string CaminhoDados(string arquivo) =>
        Path.Combine(AppContext.BaseDirectory, "Dados", arquivo);

    [Fact]
    public void Le_todas_as_vendas_do_arquivo()
    {
        var vendas = new VendaJsonRepository(CaminhoDados("vendas.json")).ListarTodas();

        Assert.Equal(36, vendas.Count);
        Assert.Equal(4, vendas.Select(v => v.Vendedor).Distinct().Count());
        Assert.Contains(vendas, v => v.Vendedor == "João Silva" && v.Valor == 1200.50m);
    }

    [Fact]
    public void Le_os_produtos_do_estoque()
    {
        var repositorio = new ProdutoJsonRepository(CaminhoDados("estoque.json"));

        Assert.Equal(5, repositorio.ListarTodos().Count);

        var produto = repositorio.ObterPorCodigo(102);
        Assert.NotNull(produto);
        Assert.Equal("Caderno Universitário", produto.Descricao);
        Assert.Equal(75, produto.QuantidadeEmEstoque);
    }

    [Fact]
    public void Arquivo_inexistente_gera_erro()
    {
        Assert.Throws<FileNotFoundException>(() => new VendaJsonRepository("nao-existe.json").ListarTodas());
    }
}
