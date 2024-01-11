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
    FOREIGN KEY (usuarioid) REFERENCES usuario(id) -- Corrige a referência para usuarioid
);
