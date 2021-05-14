CREATE PROCEDURE [dbo].[sp_ValidateSession]
	@IType		int = 0, 
	@IToken		VARCHAR(400),
	@IUserId	INT,
	@OVAL		INT OUTPUT
AS

	SET @OVAL = 0

	IF @IType = 1 --DONOR
		BEGIN
			IF EXISTS (SELECT 1 FROM Donor WHERE DonorId = @IUserId AND Token = @IToken)
				SET @OVAL = 1
		END

	--IF @IType = 3 --ONG
	--	BEGIN
	--		IF EXISTS (SELECT 1 FROM [User] U INNER JOIN OngUser OU ON U.UserId = OU.OngUserId WHERE UserId = @IUserId AND Token = @IToken)
	--			SET @OVAL = 1
	--	END
	
	IF @IType = 2 --CMS
		BEGIN
			IF EXISTS (SELECT 1 FROM [User] WHERE UserId = @IUserId AND Token = @IToken)
				SET @OVAL = 1
		END
