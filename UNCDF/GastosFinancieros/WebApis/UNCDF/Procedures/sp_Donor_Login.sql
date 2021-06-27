CREATE PROCEDURE [dbo].[sp_Donor_Login]
	--@ICellphone		VARCHAR(20),
	@IEmail			VARCHAR(200),
	@IPassword		VARCHAR(4000)
AS
BEGIN
	SELECT 

			DonorId,
			isnull(FirstName,'') FirstName,
			ISNULL(LastName,'') LastName,
			Email,
			Password,
			Cellphone,
			ISNULL(Address,'') Address,
			CountryId,
			ISNULL(Birthday,0) Birthday,
			ISNULL(Photo,'') Photo,
			Token
	FROM	Donor
	WHERE	Email = @IEmail
	AND		Password = @IPassword
	AND		Registered = 1
END