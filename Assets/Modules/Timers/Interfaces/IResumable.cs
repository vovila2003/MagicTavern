using System;

namespace Modules.Timers
{
    public interface IResumable
    {
        event Action OnResumed;
        bool Resume();
    }
}