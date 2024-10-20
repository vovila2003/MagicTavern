using System;

namespace Modules.Timers
{
    public interface IEndable
    {
        event Action OnEnded;
        bool IsEnded();
    }
}