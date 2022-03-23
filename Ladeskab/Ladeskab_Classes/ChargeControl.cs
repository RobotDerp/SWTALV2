using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab
{
    public class ChargeControl : ICharger
    {
        public event EventHandler<CurrentEventArgs>? CurrentValueEvent;

        private UsbChargerSimulator usbCharger = new UsbChargerSimulator();
        public double CurrentValue
        {
            get { return CurrentValue;}
            set { CurrentValue = value; }
        }

        private IDisplay display;
        public bool Connected { get; }
        public void StartCharge()
        {
            usbCharger.StartCharge();
        }

        public void StopCharge()
        {
            usbCharger.StopCharge();
        }


        public ChargeControl(ICharger charger)
        {
            charger.CurrentValueEvent += HandleCurrentValueEvent;
            
        }

        private void HandleCurrentValueEvent(object sender, CurrentEventArgs e)
        {
            CurrentValue = e.Current;
            if (CurrentValue > 0 && CurrentValue <= 5)
            {
                StartCharge();
            }
            else if (CurrentValue > 5 && CurrentValue <= 500)
            {
                StartCharge();
            }
            else
            {
                StopCharge();
            }
            
            
        }
    }

}
