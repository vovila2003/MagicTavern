using System;

namespace InputServices
{
    public interface IShootInput
    {
        event Action OnFire;
        event Action OnAlternativeFire;
    }
}