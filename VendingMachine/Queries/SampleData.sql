INSERT INTO CandyTypes (Flavor)
VALUES ('Sweet');

INSERT INTO CandyTypes (Flavor)
VALUES ('Sour');

INSERT INTO CandyTypes (Flavor)
VALUES ('Salty');

INSERT INTO CandyTypes (Flavor)
VALUES ('Spicy');

INSERT INTO Candies (TypeID, CandyName, Color, Price)
VALUES ((SELECT TypeID FROM CandyTypes WHERE Flavor = 'Sweet'), 'Peanut M&Ms', 'Yellow', 2.99);

INSERT INTO Candies (TypeID, CandyName, Color, Price)
VALUES ((SELECT TypeID FROM CandyTypes WHERE Flavor = 'Salty'), 'Salted Dark Chocolate', 'Black', 7.99);

INSERT INTO Candies (TypeID, CandyName, Color, Price)
VALUES ((SELECT TypeID FROM CandyTypes WHERE Flavor = 'Sour'), 'Warheads', 'Green', 0.89);

INSERT INTO Candies (TypeID, CandyName, Color, Price)
VALUES ((SELECT TypeID FROM CandyTypes WHERE Flavor = 'Spicy'), 'Red Hots', 'Red', 4.99);

INSERT INTO Purchases (CandyID, PurchaseDate)
VALUES ((SELECT CandyID FROM Candies WHERE CandyName = 'Peanut M&Ms'), GETDATE());

INSERT INTO Purchases (CandyID, PurchaseDate)
VALUES ((SELECT CandyID FROM Candies WHERE CandyName = 'Peanut M&Ms'), GETDATE());

INSERT INTO Purchases (CandyID, PurchaseDate)
VALUES ((SELECT CandyID FROM Candies WHERE CandyName = 'Red Hots'), GETDATE());

INSERT INTO Purchases (CandyID, PurchaseDate)
VALUES ((SELECT CandyID FROM Candies WHERE CandyName = 'Warheads'), GETDATE());

INSERT INTO Shelves (ShelfName)
VALUES ('Sweet');

INSERT INTO Shelves (ShelfName)
VALUES ('Sour');

INSERT INTO Shelves (ShelfName)
VALUES ('Salty');

INSERT INTO Shelves (ShelfName)
VALUES ('Spicy');

INSERT INTO Inventory (CandyID, ShelfID, DateStocked)
VALUES ((SELECT CandyID FROM Candies WHERE CandyName = 'Peanut M&Ms'), (SELECT ShelfID FROM Shelves WHERE ShelfName = 'Sweet'), GETDATE());

INSERT INTO Inventory (CandyID, ShelfID, DateStocked)
VALUES ((SELECT CandyID FROM Candies WHERE CandyName = 'Peanut M&Ms'), (SELECT ShelfID FROM Shelves WHERE ShelfName = 'Sweet'), GETDATE());

INSERT INTO Inventory (CandyID, ShelfID, DateStocked)
VALUES ((SELECT CandyID FROM Candies WHERE CandyName = 'Warheads'), (SELECT ShelfID FROM Shelves WHERE ShelfName = 'Sour'), GETDATE());
