using SeguroX.PropostaService.Domain;

namespace SeguroX.PropostaService.Application.Ports
{
    public interface ICreditoService
    {
        bool Validar(Proposta proposta);
    }
}
