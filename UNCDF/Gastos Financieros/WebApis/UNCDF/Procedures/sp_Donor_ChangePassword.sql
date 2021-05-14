CREATE PROCEDURE [dbo].[sp_Donor_ChangePassword]
	@IDonorId		INT,
	@IEmail			VARCHAR(200),--@ICellphone		VARCHAR(20),
	@IOldPassword	VARCHAR(4000),
	@INewPassword	VARCHAR(4000),
	@OResult		INT OUTPUT
AS
BEGIN

	--IF EXISTS (SELECT 1 FROM Donor WHERE Password = @IOldPassword AND Cellphone = @ICellphone AND DonorId = @IDonorId)
	IF EXISTS (SELECT 1 FROM Donor WHERE Password = @IOldPassword AND Email = @IEmail AND DonorId = @IDonorId)
		BEGIN
			UPDATE	Donor
			SET		Password = @INewPassword
			WHERE	Email = @IEmail --Cellphone = @ICellphone
			AND		Password = @IOldPassword
			AND		DonorId = @IDonorId

			SET @OResult = 0
		END
	ELSE
		SET @OResult = 1	
END