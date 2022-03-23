using System;
using System.IO;
using Ladeskab;
using NUnit.Framework;

namespace Ladeskab_Classes.Unit.Test
{
    [TestFixture]
    public class TestDisplaySimulator
    {
        private DisplaySimulator uut;
        

        [SetUp]
        public void Setup()
        {
            uut = new DisplaySimulator();
        }

        [Test]
        public void Print_PrintCalled_WriteToConsole()
        {

            

            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            // Act
            uut.Print("Charging has started");

            // Assert
            var output = stringWriter.ToString();
            Assert.That(output, Is.EqualTo("Charging has started\r\n"));
        }
    }
}
