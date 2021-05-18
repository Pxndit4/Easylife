CREATE PROCEDURE [dbo].[sp_Options_Listar]
	@IProfileId INT
AS
	SELECT	
				O.OptionId,	
				ISNULL(O.IdFather,-1) AS IdFather,	
				O.Action,		
				o.Title,
				ISNULL(O.Link,'') AS Link,
				O.Orders 
    FROM		Options O
    INNER JOIN	ProfileOptions PO ON O.OptionId = PO.OptionId
    WHERE		PO.ProfileId = @IProfileId
    AND			O.Status='1'

    UNION ALL
    SELECT		DISTINCT	
				B.OptionId,
				ISNULL(B.IdFather,-1) AS IdFather,
				B.Action,
				B.Title,
				ISNULL(B.Link,'') AS Link,
				B.Orders 
    FROM		Options B
    INNER JOIN  Options C ON C.IdFather = B.OptionId
    INNER JOIN	ProfileOptions PO ON C.OptionId = PO.OptionId
    WHERE		PO.ProfileId = @IProfileId
    AND			C.Status='1'
	AND			B.Status='1'

    UNION ALL

    SELECT DISTINCT	
				A.OptionId,
				ISNULL(A.IdFather,-1) AS IdFather,
				A.Action,
				A.Title,
				ISNULL(A.Link,'') AS Link,
				A.Orders
    FROM		Options A
    INNER JOIN  Options B ON B.IdFather = A.OptionId
    INNER JOIN  Options C ON C.IdFather = B.OptionId
    INNER JOIN	ProfileOptions PO ON C.OptionId = PO.OptionId
    WHERE		PO.ProfileId = @IProfileId
    AND			C.Status='1' 
	AND			B.Status='1' 
	AND			A.Status='1';

