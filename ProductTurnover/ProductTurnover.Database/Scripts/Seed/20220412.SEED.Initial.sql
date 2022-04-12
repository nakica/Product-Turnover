BEGIN
	PRINT 'Creating categories...'

	DECLARE @booksCategoryId INT = 1;
	DECLARE @beverageCategoryId INT = 2;
	DECLARE @medicineCategoryId INT = 3;

	BEGIN TRAN
		SET IDENTITY_INSERT [dbo].[Category] ON
			INSERT INTO [dbo].[Category] ([Id], [Name], [VAT]) VALUES (@booksCategoryId, N'Books', CAST(0.07 AS Decimal(5, 2)))
			INSERT INTO [dbo].[Category] ([Id], [Name], [VAT]) VALUES (@beverageCategoryId, N'Beverage', CAST(0.18 AS Decimal(5, 2)))
			INSERT INTO [dbo].[Category] ([Id], [Name], [VAT]) VALUES (@medicineCategoryId, N'Medicine', CAST(0.20 AS Decimal(5, 2)))
		SET IDENTITY_INSERT [dbo].[Category] OFF
    COMMIT TRAN

	PRINT 'Creating products...'

	BEGIN TRAN
		SET IDENTITY_INSERT [dbo].[Product] ON
			INSERT INTO [dbo].[Product] ([Id], [Name], [EAN], [Price], [CategoryId]) VALUES (1, N'Art of War', N'01234567', CAST(5.00 AS Decimal(18, 2)), @booksCategoryId)
			INSERT INTO [dbo].[Product] ([Id], [Name], [EAN], [Price], [CategoryId]) VALUES (2, N'Winnie the Poon', N'012345678', CAST(15.00 AS Decimal(18, 2)), @booksCategoryId)
			INSERT INTO [dbo].[Product] ([Id], [Name], [EAN], [Price], [CategoryId]) VALUES (3, N'Sprite', N'0123456789', CAST(2.00 AS Decimal(18, 2)), @beverageCategoryId)
			INSERT INTO [dbo].[Product] ([Id], [Name], [EAN], [Price], [CategoryId]) VALUES (4, N'Water', N'12345678', CAST(1.00 AS Decimal(18, 2)), @beverageCategoryId)
			INSERT INTO [dbo].[Product] ([Id], [Name], [EAN], [Price], [CategoryId]) VALUES (5, N'Vitamin C', '123456789', CAST(3.00 AS Decimal(18, 2)), @medicineCategoryId)
			INSERT INTO [dbo].[Product] ([Id], [Name], [EAN], [Price], [CategoryId]) VALUES (6, N'Aspirin', N'1234567890', CAST(4.00 AS Decimal(18, 2)), @medicineCategoryId)
		SET IDENTITY_INSERT [dbo].[Product] OFF
    COMMIT TRAN
END