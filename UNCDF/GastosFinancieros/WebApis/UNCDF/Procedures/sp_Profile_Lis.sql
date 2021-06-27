CREATE PROCEDURE [dbo].[sp_Profile_Lis]
	@IDescription		VARCHAR(400)
AS
	SELECT
			ProfileId,
			Description,
			Status
	FROM	Profiles
	WHERE	(Description like '%' + @IDescription + '%' or @IDescription = '')
