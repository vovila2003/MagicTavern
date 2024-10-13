using System;

namespace Timers.Interfaces
{
    public interface IPausable
    {
        event Action OnPaused;

        bool IsPaused();
        bool Pause();
    }
}