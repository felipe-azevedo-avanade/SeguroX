using SeguroX.PropostaService.Domain;

namespace SeguroX.PropostaService.Application
{
    public interface IPropostaRepository
    {
        Task<IEnumerable<Proposta>> ListarAsync();
        Task<Proposta?> ObterAsync(Guid id);          
        Task<Proposta?> ObterPorIdAsync(Guid id); 
        Task CriarAsync(Proposta proposta);
        Task AtualizarStatusAsync(Guid id, StatusProposta status);
    }
}
