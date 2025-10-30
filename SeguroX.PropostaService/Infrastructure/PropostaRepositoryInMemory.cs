using SeguroX.PropostaService.Application.Ports;
using SeguroX.PropostaService.Domain;

namespace SeguroX.PropostaService.Infrastructure
{
    public class PropostaRepositoryInMemory : IPropostaRepository
    {
        private readonly List<Proposta> _propostas = new();

        public PropostaRepositoryInMemory()
        {
            _propostas.AddRange(GerarPropostasDeTeste());
        }

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
                p.AlterarStatus(status);
            return Task.CompletedTask;
        }

        private static IEnumerable<Proposta> GerarPropostasDeTeste()
        {
            return new List<Proposta>
    {
        new Proposta(
            Guid.Parse("a1f79e2b-4f8e-4b0c-9d79-9f2d3c9e8a10"),
            "Ana Bezerra",
            "Vida Individual",
            StatusProposta.Aprovada
        ),
        new Proposta(
            Guid.Parse("b7df4a80-3e52-42c7-8a61-0a78e9a7b045"),
            "Carlos Andrade",
            "Residencial",
            StatusProposta.EmAnalise
        ),
        new Proposta(
            Guid.Parse("d2a9c4ee-bb6c-4b4b-88d7-5f6509b5d81a"),
            "Beatriz Lima",
            "Automóvel",
            StatusProposta.Reprovada
        ),
        new Proposta(
            Guid.Parse("e3d74b6a-7891-4fa0-a0b4-f22e7b62c8e1"),
            "Ricardo Menezes",
            "Empresarial",
            StatusProposta.Aprovada
        ),
        new Proposta(
            Guid.Parse("f5b90c9f-78a6-47aa-87f2-64d4c1b9a0af"),
            "Juliana Prado",
            "Saúde Familiar",
            StatusProposta.Pendente
        )
    };
        }
    }
}
