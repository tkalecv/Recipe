CREATE PROCEDURE SP_RetrieveRecipe 
@UserDataID INT = NULL
AS

SET XACT_ABORT ON;
SET NOCOUNT    ON;

DECLARE
	  @ErrorNumber   INT
	, @ErrorLine     INT
	, @ErrorMessage  NVARCHAR(4000)
	, @SqlQuery      NVARCHAR(1000) = 'SELECT * FROM dbo.Recipe'

BEGIN TRY

BEGIN TRANSACTION;

	IF(@UserDataID IS NOT NULL)
	BEGIN
		SELECT @SqlQuery = @SqlQuery + ' WHERE UserDataID = ' + CAST(@UserDataID as nvarchar(100));

		execute sp_executesql @SqlQuery
	END;
	ELSE
	BEGIN
		execute sp_executesql @SqlQuery
	END;

COMMIT TRANSACTION;

	PRINT 'Recipes retrieved successfully'

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
