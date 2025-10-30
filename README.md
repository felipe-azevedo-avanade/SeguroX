# 🛡️ SeguroX – Proposta Service

**Microserviço de Gestão de Propostas de Seguro**  
📘 **.NET 8 | Clean Architecture | DDD | SOLID | Testes Automatizados com xUnit | Swagger**

---

## 🚀 Visão Geral

O **SeguroX.PropostaService** é um microserviço independente responsável por **gerenciar o ciclo de vida de propostas de seguro**, desde a criação até a aprovação ou rejeição.  

O projeto faz parte de uma solução integrada com o **SeguroX.ContratacaoService**, que é responsável por gerar contratos a partir das propostas aprovadas — ou seja, **as APIs interagem entre si**:  
> 🔗 *Uma proposta só pode ser contratada se estiver aprovada no serviço de Propostas.*

Ambos os serviços podem ser executados simultaneamente e testados via **Swagger**:
- 🟩 **PropostaService:** http://localhost:5000/swagger  
- 🟦 **ContratacaoService:** http://localhost:5001/swagger  

> ⚠️ **Docker não implementado localmente** devido a limitações de ambiente na máquina de desenvolvimento.  
> A execução é feita diretamente via `dotnet run` para ambos os microserviços.

---

## 🧩 Arquitetura

A solução segue os princípios da **Clean Architecture**, garantindo baixo acoplamento, alta coesão e independência entre camadas:

```
┌────────────────────────────────────────────────────┐
│                   API Layer                         │
│  - Controllers REST (PropostasController, Contratos)│
│  - Swagger / OpenAPI Documentation                  │
└────────────────────────────────────────────────────┘
           │
           ▼
┌────────────────────────────────────────────────────┐
│               Application Layer                    │
│  - AppServices                                     │
│  - DTOs, Validadores, Orquestração de Casos        │
└────────────────────────────────────────────────────┘
           │
           ▼
┌────────────────────────────────────────────────────┐
│                  Domain Layer                      │
│  - Entidades e Regras de Negócio Puras             │
│  - Proposta, Contrato, StatusProposta              │
└────────────────────────────────────────────────────┘
           │
           ▼
┌────────────────────────────────────────────────────┐
│               Infrastructure Layer                 │
│  - Repositórios InMemory                           │
│  - Persistência simulada sem dependências externas  │
└────────────────────────────────────────────────────┘
```

📄 **Princípios aplicados:**
- **SRP:** cada classe tem uma responsabilidade clara.  
- **OCP:** fácil extensão sem modificação de código existente.  
- **DIP:** camadas de alto nível dependem de abstrações.  
- **DDD:** separação explícita entre domínio, aplicação e infraestrutura.

---

## ⚙️ Stack Técnica

| Camada | Tecnologias e Padrões |
|:-------|:----------------------|
| **API** | ASP.NET Core 8, Swagger, REST Controllers |
| **Aplicação** | Dependency Injection, DTOs, Validadores, Orquestração |
| **Domínio** | Entidades Ricas e Enums Tipados |
| **Infraestrutura** | Repositório InMemory, Task Async/Await, LINQ |
| **Testes** | xUnit, Fakes, FluentAssertions |

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
├── SeguroX.ContratacaoService/
│   ├── API/
│   │   └── ContratacoesController.cs
│   ├── Application/
│   │   ├── ContratacaoAppService.cs
│   │   └── IContratacaoRepository.cs
│   ├── Domain/
│   │   ├── Contrato.cs
│   │   └── PropostaDto.cs
│   └── Repository/
│       └── ContratacaoRepositoryInMemory.cs
│
└── SeguroX.PropostaService.Tests/
    ├── PropostaAppServiceTests.cs
    └── PropostaValidatorTests.cs
```

---

## 🧪 Testes Automatizados

Os testes unitários garantem **cobertura funcional dos fluxos principais** de negócio:

✅ Criação de Propostas  
✅ Validação de Campos Obrigatórios  
✅ Aprovação e Rejeição de Propostas  
✅ Contratação dependente de proposta aprovada  
✅ Persistência InMemory (sem banco real)  

📄 [TESTING.md](./SeguroX.PropostaService.Tests/TESTING.md) descreve as estratégias de teste, cobertura e critérios de assertividade.

---

## 🧰 Como Executar Localmente

```bash
# 1️⃣ Clonar o repositório
git clone https://github.com/felipe-azevedo-avanade/SeguroX.git
cd SeguroX

# 2️⃣ Restaurar dependências
dotnet restore

# 3️⃣ Executar os serviços
dotnet run --project SeguroX.PropostaService      # Porta 5000
dotnet run --project SeguroX.ContratacaoService   # Porta 5001

# 4️⃣ Acessar os Swaggers
Propostas:   http://localhost:5000/swagger
Contratação: http://localhost:5001/swagger
```

---

## 🔄 Integração entre Serviços

O fluxo principal de negócio pode ser validado 100% via Swagger:

1️⃣ Criar uma **Proposta** no serviço `PropostaService`  
2️⃣ Aprovar a proposta (endpoint `PUT /api/propostas/{id}/aprovar`)  
3️⃣ Criar um **Contrato** no `ContratacaoService` referenciando o ID da proposta aprovada  

Se a proposta ainda estiver em análise ou reprovada, o contrato **não será criado** — regra validada no domínio.

---

## 🧭 Decisões Arquiteturais

| Tema | Decisão |
|:------|:--------|
| **Arquitetura** | Clean Architecture + DDD + SOLID |
| **Persistência** | InMemory simulada, isolada por repositórios |
| **Testes** | xUnit + Fakes com alta cobertura |
| **Validação** | `PropostaValidator` e `ContratacaoValidator` centralizam regras |
| **Interação entre serviços** | Comunicação simples via chamadas REST simuladas |
| **Execução** | Sem Docker; execução local via `dotnet run` |
| **Portas** | PropostaService (5000), ContratacaoService (5001) |

---

## 📊 Diagrama da Arquitetura

![Arquitetura SeguroX](./docs/SeguroX_Arquitetura.png)

---

## 🧩 Próximos Passos

- [ ] Implementar persistência real (MongoDB ou SQL Server)  
- [ ] Adicionar autenticação JWT  
- [ ] Configurar CI/CD (GitHub Actions)  
- [ ] Integrar mensageria (RabbitMQ ou Kafka)

---

## 👨‍💻 Autor

**Felipe Michel de Azevedo**  
📍 São Paulo, Brasil  
💼 Arquiteto e Engenheiro de Software  
🌐 [LinkedIn](https://www.linkedin.com/in/felipe-michel-de-azevedo) | [GitHub](https://github.com/felipe-azevedo-avanade)

---

> *“Código limpo é aquele que você teria orgulho de mostrar em uma entrevista.”* 🧠
