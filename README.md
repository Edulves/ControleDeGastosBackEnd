# Controle de Gastos — Backend (ExpensesControl API)

Uma API RESTful para controle financeiro pessoal, construída com **.NET 9** e **PostgreSQL**. Permite que você registre seus gastos do dia a dia, categorize suas despesas, acompanhe contas fixas e visualize relatórios consolidados — tudo com autenticação segura via JWT.

---

## Sumário

- [Para quem é este projeto?](#para-quem-é-este-projeto)
- [Funcionalidades](#funcionalidades)
- [Tecnologias Utilizadas](#tecnologias-utilizadas)
- [Arquitetura do Projeto](#arquitetura-do-projeto)
- [Estrutura de Diretórios](#estrutura-de-diretórios)
- [Modelos de Dados](#modelos-de-dados)
- [Endpoints da API](#endpoints-da-api)
- [Configuração e Execução](#configuração-e-execução)
- [Variáveis de Ambiente](#variáveis-de-ambiente)
- [Exemplos de Uso](#exemplos-de-uso)

---

## Para quem é este projeto?

Se você está cansado de planilhas e quer uma forma prática de organizar suas finanças, esta API é o ponto de partida. Ela serve como **backend** para um aplicativo web ou mobile de controle de gastos. Com ela, você pode:

- Criar sua conta e fazer login de forma segura
- Registrar despesas diárias com valores, datas e observações
- Organizar seus gastos por categorias (ex: Alimentação, Transporte, Lazer)
- Gerenciar contas fixas (assinaturas, aluguel, etc.) e marcar como pagas/não pagas
- Visualizar relatórios: quanto gastou por categoria, por dia, total do mês
- Comparar contas fixas pagas vs. pendentes

---

## Funcionalidades

| Funcionalidade       | Descrição                                                                   |
| -------------------- | --------------------------------------------------------------------------- |
| **Autenticação**     | Cadastro e login com Identity + JWT Bearer Token                            |
| **Despesas Diárias** | CRUD completo com paginação, filtros por data, categoria e observação       |
| **Despesas Fixas**   | CRUD completo com controle de pagamento (pago/pendente)                     |
| **Categorias**       | Gerencie categorias personalizadas para classificar seus gastos             |
| **Consolidação**     | Relatórios: total por categoria, por dia, comparação de fixas, total mensal |
| **Soft Delete**      | Nenhum registro é excluído permanentemente — apenas marcado como removido   |
| **Result Pattern**   | Padronização de respostas da API (sucesso/erro com status code e detalhes)  |
| **Paginação**        | Todas as listagens suportam paginação para melhor performance               |
| **OpenAPI / Scalar** | Documentação interativa da API disponível em modo desenvolvimento           |

---

## Tecnologias Utilizadas

| Tecnologia                    | Versão  | Finalidade                                 |
| ----------------------------- | ------- | ------------------------------------------ |
| .NET                          | 9.0     | Framework principal                        |
| ASP.NET Core                  | 9.0     | API Web                                    |
| Entity Framework Core         | 9.0     | ORM / Acesso a dados                       |
| Npgsql (PostgreSQL)           | 9.0.4   | Banco de dados                             |
| Microsoft.AspNetCore.Identity | 9.0     | Gerenciamento de usuários                  |
| JWT Bearer Authentication     | 9.0     | Autenticação por token                     |
| Scalar.AspNetCore             | 2.8.11  | Documentação interativa OpenAPI            |
| ClosedXML                     | 0.105.0 | Geração de planilhas Excel                 |
| EFCore.NamingConventions      | 9.0     | Conversão automática para snake_case no BD |

---

## Arquitetura do Projeto

O projeto segue uma arquitetura em camadas com **Dependency Injection**, separando claramente as responsabilidades:

```
┌─────────────────────────────────────────────────┐
│                  Controllers                     │  ← Endpoints HTTP
├─────────────────────────────────────────────────┤
│                    Services                      │  ← Regras de negócio
├─────────────────────────────────────────────────┤
│                 Repositories                     │  ← Acesso a dados (EF Core)
├─────────────────────────────────────────────────┤
│              Models / DbContext                  │  ← Entidades e mapeamento
└─────────────────────────────────────────────────┘
```

**Fluxo típico de uma requisição:**

1. **Controller** recebe a requisição HTTP e aplica `[Authorize]` (se necessário)
2. **Controller** chama o **Service** correspondente, passando os DTOs de request
3. **Service** aplica regras de negócio e chama o **Repository**
4. **Repository** usa o **DbContext** do EF Core para consultar/alterar o banco
5. O resultado é empacotado no **ResultPattern\<T\>** e convertido para `IActionResult` pelo método de extensão `ToIActionResult()`

### Padrões de Design

- **Result Pattern** (`ResultPattern<T>`): Toda operação retorna um objeto padronizado com `IsSuccess`, `Value`, `StatusCode`, `Title` e `Detail`, garantindo consistência nas respostas.
- **Repository Pattern**: Abstração da camada de dados, facilitando testes e manutenção.
- **Query Extensions**: Métodos de extensão sobre `IQueryable<T>` para filtros reutilizáveis (ex: `FilterByUserId()`, `FilterByMonthAndYear()`, `FilterRemoveDeleted()`).
- **Soft Delete**: Classe base `Entity` com `IsDeleted`, permitindo recuperação de dados.
- **Paginação**: `PagedResult<T>` com suporte a página atual, itens por página, total de itens e total de páginas calculado automaticamente.

---

## Estrutura de Diretórios

```
📦 ControleDeGastos/
├── 📁 Controllers/               # Endpoints da API
│   ├── AuthController.cs
│   ├── DailyExpensesController.cs
│   ├── FixedExpensesController.cs
│   ├── TransactionCategoriesController.cs
│   └── DataConsolidationController.cs
│
├── 📁 Models/                    # Entidades do domínio
│   ├── Entity.cs                 # Classe base (soft delete + createdAt)
│   ├── DailyExpense.cs
│   ├── FixedExpense.cs
│   └── TransactionCategory.cs
│
├── 📁 DTOs/
│   ├── 📁 Requests/              # Objetos de entrada da API
│   │   ├── 📁 UserRequests/
│   │   ├── 📁 DailyExpensesRequests/
│   │   ├── 📁 FixedExpensesRequests/
│   │   ├── 📁 CategoriesRequests/
│   │   └── 📁 DataConsolidationRequests/
│   └── 📁 Responses/             # Objetos de saída da API
│       ├── 📁 UserReponses/
│       ├── 📁 DailyExpensesReponses/
│       └── 📁 DataConsolidationResponses/
│
├── 📁 Data/
│   ├── 📁 Context/
│   │   └── AppDbContext.cs       # DbContext do EF Core
│   ├── 📁 Configurations/        # Fluent API mappings (EF Core)
│   ├── 📁 ResultPattern/         # Padrão de resultado padronizado
│   │   ├── 📁 Base/
│   │   │   └── ResultPattern.cs
│   │   └── 📁 Extensions/
│   │       └── ResultPatternExtension.cs
│   └── 📁 PaginatedResult/       # Paginação
│       ├── PagedResult.cs
│       ├── 📁 PaginatedRequestDTO/
│       └── 📁 Extentions/
│
├── 📁 Queries/                   # Extensions de filtros IQueryable
│   ├── DailyExpensesQueries.cs
│   ├── FixedExpensesQueries.cs
│   └── TransactionsCategoriesQueries.cs
│
├── 📁 Repositories/
│   ├── 📁 RepositoryInterfaces/  # Contratos
│   └── 📁 RepositoryImplementations/  # Implementações
│
├── 📁 Service/
│   ├── 📁 ServiceInterfaces/     # Contratos
│   └── 📁 ServiceImplementations/     # Implementações (lógica de negócio)
│
├── 📁 DependecyInjection/        # Extensions para registrar DI
├── 📁 Migrations/                # Migrations do EF Core
├── 📁 Properties/                # Configurações do projeto (launchSettings)
│
├── Program.cs                    # Ponto de entrada / configuração da aplicação
├── appsettings.json              # Configurações (connection string, JWT)
├── docker-compose.yml            # Orquestração Docker
├── .env.example                  # Exemplo de variáveis de ambiente
├── ExpensesControl.csproj        # Arquivo de projeto .NET
└── ExpensesControl.http          # Arquivo de testes de requisição HTTP
```

---

## Modelos de Dados

### Entity (Base)

```csharp
public abstract class Entity
{
    public bool IsDeleted { get; set; } = false;   // Soft delete
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
```

### DailyExpense

| Campo                 | Tipo     | Descrição                 |
| --------------------- | -------- | ------------------------- |
| DailyExpenseId        | int      | PK                        |
| ExpenseDate           | DateOnly | Data do gasto             |
| Amount                | decimal  | Valor                     |
| Note                  | string?  | Observação opcional       |
| TransactionCategoryId | int?     | FK → TransactionCategory  |
| UserId                | string   | FK → IdentityUser         |
| IsDeleted             | bool     | Soft delete (herdado)     |
| CreatedAt             | DateTime | Data de criação (herdado) |

### FixedExpense

| Campo            | Tipo     | Descrição                 |
| ---------------- | -------- | ------------------------- |
| FixedExpenseId   | int      | PK                        |
| Description      | string   | Descrição da conta fixa   |
| Amount           | decimal  | Valor                     |
| IsPaid           | bool     | Status de pagamento       |
| FixedExpenseDate | DateOnly | Data de referência        |
| UserId           | string   | FK → IdentityUser         |
| IsDeleted        | bool     | Soft delete (herdado)     |
| CreatedAt        | DateTime | Data de criação (herdado) |

### TransactionCategory

| Campo                 | Tipo   | Descrição         |
| --------------------- | ------ | ----------------- |
| TransactionCategoryId | int    | PK                |
| Name                  | string | Nome da categoria |

---

## Endpoints da API

### Autenticação — `api/auth`

| Método | Rota                | Descrição               | Autenticação |
| ------ | ------------------- | ----------------------- | ------------ |
| POST   | `api/auth/register` | Cadastrar novo usuário  | ❌           |
| POST   | `api/auth/login`    | Login e retorno do JWT  | ❌           |
| GET    | `api/auth/me`       | Dados do usuário logado | ✅           |

### Despesas Diárias — `dailyexpenses`

| Método | Rota                 | Descrição                               | Autenticação |
| ------ | -------------------- | --------------------------------------- | ------------ |
| GET    | `dailyexpenses`      | Listar gastos (com paginação e filtros) | ✅           |
| POST   | `dailyexpenses`      | Criar um ou múltiplos gastos            | ✅           |
| PUT    | `dailyexpenses`      | Atualizar um ou múltiplos gastos        | ✅           |
| DELETE | `dailyexpenses/{id}` | Remover um gasto (soft delete)          | ✅           |

### Despesas Fixas — `fixedexpenses`

| Método | Rota                 | Descrição                               | Autenticação |
| ------ | -------------------- | --------------------------------------- | ------------ |
| GET    | `fixedexpenses`      | Listar contas fixas (com filtros)       | ✅           |
| POST   | `fixedexpenses`      | Criar uma ou múltiplas contas fixas     | ✅           |
| PUT    | `fixedexpenses`      | Atualizar uma ou múltiplas contas fixas | ✅           |
| DELETE | `fixedexpenses/{id}` | Remover uma conta fixa (soft delete)    | ✅           |

### Categorias — `transactioncategories`

| Método | Rota                         | Descrição                             | Autenticação |
| ------ | ---------------------------- | ------------------------------------- | ------------ |
| GET    | `transactioncategories`      | Listar todas as categorias            | ✅           |
| POST   | `transactioncategories`      | Criar uma ou múltiplas categorias     | ✅           |
| PUT    | `transactioncategories`      | Atualizar uma ou múltiplas categorias | ✅           |
| DELETE | `transactioncategories/{id}` | Remover uma categoria                 | ✅           |

### Consolidação — `dataconsolidation`

| Método | Rota                                    | Descrição                      | Autenticação |
| ------ | --------------------------------------- | ------------------------------ | ------------ |
| GET    | `dataconsolidation/ExpensesPerCategory` | Gastos agrupados por categoria | ✅           |
| GET    | `dataconsolidation/ExpensesPerDay`      | Gastos agrupados por dia       | ✅           |

---

## Configuração e Execução

### Pré-requisitos

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [PostgreSQL](https://www.postgresql.org/download/) (ou Docker com imagem PostgreSQL)
- [Docker](https://www.docker.com/) (opcional, para execução conteinerizada)

### Execução Local

```bash
# 1. Clone o repositório
git clone https://github.com/seu-usuario/ControleDeGastos.git
cd ControleDeGastos

# 2. Configure as variáveis de ambiente
#    Copie o arquivo .env.example para .env e preencha os valores
cp .env.example .env

# 3. Execute as migrations para criar o banco de dados
dotnet ef database update

# 4. Execute a aplicação
dotnet run
```

A API estará disponível em `http://localhost:5011` (ou porta configurada no `launchSettings.json`).

### Execução com Docker Compose

```bash
# Certifique-se de ter o arquivo .env configurado e uma rede Docker externa:
docker network create backend

# Suba a aplicação:
docker-compose up -d
```

O container será exposto na porta `5000:8080`.

### Documentação Interativa (Desenvolvimento)

Com a aplicação rodando em modo **Development**, acesse:

```
http://localhost:<porta>/scalar/v1
```

Lá você poderá testar todos os endpoints diretamente pela interface do **Scalar** (alternativa moderna ao Swagger UI) — incluindo a autenticação JWT persistente.

---

## Variáveis de Ambiente

Crie um arquivo `.env` na raiz do projeto com base no `.env.example`:

```env
POSTGRES_DATABASE=suaBaseDeDados
POSTGRES_USER=seu_usuario
POSTGRES_PASSWORD=sua_senha

JWT_KEY=suachavejwtcom32caracteresoumais!
JWT_ISSUER=SuaApi.Net9
JWT_AUDIENCE=seuapp
JWT_EXPIRES_IN_MINUTES=60
```

> ⚠️ **Importante:** A chave JWT (`JWT_KEY`) deve ter no mínimo 32 caracteres para garantir a segurança do algoritmo HMAC-SHA256.

### Configuração via `appsettings.json`

As mesmas configurações podem ser definidas diretamente no `appsettings.json` (que está no `.gitignore` por segurança):

```json
{
    "ConnectionStrings": {
        "DefaultConnection": "Host=localhost;Port=1234;Database=suaBaseDeDados;Username=usuario;Password=senha"
    },
    "Jwt": {
        "Key": "suachavejwtcom32caracteresoumais!",
        "Issuer": "SuaApi.Net9",
        "Audience": "seuapp",
        "ExpiresInMinutes": 60
    }
}
```

---

## Exemplos de Uso

### 1. Criar uma conta

```bash
POST http://localhost:5011/api/auth/register
Content-Type: application/json

{
  "email": "usuario@email.com",
  "password": "MinhaSenha123"
}
```

### 2. Fazer login

```bash
POST http://localhost:5011/api/auth/login
Content-Type: application/json

{
  "email": "usuario@email.com",
  "password": "MinhaSenha123"
}
```

**Resposta:**

```json
{
    "token": "eyJhbGciOiJIUzI1NiI...",
    "expiration": "2026-08-15T18:00:00Z"
}
```

### 3. Usar o token nas próximas requisições

Adicione o header `Authorization: Bearer <seu-token>` em todas as chamadas para os endpoints protegidos.

### 4. Registrar uma despesa diária

```bash
POST http://localhost:5011/dailyexpenses
Authorization: Bearer eyJhbGciOiJIUzI1NiI...
Content-Type: application/json

[
  {
    "expenseDate": "2026-08-15",
    "amount": 45.90,
    "note": "Almoço no restaurante",
    "transactionCategoryId": 1
  }
]
```

### 5. Consultar gastos do mês por categoria

```bash
GET http://localhost:5011/dataconsolidation/ExpensesPerCategory?year=2026&month=8
Authorization: Bearer eyJhbGciOiJIUzI1NiI...
```

---
