USE [GestionRestaurant]
GO
/****** Object:  Table [dbo].[client]    Script Date: 10/06/2022 21:28:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[client](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nom] [varchar](50) NULL,
	[Prenom] [varchar](50) NULL,
	[Telephone] [varchar](10) NULL,
	[PointsFidelites] [int] NULL,
	[Login] [varchar](20) NULL,
	[Password] [varchar](20) NULL,
	[NbrCommande] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[commande]    Script Date: 10/06/2022 21:28:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[commande](
	[Numero] [int] IDENTITY(1,1) NOT NULL,
	[DateCommande] [datetime] NULL,
	[IdEmploye] [int] NULL,
	[IdClient] [int] NULL,
	[TypePayement] [varchar](50) NULL,
	[PrixAPayer] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[Numero] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DetailCommande]    Script Date: 10/06/2022 21:28:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetailCommande](
	[LibelleProduit] [varchar](30) NOT NULL,
	[NumeroCommande] [int] NOT NULL,
	[QuantiteProduit] [int] NULL,
	[PrixTotal] [int] NULL,
 CONSTRAINT [Pk_LibP_NumC] PRIMARY KEY CLUSTERED 
(
	[NumeroCommande] ASC,
	[LibelleProduit] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[employe]    Script Date: 10/06/2022 21:28:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[employe](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nom] [varchar](50) NULL,
	[Prenom] [varchar](50) NULL,
	[DateEmbauche] [datetime] NULL,
	[DateDemission] [datetime] NULL,
	[Salaire] [money] NULL,
	[Nb_Heure] [int] NULL,
	[Fonction] [varchar](50) NULL,
	[Login] [varchar](20) NULL,
	[Password] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[produit]    Script Date: 10/06/2022 21:28:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[produit](
	[libelle] [varchar](30) NOT NULL,
	[Prix] [float] NULL,
	[type] [varchar](30) NULL,
PRIMARY KEY CLUSTERED 
(
	[libelle] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[commande]  WITH CHECK ADD FOREIGN KEY([IdClient])
REFERENCES [dbo].[client] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[commande]  WITH CHECK ADD FOREIGN KEY([IdEmploye])
REFERENCES [dbo].[employe] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DetailCommande]  WITH CHECK ADD  CONSTRAINT [fk_LibP] FOREIGN KEY([LibelleProduit])
REFERENCES [dbo].[produit] ([libelle])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DetailCommande] CHECK CONSTRAINT [fk_LibP]
GO
ALTER TABLE [dbo].[DetailCommande]  WITH CHECK ADD  CONSTRAINT [fk_NumC] FOREIGN KEY([NumeroCommande])
REFERENCES [dbo].[commande] ([Numero])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DetailCommande] CHECK CONSTRAINT [fk_NumC]
GO
ALTER TABLE [dbo].[client]  WITH CHECK ADD  CONSTRAINT [Ck_tele] CHECK  (([Telephone] like '06%' OR [Telephone] like '07%'))
GO
ALTER TABLE [dbo].[client] CHECK CONSTRAINT [Ck_tele]
GO
/****** Object:  StoredProcedure [dbo].[changement]    Script Date: 10/06/2022 21:28:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[changement] @TypePayement varchar(50),@PrixAPayer varchar(20),@Numero int
as
	begin
		if @PrixAPayer like '%,%'
			begin 
				update commande set TypePayement=@TypePayement, PrixAPayer = cast(replace(@PrixAPayer,',','.') as float)  where Numero=@Numero
			end 
		else
			begin
				update Commande set TypePayement=@TypePayement,PrixAPayer= cast(@PrixAPayer as float) where Numero=@Numero
			end
	end
GO
