using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorSimulator
{
    public class DoorEventArgs : EventArgs
    {
        public int DoorState { set; get; }
    }

    public interface IDoor
    {
        event EventHandler<DoorEventArgs> DoorStateEvent;

        public void OnDoorClose();
        public void OnDoorOpen();
        public void UnlockDoor();
        public void LockDoor();
    }
}
