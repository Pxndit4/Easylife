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
PRINT N'Creando [dbo].[sp_User_ChangePassword]...';


GO
CREATE PROCEDURE [dbo].[sp_User_ChangePassword]
	@IUserId	int,
	@IPassword	VARCHAR(400)
AS
	UPDATE	[User]
	SET		Password = @IPassword
	WHERE	UserId = @IUserId
GO
PRINT N'Creando [dbo].[sp_User_List]...';


GO

CREATE PROCEDURE [dbo].[sp_User_List]
	@IUser	VARCHAR(50),
	@IName	VARCHAR(400)
AS
	SELECT 
			UserId,
			Type,
			u.[User],
			Password,
			Name,
			Status
	FROM	[User] u
	WHERE	(u.[User] like '%'+@IUser+'%' or @IUser = '')
	AND		(Name like '%'+@IName+'%' or @IName = '')
	AND		u.UserId NOT IN (select onguserId from OngUser)
RETURN 0
GO
PRINT N'Creando [dbo].[sp_User_Sel]...';


GO
CREATE PROCEDURE [dbo].[sp_User_Sel]
	@IUserId int
AS
	SELECT 
			u.UserId,
			u.[User],
			u.Name,
			u.Type,
			u.Status
	FROM	[User] u
	WHERE	u.UserId = @IUserId
GO
PRINT N'Creando [dbo].[sp_User_Upd]...';


GO
CREATE PROCEDURE [dbo].[sp_User_Upd]
	@IUserId	int,
	@IUser		VARCHAR(100),
	@IName		VARCHAR(200),
	@iStatus	VARCHAR(1)
AS
	UPDATE	[User]
	SET		[User]	= @IUser,
			Name	= @IName,
			Status	= @iStatus
	WHERE	UserId	= @IUserId
GO
PRINT N'Actualización completada.';


GO
