using Ladeskab;
using Ladeskab_Classes;

class Program
    {
     

        static void Main(string[] args)
        {
        DoorSimulator door = new DoorSimulator();
        RFIDSimulator rfidReader = new RFIDSimulator();
        DisplaySimulator displaySimulator = new DisplaySimulator();
        UsbChargerSimulator usb = new UsbChargerSimulator();
        ChargeControl chargeControl = new ChargeControl(usb, displaySimulator);
        StationControl stationControl = new StationControl(door, displaySimulator, usb, rfidReader);
        //Assemble your system here from all the classes

        bool finish = false;
        do
        {
            string input;
            System.Console.WriteLine("Indtast E, O, C, R: ");
            input = Console.ReadLine();
            if (string.IsNullOrEmpty(input)) continue;

            switch (input[0])
            {
                case 'E':
                    finish = true;
                    break;

                case 'O':
                    door.OnDoorOpen();
                    break;

                case 'C':
                    door.OnDoorClose(); // door objekt skal tilføjes
                    break;

                case 'R':
                    System.Console.WriteLine("Indtast RFID id: ");
                    string idString = System.Console.ReadLine();

                    int id = Convert.ToInt32(idString);
                    rfidReader.RFIDDetected(id);
                    break;

                default:
                    break;
            }

        } while (!finish);
        while (true)
            { }
        }
    }
