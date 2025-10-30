using Microsoft.AspNetCore.Mvc;
using SeguroX.ContratacaoService.Application;

namespace SeguroX.ContratacaoService.API
{
    [ApiController]
    [Route("api/contratacoes")]
    public class ContratacoesController : ControllerBase
    {
        private readonly ContratacaoAppService _svc;

        public ContratacoesController(ContratacaoAppService svc)
        {
            _svc = svc;
        }

        [HttpPost("{propostaId:guid}")]
        public async Task<IActionResult> Contratar(Guid propostaId)
        {
            var contrato = await _svc.ContratarAsync(propostaId);

            if (contrato is null)
                return BadRequest("Proposta não aprovada ou inexistente.");

            return Ok(contrato);
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var contratos = await _svc.ListarAsync();
            return Ok(contratos);
        }
    }
}
