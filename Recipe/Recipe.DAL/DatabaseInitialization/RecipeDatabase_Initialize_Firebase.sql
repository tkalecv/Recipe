--DROP DATABASE RecipeDatabaseFirebase;
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

--Create table 'UserData'
	CREATE TABLE [UserData]
	(
		UserDataID           INT IDENTITY(1,1) NOT NULL,
		FirebaseUserID       NVARCHAR (450)    NOT NULL,
		[Address]            NVARCHAR (100)    NOT NULL,
		City                 NVARCHAR (100)    NOT NULL,
		FirstName            NVARCHAR(50)      NOT NULL,
		LastName             NVARCHAR(50)      NOT NULL,

		-- PRIMARY + UNIQUE
		CONSTRAINT PK_UserData PRIMARY KEY CLUSTERED (UserDataID ASC),
	);

		-- INDEX for table 'UserData'
		CREATE UNIQUE NONCLUSTERED INDEX IX_UserData_FirebaseUserID
		ON [UserData] (FirebaseUserID ASC);

	--Create table 'Recipe'
	CREATE TABLE Recipe
	(
		RecipeID      INT IDENTITY(1,1) NOT NULL,
		[Name]        NVARCHAR(255)     NOT NULL,
		[Description] NVARCHAR(255)     NOT NULL,
		UserDataID    INT               NOT NULL,
		SubcategoryID INT               NOT NULL
			
		-- PRIMARY + UNIQUE
		CONSTRAINT PK_Recipe PRIMARY KEY (RecipeID),

		--FK
		CONSTRAINT FK_Recipe_UserData FOREIGN KEY (UserDataID) REFERENCES dbo.[UserData] (UserDataID),
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

		--UC
		CONSTRAINT UC_RecipeAttributes_RecipeID UNIQUE (RecipeID),

		--CK
		CONSTRAINT CK_Recipe_Person CHECK (Person >= 1),
		CONSTRAINT CK_Recipe_Stars CHECK (Stars >= 0),
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



	--Default data insertion
	INSERT INTO Category ([Name])
	VALUES ('Sweet'),
	       ('Salty')

	INSERT INTO Subcategory ([Name], CategoryID)
	VALUES ('Cakes', 1),
		   ('Cookies', 1),
	       ('Pastries', 1),
	       ('Soups', 2),
	       ('Stews', 2),
	       ('Meat', 2),
	       ('Salads', 2),
	       ('Sauces', 2)

	INSERT INTO UserData (FirebaseUserID, Address, City, FirstName, LastName)
	VALUES ('testId', 'Some address', 'Some city', 'test', 'user'),
	       ('testId2', 'Some address 2', 'Some city 2', 'test2', 'user2')

	INSERT INTO Recipe (Name, Description, UserDataID, SubcategoryID)
	VALUES ('Cake recipe', 'This is some cake recipe', 1, 1),
		   ('Potato recipe', 'This is some potato recipe', 1, 2),
		   ('Soup recipe', 'This is some soup recipe', 3, 2),
		   ('Salad recipe', 'This is some salad recipe', 3, 1)