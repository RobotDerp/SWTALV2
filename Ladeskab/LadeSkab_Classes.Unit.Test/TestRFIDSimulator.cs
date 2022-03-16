using Ladeskab;
using NUnit.Framework;

namespace LadeSkab_Classes.Unit.Test
{
    public class Tests
    {
        private RFIDSimulator uut;
        [SetUp]
        public void Setup()
        {
            uut = new RFIDSimulator();
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}