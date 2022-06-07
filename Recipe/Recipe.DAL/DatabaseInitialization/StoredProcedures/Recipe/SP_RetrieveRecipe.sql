CREATE PROCEDURE SP_RetrieveRecipe 
  @UserDataID INT = NULL
, @RecipeID   INT = NULL
AS

SET XACT_ABORT ON;
SET NOCOUNT    ON;

DECLARE
	  @ErrorNumber   INT
	, @ErrorLine     INT
	, @ErrorMessage  NVARCHAR(4000)
	, @SqlQuery      NVARCHAR(1000) = 'SELECT r.RecipeID, r.[Name], r.[Description],
										sc.SubcategoryID, sc.[Name],
										ud.UserDataID,
										c.CategoryID, c.[Name]
										FROM Recipe r
										LEFT JOIN Subcategory sc ON r.SubcategoryID = sc.SubCategoryID
										LEFT JOIN UserData ud ON r.UserDataID = ud.UserDataID
										LEFT JOIN Category c ON sc.CategoryID = c.CategoryID'

BEGIN TRY

BEGIN TRANSACTION;

	IF(@UserDataID IS NOT NULL)
	BEGIN
		SELECT @SqlQuery = @SqlQuery + ' WHERE r.UserDataID = ' + CAST(@UserDataID as nvarchar(100));
	END;

		IF(@RecipeID IS NOT NULL)
	BEGIN
		IF(@UserDataID IS NOT NULL)
		BEGIN
			SELECT @SqlQuery = @SqlQuery + ' AND r.RecipeID = ' + CAST(@RecipeID as nvarchar(100));
		END;
		ELSE
		BEGIN
			SELECT @SqlQuery = @SqlQuery + ' WHERE r.RecipeID = ' + CAST(@RecipeID as nvarchar(100));
		END;
	END;

	execute sp_executesql @SqlQuery

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
