CREATE PROCEDURE [dbo].[sp_ProfileUser_Del]
	@IProfileId			INT,
	@IUserId			INT
AS
	DELETE ProfileUser WHERE ProfileId = @IProfileId AND UserId = @IUserId
RETURN 0
