CREATE PROCEDURE [dbo].[sp_Profile_Del]
	@IProfileId int 
AS
	UPDATE	Profiles
	SET		Status = 0
	WHERE	ProfileId = @IProfileId
