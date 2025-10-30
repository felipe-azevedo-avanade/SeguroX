using SeguroX.PropostaService.Application.Ports;
using SeguroX.PropostaService.Domain;

namespace SeguroX.PropostaService.Application
{
    public class MotorCreditoSimulado : ICreditoService
    {
        public bool Validar(Proposta proposta)
        {
            // Regra simulada: reprova valores acima de 500 mil
            return proposta.ValorSegurado <= 500_000 && proposta.PremioMensal > 0;
        }
    }
}
