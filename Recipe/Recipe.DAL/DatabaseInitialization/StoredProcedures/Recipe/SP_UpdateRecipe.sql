CREATE PROCEDURE SP_UpdateRecipe 
  @Name NVARCHAR(255)
, @Description NVARCHAR(255)
, @UserDataID INT
, @SubcategoryID INT
, @RecipeID INT
AS

SET XACT_ABORT ON;
SET NOCOUNT    ON;

DECLARE
	  @ErrorNumber  INT
	, @ErrorLine    INT
	, @ErrorMessage NVARCHAR(4000)

BEGIN TRY

	BEGIN TRANSACTION;

	UPDATE dbo.Recipe
	SET [Name] = @Name
	, [Description] = @Description
	, UserDataID = @UserDataID
	, SubcategoryID = @SubcategoryID
	WHERE RecipeID = @RecipeID;

	COMMIT TRANSACTION;

	PRINT 'Recipe updated successfully'

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
