using System;
using System.Threading;
using Ladeskab;
using LadeSkab_Classes;
using NUnit.Framework;

namespace Ladeskab_Classes.Unit.Test
{
    [TestFixture]
    public class TestDoorSimulator
    {
        private DoorSimulator _uut;
        private DoorEventArgs _doorEventArgs;

        [SetUp]
        public void Setup()
        {
            _uut = new DoorSimulator();
            _uut.SimulateUnlocked();

            _uut.DoorStateEvent += (sender, args) =>
            {
                _doorEventArgs = args;
            };

        }

        [Test]
        public void ctor_DoorStateIsZero()
        {
            Assert.That(_uut.LocalDoorState, Is.EqualTo(0));
        }

        [Test]
        public void OnDoorOpen_UnlockedClosedDoor_StateIsChanged()
        {
            _uut.OnDoorOpen();

            Assert.That(_uut.LocalDoorState, Is.EqualTo(1));
        }

        [Test]
        public void OnDoorOpen_UnlockedClosedDoor_EventIsSent()
        {
            _uut.OnDoorOpen();

            Assert.That(_doorEventArgs.DoorState, Is.EqualTo(1));
        }


        [Test]
        public void OnDoorOpen_LockedClosedDoor_ExceptionInvalidOperationThrown()
        {
            _uut.SimulateLocked();

            Assert.Throws<InvalidOperationException>(() => _uut.OnDoorOpen());
        }

        [Test]
        public void OnDoorOpen_OpenDoor_ExceptionInvalidOperationThrown()
        {
            _uut.LocalDoorState = 1;

            Assert.Throws<InvalidOperationException>( () => _uut.OnDoorOpen());
        }
        [Test]
        public void OnDoorClose_UnlockedClosedDoor_ExceptionInvalidOperationThrown()
        {
            Assert.Throws<InvalidOperationException>(() => _uut.OnDoorClose());
        }
        [Test]
        public void OnDoorClose_LockedClosedDoor_ExceptionInvalidOperationThrown()
        {
            _uut.SimulateLocked();
            Assert.Throws<InvalidOperationException>(() => _uut.OnDoorClose());
        }
        [Test]
        public void OnDoorClose_OpenDoor_DoorStateChangedTo0()
        {
            _uut.LocalDoorState = 1;
            _uut.OnDoorClose();
            Assert.That(_uut.LocalDoorState, Is.EqualTo(0));
        }
        [Test]
        public void OnDoorClose_OpenDoor_EventIsSentWithDoorstate0()
        {
            _uut.LocalDoorState = 1;
            _uut.OnDoorClose();
            Assert.That(_doorEventArgs.DoorState, Is.EqualTo(0));
        }

        [Test]
        public void LockDoor_LockedClosedDoor_ExceptionThrown()
        {
            _uut.SimulateLocked();
            Assert.Throws<InvalidOperationException>(() => _uut.LockDoor());
        }

        [Test]
        public void LockDoor_UnlockedClosedDoor_DoorIsLocked()
        {
            _uut.LockDoor();
            Assert.That(_uut.GetLocked(), Is.EqualTo(1));
        }

        [Test]
        public void LockDoor_OpenDoor_ExceptionThrown()
        {
            _uut.LocalDoorState = 1;
            Assert.Throws<InvalidOperationException>(() => _uut.LockDoor());
        }

        [Test]
        public void UnlockDoor_OpenDoor_ExceptionThrown()
        {
            _uut.LocalDoorState=1;
            Assert.Throws<InvalidOperationException>(() => _uut.UnlockDoor());
        }

        [Test]
        public void UnlockDoor_LockedClosedDoor_DoorIsUnlocked()
        {
            _uut.SimulateLocked();
            _uut.UnlockDoor();
            Assert.That(_uut.GetLocked(), Is.EqualTo(0));
        }

        [Test]
        public void UnlockDoor_UnlockedClosedDoor_DoorIsLocked()
        {
            Assert.Throws<InvalidOperationException>(() => _uut.UnlockDoor());
        }

        [Test]
        public void OnDoorChanged_NoEventObject_NullExceptionThrown()
        {
            _uut = null;
            Assert.Throws<NullReferenceException>(() => _uut.OnDoorChanged(_doorEventArgs));
        }
    }
}
