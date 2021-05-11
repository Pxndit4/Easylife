CREATE PROCEDURE [dbo].[sp_Profile_Sel]
	@IProfileId			INT
AS
	SELECT 
			Description,
			Status
	FROM	Profiles
	WHERE	ProfileId = @IProfileId
