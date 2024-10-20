using System;

namespace Modules.Timers
{
    public interface IStoppable
    {
        event Action OnStopped;
        bool Stop();
    }
}