CREATE PROCEDURE [dbo].[sp_Donor_ValidateCode]
	--@ICellphone		VARCHAR(20),
	@IEmail			VARCHAR(250),
	@IPassword		VARCHAR(4000),
	@OResult		INT OUTPUT
AS
BEGIN

	--IF EXISTS (SELECT 1 FROM Donor WHERE Password = @IPassword AND Cellphone = @ICellphone and Email = @IEmail and Registered = 0)
	IF EXISTS (SELECT 1 FROM Donor WHERE Password = @IPassword AND Email = @IEmail and Registered = 0)
		BEGIN
			UPDATE	Donor
			SET		Registered = 1
			WHERE	Email = @IEmail
			--AND		Cellphone = @ICellphone
			AND		Password = @IPassword

			SET @OResult = 0
		END
	--ELSE IF EXISTS (SELECT 1 FROM Donor WHERE Password = @IPassword AND Cellphone = @ICellphone and Email = @IEmail and Registered = 1)
	ELSE IF EXISTS (SELECT 1 FROM Donor WHERE Password = @IPassword AND Email = @IEmail and Registered = 1)
		SET @OResult = 3
	ELSE
		SET @OResult = 1	
END