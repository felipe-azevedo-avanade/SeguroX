using SeguroX.PropostaService.Application.Ports;
using SeguroX.PropostaService.Domain;

namespace SeguroX.PropostaService.Application
{
    /// <summary>
    /// Serviço de aplicação responsável por orquestrar o ciclo de vida de uma proposta.
    /// Implementa princípios de DDD e SOLID, mantendo o domínio isolado e coeso.
    /// </summary>
    public class PropostaAppService
    {
        private readonly IPropostaRepository _repo;
        private readonly IPropostaValidator _validator;
        private readonly ICreditoService _credito;

        public PropostaAppService(
            IPropostaRepository repo,
            IPropostaValidator validator,
            ICreditoService credito)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _credito = credito ?? throw new ArgumentNullException(nameof(credito));
        }

        // ---------------- CRIAÇÃO ----------------
        public async Task<Proposta> CriarAsync(
            string nomeCliente,
            string tipoSeguro,
            decimal valorSegurado,
            decimal premioMensal,
            string? documentoCliente = null)
        {
            var proposta = Proposta.Criar(nomeCliente, tipoSeguro, valorSegurado, premioMensal, documentoCliente);
            _validator.ValidarDadosObrigatorios(proposta);
            await _validator.VerificarDuplicidadeAsync(proposta, _repo);
            await _repo.CriarAsync(proposta);
            return proposta;
        }

        // ---------------- APROVAÇÃO ----------------
        public async Task<(bool sucesso, string mensagem)> AprovarPropostaAsync(Guid id)
        {
            var proposta = await _repo.ObterAsync(id)
                ?? throw new InvalidOperationException($"Proposta {id} não encontrada.");

            if (!_credito.Validar(proposta))
                throw new InvalidOperationException("Crédito reprovado pelo motor de crédito.");

            proposta.Aprovar();
            await _repo.AtualizarStatusAsync(id, proposta.Status);

            return (true, $"Proposta {proposta.NumeroProposta} aprovada com sucesso.");
        }

        // ---------------- REPROVAÇÃO ----------------
        public async Task<(bool sucesso, string mensagem)> ReprovarPropostaAsync(Guid id, string motivo)
        {
            if (string.IsNullOrWhiteSpace(motivo))
                throw new ArgumentException("O motivo da reprovação é obrigatório.", nameof(motivo));

            var proposta = await _repo.ObterAsync(id)
                ?? throw new InvalidOperationException($"Proposta {id} não encontrada.");

            proposta.Reprovar(motivo);
            await _repo.AtualizarStatusAsync(id, proposta.Status);

            return (true, $"Proposta {proposta.NumeroProposta} reprovada com motivo: {motivo}");
        }
    }
}
