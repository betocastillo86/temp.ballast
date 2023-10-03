CREATE DATABASE Ballast;

CREATE TABLE [dbo].[Products] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [Name]          NVARCHAR (MAX)  NULL,
    [Description]   NVARCHAR (MAX)  NULL,
    [Price]         DECIMAL (18)    NOT NULL,
    [StockQuantity] INT             NOT NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Users] (
    [Id] UNIQUEIDENTIFIER NOT NULL,
    [Name] NVARCHAR (50)   NOT NULL,
    [Email] NVARCHAR (50)   NOT NULL,
    [Password] NVARCHAR (50)   NOT NULL,
    [Salt] NVARCHAR (50)   NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC)
);