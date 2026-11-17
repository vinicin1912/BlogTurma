
CREATE DATABASE IF NOT EXISTS blog
CHARACTER SET utf8mb4
COLLATE utf8mb4_unicode_ci;

USE blog;

CREATE TABLE IF NOT EXISTS usuarios (
    Id INT NOT NULL AUTO_INCREMENT,
    Nome VARCHAR(100) NOT NULL,
    Email VARCHAR(150) NOT NULL,
    SenhaHash VARCHAR(255) NOT NULL,
    PRIMARY KEY (Id),
    UNIQUE (Email)
);

CREATE TABLE IF NOT EXISTS publicacoes (
    Id INT NOT NULL AUTO_INCREMENT,
    Titulo VARCHAR(150) NOT NULL,
    Texto TEXT NOT NULL,
    DataCriacao DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UsuarioId INT NOT NULL,
    PRIMARY KEY (Id),
    CONSTRAINT fk_publicacoes_usuarios
        FOREIGN KEY (UsuarioId)
        REFERENCES usuarios(Id)
        ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS comentarios (
    Id INT NOT NULL AUTO_INCREMENT,
    Texto TEXT NOT NULL,
    DataCriacao DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UsuarioId INT NOT NULL,
    PublicacaoId INT NOT NULL,
    PRIMARY KEY (Id),
    CONSTRAINT fk_comentarios_usuarios
        FOREIGN KEY (UsuarioId)
        REFERENCES usuarios(Id)
        ON DELETE CASCADE,
    CONSTRAINT fk_comentarios_publicacoes
        FOREIGN KEY (PublicacaoId)
        REFERENCES publicacoes(Id)
        ON DELETE CASCADE
);
