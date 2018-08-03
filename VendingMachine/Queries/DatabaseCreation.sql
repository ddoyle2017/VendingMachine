CREATE TABLE CandyTypes (
    TypeID int IDENTITY(1,1) PRIMARY KEY,
    Flavor nvarchar(50) NOT NULL
);

CREATE TABLE Shelves (
    ShelfID int IDENTITY(1,1) PRIMARY KEY,
    ShelfName nvarchar(50) UNIQUE NOT NULL
);

CREATE TABLE Candies (
    CandyID int IDENTITY(1,1) PRIMARY KEY,
    TypeID int FOREIGN KEY REFERENCES CandyTypes(TypeID),
    CandyName nvarchar(200) UNIQUE NOT NULL, 
    Color nvarchar(100) NOT NULL,
    Price decimal(9,2) NOT NULL
);

CREATE TABLE Purchases (
    PurchaseID int IDENTITY(1,1) PRIMARY KEY,
    CandyID int FOREIGN KEY REFERENCES Candies(CandyID) NOT NULL,
    PurchaseDate datetime NOT NULL
);

CREATE TABLE Inventory (
    InventoryID int IDENTITY(1,1) PRIMARY KEY,
    CandyID int FOREIGN KEY REFERENCES Candies(CandyID) NOT NULL,
    ShelfID int FOREIGN KEY REFERENCES Shelves(ShelfID) NOT NULL,
    DateStocked datetime NOT NULL
);