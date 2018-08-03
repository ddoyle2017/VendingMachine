SELECT Shelves.ShelfName, COUNT(Inventory.ShelfID) AS CandyAmount
FROM Inventory
INNER JOIN Shelves ON Inventory.ShelfID = Shelves.ShelfID
GROUP BY Shelves.ShelfName
ORDER BY Shelves.ShelfName DESC