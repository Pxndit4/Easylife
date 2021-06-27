CREATE PROCEDURE [dbo].[sp_Donor_CodeUpd]
	--@ICellphone		VARCHAR(20),
	@IPassword		VARCHAR(4000),
	@IEmail			VARCHAR(250),
	@IToken			VARCHAR(4000)
AS
BEGIN
	UPDATE	Donor
	SET		Password = @IPassword,
			Token = @IToken
	WHERE	Email = @IEmail
END