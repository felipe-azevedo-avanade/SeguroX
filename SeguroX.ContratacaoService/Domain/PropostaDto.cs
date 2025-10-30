namespace SeguroX.ContratacaoService.Domain;

public enum StatusProposta
{
    EmAnalise,
    Aprovada,
    Rejeitada
}

public class PropostaDto
{
    public Guid Id { get; set; }
    public string NomeCliente { get; set; } = default!;
    public string TipoSeguro { get; set; } = default!;
    public StatusProposta Status { get; set; }
}
