CREATE PROCEDURE [dbo].[sp_ProfileUser_Sel]
	@IProfileId			INT
AS
	SELECT		PU.ProfileId,
				U.UserId,
				U.[User],
				u.Name 
	FROM		ProfileUser PU
	INNER JOIN	[User] U ON PU.UserId = U.UserId
	WHERE		PU.ProfileId = @IProfileId
