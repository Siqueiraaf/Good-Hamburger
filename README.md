
# 🍔 GoodHamburger API

Sistema de pedidos para uma lanchonete chamado **Good Hamburger**. A API permite que os clientes realizem pedidos compostos por sanduíches, fritas e refrigerantes, aplicando regras de desconto e validando restrições.

---

## 📁 Estrutura do Projeto

```
GoodHamburger/
├── GoodHamburger.Application     # DTOs e validações (FluentValidation)
├── GoodHamburger.Domain          # Entidades, enums, exceções, regras de negócio
├── GoodHamburger.Infrastructure  # Repositórios e acesso a dados (em memória)
├── GoodHamburger.WebAPI          # Camada de apresentação (controllers, filtros, swagger)
├── GoodHamburger.Tests           # Testes unitários (xUnit + FluentAssertions)
└── .vscode                       # Configurações do ambiente (opcional)
```

---

## 🧱 Arquitetura

O projeto segue uma abordagem modular com separação em camadas:

- **Domain:** Lógica de negócio e regras.
- **Application:** Validações e modelos de dados (DTOs).
- **Infrastructure:** Simulação de persistência usando repositório em memória.
- **WebAPI:** Interface RESTful, versionamento, tratamento global de erros e Swagger.
- **Tests:** Testes unitários isolando regras do domínio.

---

## 🚀 Tecnologias Utilizadas

- .NET 8 (ASP.NET Core)
- FluentValidation
- ASP.NET API Versioning
- Swashbuckle (Swagger)
- xUnit (testes)
- FluentAssertions (testes legíveis)
- Hot reload com `dotnet watch`

---

## ✅ Funcionalidades da API

- 📦 Criar pedidos com:
  - 1 sanduíche
  - 1 fritas (opcional)
  - 1 refrigerante (opcional)
- 🧮 Aplicar desconto automaticamente:
  - **20%** → Sanduíche + Fritas + Refrigerante
  - **15%** → Sanduíche + Refrigerante
  - **10%** → Sanduíche + Fritas
- ❌ Impedir itens duplicados no pedido.
- 🔄 Versionamento via URL (ex: `/api/v1/orders`)
- ⚠️ Tratamento global de exceções
- 📄 Documentação Swagger gerada por versão

---

## 🔒 Regras de Negócio

Combinação               | Desconto
------------------------|---------
Sanduíche + Fritas      | 10%
Sanduíche + Refrigerante| 15%
Todos os 3 itens        | 20%

- 🥪 **Sanduíche obrigatório:**  
  Todo pedido deve conter **exatamente um sanduíche**. Pedidos sem sanduíche ou com múltiplos serão rejeitados.

- 🍟🥤 **Fritas e refrigerante opcionais:**  
  Ambos são **itens complementares** e **não obrigatórios**, mas se fornecidos, devem ser **únicos** — ou seja, apenas uma fritas **e/ou** um refrigerante por pedido.

- 🚫 **Proibição de duplicatas:**  
  Incluir mais de um item do mesmo tipo (ex: duas fritas ou dois refrigerantes) resultará em uma resposta de erro **HTTP 400 (Bad Request)** com uma mensagem clara indicando o problema.

---

## 🧪 Testes Unitários

Localizados em `GoodHamburger.Tests/Services/OrderServiceTests.cs`:

- ✅ Cálculo correto dos descontos
- ✅ Validação de itens duplicados
- ✅ Rejeição de pedidos inválidos (ex: sem sanduíche)

Tecnologias:
- `xUnit` para estrutura de testes
- `FluentAssertions` para asserções descritivas

---

## 🔄 Versionamento da API

Configurado com `Asp.Versioning`, via URL Segment:

- Exemplo: `/api/v1/orders`
- Suporte à evolução sem quebrar versões antigas
- Explorador de versões ativado no Swagger

---

## 🛠️ Setup e Execução

1. **Clonar o repositório:**

```bash
git clone https://github.com/seu-usuario/GoodHamburger.git
cd GoodHamburger
```

2. **Rodar com hot reload:**

```bash
dotnet watch run --project GoodHamburger.WebAPI
```

3. **Acessar Swagger:**
   - URL: [https://localhost:7021/swagger](https://localhost:7021/swagger)

---

## 📌 Considerações

- O repositório atual é **in-memory**.
- Pode ser substituído futuramente por um `DbContext` com Entity Framework.
- O projeto foi estruturado visando **testabilidade**, **manutenibilidade** e **boas práticas** de arquitetura limpa.
- O Projeto contem um Arquivo Docker.

---
## 🐳 Uso com Docker
A aplicação pode ser executada via contêiner Docker, facilitando testes locais, deploy e integração contínua.

⚙️ Build e Execução

``docker-compose up --build``

### 🌐 Acesso via navegador Swagger disponível em: 

``http://localhost:8080/swagger``


## 👨‍💻 Autor

**Rafael Siqueira**  
[dev.rafaelsiqueira@gmail.com](mailto:dev.rafaelsiqueira@gmail.com)  
[https://github.com/Siqueiraaf](https://github.com/Siqueiraaf)
