CREATE PROCEDURE [dbo].[sp_Donor_Del]
  @IDonorId VARCHAR(10)
AS
BEGIN

    DELETE Donor
    WHERE Cellphone = @IDonorId
END