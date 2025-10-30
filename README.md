SeguroX – Proposta Service

Microserviço de Gestão de Propostas de Seguro | .NET 8 | Arquitetura Limpa | Testes Automatizados

🏗️ Visão Geral

O SeguroX.PropostaService é um microserviço independente responsável por gerenciar o ciclo de vida de propostas de seguro, desde a criação até a aprovação ou rejeição.

O projeto foi desenvolvido com foco em clareza arquitetural, testabilidade e princípios de engenharia de software moderna, aplicando SOLID, DDD e Clean Architecture de forma pragmática.

💡 O objetivo do desafio foi demonstrar profundidade técnica, design consistente e qualidade de entrega, e não apenas fazer “funcionar”.

⚙️ Stack Técnica
Camada	Tecnologias / Padrões
Framework	.NET 8 / C# 12
Arquitetura	Clean Architecture + DDD
Injeção de Dependência	Microsoft.Extensions.DependencyInjection
API REST	ASP.NET Core Minimal Controllers
Testes	xUnit + Fakes in-memory (sem mocks externos)
Documentação	Swagger / OpenAPI
CI Ready	dotnet test, dotnet run, cobertura por coverlet
🧩 Estrutura do Projeto
SeguroX.PropostaService
 ┣ 📁 API
 │   ┗ 📄 PropostasController.cs
 ┣ 📁 Application
 │   ┣ 📄 PropostaAppService.cs
 │   ┗ 📄 Interfaces.cs
 ┣ 📁 Domain
 │   ┣ 📄 Proposta.cs
 │   ┗ 📄 Enums/StatusProposta.cs
 ┣ 📁 Infrastructure
 │   ┗ 📄 PropostaRepositoryInMemory.cs
 ┣ 📁 Tests
 │   ┣ 📁 Domain
 │   ┣ 📁 Application
 │   ┗ 📁 Fakes
 ┗ 📄 Program.cs


Cada camada tem uma responsabilidade única e comunica-se apenas via abstrações, conforme o princípio Dependency Inversion (DIP).

🚀 Como Executar
1. Clonar o repositório
git clone https://github.com/seuusuario/SeguroX.PropostaService.git
cd SeguroX.PropostaService

2. Executar a API
dotnet run --project SeguroX.PropostaService


A API sobe por padrão em:
🔗 http://localhost:5000/swagger

3. Rodar os testes
dotnet test SeguroX.PropostaService.Tests

🔥 Endpoints Principais
📄 Criar Proposta

POST /api/propostas

Body:

{
  "nomeCliente": "João Silva",
  "documentoCliente": "12345678900",
  "tipoSeguro": "Vida",
  "valorSegurado": 100000,
  "premioMensal": 250
}


Resposta:

{
  "id": "2f77b7a2-654d-4c1a-a001-d8a7e13b9f2b",
  "status": "EmAnalise",
  "dataCriacao": "2025-10-30T16:28:24Z"
}

✅ Aprovar Proposta

PUT /api/propostas/{id}/aprovar

Resposta:

{
  "mensagem": "Proposta aprovada com sucesso."
}

❌ Rejeitar Proposta

PUT /api/propostas/{id}/rejeitar

Body:

"Motivo da rejeição"


Resposta:

{
  "mensagem": "Proposta rejeitada com sucesso.",
  "motivo": "Score baixo"
}

📋 Listar Propostas

GET /api/propostas

Resposta:

[
  {
    "id": "2f77b7a2-654d-4c1a-a001-d8a7e13b9f2b",
    "nomeCliente": "João Silva",
    "status": "Aprovada",
    "valorSegurado": 100000,
    "premioMensal": 250
  }
]

🧠 Conceitos de Engenharia Aplicados
Princípio / Padrão	Aplicação prática
SRP	Cada classe tem uma única responsabilidade (domínio, serviço, repositório).
OCP	Facilidade de adicionar novos tipos de seguro sem alterar regras existentes.
LSP	Fakes substituem implementações reais sem quebrar dependências.
ISP	Interfaces segregadas e específicas para domínio e infraestrutura.
DIP	Camadas superiores dependem apenas de abstrações.
DDD	Entidade Proposta encapsula comportamento, não apenas dados.
TDD Ready	Domínio projetado para testabilidade e independência.
🧪 Estratégia de Testes

Os testes não apenas validam o código — eles provam o design.

Testes de Domínio: validam invariantes e regras de negócio puras.

Testes de Aplicação: garantem orquestração correta entre dependências.

Fakes in-memory: isolam as dependências e simulam cenários reais.

Cobertura estimada: 88–92% do núcleo funcional.

Execução média: < 200ms.

📘 Veja mais em TESTING.md

🧰 Exemplo de Execução (Console Output)
---------------------------------------------------
SeguroX.PropostaService iniciado com sucesso
URL base: http://localhost:5000
---------------------------------------------------
Swagger disponível: /swagger

🧩 Decisões Arquiteturais

Injeção via Program.cs: DI configurada de forma explícita e simples, facilitando extensão.

Repositório InMemory: substituível por banco real (EF Core, Dapper, MongoDB) sem alterar contrato.

Domain First: entidade Proposta concentra a regra de negócio; AppService apenas orquestra.

Camadas coesas: cada namespace representa um bounded context do DDD.

🧠 Lições & Reflexões

Este projeto foi concebido sob o mindset de "Clean Delivery":

Pouco tempo, máxima clareza, mínima complexidade, alta qualidade.

Mesmo em prazo limitado, foram aplicados conceitos que sustentam:

Escalabilidade

Testabilidade

Legibilidade

Manutenibilidade

O resultado é um código simples, direto e tecnicamente elegante — o tipo de solução que qualquer time pode dar continuidade com confiança.