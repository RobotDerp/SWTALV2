using Ladeskab_Classes;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadeSkab_Classes.Unit.Test
{
    [TestFixture]
    public class TestLogFile
    {
        private LogFile _uut;
        
        [SetUp]
        public void Setup()
        {
            _uut = new LogFile();
        }

        [Test]
        public void ctor_CounterIsZero()
        {
            Assert.That(_uut.IdCounter, Is.Zero);
        }

        [Test]
        public void CounterIsIncrementedAfterLogEntry()
        {
            long counterValueStorage = _uut.IdCounter;
            string logMessage = "Testing Log Message";

            _uut.AddLogEntry(logMessage);

            Assert.That(_uut.IdCounter, Is.EqualTo(counterValueStorage + 1));
        }

        [Test]
        public void LogFileWritesStringToTarget()
        {
            string message = "Testing log writing to test list";
            List<string> logFileListTest = new List<string>();

            _uut.FakeAddLogEntry(message, logFileListTest);

            Assert.That(logFileListTest, Is.Not.Empty);
        }

        /*
         * bekræfter at logString er en string
         * bekræfter at der laves en ny fil
         * 
         */
    }
}
