using SeguroX.PropostaService.Application.Ports;
using SeguroX.PropostaService.Domain;

namespace SeguroX.PropostaService.Tests.Fakes
{
    public class FakeCreditoService : ICreditoService
    {
        public bool Validar(Proposta proposta)
        {
            // mesma regra que você usa na app
            return proposta.ValorSegurado <= 500_000 && proposta.PremioMensal > 0;
        }
    }
}
