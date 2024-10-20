using System;

namespace Modules.Timers
{
    public interface IStartable
    {
        event Action OnStarted;
        bool Start();
    }
}