# 🍔 Good Hamburger - Web API (.NET 8)

API para pedidos da lanchonete **Good Hamburger**, construída com .NET 8, seguindo **Clean Architecture** e **DDD (Domain-Driven Design)**.

---

## 🧱 Arquitetura
```
GoodHamburger/
├── Domain # Entidades e regras de negócio
├── Application # DTOs, validadores e casos de uso
├── Infrastructure # Repositórios (ex: InMemory)
├── WebAPI # API REST (Controllers, Middlewares)
```


---

## 🚀 Tecnologias Usadas

- [.NET 8](https://dotnet.microsoft.com/en-us/)
- ASP.NET Core Web API
- FluentValidation
- Swagger/OpenAPI
- Clean Architecture
- Domain-Driven Design (DDD)
- In-Memory Repository

---

## 📦 Funcionalidades

### 📃 Endpoints

| Método | Rota                  | Descrição                                 |
|--------|-----------------------|-------------------------------------------|
| GET    | `/api/sandwiches`     | Lista todos os lanches disponíveis        |
| GET    | `/api/extras`         | Lista os adicionais                       |
| GET    | `/api/items`          | Lista lanches + adicionais                |
| GET    | `/api/orders`         | Lista todos os pedidos                    |
| POST   | `/api/orders`         | Cria um novo pedido                       |
| PUT    | `/api/orders/{id}`    | Atualiza um pedido                        |
| DELETE | `/api/orders/{id}`    | Remove um pedido                          |
| POST   | `/api/orders/total`   | Calcula o valor total com desconto        |

---

## 💰 Regras de Negócio

- Lanches disponíveis:
  - X-Burger: **R$ 5,00**
  - X-Egg: **R$ 4,50**
  - X-Bacon: **R$ 7,00**
- Adicionais:
  - Fritas: **R$ 2,00**
  - Refrigerante: **R$ 2,50**

### 🎯 Descontos

- **20%**: Sanduíche + Fritas + Refrigerante  
- **15%**: Sanduíche + Refrigerante  
- **10%**: Sanduíche + Fritas  

🚫 Cada pedido **pode conter apenas 1 sanduíche e até 1 de cada adicional**. Repetições geram erro.

---

## 🛠️ Como Executar

1. Clone o repositório:
   ```bash
   git clone https://github.com/seu-usuario/good-hamburger-api.git
   cd good-hamburger-api
