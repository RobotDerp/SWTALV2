﻿using System;
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
        public void RFID_LadeskabStateAvailableChargerConnected()
        {
            
            _door.LockDoor();
            _charger.StartCharge();
        }
        
        [Test]
        public void test1()
        {
            Assert.That(true, Is.EqualTo(true));
        }




        [Test]
        public void HandleDoorEvent_StateAvailableDoorEvent0_()
        {
            //arrange
            _state = LadeskabState.Available;
            _door.DoorStateEvent += Raise.EventWith(new DoorEventArgs() { DoorState = 0 });

            _uut.HandleDoorEvent(_door,DoorEventArgs);
            Assert.That(true, Is.EqualTo(true));
        }

        [Test]
        public void HandleDoorEvent_StateAvailableDoorEvent1_()
        {
            //arrange
            _state = LadeskabState.Available;

            Assert.That(true, Is.EqualTo(true));
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
