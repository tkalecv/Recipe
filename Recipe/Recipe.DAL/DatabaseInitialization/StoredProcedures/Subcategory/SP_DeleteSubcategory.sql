CREATE PROCEDURE SP_DeleteSubcategory
  @SubcategoryID INT = NULL
AS

SET XACT_ABORT ON;
SET NOCOUNT    ON;

DECLARE
	  @ErrorNumber   INT
	, @ErrorLine     INT
	, @ErrorMessage  NVARCHAR(4000)
	, @SqlQuery      NVARCHAR(1000) = 'DELETE FROM Subcategory WHERE SubcategoryID = ' + CAST(@SubcategoryID as nvarchar(100));

BEGIN TRY

BEGIN TRANSACTION;

	execute sp_executesql @SqlQuery

COMMIT TRANSACTION;

	PRINT 'Subcategory deleted successfully'

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
