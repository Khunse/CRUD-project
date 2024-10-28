CREATE DATABASE sampledb;
GO

USE sampledb;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ProductName] [varchar](255) NOT NULL,
    [ProductPrice]              DECIMAL (18)  CONSTRAINT [DEFAULT_Product_Price] DEFAULT 0 NOT NULL,
	[Created_at] [datetime] NOT NULL,
	[Updated_at] [datetime] NULL,
	[ProductDescription] [varchar](255) NOT NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DEFAULT_Product_IsDelete]  DEFAULT ((0)) FOR [IsDelete]
GO