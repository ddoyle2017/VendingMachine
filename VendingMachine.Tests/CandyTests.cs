using NUnit.Framework;
using VendingMachine;
using System;

namespace Tests
{
    public class CandyTests
    {
        private Candy testCandy;

        [SetUp]
        public void Setup()
        {
            testCandy = new Candy("M&M's", 2.99m, "Sweet", "Blue");
        }

        [Test]
        public void GetName_ReturnsCorrectValue()
        {
            Assert.That(testCandy.name, Is.EqualTo("M&M's"));
        }

        [Test]
        public void GetPrice_ReturnsCorrectValue()
        {
            Assert.That(testCandy.price, Is.EqualTo(2.99m));
        }

        [Test]
        public void GetFlavor_ReturnsCorrectValue()
        {
            Assert.That(testCandy.flavor, Is.EqualTo("Sweet"));
        }

        [Test]
        public void Unwrap_WrappedCandy_ReturnsFalse()
        {
            Assert.That(testCandy.IsWrapped(), Is.EqualTo(true));
            testCandy.Unwrap();
            Assert.That(testCandy.IsWrapped(), Is.EqualTo(false));
        }
    }
}