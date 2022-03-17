using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Subject;

namespace Ladeskab
{
    public class RFIDSimulator : IRFID
    {
        public event EventHandler<RFIDEventArgs>? RFIDStateEvent;
        public void RFIDDetected(int id)
        {
            RFIDEventArgs e = new RFIDEventArgs() { RFIDState = id };
            RFIDStateEvent?.Invoke(this, e);
        }
    }
}
