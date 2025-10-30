using System;
using SeguroX.PropostaService.Domain;
using Xunit;

namespace SeguroX.PropostaService.Tests.Domain
{
    public class PropostaDomainTests
    {
        [Fact]
        public void Deve_aprovar_proposta_em_analise()
        {
            var proposta = Proposta.Criar(
                nomeCliente: "João Silva",
                tipoSeguro: "Vida",
                valorSegurado: 100000,
                premioMensal: 250,
                documentoCliente: "12345678900"
            );

            proposta.Aprovar();

            Assert.Equal(StatusProposta.Aprovada, proposta.Status);
            Assert.NotNull(proposta.DataAprovacao);
            Assert.NotNull(proposta.DataExpiracao);
        }

        [Fact]
        public void Nao_deve_aprovar_proposta_que_nao_esta_em_analise()
        {
            var proposta = Proposta.Criar("Ana", "Auto", 50000, 200, "987");
            proposta.Reprovar("dados inconsistentes");

            Assert.Throws<InvalidOperationException>(() => proposta.Aprovar());
        }

        [Fact]
        public void Deve_reprovar_com_motivo()
        {
            var proposta = Proposta.Criar("Carlos", "Residencial", 80000, 180, "555");

            proposta.Reprovar("score baixo");

            Assert.Equal(StatusProposta.Reprovada, proposta.Status);
            Assert.Equal("score baixo", proposta.Observacoes);
        }

        [Fact]
        public void Nao_deve_reprovar_sem_motivo()
        {
            var proposta = Proposta.Criar("Paula", "Saúde", 25000, 150, "777");

            Assert.Throws<ArgumentException>(() => proposta.Reprovar(""));
        }
    }
}
