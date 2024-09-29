using System;

namespace InputServices
{
    public interface IPauseInput
    {
        event Action OnPause;
    }
}