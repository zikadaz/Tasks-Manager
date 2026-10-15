# Tasks-Manager

Projeto de estudo com **Client + API** para praticar requisições HTTP, integração com API REST, operações CRUD, manipulação de JSON e tratamento de erros.

O objetivo é entender como cliente e servidor se comunicam e construir uma interação confiável entre os dois.

> ⚠️ Este é um projeto de aprendizado. Não contém código profissional.

## Tecnologias Utilizadas

- C# / .NET 
- Golang
- JSON 

## Estrutura do projeto

```
Tasks-Manager/
├── client/     # Aplicação que se conecta à api
├── server/     # API REST com as regras e os endpoints
└── .gitignore
```

## Pré-requisitos

- [.NET SDK](https://dotnet.microsoft.com/download)
- [Golang](https://go.dev/doc/install)
- [Git](https://git-scm.com/install)

## Como rodar

### 1. Clonar o repositório

```bash
git clone https://github.com/zikadaz/Tasks-Manager.git
cd Tasks-Manager
```

### 2. Iniciar o servidor

```bash
cd server
cd cmd
go run main.go
```

A API ficará disponível em `http://localhost:8080`.

### 3. Iniciar o cliente (client)

Em outro terminal:

```bash
cd client
dotnet run
```

## Endpoints da API


| Método | Rota           | Descrição                  |
|--------|----------------|----------------------------|
| GET    | `/tarefas`       | Lista todas as tarefas     |
| GET    | `/tarefas/{id}`  | Busca uma tarefa pelo id   |
| POST   | `/tarefas`       | Cria uma nova tarefa       |
| PUT    | `/tarefas/{id}`  | Atualiza uma tarefa pelo id        |
| DELETE | `/tarefas/{id}`  | Remove uma tarefa pelo id          | 
| DELETE | `/tarefas`       | Remove todas as tarefas |

### Exemplo de tarefa (JSON)

```json
{
  "title": "Estudar APIs",
  "done": false
}
```

## O que estou praticando nesse projeto

- [x] Separação entre client e server
- [x] Verbos HTTP (GET, POST, PUT, DELETE)
- [x] Serialização e desserialização de JSON
- [x] Tratamento de erros e status codes


## Autor

Feito por [zikadaz](https://github.com/zikadaz) como exercício de estudo.
