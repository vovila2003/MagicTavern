using System;

namespace InputServices
{
    public interface IActionInput
    {
        event Action OnAction;
    }
}