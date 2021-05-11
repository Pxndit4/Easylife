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
