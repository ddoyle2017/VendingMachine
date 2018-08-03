using NUnit.Framework;
using VendingMachine;
using System.Collections.Generic;

namespace Tests
{
    public class VendingMachineTests
    {
        private VendingMachine.VendingMachine vendingMachine;
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
            vendingMachine = new VendingMachine.VendingMachine(products);
        }

        [Test]
        public void PickCandy_ValidCandy_ChangesStateToProcessing()
        {
            Assert.That(vendingMachine.currentState, Is.EqualTo(State.Active));
            vendingMachine.PickCandy(candyA.name);
            Assert.That(vendingMachine.currentState, Is.EqualTo(State.Processing));
        }

        [Test]
        public void BeginTransaction_ValidPayment_ChangesStateToVending()
        {
            Assert.That(vendingMachine.currentState, Is.EqualTo(State.Active));
            vendingMachine.PickCandy(candyA.name);
            vendingMachine.BeginTransaction(candyA.price);
            Assert.That(vendingMachine.currentState, Is.EqualTo(State.Vending));
        }

        [Test]
        public void VendItem_ChangesStateToVending()
        {
            Assert.That(vendingMachine.currentState, Is.EqualTo(State.Active));
            vendingMachine.PickCandy(candyA.name);
            vendingMachine.BeginTransaction(candyA.price);
            vendingMachine.VendItem();
            Assert.That(vendingMachine.currentState, Is.EqualTo(State.Active));
        }
    }
}
