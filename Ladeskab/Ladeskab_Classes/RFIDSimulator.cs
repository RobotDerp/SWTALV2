using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Observer;
using Subject;

namespace Ladeskab
{
    public class RFIDSimulator : Subject, IRFID
    {
        public event EventHandler<RFIDEventArgs>? RFIDStateEvent;
        public void RFIDDetected(int id)
        {
            RFIDEventArgs e = new RFIDEventArgs() { RFIDState = id };
            RFIDStateEvent?.Invoke(this, e);
        }  

        // Don't think this function is needed anymore, since the event does the job
        public override void Notify()
        {
            Console.WriteLine("Notify called on RFIDSimulator");
        }
    }
}
