USE [MyRecipe]
GO
/****** Object:  Table [dbo].[Ingrediente]    Script Date: 26/11/2018 20:31:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Ingrediente](
	[idReceita] [int] NOT NULL,
	[nomeIngrediente] [varchar](250) COLLATE Latin1_General_CI_AS NOT NULL,
	[qtda] [varchar](50) COLLATE Latin1_General_CI_AS NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Receita]    Script Date: 26/11/2018 20:31:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Receita](
	[idReceita] [int] IDENTITY(1,1) NOT NULL,
	[tituloReceita] [varchar](100) COLLATE Latin1_General_CI_AS NOT NULL,
	[modoPreparo] [varchar](500) COLLATE Latin1_General_CI_AS NOT NULL,
	[imagem] [varbinary](max) NULL,
	[nomeImagem] [varchar](50) COLLATE Latin1_General_CI_AS NULL,
	[calorias] [int] NULL,
	[carboidratos] [decimal](18, 0) NULL,
	[gordurasTotais] [decimal](18, 0) NULL,
	[gordurasSaturadas] [decimal](18, 0) NULL,
	[fibra] [decimal](18, 0) NULL,
	[sodio] [decimal](18, 0) NULL,
	[acucar] [decimal](18, 0) NULL,
	[proteina] [decimal](18, 0) NULL,
 CONSTRAINT [PK_Receita] PRIMARY KEY CLUSTERED 
(
	[idReceita] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[Ingrediente] ([idReceita], [nomeIngrediente], [qtda]) VALUES (2, N'milho', N'1 x')
INSERT [dbo].[Ingrediente] ([idReceita], [nomeIngrediente], [qtda]) VALUES (3, N'milho', N'1 x')
INSERT [dbo].[Ingrediente] ([idReceita], [nomeIngrediente], [qtda]) VALUES (4, N'milho', N'1 x')
SET IDENTITY_INSERT [dbo].[Receita] ON 

INSERT [dbo].[Receita] ([idReceita], [tituloReceita], [modoPreparo], [imagem], [nomeImagem], [calorias], [carboidratos], [gordurasTotais], [gordurasSaturadas], [fibra], [sodio], [acucar], [proteina]) VALUES (2, N'Milho', N'asse', 0x7E2F496D616765732F69636F6E2E706E67, NULL, 87, CAST(6 AS Decimal(18, 0)), CAST(2 AS Decimal(18, 0)), CAST(2 AS Decimal(18, 0)), CAST(8 AS Decimal(18, 0)), CAST(1 AS Decimal(18, 0)), CAST(6 AS Decimal(18, 0)), CAST(7 AS Decimal(18, 0)))
INSERT [dbo].[Receita] ([idReceita], [tituloReceita], [modoPreparo], [imagem], [nomeImagem], [calorias], [carboidratos], [gordurasTotais], [gordurasSaturadas], [fibra], [sodio], [acucar], [proteina]) VALUES (3, N'Teste', N'asse', 0x61737365, NULL, 87, CAST(6 AS Decimal(18, 0)), CAST(2 AS Decimal(18, 0)), CAST(2 AS Decimal(18, 0)), CAST(8 AS Decimal(18, 0)), CAST(1 AS Decimal(18, 0)), CAST(6 AS Decimal(18, 0)), CAST(7 AS Decimal(18, 0)))
INSERT [dbo].[Receita] ([idReceita], [tituloReceita], [modoPreparo], [imagem], [nomeImagem], [calorias], [carboidratos], [gordurasTotais], [gordurasSaturadas], [fibra], [sodio], [acucar], [proteina]) VALUES (4, N'Teste', N'asse', 0x61737365, NULL, 87, CAST(6 AS Decimal(18, 0)), CAST(2 AS Decimal(18, 0)), CAST(2 AS Decimal(18, 0)), CAST(8 AS Decimal(18, 0)), CAST(1 AS Decimal(18, 0)), CAST(6 AS Decimal(18, 0)), CAST(7 AS Decimal(18, 0)))
SET IDENTITY_INSERT [dbo].[Receita] OFF
ALTER TABLE [dbo].[Ingrediente]  WITH CHECK ADD  CONSTRAINT [FK_Ingrediente_Receita] FOREIGN KEY([idReceita])
REFERENCES [dbo].[Receita] ([idReceita])
GO
ALTER TABLE [dbo].[Ingrediente] CHECK CONSTRAINT [FK_Ingrediente_Receita]
GO
