using System.Globalization;

namespace DesafioTarget.App.Menu;

/// <summary>
/// Centraliza a leitura e escrita no console, validando as entradas do usuário.
/// </summary>
public sealed class Terminal
{
    private static readonly CultureInfo Cultura = CultureInfo.GetCultureInfo("pt-BR");

    public void Escrever(string texto = "") => Console.WriteLine(texto);

    public void PularLinha() => Console.WriteLine();

    public void EscreverTitulo(string titulo)
    {
        Console.WriteLine();
        Console.WriteLine($"=== {titulo} ===");
        Console.WriteLine();
    }

    public void EscreverErro(string mensagem)
    {
        var corAnterior = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(mensagem);
        Console.ForegroundColor = corAnterior;
    }

    public void AguardarTecla()
    {
        Console.WriteLine();
        Console.Write("Pressione ENTER para voltar ao menu...");
        Console.ReadLine();
    }

    public string LerTexto(string rotulo)
    {
        while (true)
        {
            Console.Write(rotulo);
            var texto = LerLinha().Trim();

            if (texto.Length > 0)
                return texto;

            EscreverErro("Campo obrigatório.");
        }
    }

    public int LerInteiro(string rotulo) =>
        Ler(rotulo, texto => (int.TryParse(texto, NumberStyles.Integer, Cultura, out var valor), valor),
            "Informe um número inteiro.");

    public decimal LerDecimal(string rotulo) =>
        Ler(rotulo, texto => (decimal.TryParse(texto, NumberStyles.Number, Cultura, out var valor), valor),
            "Informe um valor válido. Ex.: 1500,90");

    public DateOnly LerData(string rotulo) =>
        Ler(rotulo, texto => (DateOnly.TryParseExact(texto, "dd/MM/yyyy", Cultura, DateTimeStyles.None, out var data), data),
            "Informe a data no formato dd/mm/aaaa.");

    public static string FormatarMoeda(decimal valor) => valor.ToString("C", Cultura);

    public static string FormatarPercentual(decimal valor) => valor.ToString("P1", Cultura);

    public static string FormatarData(DateOnly data) => data.ToString("dd/MM/yyyy", Cultura);

    public static string FormatarData(DateTime data) => data.ToString("dd/MM/yyyy HH:mm", Cultura);

    private T Ler<T>(string rotulo, Func<string, (bool Sucesso, T Valor)> converter, string mensagemErro)
    {
        while (true)
        {
            Console.Write(rotulo);
            var (sucesso, valor) = converter(LerLinha().Trim());

            if (sucesso)
                return valor;

            EscreverErro(mensagemErro);
        }
    }

    private static string LerLinha() =>
        Console.ReadLine() ?? throw new EndOfStreamException("Entrada do console encerrada.");
}
