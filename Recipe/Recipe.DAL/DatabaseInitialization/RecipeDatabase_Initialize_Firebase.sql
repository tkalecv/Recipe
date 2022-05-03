--CREATE DATABASE RecipeDatabaseFirebase;
--Use Database 'RecipeDatabaseFirebase'
USE RecipeDatabaseFirebase;

	--Create table 'Category'
	CREATE TABLE Category
	(
		CategoryID INT IDENTITY(1,1) NOT NULL,
		[Name]     NVARCHAR(100)     NOT NULL

		-- PRIMARY + UNIQUE
		CONSTRAINT PK_Category_CategoryID PRIMARY KEY (CategoryID),
		CONSTRAINT UC_Category_Name UNIQUE ([Name])
	);

	--Create table 'SubCategory'
	CREATE TABLE Subcategory
	(
		SubCategoryID INT IDENTITY(1,1) NOT NULL,
		[Name]        NVARCHAR(100)     NOT NULL,
		CategoryID    INT               NOT NULL

		-- PRIMARY + UNIQUE
		CONSTRAINT PK_SubCategory_SubCategoryID PRIMARY KEY (SubCategoryID),
		CONSTRAINT UC_SubCategory_Name UNIQUE ([Name]),

		CONSTRAINT FK_Subcategory_Category FOREIGN KEY (CategoryID) REFERENCES dbo.Category (CategoryID)
		ON DELETE CASCADE,
	);

