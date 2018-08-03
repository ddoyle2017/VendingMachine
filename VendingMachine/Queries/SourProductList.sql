SELECT Candies.CandyName, Candies.Price
FROM Candies
INNER JOIN CandyTypes ON Candies.CandyID = CandyTypes.CandyID
WHERE CandyTypes.Type = 'Sour'