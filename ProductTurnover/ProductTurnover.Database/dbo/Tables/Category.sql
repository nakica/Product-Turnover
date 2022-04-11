CREATE TABLE [dbo].[Category] (
    [Id]   INT            IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (64)  NOT NULL,
    [VAT]  DECIMAL (3, 2) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
