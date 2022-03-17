using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab;

namespace Ladeskab_Classes
{
    public class DoorSimulator : IDoor
    {
        public event EventHandler<DoorEventArgs> DoorStateEvent;

        //locked when _locked = 1
        private int _locked;


            public int DoorState { set; get; }

        //Doorstate = 0 når lukket, =1 når åben

        public void SimulateLocked()
        {
            _locked = 1;
        }

        public void SimulateUnlocked()
        {
            _locked = 0;
        }

        public void OnDoorClose()
        {
            if (_locked == 0 && DoorState != 0)
            {
                OnDoorChanged(new DoorEventArgs() { DoorState = 0 });
            }
            else
            {
                throw new InvalidOperationException("Cannot close an already closed door");
            }
        }

        public void OnDoorOpen()
        {
            throw new NotImplementedException();
        }

        public void OnDoor()
        {
            if (_locked == 0)
            {
                OnDoorChanged(new DoorEventArgs() { DoorState = 1 });
            }
            else
            {
                throw new InvalidOperationException("Cannot open an already open door");
            }
        }

        public void UnlockDoor()
        {
            if (_locked == 0 && DoorState == 0)
            {
                _locked = 1;
            }
        }

        public void LockDoor()
        {
            if (_locked == 1 && DoorState == 0)
            {
                _locked = 0;
            }
        }

        public void OnDoorChanged(DoorEventArgs e)
        {
            DoorStateEvent?.Invoke(this, e);
        }

    }
}
