# Gerenciador de Tarefas

Este é um projeto de Gerenciador de Tarefas incrível. A seguir estão as instruções para configurar e executar o projeto usando Docker.

## Pré-requisitos

Certifique-se de ter o Docker instalado em sua máquina.

- [Docker](https://www.docker.com/)

## 1) Fase -  Configuração do Ambiente - ✨


### 1. Construir a Imagem Docker

```bash
docker-compose build
```
Iniciar os Contêineres Docker

## Configuração do Ambiente

### 2. Iniciar os Contêineres Docker

```bash
docker-compose up -d
```

### 3. Scripts de Base de Dados

Deve ser executado o script abaixo no SQL

```bash
CREATE DATABASE gerenciador_task;
GO
use gerenciador_task;

GO
drop table if exists project;
CREATE TABLE project(
    id int PRIMARY KEY IDENTITY,
    nome VARCHAR(50) not null,
    descricao VARCHAR(500) not null,
    datainicio DATETIME not null,
    datafim DATETIME not null
)

GO
DROP TABLE IF EXISTS usuario;
CREATE TABLE usuario (
    id INT PRIMARY KEY IDENTITY,
    nome VARCHAR(50) NOT NULL,
    email VARCHAR(100) NOT NULL,
    senha VARCHAR(1000) NOT NULL, 
    perfil int not null
);

GO
DROP TABLE IF EXISTS task;
CREATE TABLE task (
    id INT PRIMARY KEY IDENTITY,
    nome VARCHAR(100) NOT NULL,
    descricao VARCHAR(500) NOT NULL,
    status VARCHAR(20) NOT NULL,
    prioridade VARCHAR(20) NOT NULL,
    datacriacao DATETIME DEFAULT GETDATE() NOT NULL,
    dataatualizacao DATETIME NULL,
    projetoid INT,
    usuarioid INT, 
    FOREIGN KEY (projetoid) REFERENCES project(id),
    FOREIGN KEY (usuarioid) REFERENCES usuario(id) 
);

GO
DROP TABLE IF EXISTS comentario;
CREATE TABLE comentario (
    id INT PRIMARY KEY IDENTITY,
    texto VARCHAR(500) NOT NULL,
    datacriacao DATETIME DEFAULT GETDATE() NOT NULL,
    usuarioid INT,
    taskid INT,
    FOREIGN KEY (usuarioid) REFERENCES usuario(id),
    FOREIGN KEY (taskid) REFERENCES task(id)
);

GO
CREATE TRIGGER trg_AfterInsertComentario
ON comentario
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @TaskId INT;

    SELECT @TaskId = taskid FROM inserted;

    UPDATE task
    SET ultimaatualizacao = GETDATE()
    WHERE id = @TaskId;
END;
```

Execução de Testes
### 1. Restaurar as Dependências de Testes
```bash
docker-compose exec app dotnet restore --configfile ./tests/GerenciadorTarefas.Tests/GerenciadorTarefas.Tests.csproj
```

###  2. Executar os Testes 
```bash
docker-compose exec app dotnet test --configuration Release ./tests/GerenciadorTarefas.Tests/GerenciadorTarefas.Tests.csproj
```

### Parar os Contêineres
```bash
docker-compose down
```

## 2)Fase - Sugestões para Refinamento Futuro e Melhorias Contínuas ✨

Durante o desenvolvimento deste projeto.
As seguintes sugestões de melhorias são cruciais para orientar os próximos passos:

1. **Aprimoramento da Autenticação:**
   - Avaliar a implementação atual da autenticação.
   - Considerar a introdução de padrões mais recentes de autenticação, como OAuth 2.0 ou OpenID Connect.
   - Implementar uma política de senhas mais robusta, exigindo combinações de caracteres, números e símbolos.

2. **Paginação na Volumetria de Tarefas:**
   - Implementar uma solução de paginação para lidar com grandes volumes de tarefas.
   - Avaliar estratégias eficientes para buscar e exibir tarefas de forma paginada.
   - Garantir uma resposta rápida e eficiente, especialmente quando há um grande número de tarefas no sistema.

3. **Melhoria na Criação de Hash para Senha:**
   - Revisar a implementação atual de hashing de senhas.
   - Considerar algoritmos de hash mais seguros, como bcrypt ou Argon2.
   - Avaliar a introdução de um processo de "salting" para aumentar a segurança das senhas armazenadas.

4. **Controle de Acesso e Permissões:**
   - Implementar um sistema robusto de controle de acesso com base nos perfis de usuário.
   - Garantir que apenas usuários autorizados tenham acesso a determinadas funcionalidades.
   - Revisar as permissões de acesso para garantir a segurança e a privacidade dos dados.

5. **Monitoramento e Logging:**
   - Implementar ferramentas de monitoramento para acompanhar o desempenho do sistema em tempo real.
   - Configurar logs detalhados para rastrear eventos importantes e ajudar na solução de problemas.
   - Considerar a integração com ferramentas de análise de logs para insights mais profundos.

Estas sugestões são apenas pontos de partida e podem ser ajustadas conforme a evolução do projeto. O feedback contínuo dos usuários e das partes interessadas é crucial para garantir a eficácia dessas melhorias.


<!-- Markdown link & img dfn's -->
[npm-image]: https://img.shields.io/npm/v/datadog-metrics.svg?style=flat-square
[npm-url]: https://npmjs.org/package/datadog-metrics
[npm-downloads]: https://img.shields.io/npm/dm/datadog-metrics.svg?style=flat-square
[travis-image]: https://img.shields.io/travis/dbader/node-datadog-metrics/master.svg?style=flat-square
[travis-url]: https://travis-ci.org/dbader/node-datadog-metrics
[wiki]: https://github.com/yourname/yourproject/wiki


![Banana](http://cdn.osxdaily.com/wp-content/uploads/2013/07/dancing-banana.gif)
