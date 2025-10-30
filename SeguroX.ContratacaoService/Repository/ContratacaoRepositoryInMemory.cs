using SeguroX.ContratacaoService.Application;
using SeguroX.ContratacaoService.Domain;

namespace SeguroX.ContratacaoService.Infrastructure
{
    public class ContratacaoRepositoryInMemory : IContratacaoRepository
    {
        private readonly List<Contrato> _contratos = new();

        public Task<IEnumerable<Contrato>> ListarAsync()
            => Task.FromResult(_contratos.AsEnumerable());

        public Task<Contrato?> ObterAsync(Guid id)
            => Task.FromResult(_contratos.FirstOrDefault(c => c.Id == id));

        public Task CriarAsync(Contrato contrato)
        {
            _contratos.Add(contrato);
            return Task.CompletedTask;
        }
    }
}
