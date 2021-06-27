CREATE TABLE [dbo].[User]
(
	[UserId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Type] INT NULL, 
    [User] VARCHAR(100) NULL, 
    [Password] VARCHAR(4000) NULL, 
    [Name] VARCHAR(200) NULL, 
    [Status] INT NULL, 
    [Token] VARCHAR(4000) NULL
)