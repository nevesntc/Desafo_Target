namespace DesafioTarget.Domain.Comissoes;

public interface IVendaRepository
{
    IReadOnlyList<Venda> ListarTodas();
}
