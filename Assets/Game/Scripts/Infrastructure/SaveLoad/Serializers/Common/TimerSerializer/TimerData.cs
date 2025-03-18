using System;
using Modules.Timers;

namespace Tavern.Infrastructure
{
    [Serializable]
    public class TimerData
    {
        public State CurrentState;
        public float Duration;
        public float Progress;
    }
}