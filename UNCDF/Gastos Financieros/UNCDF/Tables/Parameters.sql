CREATE TABLE [dbo].[Parameters]
(
	[ParameterId] INT NOT NULL PRIMARY KEY IDENTITY, 
	 [Code] VARCHAR(30) NULL,
    [Description] VARCHAR(400) NULL, 
    [Valor1] VARCHAR(400) NULL, 
    [Valor2] VARCHAR(400) NULL, 
    [Status] INT NULL,
   
)