CREATE PROCEDURE [dbo].[sp_ProfileOptions_Ins]
	@IProfileId	int = 0,
	@IOptionId	int
AS
	INSERT INTO ProfileOptions
	(
		ProfileId,
		OptionId
	)
	VALUES
	(
		@IProfileId,
		@IOptionId
	)

