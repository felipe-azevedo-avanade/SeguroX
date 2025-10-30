namespace SeguroX.ContratacaoService.Domain;

public class Contrato
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid PropostaId { get; set; }
    public DateTime DataContratacao { get; set; } = DateTime.UtcNow;
}
