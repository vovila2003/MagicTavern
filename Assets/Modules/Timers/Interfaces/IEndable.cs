using System;

namespace Modules.Timers.Interfaces
{
    public interface IEndable
    {
        event Action OnEnded;
        bool IsEnded();
    }
}