using SeguroX.PropostaService.Application.Ports;
using SeguroX.PropostaService.Domain;

namespace SeguroX.PropostaService.Application
{
    public class PropostaValidator : IPropostaValidator
    {
        public void ValidarDadosObrigatorios(Proposta proposta)
        {
            if (proposta == null)
                throw new ArgumentException("Proposta inválida.");

            if (string.IsNullOrWhiteSpace(proposta.NomeCliente))
                throw new ArgumentException("O nome do cliente é obrigatório.");

            if (string.IsNullOrWhiteSpace(proposta.TipoSeguro))
                throw new ArgumentException("O tipo de seguro é obrigatório.");

            if (string.IsNullOrWhiteSpace(proposta.DocumentoCliente))
                throw new ArgumentException("O documento do cliente é obrigatório.");

            if (proposta.ValorSegurado <= 0)
                throw new ArgumentException("O valor segurado deve ser maior que zero.");

            if (proposta.PremioMensal <= 0)
                throw new ArgumentException("O prêmio mensal deve ser maior que zero.");
        }

        public async Task VerificarDuplicidadeAsync(Proposta proposta, IPropostaRepository repo)
        {
            var existentes = await repo.ListarAsync();
            if (existentes.Any(p =>
                p.DocumentoCliente == proposta.DocumentoCliente &&
                p.Status != StatusProposta.Reprovada))
            {
                throw new InvalidOperationException("Já existe uma proposta ativa para este cliente.");
            }
        }
    }
}
