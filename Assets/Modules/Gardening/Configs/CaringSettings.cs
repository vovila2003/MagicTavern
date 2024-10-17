using System;
using Modules.Gardening.Enums;

namespace Modules.Gardening
{
    [Serializable]
    public class CaringSettings
    {
        public CaringType Type;
        public float TimerDurationInSeconds;
        public float CriticalTimerDurationInSeconds;
    }
}