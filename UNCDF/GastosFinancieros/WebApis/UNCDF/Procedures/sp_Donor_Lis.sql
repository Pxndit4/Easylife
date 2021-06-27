
CREATE PROCEDURE [dbo].[sp_Donor_Lis]
  @IFirstName VARCHAR(200)
 ,@ILastName VARCHAR(200)
 ,@ICountryId INT
 ,@IRegistered INT
 ,@IStatus INT
AS
 BEGIN

    SELECT 
	     d.DonorId
        ,ISNULL(d.FirstName,'') AS FirstName
        ,ISNULL(d.LastName,'') AS LastName
        ,ISNULL(d.Email,'') AS Email 
        --,d.Password
        ,ISNULL(d.Cellphone,'') AS Cellphone
        ,ISNULL(d.Address , '') AS Address
        ,d.CountryId
	    ,c.Description as Country
	    ,co.Description as Continent
        ,ISNULL(d.Gender,'') AS Gender
        ,ISNULL(d.Birthday,0) AS Birthday 
        ,ISNULL(d.Photo , '') AS Photo
        ,d.Registered  
	    ,d.Status
        FROM Donor d
	    inner join Country c
	    on d.CountryId= c.CountryId
	    inner join Continent co
	    on co.ContinentId= c.ContinentId
    where 
        (d.FirstName LIKE '%' + @IFirstName + '%' OR @IFirstName = '')
	AND (d.LastName LIKE '%' + @ILastName + '%' OR @ILastName = '')
	AND	(c.CountryId = @ICountryId OR @ICountryId = -1)
	AND	(d.Registered = @IRegistered OR @IRegistered = -1)
	AND	(d.Status = @IStatus OR @IStatus = -1)
	ORDER BY  d.DonorId DESC
END
