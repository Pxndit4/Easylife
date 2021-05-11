CREATE PROCEDURE [dbo].[sp_Profile_ins]
	@IDescription		VARCHAR(400),
	@OProfileId			INT OUTPUT
AS
	INSERT INTO Profiles
	(
		Description,
		Status
	)
	VALUES
	(
		@IDescription,
		1
	)

	SET @OProfileId = @@IDENTITY
