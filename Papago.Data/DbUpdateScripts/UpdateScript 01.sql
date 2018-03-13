/*
	USE [master]
	GO
	DROP DATABASE Papago
	GO
	CREATE DATABASE Papago
	GO
	USE [papago]
	GO
*/
/*
	Restaurant
*/

CREATE TABLE dbo.Restaurant (
	Id INT IDENTITY(1,1) NOT NULL
	,CreationDate DATETIME NOT NULL
	,LastUpdate DATETIME NOT NULL
	,UpdatedBy NVARCHAR(50)
	,[Name] NVARCHAR(50) NOT NULL
	,[Location] NVARCHAR(50) NOT NULL
	,[Address] NVARCHAR(MAX)
	,[Description] NVARCHAR(MAX)
	,LogoUrl NVARCHAR(MAX)
	,HasOwnDelivery BIT NOT NULL CONSTRAINT DF_Restaurant_HasOwnDelivery DEFAULT (0)
	,WorkingHours VARCHAR(25)
	,MinimumOrderValue MONEY
	,AverageDeliveryDuration SMALLINT
	,Rating DECIMAL
)
GO

ALTER TABLE dbo.Restaurant
	ADD CONSTRAINT PK_Restaurant PRIMARY KEY CLUSTERED (Id)
GO

CREATE UNIQUE NONCLUSTERED INDEX UX_Restaurant_Name_Location
	ON dbo.Restaurant ([Name], [Location])
GO

/*
	Category
*/

CREATE TABLE dbo.Category (
	Id INT IDENTITY(1,1) NOT NULL
	,RestaurantId INT NOT NULL
	,CreationDate DATETIME NOT NULL
	,LastUpdate DATETIME NOT NULL
	,UpdatedBy NVARCHAR(50)
	,[Name] NVARCHAR(50) NOT NULL
	,LogoUrl NVARCHAR(MAX)
)
GO

ALTER TABLE dbo.Category
	ADD CONSTRAINT PK_Category PRIMARY KEY CLUSTERED (Id)
GO

CREATE UNIQUE NONCLUSTERED INDEX UX_Category_RestaurantId_Name
	ON dbo.Category (RestaurantId, [Name])
GO

ALTER TABLE dbo.Category
	ADD CONSTRAINT FK_Category_RestaurantId_Restaurant_Id
	FOREIGN KEY (RestaurantId)
	REFERENCES dbo.Restaurant (Id)
GO

/*
	Item
*/

CREATE TABLE dbo.Item (
	Id INT IDENTITY(1,1) NOT NULL
	,RestaurantId INT NOT NULL
	,CategoryId INT
	,CreationDate DATETIME NOT NULL
	,LastUpdate DATETIME NOT NULL
	,UpdatedBy NVARCHAR(50)
	,[Name] NVARCHAR(50) NOT NULL
	,LogoUrl NVARCHAR(MAX)
	,[Description] NVARCHAR(MAX)
	,Price MONEY
)
GO

ALTER TABLE dbo.Item
	ADD CONSTRAINT PK_Item PRIMARY KEY CLUSTERED (Id)
GO

CREATE UNIQUE NONCLUSTERED INDEX UX_Item_RestaurantId_Name
	ON dbo.Item (RestaurantId, [Name])
GO

ALTER TABLE dbo.Item WITH CHECK
	ADD CONSTRAINT FK_Item_RestaurantId_Restaurant_Id
	FOREIGN KEY (RestaurantId)
	REFERENCES dbo.Restaurant (Id)
GO

ALTER TABLE dbo.Item WITH CHECK
	CHECK CONSTRAINT FK_Item_RestaurantId_Restaurant_Id
GO

ALTER TABLE dbo.Item WITH CHECK
	ADD CONSTRAINT FK_Item_CategoryId_Category_Id
	FOREIGN KEY (CategoryId)
	REFERENCES dbo.Category (Id)
GO

ALTER TABLE dbo.Item WITH CHECK
	CHECK CONSTRAINT FK_Item_CategoryId_Category_Id
GO

/*
	Menu
*/

CREATE TABLE dbo.Menu (
	Id INT IDENTITY(1,1) NOT NULL
	,RestaurantId INT NOT NULL
	,CreationDate DATETIME NOT NULL
	,LastUpdate DATETIME NOT NULL
	,UpdatedBy NVARCHAR(50)
	,[Name] NVARCHAR(50) NOT NULL
	,LogoUrl NVARCHAR(MAX)
	,[Description] NVARCHAR(MAX)
	,Price MONEY
)
GO

ALTER TABLE dbo.Menu
	ADD CONSTRAINT PK_Menu PRIMARY KEY CLUSTERED (Id)
