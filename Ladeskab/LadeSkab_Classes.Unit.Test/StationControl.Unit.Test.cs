using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using Ladeskab;
using NSubstitute;
using NUnit.Framework;

namespace Ladeskab_Classes.Unit.Test
{
    [TestFixture]
    public class StationControlTests
    {
        private StationControl _uut;
        private IDisplay _display;
        private IDoor _door;
        private ICharger _charger;
        private IRFID _rfid;

        [SetUp]
        public void SetUp()
        {
            _display = Substitute.For<IDisplay>();
            _door = Substitute.For<IDoor>();
            _charger = Substitute.For<ICharger>();
            _rfid = Substitute.For<IRFID>();
            _uut = new StationControl(_door,_display,_charger,_rfid);
        }

        [Test]
        public void ctor_StationControlIsCreated()
        {
            Assert.IsNotNull(_uut);
        }

        [Test]
        public void ctor_DisplayObjectIsInjectedIntoStationControl()
        {

        }
        
        [Test]
        public void test1()
        {
            Assert.That(true, Is.EqualTo(true));
        }




        [Test]
        public void HandleDoorEvent()
        {
            //arrange
            _doorEventArgs.DoorState = 1;

            //
            Assert.That(true, Is.EqualTo(true));
        }
    }
}
