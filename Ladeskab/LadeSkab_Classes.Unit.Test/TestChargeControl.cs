using System;
using System.IO;
using Ladeskab;
using NSubstitute;
using NUnit.Framework;


namespace Ladeskab_Classes.Unit.Test
{
    [TestFixture]
    public class TestChargeControl
    {
        private ChargeControl _uut;
        private ICharger _charger;
        private IDisplay _display;

            [SetUp]
        public void Setup()
        {
            _charger = Substitute.For<ICharger>();
            _display = Substitute.For<IDisplay>();
            _uut = new ChargeControl(_charger, _display);
        }

        //Udkommenteret fordi den resulterede i Stack Overflow.

       [TestCase(25)]
       [TestCase(400)]
       [TestCase(0)]
        public void CurrentChanged_DifferentArguments_CurrentIsCorrect(double newCurrent)
        {
            _charger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() { Current = newCurrent });
            Assert.That(_uut.CurrentValue, Is.EqualTo(newCurrent));
        }

        [Test]
        public void StartCharge_StartChargeOnController_StartChargeCalledOnSim()
        {
            // Act
            _uut.StartCharge();

            // Assert
            _charger.Received(1).StartCharge();
        }
    }
}