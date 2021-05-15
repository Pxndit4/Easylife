CREATE PROCEDURE [dbo].[sp_User_Login]
	@IUser		VARCHAR(400),
	@IPassword	VARCHAR(4000)
AS
	SELECT 
			UserId,
			User,
			Name,
			Type,
			Status,
			Token
	FROM	[User]
	WHERE	[User] = @IUser
	AND		Password = @IPassword

