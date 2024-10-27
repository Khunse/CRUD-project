
## dotnet .\publish\IPAserver.dll --urls=http://localhost:5268




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ProductName] [varchar](255) NOT NULL,
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

select name from sys.databases;
select TABLE_NAME from INFORMATION_SCHEMA.TABLES where TABLE_TYPE='BASE TYPE';

docker run -d -p 5000:5000 --name my-app --network crudapp-network myappv2

docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Khunaung123!" -e "MSSQL_PID=Express" -p 1437:1433 --name my-mssql -v /d/assigments/IPAserver/sql-scripts:/sql-scripts -d mcr.microsoft.com/mssql/server:2017-latest-ubuntu

docker exec -it 39d8 /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Khunaung123!

docker exec -it 39d8 /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Khunaung123! -i /sql-scripts/init.sql
