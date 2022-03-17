using System;
using System.IO;
using Ladeskab;
using NSubstitute;
using NUnit.Framework;
using Subject;
using UsbSimulator;

namespace Ladeskab_Classes.Unit.Test
{
    [TestFixture]
    public class TestChargeControl
    {
        private ChargeControl uut;
        private ICharger charger; 

        [SetUp]
        public void Setup()
        {
            charger = Substitute.For<ICharger>();
            uut = new ChargeControl(charger);
        }

        [TestCase(25)]
        [TestCase(400)]
        [TestCase(0)]
        public void CurrentChanged_DifferentArguments_CurrentIsCorrect(double newCurrent)
        {
            charger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() { Current = newCurrent });
            Assert.That(uut.CurrentValue, Is.EqualTo(newCurrent));
        }

//        [Test]
//        public void StartCharge_StartChargeOnSim_StartChargeCalledOnSim()
//        {



//            var stringWriter = new StringWriter();
//            Console.SetOut(stringWriter);

//            // Act
//            uut.Print("Charging has started \r\n");

//            // Assert
//            var output = stringWriter.ToString();
//            Assert.That(output, Is.EqualTo("Charging has started \r\n"));
//        }
    }
}