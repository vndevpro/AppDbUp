CREATE TABLE [dbo].[Customer](
	[Id] [bigint] IDENTITY(1,1) NOT NULL  PRIMARY KEY,
	[Email] [varchar](255) NOT NULL,
	[FullName] [nvarchar](512) NOT NULL,
);
GO