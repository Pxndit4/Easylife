CREATE PROCEDURE [dbo].[sp_Aplication_Val]
	@IToken VARCHAR(255),
	@OVal	INT OUTPUT
AS
	IF EXISTS (SELECT 1 FROM Aplications WHERE Token = @IToken)
		SET @OVal = 1
	ELSE
		SET @OVal = 0
RETURN 0
