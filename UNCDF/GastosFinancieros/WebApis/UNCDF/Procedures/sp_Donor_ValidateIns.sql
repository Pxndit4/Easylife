CREATE PROCEDURE [dbo].[sp_Donor_ValidateIns]
	--@ICellphone		VARCHAR(20),
	@IEmail			VARCHAR(250),
	@OResult		INT OUTPUT,
	@ORegistered	INT = 0 OUTPUT,
	@ODonorId		INT = 0 OUTPUT
AS
BEGIN

	IF EXISTS (SELECT 1 FROM Donor WHERE Email = @IEmail)
		BEGIN
			SET @OResult = 1
			SET @ORegistered = (SELECT Registered FROM Donor WHERE Email = @IEmail)
			SET @ODonorId = (SELECT DonorId FROM Donor WHERE Email = @IEmail)
		END
	ELSE
		BEGIN
			SET @ORegistered = 0
			SET @OResult = 0
			SET @ODonorId = 0
		END
END

