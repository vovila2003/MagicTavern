using System;

namespace Modules.Timers.Interfaces
{
    public interface IPausable
    {
        event Action OnPaused;

        bool IsPaused();
        bool Pause();
    }
}