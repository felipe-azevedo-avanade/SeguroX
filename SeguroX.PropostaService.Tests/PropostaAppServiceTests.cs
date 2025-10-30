using System;
using System.Linq;
using System.Threading.Tasks;
using SeguroX.PropostaService.Application;
using SeguroX.PropostaService.Domain;
using SeguroX.PropostaService.Tests.Fakes;
using Xunit;

namespace SeguroX.PropostaService.Tests.Application
{
    public class PropostaAppServiceTests
    {
        private readonly FakePropostaRepository _repo;
        private readonly FakePropostaValidator _validator;
        private readonly FakeCreditoService _credito;
        private readonly PropostaAppService _app;

        public PropostaAppServiceTests()
        {
            _repo = new FakePropostaRepository();
            _validator = new FakePropostaValidator();
            _credito = new FakeCreditoService();
            _app = new PropostaAppService(_repo, _validator, _credito);
        }

        [Fact]
        public async Task Deve_criar_proposta_valida()
        {
            var input = Proposta.Criar(
                nomeCliente: "Felipe",
                tipoSeguro: "Vida",
                valorSegurado: 100000,
                premioMensal: 300,
                documentoCliente: "12345678900"
            );

            var criada = await _app.CriarAsync(input);

            Assert.NotEqual(Guid.Empty, criada.Id);
            Assert.Equal(StatusProposta.EmAnalise, criada.Status);

            var todas = await _repo.ListarAsync();
            Assert.Single(todas);
        }

        [Fact]
        public async Task Nao_deve_criar_proposta_duplicada_por_documento()
        {
            var p1 = Proposta.Criar("Cliente 1", "Auto", 50000, 200, "111");
            await _app.CriarAsync(p1);

            var p2 = Proposta.Criar("Cliente 1", "Vida", 50000, 200, "111");

            await Assert.ThrowsAsync<InvalidOperationException>(() => _app.CriarAsync(p2));
        }

        [Fact]
        public async Task Deve_aprovar_proposta_quando_credito_valido()
        {
            var p = Proposta.Criar("Cliente ok", "Residencial", 200000, 500, "222");
            await _repo.CriarAsync(p);

            await _app.AprovarPropostaAsync(p.Id);

            var atualizado = await _repo.ObterAsync(p.Id);
            Assert.Equal(StatusProposta.Aprovada, atualizado.Status);
        }

        [Fact]
        public async Task Nao_deve_aprovar_proposta_inexistente()
        {
            var idInexistente = Guid.NewGuid();

            await Assert.ThrowsAsync<InvalidOperationException>(() => _app.AprovarPropostaAsync(idInexistente));
        }

        [Fact]
        public async Task Deve_reprovar_proposta_com_motivo()
        {
            var p = Proposta.Criar("Cliente X", "Saúde", 10000, 120, "333");
            await _repo.CriarAsync(p);

            await _app.ReprovarPropostaAsync(p.Id, "Score baixo");

            var atualizado = await _repo.ObterAsync(p.Id);
            Assert.Equal(StatusProposta.Reprovada, atualizado.Status);
            Assert.Equal("Score baixo", atualizado.Observacoes);
        }
    }
}
