CREATE PROCEDURE [dbo].[sp_Profile_Upd]
	@IDescription		VARCHAR(400),
	@IProfileId			INT,
	@IStatus			INT
AS
	UPDATE	Profiles
	SET		Description = @IDescription,
			Status = @IStatus
	WHERE	ProfileId = @IProfileId
