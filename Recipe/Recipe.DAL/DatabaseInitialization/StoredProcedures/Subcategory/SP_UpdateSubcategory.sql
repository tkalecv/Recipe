CREATE PROCEDURE SP_UpdateSubcategory
  @Name NVARCHAR(255)
, @CategoryID INT
, @SubcategoryID INT
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
	SET [Name] = @Name
	  , CategoryID = @CategoryID
	WHERE Subcategory = @SubcategoryID;

	COMMIT TRANSACTION;

	PRINT 'Subcategory updated successfully'

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
