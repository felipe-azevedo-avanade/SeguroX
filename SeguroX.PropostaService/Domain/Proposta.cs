namespace SeguroX.PropostaService.Domain
{
    public class Proposta
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string NumeroProposta { get; set; } = string.Empty;
        public string NomeCliente { get; set; } = default!;
        public string? DocumentoCliente { get; set; }
        public string TipoSeguro { get; set; } = default!;
        public decimal ValorSegurado { get; set; }
        public decimal PremioMensal { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
        public DateTime? DataAprovacao { get; set; }
        public DateTime? DataExpiracao { get; set; }
        public StatusProposta Status { get; set; } = StatusProposta.EmAnalise;
        public string Observacoes { get; set; } = string.Empty;

        // -------------------- Regras de Negócio --------------------

        public void Aprovar()
        {
            if (Status != StatusProposta.EmAnalise)
                throw new InvalidOperationException("Somente propostas em análise podem ser aprovadas.");

            Status = StatusProposta.Aprovada;
            DataAprovacao = DateTime.UtcNow;
            DataExpiracao = DataAprovacao?.AddDays(30);
        }

        public void Reprovar(string motivo)
        {
            if (Status != StatusProposta.EmAnalise)
                throw new InvalidOperationException("Somente propostas em análise podem ser reprovadas.");

            if (string.IsNullOrWhiteSpace(motivo))
                throw new ArgumentException("O motivo da reprovação é obrigatório.");

            Status = StatusProposta.Reprovada;
            Observacoes = motivo;
        }
    }
}
