using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab
{
    public class Door : IDoor, ISubject
    {
        public event EventHandler<DoorEventArgs> DoorStateEvent;

        private int _locked;

        public void OnDoorClose()
        {
            if (_locked == 0)
            {
                OnDoorChanged(new OnDoorChanged { DoorState = 0 });
            }
        }

        public void OnDoor()
        {
            if (_locked == 0)
            {
                OnDoorChanged(new OnDoorChanged { DoorState = 1 });
            }
        }

        public void UnlockDoor()
        {
            _locked = 1;
        }

        public void LockDoor()
        {
            _locked = 0;
        }

        protected virtual void OnDoorChanged(DoorEventArgs e)
        {
            DoorStateEvent?.Invoke(this, e);
        }
    }
}
