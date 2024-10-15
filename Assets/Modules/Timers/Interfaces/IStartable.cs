using System;

namespace Modules.Timers.Interfaces
{
    public interface IStartable
    {
        event Action OnStarted;
        bool Start();
    }
}