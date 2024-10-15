using System;

namespace Modules.Timers.Interfaces
{
    public interface IResumable
    {
        event Action OnResumed;
        bool Resume();
    }
}