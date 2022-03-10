﻿using System;

namespace Subject
{
    public interface ISubject
    {
        void Attach(IObserver);
        void Detach(IObserver);
        void Notify();
    }
}