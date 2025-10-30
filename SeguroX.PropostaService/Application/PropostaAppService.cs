using SeguroX.PropostaService.Domain;

namespace SeguroX.PropostaService.Application
{
    public class PropostaAppService
    {
        private readonly IPropostaRepository _repo;

        public PropostaAppService(IPropostaRepository repo)
        {
            _repo = repo;
        }

        // ---------------- CRIAÇÃO ----------------
        public async Task<Proposta> CriarAsync(Proposta proposta)
        {
            if (proposta == null)
                throw new ArgumentException("Proposta inválida.");

            // Validações básicas
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

            // Verifica duplicidade pelo documento
            var existentes = await _repo.ListarAsync();
            if (existentes.Any(p =>
                p.DocumentoCliente == proposta.DocumentoCliente &&
                p.Status != StatusProposta.Reprovada))
            {
                throw new InvalidOperationException("Já existe uma proposta ativa para este cliente.");
            }

            // Geração de dados básicos
            proposta.Id = Guid.NewGuid();
            proposta.NumeroProposta = $"PROP-{DateTime.UtcNow.Ticks}";
            proposta.DataCriacao = DateTime.UtcNow;
            proposta.Status = StatusProposta.EmAnalise;

            await _repo.CriarAsync(proposta);
            return proposta;
        }

        // ---------------- APROVAÇÃO ----------------
        public async Task AprovarPropostaAsync(Guid id)
        {
            var proposta = await _repo.ObterAsync(id);

            if (proposta == null)
                throw new InvalidOperationException("Proposta não encontrada.");

            if (!ValidarCredito(proposta))
                throw new InvalidOperationException("Crédito reprovado pelo motor de crédito.");

            proposta.Status = StatusProposta.Aprovada;
            proposta.DataAprovacao = DateTime.UtcNow;
            proposta.DataExpiracao = proposta.DataAprovacao?.AddDays(30);

            await _repo.AtualizarStatusAsync(id, proposta.Status);
        }

        // ---------------- REPROVAÇÃO ----------------
        public async Task ReprovarPropostaAsync(Guid id, string motivo)
        {
            if (string.IsNullOrWhiteSpace(motivo))
                throw new ArgumentException("O motivo da reprovação é obrigatório.");

            var proposta = await _repo.ObterAsync(id);

            if (proposta == null)
                throw new InvalidOperationException("Proposta não encontrada.");

            proposta.Status = StatusProposta.Reprovada;
            proposta.Observacoes = motivo;

            await _repo.AtualizarStatusAsync(id, proposta.Status);
        }

        // ---------------- VALIDAÇÃO DE CRÉDITO ----------------
        private static bool ValidarCredito(Proposta proposta)
        {
            // Simulação de motor de crédito
            return proposta.ValorSegurado <= 500_000 && proposta.PremioMensal > 0;
        }
    }
}
