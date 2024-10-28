# Project

## Overview
This project is a web application developed with ASP.NET on .NET 8, utilizing MSSQL server as the databse and Dapper to acceses Database. It is designed for simple CRUD operation for products.

## Table of Contents
- [Requirements](#requirements)
- [Installation](#installation)
- [Configuration](#configuration)
- [Running the Project](#running-the-project)

## Requirements
- **.NET 8 SDK**
- **MSSQL Server** (local or hosted instance)

## Installation
1. **Clone the Repository**

   ```bash
   git clone https://github.com/Khunse/CRUD-project.git
   cd CRUD-project
   ```

2. **Restore Dependencies**

	```bash
	dotnet restore
	```

3. **Build Project**

	```bash
	dotnet build
	```


## Configuration

1. **Run this Query in MSSQL Server**

	```sql
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
	```

2. **Set Database connection in project's Appsetting.json**

	```json
	ConnectionStrings": {
		"DefaultConnection": "Server=your_server_name;Database=your_database_name;User Id=your_user_id;Password=your_password;"
		}
	```

## Running The Project

1. **Start the application**

	```bash
	dotnet run
	```