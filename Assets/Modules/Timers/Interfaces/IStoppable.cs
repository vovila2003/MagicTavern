using System;

namespace Modules.Timers.Interfaces
{
    public interface IStoppable
    {
        event Action OnStopped;
        bool Stop();
    }
}