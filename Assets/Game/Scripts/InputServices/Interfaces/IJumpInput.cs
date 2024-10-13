using System;

namespace InputServices
{
    public interface IJumpInput
    {
        event Action OnJump;
    }
}