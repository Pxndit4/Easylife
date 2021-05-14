CREATE TABLE [dbo].[Country]
(
	[CountryId] INT NOT NULL PRIMARY KEY IDENTITY, 
	[ContinentId] INT NULL,
    [Description] VARCHAR(200) NOT NULL, 
	[Prefix] VARCHAR(10) NULL, 
    [Flag] VARCHAR(200) NOT NULL, 
    [Status] INT NOT NULL,    
    CONSTRAINT [FK_Country_ToContinent] FOREIGN KEY ([ContinentId]) REFERENCES [Continent]([ContinentId])    
)
