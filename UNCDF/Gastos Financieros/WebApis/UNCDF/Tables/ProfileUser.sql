CREATE TABLE [dbo].[ProfileUser]
(
	[ProfileId] INT NOT NULL , 
    [UserId] NCHAR(10) NOT NULL, 
    PRIMARY KEY ([ProfileId], [UserId])
)
