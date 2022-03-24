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

        [TestCase(250)]
        [TestCase(750)]
        [TestCase(3)]
        [TestCase(0)]
        [TestCase(Double.MinValue)]
        [TestCase(Double.MaxValue)]
        public void CurrentChanged_DifferentArguments_CurrentIsCorrect(double newCurrent)
        {
            _charger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() { Current = newCurrent });
            Assert.That(_uut.CurrentValue, Is.EqualTo(newCurrent));
        }

        [TestCase(250, "Phone is charging")]
        [TestCase(750, "Something has gone terribly wrong. Save yourself!")]
        [TestCase(3, "Phone is fully charged!")]
        [TestCase(Double.MaxValue, "Something has gone terribly wrong. Save yourself!")]
        public void HandleCurrentValueEvent_DifferentCurrents_CorrectMessageSentToDisplay(double newCurrent, string msg)
        {
            _charger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() { Current = newCurrent });
            _display.Received(1).Print(msg);
        }

        [TestCase(0)]
        [TestCase(Double.MinValue)]
        public void HandleCurrentValueEvent_CurrentIsZero_DisplaysPrintNotCalled(double newCurrent)
        {
            _charger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() { Current = newCurrent });
            _display.DidNotReceive().Print(null);
        }

        [Test]
        public void StartCharge_StartChargeOnController_StartChargeCalledOnSim()
        {
            // Act
            _uut.StartCharge();

            // Assert
            _charger.Received(1).StartCharge();
        }

        [Test]
        public void StopCharge_StopChargeOnController_StopChargeCalledOnSim()
        {
            // Act
            _uut.StopCharge();

            // Assert
            _charger.Received(1).StopCharge();
        }

        [TestCase(true)]
        [TestCase(false)]
        public void IsConnected_CallIsConnectedAndCheckReturn_ConnectedOnControllerSameOnSim(bool connectionSts)
        {
            // Assert stub
            _charger.Connected.Returns(connectionSts);

            // Act
            bool result = _uut.IsConnected();

            // Assert
            Assert.That(result, Is.EqualTo(connectionSts));
        }

    }
}