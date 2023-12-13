CREATE DATABASE VC_SKF
Go
USE VC_SKF
GO
CREATE TABLE [dbo].[Usuario] (
    [Id]         NUMERIC (18)  IDENTITY (1, 1) NOT NULL,
    [Nome]       VARCHAR (50)  NULL,
    [login]      VARCHAR (50)  NULL,
    [senha]      VARCHAR (50)  NULL,
    [acesso]     VARCHAR (50)  NULL,
    [dateinsert] DATETIME2 (2) NULL,
    [dateupdate] DATETIME      NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE TABLE [dbo].[Configuracao] (
    [id]            NUMERIC (18)  IDENTITY (1, 1) NOT NULL,
	[estacao]       VARCHAR (50)  NULL,
	[id_Impressora] INT           NULL,
	[id_Etiqueta]   INT			  null,
	[copias]		INT			  null,
	[logo_empresa]	varbinary(max)NULL,
    [dateinsert]    DATETIME2 (2) NULL,
    [dateupdate]    DATETIME2 (2) NULL
);
GO
CREATE TABLE [dbo].[Rede] (
    [Id]             NUMERIC (18)  IDENTITY (1, 1) NOT NULL,
	[tipo]		     VARCHAR (50)  NULL,
	[tipo_impressao] INT		   NULL,/**/
	[impressora]     VARCHAR (50)  NULL,/**/
	[simplificar]    bit           NULL,/**/
	[fabricante]     VARCHAR (50)  NULL,
    [modelo]         VARCHAR (50)  NULL,
	[protocolo]	     VARCHAR (50)  NULL,
    [nome]           VARCHAR (50)  NULL,
    [addr]           INT           NULL,
    [baud_rate]      INT           NULL,
    [parent]         VARCHAR (50)  NULL,
    [full_name]      VARCHAR (50)  NULL,
	[num_parent]     INT           NULL,
	[IP]		     VARCHAR (50)  NULL,
	[porta]          INT			 NULL,
	[MAC]		     VARCHAR (50)  NULL,
	[casasDecimais]   INT			 NULL,
	[dateinsert]     DATETIME2 (2) NULL,
    [dateupdate]     DATETIME      NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO
CREATE TABLE [dbo].[Etiqueta] (
    [id]            NUMERIC (18)  IDENTITY (1, 1) NOT NULL,
	[nome_etiqueta] VARCHAR (50)  NULL,
	[arquivo]	    VARCHAR (MAX) NULL,
    [dateinsert]    DATETIME2 (2) NULL,
    [dateupdate]    DATETIME2 (2) NULL
);
GO
CREATE TABLE [dbo].[Acessos] (
    [Id]						    NUMERIC (18)  IDENTITY (1, 1) NOT NULL,
    [Acesso]				   	    VARCHAR (100)  NULL,

	[pesagem_view]					BIT			NULL DEFAULT 0,
	[pesagem_add]					BIT			NULL DEFAULT 0,
	[pesagem_edit]					BIT			NULL DEFAULT 0,
	[pesagem_remove]				BIT			NULL DEFAULT 0,
	[pesagem_search]				BIT			NULL DEFAULT 0,

	[relatorio_view]				BIT			NULL DEFAULT 0,
	[relatorio_add]					BIT			NULL DEFAULT 0,
	[relatorio_edit]				BIT			NULL DEFAULT 0,
	[relatorio_remove]				BIT			NULL DEFAULT 0,
	[relatorio_search]				BIT			NULL DEFAULT 0,

	[rede_view]						BIT			NULL DEFAULT 0,
	[rede_add]						BIT			NULL DEFAULT 0,
	[rede_edit]						BIT			NULL DEFAULT 0,
	[rede_remove]					BIT			NULL DEFAULT 0,
	[rede_search]					BIT			NULL DEFAULT 0,

	[sistema_view]					BIT			NULL DEFAULT 0,
	[sistema_add]					BIT			NULL DEFAULT 0,
	[sistema_edit]					BIT			NULL DEFAULT 0,
	[sistema_remove]				BIT			NULL DEFAULT 0,
	[sistema_search]				BIT			NULL DEFAULT 0,

	[usuario_view]					BIT			NULL DEFAULT 0,
	[usuario_add]					BIT			NULL DEFAULT 0,
	[usuario_edit]					BIT			NULL DEFAULT 0,
	[usuario_remove]				BIT			NULL DEFAULT 0,
	[usuario_search]				BIT			NULL DEFAULT 0,

	[receita_view]					BIT			NULL DEFAULT 0,
	[receita_add]					BIT			NULL DEFAULT 0,
	[receita_edit]					BIT			NULL DEFAULT 0,
	[receita_remove]				BIT			NULL DEFAULT 0,
	[receita_search]				BIT			NULL DEFAULT 0,

	[tipoReceita_view]				BIT			NULL DEFAULT 0,
	[tipoReceita_add]				BIT			NULL DEFAULT 0,
	[tipoReceita_edit]				BIT			NULL DEFAULT 0,
	[tipoReceita_remove]			BIT			NULL DEFAULT 0,
	[tipoReceita_search]			BIT			NULL DEFAULT 0,

	[Recipiente_view]				BIT			NULL DEFAULT 0,
	[Recipiente_add]				BIT			NULL DEFAULT 0,
	[Recipiente_edit]				BIT			NULL DEFAULT 0,
	[Recipiente_remove]				BIT			NULL DEFAULT 0,
	[Recipiente_search]				BIT			NULL DEFAULT 0,

	[Bandeja_view]					BIT			NULL DEFAULT 0,
	[Bandeja_add]					BIT			NULL DEFAULT 0,
	[Bandeja_edit]					BIT			NULL DEFAULT 0,
	[Bandeja_remove]				BIT			NULL DEFAULT 0,
	[Bandeja_search]				BIT			NULL DEFAULT 0,

	[Produto_view]					BIT			NULL DEFAULT 0,
	[Produto_add]					BIT			NULL DEFAULT 0,
	[Produto_edit]					BIT			NULL DEFAULT 0,
	[Produto_remove]				BIT			NULL DEFAULT 0,
	[Produto_search]				BIT			NULL DEFAULT 0,

	id_usuario						INT			NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO
CREATE TABLE [dbo].[Avisos] (
    [Id]         NUMERIC (18)  IDENTITY (1, 1) NOT NULL,
    [Mensagem]   VARCHAR (MAX)  NULL,
	[dateinsert] DATETIME2 (2) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO
CREATE TABLE [dbo].[Produto] (
    [Id]			NUMERIC (18)  IDENTITY (1, 1) NOT NULL,
    [part_number]   VARCHAR (150)  UNIQUE NULL,
	[CodigoEarn]    VARCHAR (150)  NULL, -- adicionar esse campo no banco de dados/ Mandar ele novamente para o servidor. 
	[descricao]		VARCHAR (MAX)  NULL,
	[Peso_alvo]		REAL  NULL,
	[Tolerancia]	REAL  NULL,
	[part_number_cliente]	VARCHAR(50)  NULL,
	[Foto]					VARCHAR(MAX),
	[dateinsert] DATETIME2 (2) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO
CREATE TABLE [dbo].[Bandeja] (
    [Id]					NUMERIC (18)  IDENTITY (1, 1) NOT NULL,
    [Codigo]				VARCHAR (150) UNIQUE  NULL,
	[descricao]				VARCHAR (MAX)  NULL,
	[Peso_alvo]				REAL  NULL,
	[Quantidade_Produtos]	DECIMAL  NULL,
	[Tolerancia]			REAL  NULL, --adicionar classe
	[Foto]					VARCHAR(MAX),
	[dateinsert] DATETIME2 (2) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO
CREATE TABLE [dbo].[Recipiente] (
    [Id]					NUMERIC (18)  IDENTITY (1, 1) NOT NULL,
    [Package]				VARCHAR (150) UNIQUE NULL,
	[descricao]				VARCHAR (MAX)  NULL,
	[Peso_alvo]				REAL  NULL,
	[Tolerancia]			REAL  NULL, --adicionar classe
	[Foto]					VARCHAR(MAX),
	[dateinsert] DATETIME2 (2) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO
CREATE TABLE [dbo].[Receita] (
    [Id]					NUMERIC (18)  IDENTITY (1, 1) NOT NULL,
	[Nome]					VARCHAR(250)  NOT NULL,
	[Codigo]				VARCHAR(250)	NOT NULL,
	[PkSKF]					VARCHAR(50)		NOT NULL,
	[id_produto]			INT		NOT NULL,
	[id_bandeja]			INT		NOT NULL,
	[id_recipiente]			INT		NOT NULL,
	[Quantidade_pecas]		INT		NOT NULL,
	[Quantidade_bandejas]	INT		NOT NULL,
	[Operador]				VARCHAR(200)	NOT NULL,
	[Status]				INT		NOT NULL,
	[dateinsert] DATETIME2 (2) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO
CREATE TABLE [dbo].[LogReceita] (
    [Id]						NUMERIC (18)  IDENTITY (1, 1) NOT NULL,
	[Nome]						VARCHAR(250)  NOT NULL,
	[Codigo]					VARCHAR(250)	NOT NULL,
	[id_receita]				INT		NOT NULL,
	[id_Recipiente]				INT	    NOT NULL,
	[Peso_Recipiente]			REAL NOT NULL,
	[Peso_Recipiente_Pesado]	REAL NULL,
	[id_Bandeja]				INT		NOT NULL,
	[Qtd_Bandeja]				INT		NOT NULL,
	[Qtd_Bandeja_Pesado]		INT		NULL,
	[Peso_Bandejas]				REAL NOT NULL,
	[Peso_Bandejas_Pesado]		REAL NULL,
	[id_Produto]				INT		NOT NULL,
	[Qtd_Pecas]					INT		NOT NULL,
	[Qtd_Pecas_Pesado]			INT     NULL,
	[Peso_Pecas]				REAL	NOT NULL,
	[Peso_Pecas_Pesado]			REAL	NULL,
	[Status]					INT		NOT NULL,
	[Estacao]					VARCHAR(100)	NOT NULL,
	[Operador]					VARCHAR(200)	NOT NULL,
	[Observacoes]				VARCHAR(MAX)	NULL,
	[dateinsert] DATETIME2 (2) NULL,
	[datefim] DATETIME2 (2) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO
CREATE TABLE [dbo].[Log_bandeja_receita]
(
	[Id]							NUMERIC (18)  IDENTITY (1, 1) NOT NULL,
	[id_log_receita]				INT		NOT NULL,
	[Numero_Bandeja]				INT		NOT NULL,
	[Peso_bandeja]					REAL NOT NULL,
	[Peso_Produto]					REAL NOT NULL,
);
GO
CREATE TABLE [dbo].[tipoReceita] (
    [Id]					NUMERIC (18)  IDENTITY (1, 1) NOT NULL,
	[Tipo_item]				VARCHAR(300)  NOT NULL,
	[dateinsert] DATETIME2 (2) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);



insert into Usuario(Nome, login, senha, acesso) values ('Administrador', 'admin', 'admin', 'Administrador')
insert into Configuracao(porta_arduino, baud_Rate, stop_bit) values ('COM7', '115200', '2')


--Insert padrão da SKF

INSERT INTO Produto(part_number, descricao, Peso_alvo, Tolerancia, part_number_cliente, dateinsert, CodigoEarn) VALUES
('BC1-3551','BC1-3551',0.27,0.01,'24042035',GETDATE(), 'EAN01');
INSERT INTO Produto(part_number,descricao, Peso_alvo, Tolerancia, part_number_cliente, dateinsert, CodigoEarn) VALUES
('BC1-3549','BC1-3549',0.31,0.01,'24042033',GETDATE(), 'EARN02');
INSERT INTO Produto(part_number, descricao, Peso_alvo, Tolerancia, part_number_cliente, dateinsert, CodigoEarn) VALUES
('BC1-1794','BC1-1794',0.084,0.01,'24582741',GETDATE(), 'EARN03');
INSERT INTO Bandeja(Codigo, descricao, Peso_alvo, Quantidade_Produtos, Tolerancia, dateinsert) VALUES
('76180579','BC1-3551 Band. 1', 0.05, 24, 0.05, GETDATE())
INSERT INTO Bandeja(Codigo, descricao, Peso_alvo, Quantidade_Produtos, Tolerancia, dateinsert) VALUES
('76180578','BC1-3549 Band. 1', 0.05, 32, 0.05, GETDATE())
INSERT INTO Bandeja(Codigo, descricao, Peso_alvo, Quantidade_Produtos, Tolerancia, dateinsert) VALUES
('n/a','BC1-1794 Band. 1', 0.05, 23, 0.05, GETDATE())
INSERT INTO Recipiente (Package, descricao, Peso_alvo,Tolerancia, dateinsert)VALUES
('Caixa F1','Caixa F1',0.5,0.1,GETDATE())
INSERT INTO Recipiente (Package, descricao, Peso_alvo,Tolerancia, dateinsert)VALUES
('CCaixa Casette','Caixa Casette',1.63,0.1,GETDATE())
INSERT INTO tipoReceita (Tipo_item, dateinsert) VALUES
('PK 88',GETDATE())
INSERT INTO tipoReceita (Tipo_item, dateinsert) VALUES
('PK 75',GETDATE())
INSERT INTO Receita (Nome, Codigo, PkSKF, id_produto, id_bandeja, id_recipiente, Quantidade_pecas, Quantidade_bandejas, Operador, Status, dateinsert) VALUES
('Receita 01', '00001', 'PK 75', 1, 1, 1,24,4,'Administrador', 0,GETDATE())
INSERT INTO Receita (Nome, Codigo, PkSKF, id_produto, id_bandeja, id_recipiente, Quantidade_pecas, Quantidade_bandejas, Operador, Status, dateinsert) VALUES
('Receita 02', '00002', 'PK 75', 2, 2, 1,32,4,'Administrador', 0,GETDATE())
INSERT INTO Receita (Nome, Codigo, PkSKF, id_produto, id_bandeja, id_recipiente, Quantidade_pecas, Quantidade_bandejas, Operador, Status, dateinsert) VALUES
('Receita 03', '00003', 'PK 88', 3, 3, 2,138,6,'Administrador', 0,GETDATE())

