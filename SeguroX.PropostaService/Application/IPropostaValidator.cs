using SeguroX.PropostaService.Domain;

namespace SeguroX.PropostaService.Application
{
    public interface IPropostaValidator
    {
        void ValidarDadosObrigatorios(Proposta proposta);
        Task VerificarDuplicidadeAsync(Proposta proposta, IPropostaRepository repo);
    }
}
