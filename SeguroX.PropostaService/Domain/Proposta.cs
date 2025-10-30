namespace SeguroX.PropostaService.Domain
{
    public class Proposta
    {
        public Guid Id { get; private set; }
        public string NumeroProposta { get; private set; } = string.Empty;
        public string NomeCliente { get; private set; } = default!;
        public string? DocumentoCliente { get; private set; }
        public string TipoSeguro { get; private set; } = default!;
        public decimal ValorSegurado { get; private set; }
        public decimal PremioMensal { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public DateTime? DataAprovacao { get; private set; }
        public DateTime? DataExpiracao { get; private set; }
        public StatusProposta Status { get; private set; } = StatusProposta.EmAnalise;
        public string Observacoes { get; private set; } = string.Empty;

        // 🔹 Construtor usado pela fábrica
        private Proposta() { }

        // 🔹 Construtor usado para testes e repositórios in-memory
        internal Proposta(
            Guid id,
            string nomeCliente,
            string tipoSeguro,
            StatusProposta status)
        {
            Id = id;
            NomeCliente = nomeCliente;
            TipoSeguro = tipoSeguro;
            Status = status;
            DataCriacao = DateTime.UtcNow;
            NumeroProposta = $"PROP-{DateTime.UtcNow.Ticks}";
        }

        // 🔹 Método de fábrica
        public static Proposta Criar(
            string nomeCliente,
            string tipoSeguro,
            decimal valorSegurado,
            decimal premioMensal,
            string? documentoCliente = null)
        {
            return new Proposta
            {
                Id = Guid.NewGuid(),
                NumeroProposta = $"PROP-{DateTime.UtcNow.Ticks}",
                NomeCliente = nomeCliente,
                TipoSeguro = tipoSeguro,
                ValorSegurado = valorSegurado,
                PremioMensal = premioMensal,
                DocumentoCliente = documentoCliente,
                DataCriacao = DateTime.UtcNow,
                Status = StatusProposta.EmAnalise
            };
        }

        // 🔹 Regras de negócio
        public void Aprovar()
        {
            if (Status != StatusProposta.EmAnalise)
                throw new InvalidOperationException("Somente propostas em análise podem ser aprovadas.");

            Status = StatusProposta.Aprovada;
            DataAprovacao = DateTime.UtcNow;
            DataExpiracao = DataAprovacao?.AddDays(30);
        }
        public void AlterarStatus(StatusProposta novoStatus)
        {
            Status = novoStatus;
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
