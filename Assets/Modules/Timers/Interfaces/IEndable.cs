using System;

namespace Timers.Interfaces
{
    public interface IEndable
    {
        event Action OnEnded;
        bool IsEnded();
    }
}