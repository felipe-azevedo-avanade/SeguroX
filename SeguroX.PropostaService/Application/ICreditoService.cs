using SeguroX.PropostaService.Domain;

namespace SeguroX.PropostaService.Application
{
    public interface ICreditoService
    {
        bool Validar(Proposta proposta);
    }
}
