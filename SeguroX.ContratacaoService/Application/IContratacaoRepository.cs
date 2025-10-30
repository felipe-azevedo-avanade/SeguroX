using SeguroX.ContratacaoService.Domain;

namespace SeguroX.ContratacaoService.Application
{
    public interface IContratacaoRepository
    {
        Task<IEnumerable<Contrato>> ListarAsync();
        Task<Contrato?> ObterAsync(Guid id);
        Task CriarAsync(Contrato contrato);
    }
}
