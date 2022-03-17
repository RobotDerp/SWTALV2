using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using Ladeskab;
using NUnit.Framework;

namespace Ladeskab_Classes.Unit.Test
{
    [TestFixture]
    public class StationControlTests
    {
        private StationControl _uut;
        private IDisplay _display;

        [SetUp]
        public void SetUp()
        {
            _display = Subsitute.For<IDisplay>();
            _uut = new StationControl(_display);
        }

        [Test]
        public void test1()
        {

        }
    }
}
