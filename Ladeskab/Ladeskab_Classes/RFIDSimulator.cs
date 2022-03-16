using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Observer;
using Subject;

namespace Ladeskab
{
    public class RFIDSimulator : IRFID, ISubject
    {
        public event EventHandler<RFIDEventArgs>? RFIDStateEvent;
        public int SavedID { get; set; }
        public void RFIDDetected(int id)
        {
            throw new NotImplementedException();
        }

        public void Attach(IObserver observer)
        {
            throw new NotImplementedException();
        }

        public void Detach(IObserver observer)
        {
            throw new NotImplementedException();
        }

        public void Notify()
        {
            throw new NotImplementedException();
        }
    }
}
