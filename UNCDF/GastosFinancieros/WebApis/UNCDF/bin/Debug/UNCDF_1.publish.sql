/*
Script de implementación para Uncdf

Una herramienta generó este código.
Los cambios realizados en este archivo podrían generar un comportamiento incorrecto y se perderán si
se vuelve a generar el código.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "Uncdf"
:setvar DefaultFilePrefix "Uncdf"
:setvar DefaultDataPath "D:\rdsdbdata\DATA\"
:setvar DefaultLogPath "D:\rdsdbdata\DATA\"

GO
:on error exit
GO
/*
Detectar el modo SQLCMD y deshabilitar la ejecución del script si no se admite el modo SQLCMD.
Para volver a habilitar el script después de habilitar el modo SQLCMD, ejecute lo siguiente:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'El modo SQLCMD debe estar habilitado para ejecutar correctamente este script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
PRINT N'Creando [dbo].[Options]...';


GO
CREATE TABLE [dbo].[Options] (
    [OptionId]    INT           NOT NULL,
    [IdFather]    INT           NULL,
    [Title]       VARCHAR (50)  NULL,
    [Link]        VARCHAR (200) NULL,
    [Status]      VARCHAR (10)  NULL,
    [Action]      INT           NULL,
    [Orders]      INT           NULL,
    [Description] VARCHAR (250) NULL,
    PRIMARY KEY CLUSTERED ([OptionId] ASC)
);


GO
PRINT N'Creando [dbo].[ProfileOptions]...';


GO
CREATE TABLE [dbo].[ProfileOptions] (
    [ProfileId] INT NOT NULL,
    [OptionId]  INT NOT NULL,
    PRIMARY KEY CLUSTERED ([OptionId] ASC, [ProfileId] ASC)
);


GO
PRINT N'Creando [dbo].[Profiles]...';


GO
CREATE TABLE [dbo].[Profiles] (
    [ProfileId]   INT           IDENTITY (1, 1) NOT NULL,
    [Description] VARCHAR (250) NOT NULL,
    [Status]      INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([ProfileId] ASC)
);


GO
PRINT N'Creando [dbo].[ProfileUser]...';


GO
CREATE TABLE [dbo].[ProfileUser] (
    [ProfileId] INT        NOT NULL,
    [UserId]    NCHAR (10) NOT NULL,
    PRIMARY KEY CLUSTERED ([ProfileId] ASC, [UserId] ASC)
);


GO
PRINT N'Creando [dbo].[sp_Profile_Del]...';


GO
CREATE PROCEDURE [dbo].[sp_Profile_Del]
	@IProfileId int 
AS
	UPDATE	Profiles
	SET		Status = 0
	WHERE	ProfileId = @IProfileId
GO
PRINT N'Creando [dbo].[sp_Profile_ins]...';


GO
CREATE PROCEDURE [dbo].[sp_Profile_ins]
	@IDescription		VARCHAR(400),
	@OProfileId			INT OUTPUT
AS
	INSERT INTO Profiles
	(
		Description,
		Status
	)
	VALUES
	(
		@IDescription,
		1
	)

	SET @OProfileId = @@IDENTITY
GO
PRINT N'Creando [dbo].[sp_Profile_Lis]...';


GO
CREATE PROCEDURE [dbo].[sp_Profile_Lis]
	@IDescription		VARCHAR(400)
AS
	SELECT
			ProfileId,
			Description,
			Status
	FROM	Profiles
	WHERE	(Description like '%' + @IDescription + '%' or @IDescription = '')
GO
PRINT N'Creando [dbo].[sp_Profile_Sel]...';


GO
CREATE PROCEDURE [dbo].[sp_Profile_Sel]
	@IProfileId			INT
AS
	SELECT 
			Description,
			Status
	FROM	Profiles
	WHERE	ProfileId = @IProfileId
GO
PRINT N'Creando [dbo].[sp_Profile_Upd]...';


GO
CREATE PROCEDURE [dbo].[sp_Profile_Upd]
	@IDescription		VARCHAR(400),
	@IProfileId			INT,
	@IStatus			INT
AS
	UPDATE	Profiles
	SET		Description = @IDescription,
			Status = @IStatus
	WHERE	ProfileId = @IProfileId
GO
PRINT N'Creando [dbo].[sp_ProfileOptions_Del]...';


GO
CREATE PROCEDURE [dbo].[sp_ProfileOptions_Del]
	@IProfileId			INT
AS
	DELETE FROM ProfileOptions WHERE ProfileId = @IProfileId
GO
PRINT N'Creando [dbo].[sp_ProfileOptions_Ins]...';


GO
CREATE PROCEDURE [dbo].[sp_ProfileOptions_Ins]
	@IProfileId	int = 0,
	@IOptionId	int
AS
	INSERT INTO ProfileOptions
	(
		ProfileId,
		OptionId
	)
	VALUES
	(
		@IProfileId,
		@IOptionId
	)
GO
PRINT N'Creando [dbo].[sp_ProfileOptions_Sel]...';


GO
CREATE PROCEDURE [dbo].[sp_ProfileOptions_Sel]
	@IProfileId			INT
AS
	SELECT 
						ISNULL(D.ProfileId,0) ProfileId, 
						ISNULL(C.OptionId,0) OptionId, 	
						CASE WHEN ISNULL(D.OptionId,0) > 0 THEN 1 ELSE 0 END FlagActive , 
						C.Title Title, 
						B.Title TitleSubModule, 
						A.Title TitleModule 
	FROM				Options C 
	INNER JOIN			Options B ON C.IdFather = B.OptionId AND C.Status = 1
	INNER JOIN			Options A ON B.IdFather = A.OptionId AND A.Status = 1 AND B.Status = 1  
	LEFT OUTER JOIN		ProfileOptions D ON D.OptionId = C.OptionId AND D.ProfileId = @IProfileId
	WHERE				ISNULL(A.IdFather,0)  = 0
GO
PRINT N'Creando [dbo].[sp_ProfileUser_Del]...';


GO
CREATE PROCEDURE [dbo].[sp_ProfileUser_Del]
	@IProfileId			INT,
	@IUserId			INT
AS
	DELETE ProfileUser WHERE ProfileId = @IProfileId AND UserId = @IUserId
RETURN 0
GO
PRINT N'Creando [dbo].[sp_ProfileUser_Ins]...';


GO
CREATE PROCEDURE [dbo].[sp_ProfileUser_Ins]
	@IProfileId			INT,
	@IUserId			INT
AS
	INSERT INTO ProfileUser
	VALUES
	(
		@IProfileId,
		@IUserId
	)
GO
PRINT N'Creando [dbo].[sp_ProfileUser_LisUnAsiggned]...';


GO
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
GO
PRINT N'Creando [dbo].[sp_ProfileUser_Sel]...';


GO
CREATE PROCEDURE [dbo].[sp_ProfileUser_Sel]
	@IProfileId			INT
AS
	SELECT		PU.ProfileId,
				U.UserId,
				U.[User],
				u.Name 
	FROM		ProfileUser PU
	INNER JOIN	[User] U ON PU.UserId = U.UserId
	WHERE		PU.ProfileId = @IProfileId
GO
PRINT N'Actualización completada.';


GO
