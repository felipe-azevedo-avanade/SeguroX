# 🛡️ SeguroX – Proposta Service

**Microserviço de Gestão de Propostas de Seguro**  
📘 **.NET 8 | Clean Architecture | DDD | SOLID | Testes Automatizados com xUnit | Swagger**

---

## 🚀 Visão Geral

O **SeguroX.PropostaService** é um microserviço independente responsável por **gerenciar o ciclo de vida de propostas de seguro**, desde a criação até a aprovação ou rejeição.

Este projeto foi desenvolvido com **foco em clareza arquitetural, testabilidade, coesão e separação de responsabilidades**, aplicando os princípios **SOLID**, **DDD (Domain-Driven Design)** e **Clean Architecture** de forma pragmática.

O objetivo deste desafio é **demonstrar maturidade técnica, design consistente, qualidade de código e domínio de boas práticas modernas** — não apenas “fazer funcionar”, mas entregar software **escalável, legível e sustentável**.

---

## 🧩 Arquitetura

A solução foi estruturada com base em **Clean Architecture**, garantindo o isolamento entre camadas e a inversão de dependências (DIP):

```
┌────────────────────────────────────────────────────┐
│                   API Layer                         │
│  - Controllers REST (PropostasController)           │
│  - Swagger / OpenAPI Documentation                 │
└────────────────────────────────────────────────────┘
           │
           ▼
┌────────────────────────────────────────────────────┐
│               Application Layer                    │
│  - PropostaAppService                              │
│  - Validadores, DTOs e Orquestração de Casos       │
└────────────────────────────────────────────────────┘
           │
           ▼
┌────────────────────────────────────────────────────┐
│                  Domain Layer                      │
│  - Entidades (Proposta, StatusProposta)             │
│  - Regras de Negócio Puras                         │
└────────────────────────────────────────────────────┘
           │
           ▼
┌────────────────────────────────────────────────────┐
│               Infrastructure Layer                 │
│  - Repositórios InMemory                           │
│  - Implementações concretas de persistência         │
└────────────────────────────────────────────────────┘
```

📄 **Princípios aplicados:**
- **SRP:** cada classe possui uma única responsabilidade.  
- **OCP:** código aberto para extensão, fechado para modificação.  
- **LSP:** substituição segura de implementações.  
- **ISP:** interfaces específicas, evitando dependências desnecessárias.  
- **DIP:** camadas de alto nível dependem de abstrações, não de implementações.

---

## ⚙️ Stack Técnica

| Camada | Tecnologias e Padrões |
|:-------|:----------------------|
| **API** | ASP.NET Core 8, Swagger, REST Controllers |
| **Aplicação** | .NET Dependency Injection, DTOs, Services, Validadores |
| **Domínio** | Entidades Ricas, Enums Fortemente Tipados |
| **Infraestrutura** | InMemory Repository, Task Async/Await, LINQ |
| **Testes** | xUnit, Fakes, FluentAssertions, Coverage 90%+ |

---

## 🧠 Estrutura de Projeto

```
SeguroX/
├── SeguroX.PropostaService/
│   ├── API/
│   │   └── PropostasController.cs
│   ├── Application/
│   │   ├── Dtos/
│   │   │   └── CriarPropostaRequest.cs
│   │   ├── PropostaAppService.cs
│   │   └── PropostaValidator.cs
│   ├── Domain/
│   │   ├── Proposta.cs
│   │   └── StatusProposta.cs
│   ├── Infrastructure/
│   │   └── PropostaRepositoryInMemory.cs
│   └── Program.cs
│
├── SeguroX.PropostaService.Tests/
│   ├── PropostaAppServiceTests.cs
│   └── PropostaValidatorTests.cs
│
└── SeguroX.ContratacaoService/
    ├── API/
    ├── Application/
    ├── Domain/
    └── Repository/
```

---

## 🧪 Testes Automatizados

Os testes foram desenvolvidos em **xUnit**, garantindo cobertura funcional dos principais fluxos do domínio:

✅ Criação de Propostas  
✅ Validação de Campos Obrigatórios  
✅ Aprovação e Rejeição de Propostas  
✅ Persistência InMemory  
✅ Isolamento total sem dependências externas  

📄 [TESTING.md](./SeguroX.PropostaService.Tests/TESTING.md) descreve as estratégias de teste, critérios de cobertura e exemplos de execução.

---

## 🧰 Como Executar Localmente

```bash
# 1️⃣ Clonar o repositório
git clone https://github.com/felipe-azevedo-avanade/SeguroX.git
cd SeguroX/SeguroX.PropostaService

# 2️⃣ Restaurar dependências
dotnet restore

# 3️⃣ Executar a aplicação
dotnet run --project SeguroX.PropostaService

# 4️⃣ Acessar o Swagger
http://localhost:5000/swagger
```

---

## 🧱 Como Rodar os Testes

```bash
dotnet test SeguroX.PropostaService.Tests --collect:"XPlat Code Coverage"
```

O relatório de cobertura é gerado automaticamente e pode ser visualizado em:
```
/TestResults/<GUID>/coverage.cobertura.xml
```

---

## 🧭 Decisões Arquiteturais

| Tema | Decisão |
|:------|:--------|
| **Arquitetura** | Clean Architecture com DDD e princípios SOLID |
| **Persistência** | Repositório InMemory para isolar dependências |
| **Testes** | Fakes ao invés de Mocks, validando comportamento real |
| **Validação** | Centralizada via `PropostaValidator` |
| **Extensibilidade** | Facilidade para troca de camada Infrastructure por banco real |
| **Resiliência** | Tratamento de exceções via `try/catch` e respostas padronizadas HTTP |

---

## 📊 Diagrama da Arquitetura

![Arquitetura SeguroX](./docs/SeguroX_Arquitetura.png)

---

## 🧩 Próximos Passos

- [ ] Adicionar autenticação via JWT  
- [ ] Implementar persistência real (MongoDB ou SQL Server)  
- [ ] Configurar CI/CD com GitHub Actions  
- [ ] Adicionar observabilidade (Serilog + HealthChecks)

---

## 👨‍💻 Autor

**Felipe Michel de Azevedo**  
📍 São Paulo, Brasil  
💼 Arquiteto e Engenheiro de Software  
🌐 [LinkedIn](https://www.linkedin.com/in/felipe-michel-de-azevedo) | [GitHub](https://github.com/felipe-azevedo-avanade)

---

> *“Código limpo é aquele que você teria orgulho de mostrar em uma entrevista.”* 🧠
