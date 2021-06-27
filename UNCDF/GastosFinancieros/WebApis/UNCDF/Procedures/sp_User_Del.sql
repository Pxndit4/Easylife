CREATE PROCEDURE [dbo].[sp_User_Del]
	@IUserId int
AS
	UPDATE	[User]
	SET		Status = 0
	WHERE	UserId = @IUserId

