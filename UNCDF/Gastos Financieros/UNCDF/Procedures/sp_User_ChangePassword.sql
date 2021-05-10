CREATE PROCEDURE [dbo].[sp_User_ChangePassword]
	@IUserId	int,
	@IPassword	VARCHAR(400)
AS
	UPDATE	[User]
	SET		Password = @IPassword
	WHERE	UserId = @IUserId