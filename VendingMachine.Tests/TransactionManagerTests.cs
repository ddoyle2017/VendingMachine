using NUnit.Framework;
using VendingMachine;
using System.Collections.Generic;

namespace Tests
{
    public class TransactionManagerTests
    {
        private TransactionManager transactions;
        private Candy candyA;

        [SetUp]
        public void Setup()
        {
            candyA = new Candy("Hershey", 2.99m, "Sweet", "Blue");

            Dictionary<string, Candy> products = new Dictionary<string, Candy>()
            {
                {candyA.name, candyA},
            };
            transactions = new TransactionManager(products);
        }

        [Test]
        public void AttemptPayment_NegativeAmount_ReturnsFalse()
        {
            Assert.That(transactions.AttemptPayment(candyA.name, -1.00m), Is.EqualTo(false));
        }

        [Test]
        public void AttemptPayment_AmountIsZero_ReturnsFalse()
        {
            Assert.That(transactions.AttemptPayment(candyA.name, 0.00m), Is.EqualTo(false));
        }

        [Test]
        public void AttemptPayment_CandyNotFound_ReturnsFalse()
        {
            Assert.That(transactions.AttemptPayment("random name", 2.00m), Is.EqualTo(false));
        }

        [Test]
        public void AttemptPayment_InsufficientAmount_ReturnsFalse()
        {
            Assert.That(transactions.AttemptPayment(candyA.name, 1.00m), Is.EqualTo(false));
        }

        public void AttemptPayment_AmountEqualToPrice_ReturnsTrue()
        {
            Assert.That(transactions.AttemptPayment(candyA.name, candyA.price), Is.EqualTo(true));
        }

        public void AttemptPayment_AmountGreaterThanPrice_ReturnsTrue()
        {
            Assert.That(transactions.AttemptPayment(candyA.name, 5.00m), Is.EqualTo(true));
        }

        [Test]
        public void GetTotalCash_AfterNewPayment_ReturnsUpdatedAmount()
        {
            decimal currentAmount = transactions.GetTotalCash();
            transactions.AttemptPayment(candyA.name, candyA.price);

            Assert.That(transactions.GetTotalCash(), Is.EqualTo(candyA.price));
            Assert.That(transactions.GetTotalCash(), Is.GreaterThan(currentAmount));
        }
    }
}