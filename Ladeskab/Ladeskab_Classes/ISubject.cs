using System;
using Observer;

namespace Subject
{
    public abstract class Subject
    {
        protected List<IObserver> Observers { get; set; }

        protected Subject()
        {
            Observers = new List<IObserver>();
        }

        public void Attach(IObserver observer)
        {
            Observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            Observers.Remove(observer);
        }
        public abstract void Notify();
    }
}