CREATE TABLE [dbo].[ProfileOptions]
(
	[ProfileId] INT NOT NULL , 
    [OptionId] INT NOT NULL, 
    PRIMARY KEY ([OptionId], [ProfileId])
)
