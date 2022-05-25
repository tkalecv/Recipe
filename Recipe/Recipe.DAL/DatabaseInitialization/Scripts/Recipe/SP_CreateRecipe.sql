CREATE PROCEDURE SP_CreateRecipe @Name NVARCHAR(255), @Description NVARCHAR(255), @UserDataID INT, @SubcategoryID INT
AS

SET XACT_ABORT ON;
SET NOCOUNT    ON;

DECLARE
	  @ErrorNumber  INT
	, @ErrorLine    INT
	, @ErrorMessage NVARCHAR(4000)

BEGIN TRY

	BEGIN TRANSACTION;

	INSERT INTO dbo.Recipe ([Name], [Description], UserDataID, SubcategoryID)
	VALUES (@Name, @Description, @UserDataID, @SubcategoryID);

	COMMIT TRANSACTION;

	PRINT 'Recipe added successfully'

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
