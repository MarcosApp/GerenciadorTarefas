# GerenciadorTarefas

CREATE DATABASE gerenciador_task;
go
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

drop table if exists task;
CREATE TABLE task (
    id INT PRIMARY KEY IDENTITY,
    nome VARCHAR(100) NOT NULL,
    descricao VARCHAR(500) NOT NULL,
    status VARCHAR(20) NOT NULL,
    prioridade VARCHAR(20) NOT NULL,
    datacriacao DATETIME DEFAULT GETDATE() NOT NULL,
    dataatualizacao DATETIME NULL,
    projetoid INT,
    FOREIGN KEY (projetoid) REFERENCES project(id)
);