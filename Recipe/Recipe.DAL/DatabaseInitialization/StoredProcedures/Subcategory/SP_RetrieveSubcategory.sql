CREATE PROCEDURE SP_RetrieveSubcategory
  @SubcategoryID INT = NULL
, @CategoryID INT = NULL
AS

SET XACT_ABORT ON;
SET NOCOUNT    ON;

DECLARE
	  @ErrorNumber   INT
	, @ErrorLine     INT
	, @ErrorMessage  NVARCHAR(4000)
	, @SqlQuery      NVARCHAR(1000) = 'SELECT sc.SubcategoryID, sc.Name, sc.CategoryID, c.Name
									   FROM dbo.Subcategory sc
									   LEFT JOIN Category c ON sc.CategoryID = c.CategoryID'

BEGIN TRY

BEGIN TRANSACTION;


	IF(@SubcategoryID IS NOT NULL)
	BEGIN
		SELECT @SqlQuery = @SqlQuery + ' WHERE sc.SubcategoryID = ' + CAST(@SubcategoryID as nvarchar(100));;
	END;

		IF(@CategoryID IS NOT NULL)
	BEGIN
		IF(@SubcategoryID IS NOT NULL)
		BEGIN
			SELECT @SqlQuery = @SqlQuery + ' AND sc.CategoryID = ' + CAST(@CategoryID as nvarchar(100));
		END;
		ELSE
		BEGIN
			SELECT @SqlQuery = @SqlQuery + ' WHERE sc.CategoryID = ' + CAST(@CategoryID as nvarchar(100));
		END;
	END;

	execute sp_executesql @SqlQuery

COMMIT TRANSACTION;

	PRINT 'Subcategories retrieved successfully'

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
