using System;

namespace Timers.Interfaces
{
    public interface IResumable
    {
        event Action OnResumed;
        bool Resume();
    }
}