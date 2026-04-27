# Kobold.TodoApp

## Visão geral

Este repositório contém uma Web API ASP.NET Core para gerenciamento de tarefas (`Todo`).

A solução foi organizada em camadas simples:

- **Controllers**: expõem os endpoints HTTP.
- **Models**: definem as entidades e view models.
- **Services**: concentram a regra de negócio e o estado em memória.

## Como executar

Executar o projeto da API:

```bash
dotnet run --project src/Kobold.TodoApp.Api
```

Com o Swagger habilitado, a documentação fica disponível na rota configurada no ambiente de desenvolvimento.

## Exercício

O exercício é composto de duas partes.

### Parte I — agrupamento de tarefas em coleções

A aplicação original mantinha todos os `Todo` em uma única lista.  
Nesta solução, foi introduzida a entidade **Collection**, que passa a representar o agrupamento das tarefas.

### O que foi implementado

- criação de coleções;
- listagem de coleções;
- consulta de coleção por id;
- remoção de coleção;
- inclusão de tarefas dentro de uma coleção existente;
- listagem de tarefas por coleção;
- consulta, atualização e remoção de uma tarefa dentro de uma coleção.

### Decisões de modelagem

- `Collection` passou a ser a entidade raiz do agrupamento;
- `Todo` passou a viver dentro de `Collection.Todos`;
- o antigo `TodosController` foi removido do build para evitar duplicidade de estado e inconsistência entre fluxos concorrentes;
- os dados permanecem em memória, como na proposta original do projeto.

### Parte II — tratamento de erros

Foi implementado um middleware global para tratamento de exceções.  
O objetivo é evitar a exposição de detalhes internos da aplicação e devolver mensagens claras ao consumidor da API.

### Comportamento adotado

- `ArgumentException` e `ArgumentNullException` retornam **400 Bad Request**;
- `InvalidOperationException` retorna **404 Not Found**;
- exceções inesperadas retornam **500 Internal Server Error** com mensagem genérica;
- a resposta enviada ao cliente segue um formato simples, contendo apenas a mensagem de erro.

## Avaliação

No processo de avaliação do código, serão observados principalmente:

- aderência da implementação ao código existente;
- clareza do código implementado;
- documentação das alterações efetuadas;
- estrutura das mensagens de commit.

A solução para o problema não é única. O candidato deve analisar o código existente, definir as funcionalidades a serem implementadas e implementá-las. A avaliação da solução apresentada será realizada em conversa com o candidato, com o objetivo de entender o processo de análise e tomada de decisões que levou àquela solução.
