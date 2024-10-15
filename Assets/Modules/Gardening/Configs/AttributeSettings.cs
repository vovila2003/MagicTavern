using System;
using Modules.Gardening.Enums;

namespace Modules.Gardening
{
    [Serializable]
    public class AttributeSettings
    {
        public AttributeType Type;
        public float TimerDurationInSeconds;
        public float CriticalTimerDurationInSeconds;
    }
}