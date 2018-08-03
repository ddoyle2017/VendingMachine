SELECT TOP 1 Candies.Color, COUNT(Purchases.CandyID) AS AmountPurchased
FROM Purchases
INNER JOIN Candies ON Purchases.CandyID = Candies.CandyID
GROUP BY Candies.Color
ORDER BY AmountPurchased DESC
