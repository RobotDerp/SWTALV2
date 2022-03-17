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

        /*
         * bekræfter at counter 0 når objektet laves
         * bekræfter at counter er talt op
         * bekræfter at logString er en string
         * bekræfter at der laves en ny fil
         * 
         */
    }
}
