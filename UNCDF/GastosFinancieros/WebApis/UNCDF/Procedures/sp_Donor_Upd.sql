CREATE PROCEDURE [dbo].[sp_Donor_Upd]
  @IDonorId INT
 ,@IFirstName VARCHAR(200)
 ,@ILastName VARCHAR(200)
 ,@IEmail VARCHAR(400)
 ,@ICellphone VARCHAR(20)
 ,@IAddress VARCHAR(250)
 ,@ICountryId INT
 ,@IGender CHAR
 ,@IBirthday  NUMERIC(8,0)
 ,@IPhoto VARCHAR(400)
 ,@IStatus INT
AS
 BEGIN

    UPDATE Donor SET 
      FirstName = @IFirstName
     ,LastName = @ILastName
     ,Email = @IEmail
     ,Cellphone = @ICellphone
     ,Address = @IAddress
     ,CountryId = @ICountryId
     ,Gender = @IGender
     ,Birthday = @IBirthday
     ,Photo = @IPhoto
     ,Status = @IStatus
    WHERE DonorId = @IDonorId
END