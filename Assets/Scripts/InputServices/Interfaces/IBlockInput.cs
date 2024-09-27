using System;

namespace InputServices
{
    public interface IBlockInput
    {
        event Action OnBlock;
    }
}