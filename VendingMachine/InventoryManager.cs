using System;
using System.Collections.Generic;

namespace VendingMachine
{
    public class InventoryManager
    {
        public IDictionary<string, Candy> products { get; set; }
        public IDictionary<string, Shelf> shelves { get; set; }
        private TrashCompartment trash;

        public InventoryManager(IDictionary<string, Candy> products)
        {
            this.shelves = new Dictionary<string, Shelf>()
            {
                {"Sweet", new Shelf()},
                {"Sour", new Shelf()},
                {"Salty", new Shelf()},
                {"Spicy", new Shelf()}
            };
            this.products = products;
            this.trash = new TrashCompartment();
        }

        public bool IsCandyInStock(string candyName)
        {
            try
            {
                Candy candyChoice;
                if (!products.TryGetValue(candyName, out candyChoice))
                {
                    return false;
                }
                if (shelves[candyChoice.flavor].GetItemStock(candyName) <= 0 )
                {
                    return false;
                } 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
            return true;
        }

        public bool DispenseCandy(string candyName)
        {
            try
            {
                Candy candyChoice;
                if (!products.TryGetValue(candyName, out candyChoice))
                {
                    return false;
                }
                shelves[candyChoice.flavor].GetItem(candyName).Unwrap();
                trash.AddWrapper();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
            return true;
        }

        public void StockCandy(Candy candy, int amount)
        {
            try
            {
                if (shelves[candy.flavor].items.ContainsKey(candy.name))
                {
                    shelves[candy.flavor].RestockItem(candy, amount);
                }
                else
                {
                    shelves[candy.flavor].AddNewItem(candy, amount);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }
    }
}