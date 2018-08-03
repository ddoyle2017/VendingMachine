using System.Collections.Generic;
using System.Collections;
using System;

namespace VendingMachine
{
    /// <summary>
    /// The Shelf class represents one shelf in a vending machine, with each product in the shelf having its own queue
    /// </summary>
    public class Shelf
    {
        public IDictionary<string, Queue<IProduct>> items;

        public Shelf()
        {
            this.items = new Dictionary<string, Queue<IProduct>>();
        }

        public void AddNewItem(IProduct product, int amount)
        {
            Queue<IProduct> newItems = new Queue<IProduct>();
            for (int i = 0; i < amount; i++)
            {
                newItems.Enqueue(product);
            }
            items.TryAdd(product.name, newItems);
        }

        public IProduct GetItem(string productName)
        {
            Queue<IProduct> candy;
            if (items.TryGetValue(productName, out candy))
            {
                return candy.Dequeue();
            }
            return null;
        }

        public bool RestockItem(IProduct product, int amount)
        {
            Queue<IProduct> result;
            if (items.TryGetValue(product.name, out result))
            {
                for (int i = 0; i < amount; i++)
                {
                    result.Enqueue(product);
                }
                return true;
            }
            return false;
        }

        public int GetItemStock(string productName)
        {
            Queue<IProduct> result;
            if (items.TryGetValue(productName, out result))
            {
                return result.Count;
            }
            return 0;
        }
    }
}