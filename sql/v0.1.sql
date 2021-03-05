﻿IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210305181524_Initial')
BEGIN
    CREATE TABLE [Fornecedores] (
        [Id] uniqueidentifier NOT NULL,
        [Nome] varchar(200) NOT NULL,
        [Documento] varchar(14) NOT NULL,
        [TipoFornecedor] int NOT NULL,
        [Ativo] bit NOT NULL,
        CONSTRAINT [PK_Fornecedores] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210305181524_Initial')
BEGIN
    CREATE TABLE [Enderecos] (
        [Id] uniqueidentifier NOT NULL,
        [Logradouro] varchar(200) NOT NULL,
        [Numero] varchar(50) NOT NULL,
        [Complemento] varchar(200) NULL,
        [Cep] varchar(8) NOT NULL,
        [Bairro] varchar(100) NOT NULL,
        [Cidade] varchar(100) NOT NULL,
        [Estado] varchar(50) NOT NULL,
        [FornecedorId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_Enderecos] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Enderecos_Fornecedores_FornecedorId] FOREIGN KEY ([FornecedorId]) REFERENCES [Fornecedores] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210305181524_Initial')
BEGIN
    CREATE TABLE [Produtos] (
        [Id] uniqueidentifier NOT NULL,
        [Nome] varchar(200) NOT NULL,
        [Descricao] varchar(1000) NOT NULL,
        [Imagem] varchar(100) NOT NULL,
        [Valor] decimal(18,2) NOT NULL,
        [DataCadastro] datetime2 NOT NULL,
        [Ativo] bit NOT NULL,
        [FornecedorId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_Produtos] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Produtos_Fornecedores_FornecedorId] FOREIGN KEY ([FornecedorId]) REFERENCES [Fornecedores] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210305181524_Initial')
BEGIN
    CREATE UNIQUE INDEX [IX_Enderecos_FornecedorId] ON [Enderecos] ([FornecedorId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210305181524_Initial')
BEGIN
    CREATE INDEX [IX_Produtos_FornecedorId] ON [Produtos] ([FornecedorId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210305181524_Initial')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210305181524_Initial', N'5.0.3');
END;
GO

COMMIT;
GO

