using System;
using System.Collections.Generic;

namespace VendingMachine
{
    public enum State { Active, CheckingInventory, Processing, CheckingPayment, Vending };

    public class VendingMachine
    {
        public State currentState { get; set; }
        private InventoryManager inventory;
        private TransactionManager transactions;
        private string candyChoice;
        private IDictionary<string, Candy> products;

        public VendingMachine(IDictionary<string, Candy> products)
        {
            this.currentState = State.Active;
            this.inventory = new InventoryManager(products);
            this.transactions = new TransactionManager(products);
            this.products = products;
            this.candyChoice = String.Empty;

            // This is used mainly for testing. A production version of this app would read inventory data from another source
            // like a database or resource file, and then stock the items accordingly.
            foreach (KeyValuePair<string, Candy> c in products)
            {
                inventory.StockCandy(c.Value, 10);
            }
        }

        public void PickCandy(string candyName)
        {
            if (currentState != State.Active)
            {
                Console.WriteLine("Vending Machine is not active.");
                return;
            }

            try
            {
                this.currentState = State.CheckingInventory;
                if (inventory.IsCandyInStock(candyName))
                {
                    this.candyChoice = candyName; 
                    AcceptChoice();
                }
                else
                {
                    RejectChoice();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return;
            }
        }

        public void BeginTransaction(decimal payment)
        {
            if (currentState != State.Processing)
            {
                Console.WriteLine("No candy was picked or choice is invalid");
                return;
            }

            try
            {
                this.currentState = State.CheckingPayment;
                if (transactions.AttemptPayment(candyChoice, payment))
                {
                    AcceptPayment();
                }
                else
                {
                    RejectPayment();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return;
            }
        }

        public void VendItem()
        {
            if (currentState != State.Vending)
            {
                Console.WriteLine("Payment invalidated.");
                return;
            }

            try
            {
                inventory.DispenseCandy(candyChoice);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return;
            }

            candyChoice = String.Empty;
            this.currentState = State.Active;
        }

        public void DisplayMenu()
        {
            Console.WriteLine("\nAvailable Items:");
            foreach (KeyValuePair<string, Candy> c in products)
            {
                Console.WriteLine(c.Key + " " + c.Value.price);
            }
            Console.WriteLine();
        }

        private void AcceptChoice()
        {
            if (currentState != State.CheckingInventory) return;
            Console.WriteLine("You have chosen: " + this.candyChoice);
            this.currentState = State.Processing;
        }

        private void RejectChoice()
        {
            if (currentState != State.CheckingInventory) return;
            Console.WriteLine("Item not in stock!");
            this.currentState = State.Active;
        }

        private void AcceptPayment()
        {
            if (currentState != State.CheckingPayment) return;
            Console.WriteLine("Payment accepted");
            this.currentState = State.Vending;
        }

        private void RejectPayment()
        {
            if (currentState != State.CheckingPayment) return;
            Console.WriteLine("Insufficient Funds");
            this.currentState = State.Processing;
        }
    }
}