--Create table 'User'
	CREATE TABLE [User]
	(
		UserID               INT            NOT NULL,
		FirebaseUserID       NVARCHAR (450) NOT NULL,
		UserName             NVARCHAR (256) NOT NULL,
		Email                NVARCHAR (256) NOT NULL,
		FirstName            NVARCHAR (256) NOT NULL,
		LastName             NVARCHAR (256) NOT NULL,

		-- PRIMARY + UNIQUE
		CONSTRAINT PK_User PRIMARY KEY CLUSTERED (UserID ASC),
	);

		-- INDEX for table 'User'
		CREATE UNIQUE NONCLUSTERED INDEX IX_User_Email_FirebaseUserID
		ON [User] (Email, FirebaseUserID ASC);

	--Create table 'Recipe'
	CREATE TABLE Recipe
	(
		RecipeID      INT IDENTITY(1,1) NOT NULL,
		[Name]        NVARCHAR(255)     NOT NULL,
		[Description] NVARCHAR(255)     NOT NULL,
		UserID        INT               NOT NULL,
		SubcategoryID INT               NOT NULL
			
		-- PRIMARY + UNIQUE
		CONSTRAINT PK_Recipe PRIMARY KEY (RecipeID),

		--FK
		CONSTRAINT FK_Recipe_User FOREIGN KEY (UserID) REFERENCES dbo.[User] (UserID),
		CONSTRAINT FK_Recipe_Subcategory FOREIGN KEY (SubcategoryID) REFERENCES dbo.Subcategory (SubcategoryID)
		ON DELETE CASCADE,

	);

	--Create table 'RecipeAttributes'
	CREATE TABLE RecipeAttributes
	(
		RecipeAttributesID INT IDENTITY(1,1) NOT NULL,
		RecipeID      INT                    NOT NULL,
		CreatedDate   DATETIME               NOT NULL CONSTRAINT DF_RecipeAttributes_CreatedDate DEFAULT CURRENT_TIMESTAMP,
		Person        INT                    NOT NULL,
		PrepareTime   TIME                   NOT NULL,
		Serving       NVARCHAR(255)	         NOT NULL,
		Advice        NVARCHAR(255)	         NULL,
		Stars         INT			         NOT NULL CONSTRAINT DF_RecipeAttributes_Stars DEFAULT 0,
		Likes         INT			         NOT NULL CONSTRAINT DF_RecipeAttributes_Likes DEFAULT 0

		--UC
		CONSTRAINT UC_RecipeAttributes_RecipeID UNIQUE (RecipeID),

		--CK
		CONSTRAINT CK_Recipe_Person CHECK (Person >= 1),
		CONSTRAINT CK_Recipe_Stars CHECK (Stars >= 0),
		CONSTRAINT CK_Recipe_Likes CHECK (Likes >= 0),
		CONSTRAINT CK_Recipe_CreatedDate CHECK (CreatedDate <= GETDATE()),

		--FK
		CONSTRAINT FK_RecipeAttributes_Recipe FOREIGN KEY (RecipeID) REFERENCES dbo.Recipe (RecipeID)
	);

	--Create table 'PreparationStep'
	CREATE TABLE PreparationStep
	(
		PreparationStepID INT IDENTITY(1,1) NOT NULL,
		RecipeID          INT               NOT NULL,
		Number            INT               NOT NULL,
		[Description]     TEXT              NOT NULL

		-- PRIMARY + UNIQUE
		CONSTRAINT PK_Step PRIMARY KEY (PreparationStepID),

		--CK
		CONSTRAINT CK_Step_Number CHECK (Number >= 1),

		--FK
		CONSTRAINT FK_Step_Recipe FOREIGN KEY (PreparationStepID) REFERENCES dbo.Recipe (RecipeID)
	);

	--Create table 'Picture'
	CREATE TABLE [Picture]
	(
		PictureID INT IDENTITY(1,1) NOT NULL,
		RecipeID  INT               NOT NULL,
		[Image] IMAGE               NOT NULL

		-- PRIMARY + UNIQUE
		CONSTRAINT PK_Picture PRIMARY KEY (PictureID),

		--FK
		CONSTRAINT FK_Picture_Recipe FOREIGN KEY (RecipeID) REFERENCES Recipe (RecipeID)
	);

	--Create table 'MeasuringUnit'
	CREATE TABLE MeasuringUnit
	(
		MeasuringUnitID INT IDENTITY(1,1) NOT NULL,
		[Name]          NVARCHAR(50)      NOT NULL,
		Abbreviation    NVARCHAR(10)      NOT NULL

		-- PRIMARY + UNIQUE
		CONSTRAINT PK_MeasuringUnit PRIMARY KEY (MeasuringUnitID),
		CONSTRAINT UC_Ingredient_Name_Abbreviation UNIQUE ([Name], Abbreviation)
	);

	--Create table 'Ingredient'
	CREATE TABLE Ingredient
	(
		IngredientID INT IDENTITY(1,1) NOT NULL,
		[Name]       NVARCHAR(255)     NOT NULL

		-- PRIMARY + UNIQUE
		CONSTRAINT PK_Ingredient_IngredientID PRIMARY KEY (IngredientID),
		CONSTRAINT UC_Ingredient_Name UNIQUE ([Name]),
	);

	--Create table 'IngredientMeasuringUnit'
	CREATE TABLE IngredientMeasuringUnit
	(
		IngredientID     INT NOT NULL,
		MeasuringUnitID  INT NOT NULL

		-- PRIMARY + UNIQUE
		CONSTRAINT PK_IngredientMeasuringUnit_IngredientID_MeasuringUnitID PRIMARY KEY (IngredientID, MeasuringUnitID),

		--FK
		CONSTRAINT FK_IngredientMeasuringUnit_Ingredient FOREIGN KEY (IngredientID) REFERENCES dbo.Ingredient (IngredientID)
		ON DELETE CASCADE,
		CONSTRAINT FK_IngredientMeasuringUnit_MeasuringUnit FOREIGN KEY (MeasuringUnitID) REFERENCES dbo.MeasuringUnit (MeasuringUnitID)
		ON DELETE CASCADE
	);

	--Create table 'UserLikedRecipe'
	CREATE TABLE UserLikedRecipe
	(
		UserID   INT NOT NULL,
		RecipeID INT NOT NULL

		-- PRIMARY + UNIQUE
		CONSTRAINT PK_UserLikedRecipe_UserID_RecipeID PRIMARY KEY (UserID, RecipeID),

		--FK
		CONSTRAINT FK_UserLikedRecipe_User FOREIGN KEY (UserID) REFERENCES dbo.[User] (UserID)
		ON DELETE CASCADE,
		CONSTRAINT FK_UserLikedRecipe_Recipe FOREIGN KEY (RecipeID) REFERENCES dbo.Recipe (RecipeID)
		ON DELETE CASCADE
	);