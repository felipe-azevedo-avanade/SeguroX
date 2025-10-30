using SeguroX.PropostaService.Domain;

namespace SeguroX.PropostaService.Application.Dtos
{
    public class PropostaResponse
    {
        public Guid Id { get; set; }
        public string NumeroProposta { get; set; } = string.Empty;
        public string NomeCliente { get; set; } = default!;
        public string? DocumentoCliente { get; set; }
        public string TipoSeguro { get; set; } = default!;
        public decimal ValorSegurado { get; set; }
        public decimal PremioMensal { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAprovacao { get; set; }
        public DateTime? DataExpiracao { get; set; }
        public StatusProposta Status { get; set; }
        public string Observacoes { get; set; } = string.Empty;
    }
}
