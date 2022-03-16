using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public void Attach(IObserver<T> missing_name)
        {
            throw new NotImplementedException();
        }

        public void Detach(IObserver<T> missing_name)
        {
            throw new NotImplementedException();
        }

        public void Notify()
        {
            throw new NotImplementedException();
        }
    }
}
