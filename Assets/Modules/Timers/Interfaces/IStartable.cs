using System;

namespace Timers.Interfaces
{
    public interface IStartable
    {
        event Action OnStarted;
        bool Start();
    }
}