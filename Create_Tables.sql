
CREATE TABLE [dbo].[Product](
	[Id] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[Price] [numeric](12, 2) NOT NULL,
	[ProductName] [varchar](60) NOT NULL,
	[Description] [varchar](200) NOT NULL
)