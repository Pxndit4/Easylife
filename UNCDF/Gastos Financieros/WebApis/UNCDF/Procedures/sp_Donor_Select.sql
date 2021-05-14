CREATE PROCEDURE [dbo].[sp_Donor_Select]
	@iDonorId int
AS
	SELECT 

			DonorId,
			ISNULL(FirstName,'') FirstName,
			ISNULL(LastName,'') LastName,
			Email,
			Password,
			Cellphone,
			ISNULL(Address,'') Address,
			CountryId,
			ISNULL(Birthday,0) Birthday,
			ISNULL(Photo,'') Photo,
			Token,
			ISNULL(Gender,'') Gender
	FROM	Donor
	WHERE	DonorId = @iDonorId
