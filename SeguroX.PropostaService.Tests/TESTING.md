SeguroX — Estratégia de Testes Automatizados
🎯 Visão Geral

Os testes do projeto SeguroX foram planejados para validar regras de negócio críticas, garantir a robustez dos serviços de aplicação e manter o sistema extensível segundo os princípios SOLID e DDD (Domain-Driven Design).

A suíte de testes cobre o núcleo da aplicação (PropostaService), garantindo que o comportamento do domínio e da camada de aplicação se mantenham consistentes e previsíveis, mesmo sob cenários de erro.

🧱 Estrutura de Testes
SeguroX.PropostaService.Tests
 ┣ 📁 Domain
 │   ┗ 📄 PropostaDomainTests.cs
 ┣ 📁 Application
 │   ┣ 📄 PropostaAppServiceTests.cs
 │   ┣ 📄 FakePropostaRepository.cs
 │   ┣ 📄 FakePropostaValidator.cs
 │   ┗ 📄 FakeCreditoService.cs


Cada pasta segue a arquitetura hexagonal aplicada no projeto principal, espelhando os contextos testados.

🧩 Tipos de Testes
1. Testes de Domínio (Domain)

Objetivo: validar regras de negócio e invariantes da entidade Proposta.

Abrangência:

Criação de proposta válida.

Aprovação e reprovação de propostas com restrições de estado.

Validação de motivos obrigatórios para reprovação.

Prevenção de aprovações múltiplas.

Características:

Executam sem dependências externas.

Garantem pureza do domínio e aderência ao Single Responsibility Principle (SRP).

Exemplo:

[Fact]
public void Deve_Aprovar_Proposta_EmAnalise()
{
    var proposta = Proposta.Criar("João", "Vida", 100000, 250, "123");
    proposta.Aprovar();
    Assert.Equal(StatusProposta.Aprovada, proposta.Status);
}

2. Testes de Aplicação (Application)

Objetivo: validar o comportamento da camada de orquestração, que conecta o domínio a dependências externas (repositórios, motores de crédito e validações).

Abrangência:

Criação de propostas via PropostaAppService.

Verificação de duplicidade (DocumentoCliente).

Aprovação com motor de crédito simulado.

Reprovação com motivo obrigatório.

Exceções esperadas para fluxos inválidos.

Características:

Utilizam fakes ao invés de mocks dinâmicos, mantendo clareza e legibilidade.

Cada dependência do serviço é injetada via construtor, reforçando o Dependency Inversion Principle (DIP).

Isolam o teste da infraestrutura real, reduzindo acoplamento.

🧰 Implementações Fake

As dependências foram substituídas por implementações in-memory simples, otimizadas para performance e leitura:

FakePropostaRepository: simula persistência em memória.

FakePropostaValidator: aplica validações sintéticas e duplicidade.

FakeCreditoService: simula o motor de crédito.

Esses componentes seguem Liskov Substitution Principle (LSP) — podendo ser substituídos por implementações reais sem quebrar o comportamento dos testes.