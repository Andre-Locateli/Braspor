CREATE DATABASE VC_BRASPOR

Go
USE VC_BRASPOR
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

CREATE TABLE [dbo].[MateriaPrima] (
    [Id]					NUMERIC (18)   IDENTITY (1, 1) NOT NULL,
    [Codigo]				VARCHAR (20)   NULL,
	[Descricao]				VARCHAR (200)  NULL,
	[Tolerancia_erro]		VARCHAR (20)   NULL,
	[quantidade_minima]		VARCHAR (20)   NULL,
	[bit_status]			BIT			   NULL,
	[dateinsert] DATETIME2 (2) NULL,
	[dateupdate] DATETIME2 (2) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

-



insert into Usuario(Nome, login, senha, acesso) values ('Administrador', 'admin', 'admin', 'Administrador')
insert into Configuracao(porta_arduino, baud_Rate, stop_bit) values ('COM7', '115200', '2')


