using System;

namespace Timers.Interfaces
{
    public interface IStoppable
    {
        event Action OnStopped;
        bool Stop();
    }
}