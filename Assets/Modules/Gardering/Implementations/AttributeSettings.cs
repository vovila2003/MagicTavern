using System;

namespace Gardering.Implementations
{
    [Serializable]
    public class AttributeSettings
    {
        public bool IsEnabled;
        public float TimerDurationInSeconds;
        public float CriticalTimerDurationInSeconds;
    }
}