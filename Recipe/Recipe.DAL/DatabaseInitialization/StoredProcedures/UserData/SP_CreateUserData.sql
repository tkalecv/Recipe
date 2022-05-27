CREATE PROCEDURE SP_CreateUserData 
  @FirebaseUserID NVARCHAR (450)
, @Address NVARCHAR (100)
, @City NVARCHAR (100)
, @FirstName NVARCHAR(50)
, @LastName NVARCHAR(50)
AS

SET XACT_ABORT ON;
SET NOCOUNT    ON;

DECLARE
	  @ErrorNumber  INT
	, @ErrorLine    INT
	, @ErrorMessage NVARCHAR(4000)

BEGIN TRY

	BEGIN TRANSACTION;

	INSERT INTO dbo.UserData(FirebaseUserID, [Address], City, FirstName, LastName )
	VALUES (@FirebaseUserID, @Address, @City, @FirstName, @LastName);

	COMMIT TRANSACTION;

	PRINT 'UserData added successfully'

END TRY
BEGIN CATCH

	SELECT
		 @ErrorNumber  = ERROR_NUMBER()
		,@ErrorLine    = ERROR_LINE()
		,@ErrorMessage = ERROR_MESSAGE();

	IF(XACT_STATE() <> 0 AND @@TRANCOUNT > 0)
	BEGIN
		ROLLBACK TRANSACTION;
	END;

	RAISERROR('ERROR : Number - %d, Line - %d, Message - %s'
			,16
			,-1
			,@ErrorNumber
			,@ErrorLine
			,@ErrorMessage);

END CATCH;
