using System;
using System.IO;
using Ladeskab;
using NUnit.Framework;
using Subject;
using UsbSimulator;

namespace Ladeskab_Classes.Unit.Test
{
    public class TestChargeControl
    {
        private ChargeControl uut;
        private UsbChargerSimulator charger; 

        [SetUp]
        public void Setup()
        {
            charger = new UsbChargerSimulator();
            uut = new ChargeControl(charger);
        }

        [Test]
        public void StartCharge_PrintCalled_WriteToConsole()
        {



            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            // Act
            uut.Print("Charging has started \r\n");

            // Assert
            var output = stringWriter.ToString();
            Assert.That(output, Is.EqualTo("Charging has started \r\n"));
        }
    }
}