using System;

namespace Tavern.InputServices.Interfaces
{
    public interface IShootInput
    {
        event Action OnFire;
        event Action OnAlternativeFire;
    }
}
