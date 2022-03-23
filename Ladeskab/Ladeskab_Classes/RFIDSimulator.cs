using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab
{
    public class RFIDSimulator : IRFID
    {
        public event EventHandler<RFIDEventArgs>? RFIDStateEvent;
        public void RFIDDetected(int id)
        {
            RFIDEventArgs e = new RFIDEventArgs() { RFID_ID = id };
            RFIDStateEvent?.Invoke(this, e);
        }
    }
}
