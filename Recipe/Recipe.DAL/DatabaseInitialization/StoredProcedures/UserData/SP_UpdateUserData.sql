CREATE PROCEDURE SP_UpdateUserData
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

	UPDATE dbo.Category
	SET Address = @Address 
	  , City = @City
	  , FirstName = @FirstName
	  , LastName = @LastName
	WHERE FirebaseUserID = @FirebaseUserID;

	COMMIT TRANSACTION;

	PRINT 'UserData updated successfully'

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
