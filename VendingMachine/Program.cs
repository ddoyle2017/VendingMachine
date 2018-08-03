using System;
using System.Collections.Generic;

namespace VendingMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            // init sample data here
            Candy testCandyA = new Candy("Peanut M&Ms", 1.99m, "Sweet", "Yellow");
            Candy testCandyB = new Candy("Warheads", 0.89m, "Sour", "Green");
            Candy testCandyC = new Candy("Red Hots", 1.89m, "Spicy", "Red");
            Dictionary<string, Candy> products = new Dictionary<string, Candy>()
            {
                {testCandyA.name, testCandyA},
                {testCandyB.name, testCandyB},
                {testCandyC.name, testCandyC}
            };

            VendingMachine vendingMachine = new VendingMachine(products);
            
            bool loop = true;
            while (loop)
            {
                switch (vendingMachine.currentState)
                {
                    case State.Active:
                        vendingMachine.DisplayMenu();
                        Console.Write("Please select an item: ");
                        vendingMachine.PickCandy(Console.ReadLine());
                        break;
                    case State.Processing:
                        Console.Write("Please enter payment: ");
                        vendingMachine.BeginTransaction(Convert.ToDecimal(Console.ReadLine()));
                        break;
                    case State.Vending:
                        Console.WriteLine("Thank you for your purchase");
                        Console.WriteLine("... Dispensing item ...");
                        vendingMachine.VendItem();
                        break;
                }
            }
        }
    }
}
