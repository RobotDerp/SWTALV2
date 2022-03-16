using System;

namespace Subject
{
    public class RFIDEventArgs : EventArgs
    {
        // Value in mA (milliAmpere)
        public int RFIDState { set; get; }
    }

    public interface IRFID
    {
        // Event triggered on new current value
        event EventHandler<RFIDEventArgs> RFIDStateEvent;

        int SavedID{ get; set; }

        void RFIDDetected(int id);
    }
}