using NUnit.Framework;
using VendingMachine;

namespace Tests
{
    public class TrashCompartmentTests
    {
        private TrashCompartment trash;

        [SetUp]
        public void SetUp()
        {
            trash = new TrashCompartment();
        }

        [Test]
        public void AddWrapper_IncrementsCount()
        {
            int currentCount = trash.getCount();
            trash.addWrapper();
            Assert.That(trash.getCount(), Is.GreaterThan(currentCount));
        }
    }
}
