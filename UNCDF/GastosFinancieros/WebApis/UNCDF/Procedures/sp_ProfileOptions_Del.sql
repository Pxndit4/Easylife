CREATE PROCEDURE [dbo].[sp_ProfileOptions_Del]
	@IProfileId			INT
AS
	DELETE FROM ProfileOptions WHERE ProfileId = @IProfileId
