using SeguroX.PropostaService.Application.Ports;
using SeguroX.PropostaService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeguroX.PropostaService.Tests.Fakes
{
    public class FakePropostaRepository : IPropostaRepository
    {
        private readonly List<Proposta> _propostas = new();

        public Task<IEnumerable<Proposta>> ListarAsync()
            => Task.FromResult(_propostas.AsEnumerable());

        public Task<Proposta?> ObterAsync(Guid id)
            => Task.FromResult(_propostas.FirstOrDefault(p => p.Id == id));

        public Task<Proposta?> ObterPorIdAsync(Guid id)
            => Task.FromResult(_propostas.FirstOrDefault(p => p.Id == id));

        public Task CriarAsync(Proposta proposta)
        {
            _propostas.Add(proposta);
            return Task.CompletedTask;
        }

        public Task AtualizarStatusAsync(Guid id, StatusProposta status)
        {
            var p = _propostas.FirstOrDefault(x => x.Id == id);
            if (p != null)
            {
                // como é teste, podemos setar internamente
                var campoStatus = typeof(Proposta).GetProperty(nameof(Proposta.Status));
                campoStatus?.SetValue(p, status);
            }
            return Task.CompletedTask;
        }
    }
}
