
CREATE PROCEDURE [dbo].[sp_User_List]
	@IUser	VARCHAR(50),
	@IName	VARCHAR(400)
AS
	SELECT 
			UserId,
			Type,
			u.[User],
			Password,
			Name,
			Status
	FROM	[User] u
	WHERE	(u.[User] like '%'+@IUser+'%' or @IUser = '')
	AND		(Name like '%'+@IName+'%' or @IName = '')
RETURN 0
