CREATE TABLE [dbo].[Donor]
(
	[DonorId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [FirstName] VARCHAR(200) NULL, 
    [LastName] VARCHAR(200) NULL, 
    [Email] VARCHAR(400) NULL, 
    [Password] VARCHAR(4000) NOT NULL, 
    [Cellphone] VARCHAR(20) NULL, 
    [Address] VARCHAR(250) NULL, 
    [CountryId] INT NULL, 
    [Gender] CHAR NULL, 
    [Birthday] NUMERIC(8) NULL, 
    [Photo] VARCHAR(400) NULL, 
    [Status] INT NOT NULL, 
    [Registered] INT NULL, 
    [Token] VARCHAR(4000) NULL
)

GO
