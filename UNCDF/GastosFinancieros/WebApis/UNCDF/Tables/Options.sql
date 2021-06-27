CREATE TABLE [dbo].[Options]
(
	[OptionId] INT NOT NULL PRIMARY KEY, 
    [IdFather] INT NULL, 
    [Title] VARCHAR(50) NULL, 
    [Link] VARCHAR(200) NULL, 
    [Status] VARCHAR(10) NULL, 
    [Action] INT NULL, 
    [Orders] INT NULL, 
    [Description] VARCHAR(250) NULL
)
