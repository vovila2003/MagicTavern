using System;

namespace Modules.Timers
{
    public interface IPausable
    {
        event Action OnPaused;

        bool IsPaused();
        bool Pause();
    }
}