CREATE PROCEDURE [dbo].[sp_Donor_ValUser]
	--@ICellphone		VARCHAR(20),
	@IEmail			VARCHAR(200)
AS
	SELECT 
			Cellphone,
			Email,
			CountryId			
	FROM	Donor 
	--WHERE	Cellphone = @ICellphone or Email = @IEmail
	WHERE	Email = @IEmail
