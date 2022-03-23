using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab
{
    public class ChargeControl
    {
        private ICharger _usbCharger;
        private IDisplay _display;

        public double CurrentValue { get; set; }

        // Ctor
        public ChargeControl(ICharger charger, IDisplay display)
        {
            _usbCharger = charger;
            _usbCharger.CurrentValueEvent += HandleCurrentValueEvent;
            _display = display;
        }
        public void StartCharge()
        {
            _usbCharger.StartCharge();
        }

        public void StopCharge()
        {
            _usbCharger.StopCharge();
        }

        public bool IsConnected()
        {
            return _usbCharger.Connected;
        }

        private void HandleCurrentValueEvent(object? sender, CurrentEventArgs e)
        {
            if (e.Current > 0 && e.Current <= 5)
            {
                _display.Print("Phone is fully charged!");
            }
            else if (e.Current > 5 && e.Current <= 500)
            {
                _display.Print("Phone is charging");
            }
            else if (e.Current > 500)
            {
                _usbCharger.StopCharge();
                _display.Print("Something has gone terribly wrong. Save yourself!");
            }

            CurrentValue = e.Current;
        }
    }

}
