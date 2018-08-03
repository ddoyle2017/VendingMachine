using NUnit.Framework;
using VendingMachine;
using System.Collections.Generic;

namespace Tests
{
    public class ShelfTests
    {
        private Shelf testShelf;
        private Candy twixBar;
        private Candy snickers;
        private Candy almondJoy;

        [SetUp]
        public void Setup()
        {
            testShelf = new Shelf();
            twixBar = new Candy("Twix", 1.99m, "Sweet", "Gold");
            snickers = new Candy("Snickers", 0.99m, "Salty", "Brown");
            almondJoy = new Candy("Almond Joy", 5.89m, "Sweet", "Blue");
        }

        [Test]
        public void AddNewItem_GivenItemAndAmount_AddsNewQueueToList()
        {
            int currentItemCount = testShelf.items.Count;

            testShelf.AddNewItem(twixBar, 10);
            Assert.That(currentItemCount, Is.LessThan(testShelf.items.Count));
        }

        [Test]
        public void GetItem_GivenValidItemName_ReturnsItem()
        {
            testShelf.AddNewItem(twixBar, 10);
            testShelf.AddNewItem(snickers, 20);

            int previousItemCount = testShelf.items[twixBar.name].Count;
            IProduct results = testShelf.GetItem(twixBar.name);
            int currentItemCount = testShelf.items[twixBar.name].Count;

            Assert.That(results, Is.EqualTo(twixBar));
            Assert.That(previousItemCount, Is.GreaterThan(currentItemCount));
        }

        [Test]
        public void GetItem_GivenInvalidItemName_ReturnsNull()
        {
            testShelf.AddNewItem(twixBar, 10);
            testShelf.AddNewItem(snickers, 20);

            IProduct results = testShelf.GetItem("M&M's");
            Assert.That(results, Is.EqualTo(null));
        }

        [Test]
        public void RestockItem_GivenExistingItem_ReturnsTrue()
        {
            testShelf.AddNewItem(twixBar, 3);

            bool result = testShelf.RestockItem(twixBar, 5);
            Assert.That(result, Is.EqualTo(true));

            Queue<IProduct> itemChoice;
            testShelf.items.TryGetValue(twixBar.name, out itemChoice);
            Assert.That(itemChoice.Count, Is.EqualTo(8));
        }

        [Test]
        public void RestockItem_GivenNonexistantItem_ReturnsFalse()
        {
            bool result = testShelf.RestockItem(almondJoy, 5);
            Assert.That(result, Is.EqualTo(false));

            Queue<IProduct> itemChoice;
            Assert.That(testShelf.items.TryGetValue(almondJoy.name, out itemChoice), Is.EqualTo(false));
        }

        [Test]
        public void GetItemStock_GivenValidItemName_ReturnsItemCount()
        {
            testShelf.AddNewItem(twixBar, 10);
            Assert.That(testShelf.GetItemStock(twixBar.name), Is.EqualTo(10));
        }

        [Test]
        public void GetItemStock_GivenInvalidItemName_ReturnsZero()
        {
            testShelf.AddNewItem(twixBar, 10);
            Assert.That(testShelf.GetItemStock(almondJoy.name), Is.EqualTo(0));
        }
    }
}