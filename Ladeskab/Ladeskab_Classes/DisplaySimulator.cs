using System;

namespace Ladeskab
{
    public class DisplaySimulator : IDisplay
    {
        public void Print(string print)
        {
            Console.WriteLine(print);

        }

    }
}