CREATE PROCEDURE SP_RetrieveCategory
@CategoryID INT = NULL
AS

SET XACT_ABORT ON;
SET NOCOUNT    ON;

DECLARE
	  @ErrorNumber   INT
	, @ErrorLine     INT
	, @ErrorMessage  NVARCHAR(4000)
	, @SqlQuery      NVARCHAR(1000) = 'SELECT * FROM dbo.Category'

BEGIN TRY

BEGIN TRANSACTION;

	IF(@CategoryID IS NOT NULL)
	BEGIN
		SELECT @SqlQuery = @SqlQuery + ' WHERE CategoryID = ' + CAST(@CategoryID as nvarchar(100));;
	END;

	execute sp_executesql @SqlQuery

COMMIT TRANSACTION;

	PRINT 'Categories retrieved successfully'

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
