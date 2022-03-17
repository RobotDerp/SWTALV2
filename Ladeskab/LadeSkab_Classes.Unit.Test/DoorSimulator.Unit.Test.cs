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
        [SetUp]
        public void Setup()
        {
            _uut = new DoorSimulator();
            _uut.SimulateUnlocked();
        }

        [Test]
        public void ctor_DoorStateIsZero()
        {
            Assert.That(_uut.DoorState, Is.EqualTo(0));
        }

        [Test]
        public void OnDoorOpen_UnlockedClosedDoor_StateIsChanged()
        {
            _uut.OnDoorOpen();

            Assert.That(_uut.DoorState, Is.EqualTo(1));
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
            _uut.DoorState = 1;

            Assert.Throws<InvalidOperationException>( () => _uut.OnDoorOpen());
        }

        public void OnDoorClose_UnlockedClosedDoor_ExceptionInvalidOperationThrown()
        {
            Assert.Throws<InvalidOperationException>(() => _uut.OnDoorClose());
        }
        public void OnDoorClose_LockedClosedDoor_ExceptionInvalidOperationThrown()
        {
            _uut.SimulateLocked();
            Assert.Throws<InvalidOperationException>(() => _uut.OnDoorClose());
        }
        public void OnDoorClose_OpenDoor_DoorStateChangedTo0()
        {
            _uut.DoorState = 1;
            Assert.That(_uut.DoorState, Is.EqualTo(0));
        }
    }
}
