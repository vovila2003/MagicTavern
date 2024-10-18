using System;

namespace Tavern.InputServices.Interfaces
{
    public interface IBlockInput
    {
        event Action OnBlock;
    }
}