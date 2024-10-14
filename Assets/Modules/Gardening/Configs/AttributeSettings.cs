using System;

namespace Gardening
{
    [Serializable]
    public class AttributeSettings
    {
        public bool IsEnabled;
        public float TimerDurationInSeconds;
        public float CriticalTimerDurationInSeconds;
    }
}