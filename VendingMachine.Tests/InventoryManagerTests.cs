using NUnit.Framework;
using VendingMachine;
using System.Collections.Generic;

namespace Tests
{
    public class InventoryManagerTests
    {
        private InventoryManager inventory;
        private Candy candyA;
        private Candy candyB;

        [SetUp]
        public void Setup()
        {
            candyA = new Candy("Hershey", 2.99m, "Sweet", "Blue");
            candyB = new Candy("Twix", 2.99m, "Sweet", "Blue"); 

            Dictionary<string, Candy> products = new Dictionary<string, Candy>()
            {
                {candyA.name, candyA},
                {candyB.name, candyB}
            };
            inventory = new InventoryManager(products);
            inventory.StockCandy(candyA, 2);
            inventory.StockCandy(candyB, 1);
        }

        [Test]
        public void IsCandyInStock_StockedCandy_ReturnsTrue()
        {
            Assert.That(inventory.IsCandyInStock(candyA.name), Is.EqualTo(true));
        }

        [Test]
        public void IsCandyInStock_OutOfStockCandy_ReturnsFalse()
        {
            inventory.DispenseCandy(candyB.name);
            Assert.That(inventory.IsCandyInStock(candyB.name), Is.EqualTo(false));
        }

        [Test]
        public void IsCandyInStock_InvalidCandy_ReturnsFalse()
        {
            Assert.That(inventory.IsCandyInStock("some random name"), Is.EqualTo(false));
        }

        [Test]
        public void DispenseCandy_ValidCandy_ReturnsTrue()
        {
            Assert.That(inventory.DispenseCandy(candyA.name), Is.EqualTo(true));
        }

        [Test]
        public void DispenseCandy_InvalidCandy_ReturnsFalse()
        {
            Assert.That(inventory.DispenseCandy("random name"), Is.EqualTo(false));
        }

        [Test]
        public void StockCandy_RestockCurrentCandy_AddsCandiesToQueue()
        {
            int previousItemStock = inventory.shelves["Sweet"].items[candyA.name].Count;
            inventory.StockCandy(candyA, 10);
            int currentItemStock = inventory.shelves["Sweet"].items[candyA.name].Count;
            Assert.That(currentItemStock, Is.GreaterThan(previousItemStock));
        }

        [Test]
        public void StockCandy_AddNewCandy_CreatesNewQueue()
        {
            Candy candyC = new Candy("Chocolate bar", 3.99m, "Sweet", "Black");
            int currentShelfSize = inventory.shelves["Sweet"].items.Count;
            inventory.StockCandy(candyC, 5);
            Assert.That(inventory.shelves["Sweet"].items.Count, Is.GreaterThan(currentShelfSize));
        }

    }
}