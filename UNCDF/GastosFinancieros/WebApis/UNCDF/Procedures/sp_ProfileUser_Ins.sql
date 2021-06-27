CREATE PROCEDURE [dbo].[sp_ProfileUser_Ins]
	@IProfileId			INT,
	@IUserId			INT
AS
	INSERT INTO ProfileUser
	VALUES
	(
		@IProfileId,
		@IUserId
	)
