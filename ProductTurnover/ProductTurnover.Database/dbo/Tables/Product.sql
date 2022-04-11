CREATE TABLE [dbo].[Product] (
    [Id]         INT             IDENTITY (1, 1) NOT NULL,
    [Name]       NVARCHAR (64)   NOT NULL,
    [EAN]        NVARCHAR (13)   NOT NULL,
    [Price]      DECIMAL (18, 2) NOT NULL,
    [CategoryId] INT             NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Product_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Category] ([Id])
);

