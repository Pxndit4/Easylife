CREATE PROCEDURE [dbo].[sp_Donor_Ins]
      @IFirstName VARCHAR(200) = null
     ,@ILastName VARCHAR(200)  = null
     ,@IEmail VARCHAR(400)  = null
     ,@IPassword VARCHAR(500)  = null
     ,@ICellphone VARCHAR(20)  = null
     ,@IAddress VARCHAR(250)  = null
     ,@ICountryId INT  = null
     ,@IGender CHAR  = null
     ,@IBirthday NUMERIC(8,0)  = null
     ,@IPhoto VARCHAR(400)  = null
     ,@IStatus INT  = null
     ,@IToken	VARCHAR(4000) = NULL
     ,@ODonorID INT OUTPUT
AS
 BEGIN

    INSERT INTO Donor(
     FirstName
    ,LastName
    ,Email
    ,Password
    ,Cellphone
    ,Address
    ,CountryId
    ,Gender
    ,Birthday
    ,Photo
    ,Status
	,Registered
	,Token
 )
 VALUES(
     @IFirstName
    ,@ILastName
    ,@IEmail
    ,@IPassword
    ,@ICellphone
    ,@IAddress
    ,@ICountryId
    ,@IGender
    ,@IBirthday
    ,@IPhoto
    ,1
	,0
	,@IToken
 )

 SET @ODonorID = @@IDENTITY

 END
