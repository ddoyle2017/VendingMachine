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

        /// <summary>
        /// Adds given product to the shelf.
        /// </summary>
        /// <param name="product">The product to be added.</param>
        /// <param name="amount">The amount to be added.</param>
        public void AddNewItem(IProduct product, int amount)
        {
            Queue<IProduct> newItems = new Queue<IProduct>();
            for (int i = 0; i < amount; i++)
            {
                newItems.Enqueue(product);
            }
            items.TryAdd(product.name, newItems);
        }

        /// <summary>
        /// Searches for and retrieves the specified product from the shelf.
        /// </summary>
        /// <param name="productName">The name of the desired product</param>
        /// <returns>The chosen product.</returns>
        public IProduct GetItem(string productName)
        {
            Queue<IProduct> candy;
            if (items.TryGetValue(productName, out candy))
            {
                return candy.Dequeue();
            }
            return null;
        }

        /// <summary>
        /// Restocks the given product, if it exists, by the specified amount.
        /// </summary>
        /// <param name="product">Product to be restocked.</param>
        /// <param name="amount">The amount to add.</param>
        /// <returns>True if the restock was successful, false if it fails.</returns>
        public bool RestockItem(Candy product, int amount)
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

        /// <summary>
        /// Search for the given product name and returns the amount of the item on the shelf.
        /// </summary>
        /// <param name="productName"></param>
        /// <returns>The amount of the given product, 0 if not found.</returns>
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