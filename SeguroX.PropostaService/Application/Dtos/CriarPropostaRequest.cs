namespace SeguroX.PropostaService.Application.Dtos
{
    public class CriarPropostaRequest
    {
        public string NomeCliente { get; set; } = default!;
        public string? DocumentoCliente { get; set; }
        public string TipoSeguro { get; set; } = default!;
        public decimal ValorSegurado { get; set; }
        public decimal PremioMensal { get; set; }
    }
}
