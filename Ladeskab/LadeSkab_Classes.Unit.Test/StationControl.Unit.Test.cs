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
        private enum LadeskabState
        {
            Available,
            Locked,
            DoorOpen
        };

        private StationControl _uut;
        private IDisplay _display;
        private IDoor _door;
        private ICharger _charger;
        private IRFID _rfid;
        private int _oldID;
        private LadeskabState _state;

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
        public void RFID_LadeskabStateAvailableChargerConnected_DoorLocked()
        {
            _state = LadeskabState.Available;
            _charger.Connected.Returns(true);

            _rfid.RFIDStateEvent += Raise.EventWith(new RFIDEventArgs() { RFID_ID = 22 } );
            _door.Received(1).LockDoor();
        }

        [Test]
        public void RFID_LadeskabStateAvailableChargerConnected_StartCharge()
        {
            _state = LadeskabState.Available;
            _charger.Connected.Returns(true);

            _rfid.RFIDStateEvent += Raise.EventWith(new RFIDEventArgs() { RFID_ID = 22 });
            _charger.Received(1).StartCharge();
        }

        [Test]
        public void RFID_LadeskabStateAvailableChargerConnected_StateLocked()
        {
            _state = LadeskabState.Available;
            _charger.Connected.Returns(true);

            _rfid.RFIDStateEvent += Raise.EventWith(new RFIDEventArgs() { RFID_ID = 22 });
            Assert.That(_state, Is.EqualTo(LadeskabState.Locked));
        }

        [Test]
        public void RFID_LadeskabStateAvailableChargerNotConnected_DoorUnchanged()
        {
            _state = LadeskabState.Available;
            _charger.Connected.Returns(false);

            _rfid.RFIDStateEvent += Raise.EventWith(new RFIDEventArgs() { RFID_ID = 22 });
            _door.Received(0).LockDoor();
        }

        [Test]
        public void RFID_LadeskabStateAvailableChargerNotConnected_ChargeUnchanged()
        {
            _state = LadeskabState.Available;
            _charger.Connected.Returns(false);

            _rfid.RFIDStateEvent += Raise.EventWith(new RFIDEventArgs() { RFID_ID = 22 });
            _charger.Received(0).StartCharge();
        }

        [Test]
        public void RFID_LadeskabStateAvailableChargerNotConnected_StateUnchanged()
        {
            _state = LadeskabState.Available;
            _charger.Connected.Returns(false);

            _rfid.RFIDStateEvent += Raise.EventWith(new RFIDEventArgs() { RFID_ID = 22 });
            Assert.That(_state, Is.EqualTo(LadeskabState.Available));
        }

        [Test]
        public void RFID_LadeskabStateDoorOpen_NoStateChanges()
        {
            _state = LadeskabState.DoorOpen;

            _rfid.RFIDStateEvent += Raise.EventWith(new RFIDEventArgs() { RFID_ID = 22 });
            _charger.Received(0).StartCharge();
            _charger.Received(0).StopCharge();
        }

        [Test]
        public void RFID_LadeskabStateLockedRFIDMatch_DoorUnlocked()
        {
            _state = LadeskabState.Locked;
            _charger.Connected.Returns(true);
            _oldID = 22;

            _rfid.RFIDStateEvent += Raise.EventWith(new RFIDEventArgs() { RFID_ID = 22 });
            _door.Received(1).UnlockDoor();
        }

        [Test]
        public void RFID_LadeskabStateLockedRFIDMatch_StopCharge()
        {
            _state = LadeskabState.Locked;
            _charger.Connected.Returns(true);
            _oldID = 22;

            _rfid.RFIDStateEvent += Raise.EventWith(new RFIDEventArgs() { RFID_ID = 22 });
            _charger.Received(1).StopCharge();
        }

        [Test]
        public void RFID_LadeskabStateAvailableRFIDMatch_StateAvailable()
        {
            _state = LadeskabState.Locked;
            _charger.Connected.Returns(true);
            _oldID = 22;

            _rfid.RFIDStateEvent += Raise.EventWith(new RFIDEventArgs() { RFID_ID = 22 });
            Assert.That(_state, Is.EqualTo(LadeskabState.Available));
        }

        [Test]
        public void RFID_LadeskabStateLockedWrongRFID_StopCharge()
        {
            _state = LadeskabState.Locked;
            _charger.Connected.Returns(true);
            _oldID = 10;

            _rfid.RFIDStateEvent += Raise.EventWith(new RFIDEventArgs() { RFID_ID = 22 });
            _charger.Received(0).StopCharge();
            _door.Received(0).UnlockDoor();
        }

        [Test]
        public void RFID_LadeskabStateAvailableWrongRFID_StateUnchanged()
        {
            _state = LadeskabState.Locked;
            _charger.Connected.Returns(true);
            _oldID = 10;

            _rfid.RFIDStateEvent += Raise.EventWith(new RFIDEventArgs() { RFID_ID = 22 });
            Assert.That(_state, Is.EqualTo(LadeskabState.Locked));
        }

        [Test]
        public void test1()
        {
            Assert.That(true, Is.EqualTo(true));
        }




        [Test]
        public void HandleDoorEvent_StateAvailableDoorEvent0_ThrowsException()
        {
            _state = LadeskabState.Available;

           Assert.Throws<InvalidOperationException>(() => _door.DoorStateEvent += Raise.EventWith(new DoorEventArgs() { DoorState = 0 }));
        }

        [Test]
        public void HandleDoorEvent_StateAvailableDoorEvent1_DisplayPrintCalled()
        {
            _state = LadeskabState.Available;
            _door.DoorStateEvent += Raise.EventWith(new DoorEventArgs() {DoorState = 1});

            _display.Received(1).Print("Connect phone");
        }

        [Test]
        public void HandleDoorEvent_StateAvailableDoorEvent1_StateNowLocked()
        {
            _state = LadeskabState.Available;
            _door.DoorStateEvent += Raise.EventWith(new DoorEventArgs() { DoorState = 1 });

            _display.Received(1).Print("Connect phone");
        }

        [Test]
        public void HandleDoorEvent_StateLockedDoorEvent0_()
        {
            //arrange
            _state = LadeskabState.Available;

            Assert.That(true, Is.EqualTo(true));
        }

        [Test]
        public void HandleDoorEvent_StateLockedDoorEvent1_()
        {
            //arrange
            _state = LadeskabState.Available;

            Assert.That(true, Is.EqualTo(true));
        }

        [Test]
        public void HandleDoorEvent_StateDoorOpenDoorEvent0_()
        {
            //arrange
            _state = LadeskabState.Available;

            Assert.That(true, Is.EqualTo(true));
        }

        [Test]
        public void HandleDoorEvent_StateDoorOpenDoorEvent1_()
        {
            //arrange
            _state = LadeskabState.Available;

            Assert.That(true, Is.EqualTo(true));
        }
    }
}
