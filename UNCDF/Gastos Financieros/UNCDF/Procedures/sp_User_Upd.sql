CREATE PROCEDURE [dbo].[sp_User_Upd]
	@IUserId	int,
	@IUser		VARCHAR(100),
	@IName		VARCHAR(200),
	@iStatus	VARCHAR(1)
AS
	UPDATE	[User]
	SET		[User]	= @IUser,
			Name	= @IName,
			Status	= @iStatus
	WHERE	UserId	= @IUserId	

