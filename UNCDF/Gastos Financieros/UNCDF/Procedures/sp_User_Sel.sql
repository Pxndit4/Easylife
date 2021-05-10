CREATE PROCEDURE [dbo].[sp_User_Sel]
	@IUserId int
AS
	SELECT 
			u.UserId,
			u.[User],
			u.Name,
			u.Type,
			u.Status
	FROM	[User] u
	WHERE	u.UserId = @IUserId
