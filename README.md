# Vending Machine
## Program Design
![State Diagram](/diagrams/state-machine-diagram.png)
***Fig 1:*** *State diagram for the Vending Machine.*

My approach for designing the vending machine was to model it as a finite state machine. I chose this approach because a vending machine does not need to keep a history of previous states, the next transition only depends on the current state.

The Vending Machine starts in the Active state, where a menu of candy for sale is displayed. The user then inputs their choice, which moves the Vending Machine to the Checking Inventory State. If the candy is out-of-stock, the machine is reverted back to Active and the user is prompted for another choice. If accepted, the machine moves to the Processing start, which is where a transaction is begun.

In this state, the Vending Machine prompts the user for a payment and waits until it receives one. Upon receiving a payment, the machine transitions to the Checking Payment state where payment validation is done, e.g. is the given amount greater than or equal to the price of the candy choice. If rejected, the machine reverts back to Processing and awaits another payment.

When a payment is accepted, the Vending Machine changes to the Vending state, where it goes and retrieves the item. Popping the item off the appropriate queue, updating the inventory, unwrapping the candy, and adding the wrapper to the trash compartment all happens here. When the item is successfully dispensed, the machine loops back to the Active state and the entire process begins again.

## Potential Improvements
* Another approach, that I learned about later while making this project, would be to handle the states of the Vending Machine by implementing the State design pattern.
* All display messages and SQL queries should be stored in some type of resource file, e.g. a .resx or .json, to avoid the Magic String anti-pattern.
___
## Classes
![Class Diagram]()

### IProduct
>An interface that defines the fields and methods necessary for basic Vending Machine operations. Name, price, the state of the wrapper is held here. An interface was chosen so down the line the Vending Machine can support other products that implement this interface, like a Chip or Ice Cream class.
>
>The decimal type was used to store price information, over the other floating point types, because it is less prone to potential rounding errors that come with floating point math.

### Candy
>The Candy class implements IProduct. In addition to the functionality provided by IProduct, the Candy class contains candy specific fields and methods, e.g. flavor.

### Shelf
>This class represents a shelf on the vending machine. Each shelf has a contains a set of queues. Each queue holds one type of item (similar to a real vending machine) and is stored in a dictionary with the item name as the key. A dictionary was used to take advantage of the O(1) look-up time when retrieving an item.
>
>The class contains methods for adding new products to the shelf, restocking existing ones, retrieving an item, as well as reporting back the stock of a particular item.

### TrashCompartment
> Manages the trash compartment for the vending machine. Keeps a tally of all wrappers placed there after an item is dispensed.

### InventoryManager
>This class contains all the logic for the Vending Machine's inventory. All actions involving the machine's inventory are handled by this class. It contains the shelves that make up the inventory, manages the trash compartment, logic for which candies go where when stocking, a list of all available products, as well as the method for dispensing an item and item and adding its wrapper to the trash.
>
>The class, however, does not deal with any of the logic concerning pricing and payment. That is contained in the transaction manager. My reasoning for this was to better follow separation of concerns and the Single Responsibility principle.

### TransactionManager
>Contains the logic for financial operations with the Vending Machine. This class validates any payments the machine receives. A list of candy price information and a record of the totaly amount of cash the machine has is maintained. Features that could be built out in this class are: refunds, calcuting change, verifying credit card payments with an outside system, etc.
>
>As mentioned above, separating the financial logic from the inventory logic was to better follow separation of concerns and the Single Responsibility principle.

### VendingMachine
> The Vending Machine class ties everything together. It is responsible for maintaining/transitioning through the different states and calling the appropriate methods. The class initializes both of the Manager methods, provides the product information dictionary, reads user input, and displays user feedback through the console app.

In an actually production version of this app, the Vending Machine class could be integrated with a GUI for accepting user input. A database would also be used for read/writing product and inventory data, rather than having sample data initialized within the Vending Machine class.


# Database
``` SQL
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
    Price decimal(15,2) NOT NULL
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
```
___
![Database Diagram](/diagrams/vending-machine-database.png)
***Fig 3:*** *Entity Relationship Diagram for the Vending Machine's database.*

This is the schema of the Vending Machine's database. The Candies Table contains a unique record for each possible candy product that is sold in the vending machine. Contains pricing information, wrapper color, and flavor. The Inventory Table holds all information about the *current* inventory of the vending machine. What candies are in stock, which shelf they're stored in, as well as what date-time they were added is kept track of. 

The Purchases Table is for recording each *successful* transaction at the vending machine, i.e. candy choosen, money is accepted, item is dispense, wrapper is stored and removed. What was purchased, as well as the date-time is stored here.

Lastly, there are two lookup tables: Shelves and CandyTypes, which contain strict definitions for database. The predefined candy flavors are stored in CandyTypes, and the shelves that are used for inventory are defined in Shelves.

## Queries
>List of all products in the vending machine that are sour.
``` SQL
SELECT Candies.CandyName, Candies.Price
FROM Candies
INNER JOIN CandyTypes ON Candies.CandyID = CandyTypes.CandyID
WHERE CandyTypes.Type = 'Sour'
```

>The wrapper color in the trash compartment that is most common.**
``` SQL
SELECT TOP 1 Candies.Color, COUNT(Purchases.CandyID) AS AmountPurchased
FROM Purchases
INNER JOIN Candies ON Purchases.CandyID = Candies.CandyID
GROUP BY Candies.Color
ORDER BY AmountPurchased DESC
```

>The amount of candies on each shelf of the vending machine.
``` SQL
SELECT Shelves.ShelfName, COUNT(Inventory.ShelfID) AS CandyAmount
FROM Inventory
INNER JOIN Shelves ON Inventory.ShelfID = Shelves.ShelfID
GROUP BY Shelves.ShelfName
ORDER BY Shelves.ShelfName DESC
```