GO

CREATE UNIQUE NONCLUSTERED INDEX UX_Menu_RestaurantId_Name
	ON dbo.Menu (RestaurantId, [Name])
GO

ALTER TABLE dbo.Menu WITH CHECK
	ADD CONSTRAINT FK_Menu_RestaurantId_Restaurant_Id
	FOREIGN KEY (RestaurantId)
	REFERENCES dbo.Restaurant (Id)
GO

ALTER TABLE dbo.Menu WITH CHECK
	CHECK CONSTRAINT FK_Menu_RestaurantId_Restaurant_Id
GO

/*
	MenuItem
*/

CREATE TABLE dbo.MenuItem (
	Id INT IDENTITY(1,1) NOT NULL
	,MenuId INT NOT NULL
	,ItemId INT NOT NULL
	,CreationDate DATETIME NOT NULL
	,LastUpdate DATETIME NOT NULL
	,UpdatedBy NVARCHAR(50)
)
GO

ALTER TABLE dbo.MenuItem
	ADD CONSTRAINT PK_MenuItem PRIMARY KEY CLUSTERED (Id)
GO

CREATE UNIQUE NONCLUSTERED INDEX UX_MenuItem_MenuId_ItemId
	ON dbo.MenuItem (MenuId, ItemId)
GO

ALTER TABLE dbo.MenuItem WITH CHECK
	ADD CONSTRAINT FK_MenuItem_MenuId_Menu_Id
	FOREIGN KEY (MenuId)
	REFERENCES dbo.Menu (Id)
GO

ALTER TABLE dbo.MenuItem WITH CHECK
	CHECK CONSTRAINT FK_MenuItem_MenuId_Menu_Id
GO

ALTER TABLE dbo.MenuItem WITH CHECK
	ADD CONSTRAINT FK_MenuItem_ItemId_Item_Id
	FOREIGN KEY (ItemId)
	REFERENCES dbo.Item (Id)
GO

ALTER TABLE dbo.MenuItem WITH CHECK
	CHECK CONSTRAINT FK_MenuItem_ItemId_Item_Id
GO

/*
	Client
*/

CREATE TABLE dbo.Client (
	Id INT IDENTITY(1,1) NOT NULL
	,CreationDate DATETIME NOT NULL
	,LastUpdate DATETIME NOT NULL
	,UpdatedBy NVARCHAR(50)
	,[Name] NVARCHAR(50) NOT NULL
	,Email VARCHAR(50) NOT NULL
	,[Address] NVARCHAR(MAX)
	,Latitude VARCHAR(25)
	,Longitude VARCHAR(25)
)
GO

ALTER TABLE dbo.Client
	ADD CONSTRAINT PK_Client PRIMARY KEY CLUSTERED (Id)
GO

CREATE UNIQUE NONCLUSTERED INDEX UX_Client_Email
	ON dbo.Client (Email)
GO

/*
	Client
*/

CREATE TABLE dbo.[Order] (
	Id INT IDENTITY(1,1) NOT NULL
	,RestaurantId INT NOT NULL
	,ClientId INT NOT NULL
	,CreationDate DATETIME NOT NULL
	,LastUpdate DATETIME NOT NULL
	,UpdatedBy NVARCHAR(50)
	,[Text] NVARCHAR(50) NOT NULL
	,Price MONEY NOT NULL
	,DeliveryTime INT
)
GO

ALTER TABLE dbo.[Order]
	ADD CONSTRAINT PK_Order PRIMARY KEY CLUSTERED (Id)
GO

ALTER TABLE dbo.[Order] WITH CHECK
	ADD CONSTRAINT FK_Order_RestaurantId_Restaurant_Id
	FOREIGN KEY (RestaurantId)
	REFERENCES dbo.Restaurant (Id)
GO

ALTER TABLE dbo.[Order] WITH CHECK
	CHECK CONSTRAINT FK_Order_RestaurantId_Restaurant_Id
GO

ALTER TABLE dbo.[Order] WITH CHECK
	ADD CONSTRAINT FK_Order_ClientId_Client_Id
	FOREIGN KEY (ClientId)
	REFERENCES dbo.Client (Id)
GO

ALTER TABLE dbo.[Order] WITH CHECK
	CHECK CONSTRAINT FK_Order_ClientId_Client_Id
GO

/*
DROP TABLE dbo.MenuItem
DROP TABLE dbo.Menu
DROP TABLE dbo.Item
DROP TABLE dbo.Category
DROP TABLE dbo.[Order]
DROP TABLE dbo.Client
DROP TABLE dbo.Restaurant
*/