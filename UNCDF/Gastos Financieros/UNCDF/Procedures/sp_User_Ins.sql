CREATE PROCEDURE [dbo].[sp_User_Ins]
	@IType		int,
	@IUser		VARCHAR(100),
	@IPassword	VARCHAR(4000),
	@IName		VARCHAR(200),
	@IStatus	int,
	@IToken		VARCHAR(4000),
	@OUserId	INT OUTPUT
AS
	INSERT INTO [User]
	(
		Type,
		[User],
		Password,
		Name,
		Status,
		Token
	)
	VALUES
	(
		@IType,
		@IUser,
		@IPassword,
		@IName,
		@IStatus,
		@IToken
	)

	SET @OUserId = @@IDENTITY

