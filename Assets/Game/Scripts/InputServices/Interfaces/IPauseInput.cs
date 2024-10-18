using System;

namespace Tavern.InputServices.Interfaces
{
    public interface IPauseInput
    {
        event Action OnPause;
    }
}