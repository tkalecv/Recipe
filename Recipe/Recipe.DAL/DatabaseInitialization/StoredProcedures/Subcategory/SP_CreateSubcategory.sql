CREATE PROCEDURE SP_CreateSubcategory
  @Name NVARCHAR(100)
, @CategoryID INT
AS

SET XACT_ABORT ON;
SET NOCOUNT    ON;

DECLARE
	  @ErrorNumber  INT
	, @ErrorLine    INT
	, @ErrorMessage NVARCHAR(4000)

BEGIN TRY

	BEGIN TRANSACTION;

	INSERT INTO dbo.Subcategory([Name], CategoryID)
	VALUES (@Name, @CategoryID);

	COMMIT TRANSACTION;

	PRINT 'Subcategory added successfully'

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
