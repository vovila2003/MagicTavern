using System;

namespace Tavern.InputServices.Interfaces
{
    public interface IActionInput
    {
        event Action OnAction;
    }
}