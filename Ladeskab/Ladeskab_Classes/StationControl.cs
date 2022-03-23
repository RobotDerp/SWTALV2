using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using Ladeskab;

namespace Ladeskab
{


    public class StationControl
    {

        // Enum med tilstande ("states") svarende til tilstandsdiagrammet for klassen
        public enum LadeskabState
        {
            Available,
            Locked,
            DoorOpen
        };

        // Her mangler flere member variable
        private IDisplay iDisplay;
        public LadeskabState _state;
        public int _oldId;
        private IDoor _door;
        private IDisplay _display;
        private ICharger _charger;
        private IRFID _rfid;

        private string logFile = "logfile.txt"; // Navnet på systemets log-fil

        public StationControl(IDoor door, IDisplay display, ICharger charger, IRFID rfid)
        {
            door.DoorStateEvent += HandleDoorEvent;
            rfid.RFIDStateEvent += RfidDetected;
            _door = door;
            _display = display;
            _charger = charger;
            _rfid = rfid;
        }

        public void HandleDoorEvent(object sender, DoorEventArgs e)
        {
            switch (_state)
            {
                case LadeskabState.Available:
                    switch (e.DoorState)
                    {
                        case 0:
                            throw new InvalidOperationException("Cannot close already closed door - StationControl");
                        case 1:
                            _display.Print("Connect phone");
                            _state = LadeskabState.DoorOpen;
                            break;
                    }

                    break;
                case LadeskabState.Locked:
                    switch (e.DoorState)
                    {
                        case 0:
                            throw new InvalidOperationException(
                                "Cannot close already closed and locked door - StationControl");
                        case 1:
                            throw new InvalidOperationException("Cannot open locked door - StationControl");
                    }
                    break;
                case LadeskabState.DoorOpen:
                    switch (e.DoorState)
                    {
                        case 0:
                            _display.Print("Load RFID");
                            _state = LadeskabState.Available;
                            break;
                        case 1:
                            throw new InvalidOperationException("Cannot open already open door - StationControl");
                    }
                    break;
            }

        }

        // Eksempel på event handler for eventet "RFID Detected" fra tilstandsdiagrammet for klassen
        public void RfidDetected(object sender, RFIDEventArgs e)
        {
            switch (_state)
            {
                case LadeskabState.Available:
                    // Check for ladeforbindelse
                    if (_charger.Connected)
                    {
                        _door.LockDoor();
                        _charger.StartCharge();
                        _oldId = e.RFID_ID;
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst med RFID: {0}", e.RFID_ID);
                        }

                        _display.Print("Skabet er låst og din telefon lades.Brug dit RFID tag til at låse op.");
                        _state = LadeskabState.Locked;
                    }
                    else
                    {
                        _display.Print("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
                    }

                    break;

                case LadeskabState.DoorOpen:
                    // Ignore
                    break;

                case LadeskabState.Locked:
                    // Check for correct ID
                    if (e.RFID_ID == _oldId)
                    {
                        _charger.StopCharge();
                        _door.UnlockDoor();
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst op med RFID: {0}", e.RFID_ID);
                        }

                        _display.Print("Tag din telefon ud af skabet og luk døren");
                        _state = LadeskabState.Available;
                    }
                    else
                    {
                        _display.Print("Forkert RFID tag");
                    }

                    break;
            }
        }

        // Her mangler de andre trigger handlere
    }
}
