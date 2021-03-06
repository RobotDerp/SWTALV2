using Ladeskab;
using NUnit.Framework;

namespace Ladeskab_Classes.Unit.Test
{
    [TestFixture]
    public class TestRFIDSimulator
    {
        private RFIDSimulator _uut;
        private RFIDEventArgs _receivedEventArgs;

        [SetUp]
        public void Setup()
        {
            _receivedEventArgs = null;

            _uut = new RFIDSimulator();
            _uut.RFIDDetected(5);

            _uut.RFIDStateEvent += (sender, args) =>
            {
                _receivedEventArgs = args;
            };
        }

        [Test]
        public void RFIDDetected_RFIDIdRead_EventFired()
        {
            _uut.RFIDDetected(5);
            Assert.That(_receivedEventArgs, Is.Not.Null);
        }

        [Test]
        public void RFIDDetected_RFIDIdRead_CorrectEventArgsReceived()
        {
            _uut.RFIDDetected(5);
            Assert.That(_receivedEventArgs.RFID_ID, Is.EqualTo(5));
        }
    }
}