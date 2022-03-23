using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using Ladeskab;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;

namespace Ladeskab
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
        public void RfidDetected_LadeskabStateAvailableChargerConnected_DoorLocked()
        {
            _uut._state = StationControl.LadeskabState.Available;
            _charger.Connected.Returns(true);

            _rfid.RFIDStateEvent += Raise.EventWith(new RFIDEventArgs() { RFID_ID = 22 } );
            _door.Received(1).LockDoor();
        }

        [Test]
        public void RfidDetected_LadeskabStateAvailableChargerConnected_StartCharge()
        {
            _uut._state = StationControl.LadeskabState.Available;
            _charger.Connected.Returns(true);

            _rfid.RFIDStateEvent += Raise.EventWith(new RFIDEventArgs() { RFID_ID = 22 });
            _charger.Received(1).StartCharge();
        }

        [Test]
        public void RfidDetected_LadeskabStateAvailableChargerConnected_StateLocked()
        {
            _uut._state = StationControl.LadeskabState.Available;
            _charger.Connected.Returns(true);

            _rfid.RFIDStateEvent += Raise.EventWith(new RFIDEventArgs() { RFID_ID = 22 });
            Assert.That(_uut._state, Is.EqualTo(StationControl.LadeskabState.Locked));
        }

        [Test]
        public void RfidDetected_LadeskabStateAvailableChargerNotConnected_DoorUnchanged()
        {
            _uut._state = StationControl.LadeskabState.Available;
            _charger.Connected.Returns(false);

            _rfid.RFIDStateEvent += Raise.EventWith(new RFIDEventArgs() { RFID_ID = 22 });
            _door.Received(0).LockDoor();
        }

        [Test]
        public void RfidDetected_LadeskabStateAvailableChargerNotConnected_ChargeUnchanged()
        {
            _uut._state = StationControl.LadeskabState.Available;
            _charger.Connected.Returns(false);

            _rfid.RFIDStateEvent += Raise.EventWith(new RFIDEventArgs() { RFID_ID = 22 });
            _charger.Received(0).StartCharge();
        }

        [Test]
        public void RfidDetected_LadeskabStateAvailableChargerNotConnected_StateUnchanged()
        {
            _uut._state = StationControl.LadeskabState.Available;
            _charger.Connected.Returns(false);

            _rfid.RFIDStateEvent += Raise.EventWith(new RFIDEventArgs() { RFID_ID = 22 });
            Assert.That(_uut._state, Is.EqualTo(StationControl.LadeskabState.Available));
        }

        [Test]
        public void RfidDetected_LadeskabStateDoorOpen_StartChargeNotCalled()
        {
            _uut._state = StationControl.LadeskabState.DoorOpen;

            _rfid.RFIDStateEvent += Raise.EventWith(new RFIDEventArgs() { RFID_ID = 22 }); ;
            _charger.DidNotReceive().StartCharge();
        }

        [Test]
        public void RfidDetected_LadeskabStateDoorOpen_StopChargeNotCalled()
        {
            _uut._state = StationControl.LadeskabState.DoorOpen;

            _rfid.RFIDStateEvent += Raise.EventWith(new RFIDEventArgs() { RFID_ID = 22 }); ;
            _charger.DidNotReceive().StopCharge();
        }

        [Test]
        public void RfidDetected_LadeskabStateLockedRFIDMatch_DoorUnlocked()
        {
            _uut._state = StationControl.LadeskabState.Locked;
            _charger.Connected.Returns(true);
            _uut._oldId = 22;

            _rfid.RFIDStateEvent += Raise.EventWith(new RFIDEventArgs() { RFID_ID = 22 });
            _door.Received(1).UnlockDoor();
        }

        [Test]
        public void RfidDetected_LadeskabStateLockedRFIDMatch_StopCharge()
        {
            _uut._state = StationControl.LadeskabState.Locked;
            _charger.Connected.Returns(true);
            _uut._oldId = 22;

            _rfid.RFIDStateEvent += Raise.EventWith(new RFIDEventArgs() { RFID_ID = 22 });
            _charger.Received(1).StopCharge();
        }

        [Test]
        public void RfidDetected_LadeskabStateAvailableRFIDMatch_StateAvailable()
        {
            _uut._state = StationControl.LadeskabState.Locked;
            _charger.Connected.Returns(true);
            _uut._oldId = 22;

            _rfid.RFIDStateEvent += Raise.EventWith(new RFIDEventArgs() { RFID_ID = 22 });
            Assert.That(_uut._state, Is.EqualTo(StationControl.LadeskabState.Available));
        }

        [Test]
        public void RfidDetected_LadeskabStateLockedWrongRFID_StopCharge()
        {
            _uut._state = StationControl.LadeskabState.Locked;
            _charger.Connected.Returns(true);
            _uut._oldId = 10;

            _rfid.RFIDStateEvent += Raise.EventWith(new RFIDEventArgs() { RFID_ID = 22 });
            _charger.Received(0).StopCharge();
            _door.Received(0).UnlockDoor();
        }

        [Test]
        public void RfidDetected_LadeskabStateAvailableWrongRFID_StateUnchanged()
        {
            _uut._state = StationControl.LadeskabState.Locked;
            _charger.Connected.Returns(true);
            _uut._oldId = 10;

            _rfid.RFIDStateEvent += Raise.EventWith(new RFIDEventArgs() { RFID_ID = 22 });
            Assert.That(_uut._state, Is.EqualTo(StationControl.LadeskabState.Locked));
        }

        [Test]
        public void HandleDoorEvent_StateAvailableDoorEvent0_ThrowsException()
        {
            _uut._state = StationControl.LadeskabState.Available;

           Assert.Throws<InvalidOperationException>(() => _door.DoorStateEvent += Raise.EventWith(new DoorEventArgs() { DoorState = 0 }));
        }

        [Test]
        public void HandleDoorEvent_StateAvailableDoorEvent1_DisplayPrintCalled()
        {
            _uut._state = StationControl.LadeskabState.Available;
            _door.DoorStateEvent += Raise.EventWith(new DoorEventArgs() {DoorState = 1});

            _display.Received(1).Print("Connect phone");
        }

        [Test]
        public void HandleDoorEvent_StateAvailableDoorEvent1_StateNowLocked()
        {
            _uut._state = StationControl.LadeskabState.Available;
            _door.DoorStateEvent += Raise.EventWith(new DoorEventArgs() { DoorState = 1 });

            Assert.That(_uut._state, Is.EqualTo(StationControl.LadeskabState.DoorOpen));
        }

        [Test]
        public void HandleDoorEvent_StateLockedDoorEvent0_ExceptionThrown()
        {
            //arrange
            _uut._state = StationControl.LadeskabState.Locked;

            Assert.Throws<InvalidOperationException>(() => _door.DoorStateEvent += Raise.EventWith(new DoorEventArgs() { DoorState = 0 }));
        }

        [Test]
        public void HandleDoorEvent_StateLockedDoorEvent1_ExceptionThrown()
        {
            //arrange
            _uut._state = StationControl.LadeskabState.Locked;

            Assert.Throws<InvalidOperationException>(() => _door.DoorStateEvent += Raise.EventWith(new DoorEventArgs() { DoorState = 1 }));
        }


        [Test]
        public void HandleDoorEvent_StateDoorOpenDoorEvent1_DisplayPrintCalled()
        {
            _uut._state = StationControl.LadeskabState.DoorOpen;
            _door.DoorStateEvent += Raise.EventWith(new DoorEventArgs() { DoorState = 0 });

            _display.Received(1).Print("Load RFID");
        }

        [Test]
        public void HandleDoorEvent_StateDoorOpenDoorEvent1_StateNowLocked()
        {
            _uut._state = StationControl.LadeskabState.DoorOpen;
            _door.DoorStateEvent += Raise.EventWith(new DoorEventArgs() { DoorState = 0 });

            Assert.That(_uut._state, Is.EqualTo(StationControl.LadeskabState.Available));
        }

        [Test]
        public void HandleDoorEvent_StateOpenDoorDoorEvent1_ExceptionThrown()
        {
            //arrange
            _uut._state = StationControl.LadeskabState.DoorOpen;

            Assert.Throws<InvalidOperationException>(() => _door.DoorStateEvent += Raise.EventWith(new DoorEventArgs() { DoorState = 1 }));
        }
    }
}
