CREATE PROCEDURE sp_ProfileUser_LisUnAsiggned(
	@IProfileId		INT,
	@Iuser			VARCHAR(200),
	@IName			VARCHAR(200)
)
AS
SELECT
					ISNULL(ProfileId, 0) ProfileId,
					u.UserId,
					[User],
					Name
FROM				ProfileUser PU
RIGHT OUTER JOIN	[User] U ON PU.UserId = u.UserId AND PU.ProfileId = @IProfileId
WHERE				ISNULL(ProfileId, 0) = 0
AND					(u.[User] LIKE '%'+@Iuser+ '%' OR @Iuser = '')
AND					(U.Name LIKE '%' + @IName+'%' or @IName = '')
AND					U.UserId NOT IN (select OngUserId from OngUser)