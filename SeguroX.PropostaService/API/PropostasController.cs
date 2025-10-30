using Microsoft.AspNetCore.Mvc;
using SeguroX.PropostaService.Application;
using SeguroX.PropostaService.Domain;

namespace SeguroX.PropostaService.API
{
    [ApiController]
    [Route("api/propostas")]
    public class PropostasController : ControllerBase
    {
        private readonly IPropostaRepository _repo;
        private readonly PropostaAppService _app;

        public PropostasController(IPropostaRepository repo, PropostaAppService app)
        {
            _repo = repo;
            _app = app;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var propostas = await _repo.ListarAsync();
            return Ok(propostas);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            var proposta = await _repo.ObterPorIdAsync(id);
            if (proposta == null)
                return NotFound(new { mensagem = $"Proposta {id} não encontrada." });

            return Ok(proposta);
        }

        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] Proposta proposta)
        {
            try
            {
                var nova = await _app.CriarAsync(proposta);
                return CreatedAtAction(nameof(ObterPorId), new { id = nova.Id }, nova);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro interno ao criar proposta.", detalhes = ex.Message });
            }
        }

        [HttpPut("{id:guid}/aprovar")]
        public async Task<IActionResult> Aprovar(Guid id)
        {
            try
            {
                await _app.AprovarPropostaAsync(id);
                return Ok(new { mensagem = "Proposta aprovada com sucesso." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { erro = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro inesperado ao aprovar a proposta.", detalhes = ex.Message });
            }
        }

        [HttpPut("{id:guid}/rejeitar")]
        public async Task<IActionResult> Rejeitar(Guid id, [FromBody] string motivo)
        {
            if (string.IsNullOrWhiteSpace(motivo))
                return BadRequest(new { erro = "O motivo da rejeição é obrigatório." });

            try
            {
                await _app.ReprovarPropostaAsync(id, motivo);
                return Ok(new { mensagem = "Proposta rejeitada com sucesso.", motivo });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { erro = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro inesperado ao rejeitar a proposta.", detalhes = ex.Message });
            }
        }
    }
}
