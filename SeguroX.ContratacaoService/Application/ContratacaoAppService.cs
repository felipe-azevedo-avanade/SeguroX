using SeguroX.ContratacaoService.Domain;
using System.Net.Http.Json;

namespace SeguroX.ContratacaoService.Application;

public class ContratacaoAppService
{
    private readonly HttpClient _http;
    private readonly IContratacaoRepository _repo;

    public ContratacaoAppService(HttpClient http, IContratacaoRepository repo)
    {
        _http = http;
        _repo = repo;
    }

    public async Task<Contrato?> ContratarAsync(Guid propostaId)
    {
        // Busca a proposta na outra API
        var proposta = await _http.GetFromJsonAsync<PropostaDto>($"api/propostas/{propostaId}");

        // Se não existir, retorna nulo
        if (proposta is null)
            return null;

        // Se não estiver aprovada, também retorna nulo
        if (proposta.Status != StatusProposta.Aprovada)
            return null;

        // Cria o contrato normalmente
        var contrato = new Contrato
        {
            PropostaId = propostaId
        };

        await _repo.CriarAsync(contrato);
        return contrato;
    }

    public async Task<IEnumerable<Contrato>> ListarAsync() => await _repo.ListarAsync();
}
