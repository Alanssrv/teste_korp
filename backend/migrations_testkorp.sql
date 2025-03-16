--CREATE TABLE [Products] (
--	Id BIGINT IDENTITY(1, 1) PRIMARY KEY,
--	[Name] VARCHAR(60) NOT NULL,
--	Price DECIMAL(15, 2) NOT NULL,
--	Code VARCHAR(20) NOT NULL UNIQUE,
--	InventoryBalance INT NOT NUll,
--	CreationDate DATETIME NOT NULL
--)

--CREATE TABLE Invoice (
--	Id BIGINT IDENTITY(1, 1) PRIMARY KEY,
--	[State] TINYINT NOT NULL,
--	CreationDate DATETIME NOT NULL
--)

--CREATE TABLE InvoiceProducts (
--	Id BIGINT IDENTITY(1, 1) PRIMARY KEY,
--	CreationDate DATETIME NOT NULL,
--	InvoiceId BIGINT NOT NULL,
--	ProductId BIGINT NOT NULL,
--	FOREIGN KEY (InvoiceId) REFERENCES Invoice(Id),
--	FOREIGN KEY (ProductId) REFERENCES [Products](Id),
--)

use TestKorp
go

select * from Products (nolock)
select * from Invoice (nolock)
select * from InvoiceProducts (nolock)