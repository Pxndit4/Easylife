
CREATE PROCEDURE [dbo].[sp_Parameters_List]
	@ICode	Varchar(30) = '',
	@IDescription Varchar(400)=''
AS
	SELECT
			ParameterId,
			Code,
			ISNULL(Description,'') AS Description,
			ISNULL(Valor1,'') AS Valor1,
			ISNULL(Valor2,'') AS Valor2
	FROM	Parameters
	WHERE	Status = 1
	AND		(Code = @ICode OR @ICode = '')
	AND		(Description = @IDescription OR @IDescription='')
